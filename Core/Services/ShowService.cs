using System;
using System.Collections.Generic;
using System.Linq;
using Core.Helpers;
using Core.DomainObjects;
using Core.Infrastructure;
using Core.Repository;
using Core.Services.Interfaces;
using System.Web;
using LinqToCache;
using System.Data.SqlClient;

namespace Core.Services
{
    public class ShowService : IShowService
    {
        IShowRepository _repo;

        public ShowService( IShowRepository repo ) {
            Checks.Argument.IsNotNull( repo, "repo" );

            _repo = repo;
        }

        public IList<IShow> GetAllShows() {
            var shows = _repo.FindAll().AsCached( "Shows", new CachedQueryOptions() {
                OnInvalidated = ( sender, args ) => {
                    // This code here is invoked when the query result is invalidated
                    // Always check the args parameter to understand the type of invalidation that occured
                    if ( SqlNotificationSource.Data == args.NotificationEventArgs.Source
                       && SqlNotificationType.Change == args.NotificationEventArgs.Type ) {
                        // This is a data change notificaiton, the result set has changed
                    }
                }
            } );

            return shows.ToList();
        }

        public IShow GetShow( Guid id ) {
            return _repo.FindById( id );
        }

        public IShow GetShow( DateTime date ) {
            return _repo.FindByShowDate( date );
        }

        public IList<IShow> GetShows( IList<DateTime> dates ) {
            return ( from date in dates
                     select GetShow( date ) ).ToList();
        }

        public IList<int> GetYears() {
            return GetAllShows().Select( x => x.ShowDate.Value.Year ).Distinct().OrderByDescending( y => y ).ToList();
        }

        public IList<IShow> GetShows( IList<Guid> showIds ) {
            return ( from showId in showIds
                     select GetShow( showId ) ).ToList();
        }

        public IList<IShow> GetShowsByYear( int year ) {
            if ( year != 2003 ) {
                return GetAllShows().Where( x => x.ShowDate.Value.Year == year ).OrderBy( y => y.ShowDate ).ToList();
            }

            //Hack to get 2002 New Years Eve into 2003 shows
            return GetAllShows().Where( x => x.ShowDate.Value.Year == 2003 || x.ShowDate.Value.Year == 2002 ).OrderBy( y => y.ShowDate ).ToList();
        }

        public List<ShowStatus> GetShowStatusByYear( int year ) {
            return ( from s in GetShowsByYear( year )
                     select new ShowStatus( s.Id, (int)ListenedStatus.None, s.ShowDate.Value, s.GetShowName() ) ).ToList();
        }

        public void SaveCommit( IShow show, out bool success ) {
            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                Save( show, out success );
                if ( success )
                    u.Commit();
            }
        }

        public void Save( IShow show, out bool success ) {
            Checks.Argument.IsNotNull( show, "show" );

            success = false;

            if ( null == _repo.FindById( show.Id ) ) {
                try {
                    _repo.Add( show );
                    success = true;
                }
                catch ( Exception ex ) {
                    success = false;
                }
            }
        }

        public void Delete( IShow show ) {
            Checks.Argument.IsNotNull( show, "show" );

            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                _repo.Remove( show );
                u.Commit();
            }
        }

    }
}