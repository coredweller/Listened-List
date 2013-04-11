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

        public IList<IShowTag> GetTagsByShowAndUser( Guid showId, Guid userId ) {
            return GetAllTags().Where( x => x.ShowId == showId && x.UserId == userId ).ToList();
        }

        public IList<IShowTag> GetShowTagsByTagId( Guid tagId ) {
            return GetAllTags().Where( x => x.TagId == tagId ).ToList();
        }

        public IQueryable<IShowTag> GetTagsByTagAndUser( Guid tagId, Guid userId ) {
            return GetAllTags().Where( x => x.TagId == tagId && x.UserId == userId );
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

        //Delete all ShowTags associated with that Tag
        public void Delete( Guid tagId ) {
            Checks.Argument.IsNotNull( tagId, "tagId" );

            var tags = GetShowTagsByTagId( tagId );

            if ( tags == null || tags.Count <= 0 ) return;

            using ( IUnitOfWork u = UnitOfWork.Begin() ) {
                foreach ( IShowTag tag in tags ) {
                    _repo.Remove( tag );
                }

                u.Commit();
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
