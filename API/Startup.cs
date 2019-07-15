using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Swashbuckle.AspNetCore.Swagger;
using API.Configuration.Settings;

namespace API
{
    public class Startup
    {
        private readonly ConfigurationSettings _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = new ConfigurationSettings();
            configuration.Bind(_configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add<GlobalExceptionFilter>())
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = _configuration.ApiName , Version = _configuration.ApiVersion });
            });

            return ConfigureContainer(services);
        }

        private IServiceProvider ConfigureContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterAssemblyModules(typeof(Startup).Assembly);
            containerBuilder.Register((ctx) => _configuration).SingleInstance();

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_configuration.ApiName} {_configuration.ApiVersion}");
                    });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
