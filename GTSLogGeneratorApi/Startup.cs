using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Infrastructure.AutofacModules;
using GTSLogGeneratorApi.Infrastructure.Extensions.ServiceCollectionExtensions;
using GTSLogGeneratorApi.Infrastructure.Middlewares;
using GTSLogGeneratorApi.Infrastructure.RequestExamples;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace GTSLogGeneratorApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryHangfire();
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "GTS-Log Generator"});
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<UpdateConfigRequestExample>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<MediatorModule>();
            containerBuilder.RegisterModule<MapperModule>();
            containerBuilder.RegisterModule<ApplicationModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
          
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHangfireDashboard();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseCors("ApiCorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GTS-Log Generator");
            });

            app.UseMvc();
        }
    }
}
