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
        public IListenedShow LatestShow { get; set; }
        public UserProfile Profile { get; set; }

        public LatestProfile( IListenedShow latestShow, UserProfile profile ) {
            LatestShow = latestShow;
            Profile = profile;
        }
    }
}
