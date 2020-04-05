using System.Reflection;
using Autofac;
using FluentValidation;
using GTSLogGeneratorApi.Infrastructure.Behaviors;
using MediatR;
using Module = Autofac.Module;

namespace GTSLogGeneratorApi.Infrastructure.AutofacModules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}