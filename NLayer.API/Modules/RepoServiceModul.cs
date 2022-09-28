using Autofac;
//using NLayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModul : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            // auto da önce class sora interface ama programcs de önce intr. sora class 

            builder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>)).InstancePerLifetimeScope();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly(); // üzerinde çalıştığın assembly
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext)); // üzerinde çalıştığın assembly (herhangi bir classdı biz de daimi üye classı verdik)
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile)); // üzerinde çalıştığın assembly (herhangi bir classdı biz de daimi üye classı verdik)

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            // Repository ile bitenleri al => interfacelerini de implemente et => scope denk gelsin
            // InstancePerLifetimeScope => Scope

            // scope => request başladı ve bitene kadar aynı instanceyi kullansın
            // transit => herhangi bir classın ctor da o interface nerde geçtiyse yeni bir instance oluştur



            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
            // Repository ile bitenleri al => interfacelerini de implemente et => scope denk gelsin
            // InstancePerDependency => transit

            // scope => request başladı ve bitene kadar aynı instanceyi kullansın
            // transit => herhangi bir classın ctor da o interface nerde geçtiyse yeni bir instance oluştur




            
        }
    }
}
