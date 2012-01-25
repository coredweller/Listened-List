using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Repository;
using Core.Helpers;
using Core.DomainObjects;
using Core.Infrastructure;

namespace Core.Services
{
    public class ListenedShowService
    {
        IListenedShowRepository _repo;

        public ListenedShowService(IListenedShowRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");

            _repo = repo;
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

        public IListenedShow GetByUserAndShowId( Guid userId, Guid showId ) {
            return GetAllShows().SingleOrDefault( x => x.UserId == userId && x.ShowId == showId );
        }

        public IQueryable<IListenedShow> GetByUser( Guid userId ) {
            return GetAllShows().Where( x => x.UserId == userId );
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
}
