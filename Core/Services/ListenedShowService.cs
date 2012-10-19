using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Repository;
using Core.Helpers;
using Core.DomainObjects;
using Core.Infrastructure;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class ListenedShowService : IListenedShowService
    {
        IShowRepository _showRepo;
        IListenedShowRepository _repo;

        public ListenedShowService(IListenedShowRepository repo,
                                    IShowRepository showRepo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            Checks.Argument.IsNotNull( showRepo, "showRepo" );

            _repo = repo;
            _showRepo = showRepo;
        }

        public IQueryable<IListenedShow> GetAllShows()
        {
            return _repo.FindAll();
        }

        public IListenedShow GetById(Guid id)
        {
            return _repo.FindById(id);
        }

        public IListenedShow GetByShowId(Guid id)
        {
            return _repo.FindByShowId( id );
        }

        public IList<IListenedShow> GetByAttendedForUser( Guid userId ) {
            return GetAllShows().Where( x => x.UserId == userId && x.Attended ).ToList();
        }

        public IListenedShow GetByUserAndShowId( Guid userId, Guid showId ) {
            return GetAllShows().SingleOrDefault( x => x.UserId == userId && x.ShowId == showId );
        }

        public IQueryable<IListenedShow> GetByUser( Guid userId ) {
            return GetAllShows().Where( x => x.UserId == userId );
        }

        public IQueryable<IListenedShow> GetShowsByYear( int year, Guid userId ) {
            return GetAllShows().Where( x => x.UserId == userId && x.ShowDate.Year == year );
        }

        public IDictionary<IShow, IListenedShow> GetBothShowsByYear(int year, Guid userId) {
            var shows = _showRepo.FindAll().Where( x => x.ShowDate.Value.Year == year );
            var listened = GetAllShows().Where( x => x.UserId == userId && x.ShowDate.Year == year);

            var result = ( from show in shows
                           join listen in listened on show.Id equals listen.ShowId into temp
                           from t in temp.DefaultIfEmpty()
                           select new { Show = show, ListenedShow = t } );

            var dict = new Dictionary<IShow, IListenedShow>();

            foreach ( var r in result ) {
                dict.Add( r.Show, r.ListenedShow );
            }

            return dict;
        }

        public IList<IListenedShow> GetByUserIds( IList<Guid> userIds ) {
            IList<IListenedShow> shows = new List<IListenedShow>();

            foreach( var u in userIds){
                var latest = GetLatestByUserId( u );

                if ( latest != null ) shows.Add( latest );
            }

            return shows.OrderByDescending( x => x.UpdatedDate ).ToList();
        }

        public IListenedShow GetLatestByUserId( Guid userId ) {
            return GetByUser( userId ).ToList().OrderByDescending( x => x.UpdatedDate ).FirstOrDefault();
        }

        public IListenedShow GetByUserAndShow( Guid userId, DateTime showDate ) {
            return GetByUser( userId ).SingleOrDefault( x => x.ShowDate == showDate );
        }

        public void SaveCommit(IListenedShow show, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(show, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IListenedShow show, out bool success)
        {
            Checks.Argument.IsNotNull(show, "show");

            success = false;

            if (null == _repo.FindByShowId(show.Id))
            {
                try
                {
                    _repo.Add(show);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void Delete(IListenedShow show)
        {
            Checks.Argument.IsNotNull(show, "show");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(show);
                u.Commit();
            }
        }

    }

    public enum ListenedStatus
    {
        None = 0,
        InProgress = 1,
        Finished = 2,
        NeedToListen = 3,
        EditNotes = 5,
        Attended = 7,
        Cancel = 11
    }
}
