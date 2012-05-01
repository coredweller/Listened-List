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
    public class SubscriptionService : ISubscriptionService
    {
        private ISubscriptionRepository _repo;

        public SubscriptionService( ISubscriptionRepository repo ) {
            Checks.Argument.IsNotNull( repo, "repo" );

            _repo = repo;
        }

        public IQueryable<ISubscription> GetAllSubscriptions() {
            return _repo.FindAll();
        }

        public ISubscription GetSubscription( Guid id ) {
            return _repo.FindById( id );
        }

        public IList<ISubscription> GetSubscriptionsByUser( Guid userId ) {
            return _repo.FindAll().Where( x => x.UserId == userId ).ToList();
        }

        public void SaveCommit( ISubscription subscription, out bool success ) {
            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                Save( subscription, out success );
                if ( success )
                    u.Commit();
            }
        }

        public void Save( ISubscription subscription, out bool success ) {
            Checks.Argument.IsNotNull( subscription, "subscription" );

            success = false;

            if ( null == _repo.FindById( subscription.Id ) ) {
                try {
                    _repo.Add( subscription );
                    success = true;
                }
                catch ( Exception ex ) {
                    success = false;
                }
            }
        }

        public void Delete( ISubscription subscription ) {
            Checks.Argument.IsNotNull( subscription, "subscription" );

            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                _repo.Remove( subscription );
                u.Commit();
            }
        }
    }
}
