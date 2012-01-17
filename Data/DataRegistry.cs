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
