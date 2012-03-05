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
    public class ShowTagService : IShowTagService
    {
        private IShowTagRepository _repo;

        public ShowTagService( IShowTagRepository repo ) {
            Checks.Argument.IsNotNull( repo, "repo" );

            _repo = repo;
        }

        public IQueryable<IShowTag> GetAllTags() {
            return _repo.FindAll();
        }

        public IShowTag GetTag( Guid id ) {
            return _repo.FindById( id );
        }

        public IList<IShowTag> GetTagsByShow( Guid showId ) {
            return GetAllTags().Where( x => x.ShowId == showId ).ToList();
        }
        
        public void SaveCommit( IShowTag tag, out bool success ) {
            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                Save( tag, out success );
                if ( success )
                    u.Commit();
            }
        }

        public void Save( IShowTag tag, out bool success ) {
            Checks.Argument.IsNotNull( tag, "tag" );

            success = false;

            if ( null == _repo.FindById( tag.Id ) ) {
                try {
                    _repo.Add( tag );
                    success = true;
                }
                catch ( Exception ex ) {
                    success = false;
                }
            }
        }

        public void Delete( IShowTag tag ) {
            Checks.Argument.IsNotNull( tag, "tag" );

            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                _repo.Remove( tag );
                u.Commit();
            }
        }
    }
}
