using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MessageBroker;
using MessageBroker.RabbitMQ;
using IntegrationEvents;
using FundamentalData.Integration;

namespace FundamentalData
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            EventBus = new EventBusRabbitMQ();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        public IEventBus EventBus { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            if(Environment.IsDevelopment())
            {
                services.AddDbContext<FundamentalDataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FundamentalDatabase")));

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Fundamental Data", Version = "v1" });
                });
            }
            else
            {
                services.AddDbContext<FundamentalDataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FundamentalDatabase")));
            }
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            EventBus.Subscribe<NewMarketEvent, NewMarketEventConsumer>(new NewMarketEventConsumer(EventBus));
        }
    }
}
