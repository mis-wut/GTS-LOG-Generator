using System.Reflection;
using Autofac;
using GTSLogGeneratorApi.Infrastructure.Services;
using Module = Autofac.Module;

namespace GTSLogGeneratorApi.Infrastructure.AutofacModules
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IMapper<,>))
                .AsImplementedInterfaces();
        }
    }
}