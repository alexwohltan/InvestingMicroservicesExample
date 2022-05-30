using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using InvestingMicroservicesAPIClient;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            if (Environment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Fundamental Data", Version = "v1" });
                });
            }

            services.AddSingleton<APIGatewayClient>(serviceProvider =>
            {
                var hostname = "localhost";
                var port = 7199;
                var baseUrl = "api/";

                if (!string.IsNullOrEmpty(Configuration["APIGatewayHostname"]))
                    hostname = Configuration["APIGatewayHostname"];

                if (!string.IsNullOrEmpty(Configuration["APIGatewayPort"]))
                    port = int.Parse(Configuration["APIGatewayPort"]);

                if (!string.IsNullOrEmpty(Configuration["APIGatewayBaseUrl"]))
                    baseUrl = Configuration["APIGatewayBaseUrl"];

                return new APIGatewayClient(hostname, port, baseUrl);
            });

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

