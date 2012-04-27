using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;
using Core.Membership;

namespace Core.Helpers
{
    public class LatestProfile
    {
        public IListenedShow LatestListenedShow { get; set; }
        public IShow LatestShow { get; set; }
        public UserProfile Profile { get; set; }

        public LatestProfile( IListenedShow latestShow, IShow show, UserProfile profile ) {
            LatestListenedShow = latestShow;
            LatestShow = show;
            Profile = profile;
        }
    }
}
