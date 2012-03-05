using StructureMap.Configuration.DSL;
using Core.Configuration;
using Core.Infrastructure.Logging;
using Core.Services.Interfaces;
using Core.Services;

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
        }
    }
}
