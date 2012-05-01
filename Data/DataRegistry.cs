using StructureMap.Configuration.DSL;
using Data.Repository;
using Core.Configuration;
using Core.Repository;

namespace Data
{
    public class DataRegistry : Registry
    {

        public DataRegistry() {


            For<IShowRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ShowRepository>()
                .Ctor<IDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<ShowRepository>(() => new ShowRepository((IDatabaseFactory)null));


            For<IListenedShowRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ListenedShowRepository>()
                .Ctor<IDatabaseFactory>( "factory" ).IsTheDefault();

            SelectConstructor<ListenedShowRepository>(() => new ListenedShowRepository((IDatabaseFactory)null));


            For<ITagRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<TagRepository>()
                .Ctor<IDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<TagRepository>(() => new TagRepository((IDatabaseFactory)null));

            For<IShowTagRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ShowTagRepository>()
                .Ctor<IDatabaseFactory>( "factory" ).IsTheDefault();

            SelectConstructor<ShowTagRepository>( () => new ShowTagRepository( (IDatabaseFactory)null ) );

            For<ISubscriptionRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SubscriptionRepository>()
                .Ctor<IDatabaseFactory>( "factory" ).IsTheDefault();

            SelectConstructor<SubscriptionRepository>( () => new SubscriptionRepository( (IDatabaseFactory)null ) );


            ///INFRASTRUCTURE IOC SETUP BELOW.  DO NOT ALTER ANYTHING BELOW THIS LINE
            ///-----------------------------------------------------------------------

            For<Core.Infrastructure.IUnitOfWork>()
                        .HybridHttpOrThreadLocalScoped()
                        .Use<Data.Repository.UnitOfWork>();
            SelectConstructor<Data.Repository.UnitOfWork>( () => new Data.Repository.UnitOfWork( (IDatabaseFactory)null ) );

            For<IDatabase>()
                .HybridHttpOrThreadLocalScoped()
                .Use<Database>()
                .Ctor<IConnectionString>("connectionString").IsTheDefault();
            SelectConstructor<Database>(() => new Database((IConnectionString)null));

            For<IDatabaseFactory>()
                .HybridHttpOrThreadLocalScoped()
                .Use<DatabaseFactory>();
        }
    }
}
