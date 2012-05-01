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
        public ISubscription Subscription { get; set; }
        public bool Subscribed {
            get {
                return false;///LEFT OFF HERE
            }
            set {
                Subscribed = value;
            }
        }

        public LatestProfile( IListenedShow latestShow, IShow show, UserProfile profile, ISubscription subscription ) {
            LatestListenedShow = latestShow;
            LatestShow = show;
            Profile = profile;
            Subscription = subscription;
        }
    }
}
