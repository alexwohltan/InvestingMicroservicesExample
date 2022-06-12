namespace APIGateway
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth = 32;
            });

            if (Environment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Gateway", Version = "v1" });
                });
            }

            services.AddSingleton<BenchmarkClient>(serviceProvider =>
            {
                var hostname = "localhost";
                var port = 7126;
                var baseUrl = "api/";

                if (!string.IsNullOrEmpty(Configuration["BenchmarkDataHostname"]))
                    hostname = Configuration["BenchmarkDataHostname"];

                if (!string.IsNullOrEmpty(Configuration["BenchmarkDataPort"]))
                    port = int.Parse(Configuration["BenchmarkDataPort"]);

                if (!string.IsNullOrEmpty(Configuration["BenchmarkDataBaseUrl"]))
                    baseUrl = Configuration["BenchmarkDataBaseUrl"];

                return new BenchmarkClient(hostname, port, baseUrl);
            });

            services.AddSingleton<FundamentalDataClient>(serviceProvider =>
            {
                var hostname = "localhost";
                var port = 60958;
                var baseUrl = "api/";

                if (!string.IsNullOrEmpty(Configuration["FundamentalDataHostname"]))
                    hostname = Configuration["FundamentalDataHostname"];

                if (!string.IsNullOrEmpty(Configuration["FundamentalDataPort"]))
                    port = int.Parse(Configuration["FundamentalDataPort"]);

                if (!string.IsNullOrEmpty(Configuration["FundamentalDataBaseUrl"]))
                    baseUrl = Configuration["FundamentalDataBaseUrl"];

                return new FundamentalDataClient(hostname, port, baseUrl);
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

