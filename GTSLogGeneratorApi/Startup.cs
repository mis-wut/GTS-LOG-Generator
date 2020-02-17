using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Extensions.ServiceCollectionExtensions;
using GTSLogGeneratorApi.Jobs;
using GTSLogGeneratorApi.Mappers;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace GTSLogGeneratorApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryHangfire();

            services.AddScoped<ILogsGenerationJob, LogsGenerationJob>();
            services.AddScoped<ILogsGenerationParametersMapper, LogsGenerationParametersMapper>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GTS-Log Generator", Version = "v1" });
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHangfireDashboard();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GTS-Log Generator");
            });

            app.UseMvc();
        }
    }
}
