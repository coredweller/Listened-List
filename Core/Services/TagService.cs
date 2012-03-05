using System;
using System.Linq;
using Core.Repository;
using Core.Helpers;
using Core.DomainObjects;
using Core.Infrastructure;
using Core.Services.Interfaces;
using System.Collections.Generic;

namespace Core.Services
{
    public class TagService : ITagService
    {
        private ITagRepository _repo;

        public TagService( ITagRepository repo ) {
            Checks.Argument.IsNotNull( repo, "repo" );

            _repo = repo;
        }

        public IQueryable<ITag> GetAllTags() {
            return _repo.FindAll();
        }

        public ITag GetTag( Guid id ) {
            return _repo.FindById( id );
        }

        public IList<ITag> GetTags( Guid showId, Guid userId ) {
            return GetAllTags().Where( x => x.ShowId == showId && x.UserId == userId ).ToList();
        }

        public ITag GetTag( string name, Guid userId ) {
            return GetAllTags().SingleOrDefault( x => x.UserId == userId && x.Name.ToLower() == name.ToLower() );
        }

        public void SaveCommit( ITag tag, out bool success ) {
            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                Save( tag, out success );
                if ( success )
                    u.Commit();
            }
        }

        public void Save( ITag tag, out bool success ) {
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

        public void Delete( ITag tag ) {
            Checks.Argument.IsNotNull( tag, "tag" );

            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                _repo.Remove( tag );
                u.Commit();
            }
        }
    }
}
