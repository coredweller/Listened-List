using StructureMap.Configuration.DSL;
using Core.Configuration;
using Core.Infrastructure.Logging;
using Core.Services.Interfaces;
using Core.Services;
using Core.Membership;

namespace Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<IAppConfigManager>()
                .Singleton()
                .Use<AppConfigManager>();

            For<IConnectionString>()
                .Singleton()
                .Use<ConnectionString>()
                .WithCtorArg("connectionStringKey").EqualTo("Listened");

            For<ILogWriter>()
                .HybridHttpOrThreadLocalScoped()
                .Use<DebuggerWriter>();
            SelectConstructor<DebuggerWriter>(() => new DebuggerWriter());
            
            For<IMembershipProvider>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ListenedMembershipProvider>();
            SelectConstructor<ListenedMembershipProvider>(() => new ListenedMembershipProvider());

            For<IRoleProvider>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ListenedRoleProvider>();
            SelectConstructor<ListenedRoleProvider>(() => new ListenedRoleProvider());

            //Services
            For<IListenedShowService>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ListenedShowService>();

            For<IShowService>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ShowService>();

            For<ITagService>()
                .HybridHttpOrThreadLocalScoped()
                .Use<TagService>();

            For<IShowTagService>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ShowTagService>();

            For<ISubscriptionService>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SubscriptionService>();
        }
    }
}