using RabbitMQ.Client;

namespace Benchmark
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
                //services.AddDbContext<BenchmarkDbContext>(options =>
                //options.UseSqlServer(
                //    Configuration.GetConnectionString("IndustryBenchmark"))
                //.UseLazyLoadingProxies());

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Fundamental Data", Version = "v1" });
                });
            }
            else
            {
                //services.AddDbContext<BenchmarkDbContext>(options =>
                //options.UseSqlServer(
                //    Configuration.GetConnectionString("IndustryBenchmark"))
                //.UseLazyLoadingProxies());
            }

            services.AddSingleton<IBenchmarkRepository, BenchmarkInMemoryRepository>();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                {
                    factory.UserName = Configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                {
                    factory.Password = Configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            RegisterEventBus(services);

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

            ConfigureEventBus(app);
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<EventBus.Abstractions.IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<NewFilingEventConsumer>();
            services.AddTransient<NewCompanyEventConsumer>();
            services.AddTransient<NewIndustryEventConsumer>();
            services.AddTransient<NewSectorEventConsumer>();
            services.AddTransient<NewMarketEventConsumer>();

            services.AddTransient<UpdatedCompanyEventConsumer>();
            services.AddTransient<UpdatedFilingEventConsumer>();

            services.AddTransient<DeletedCompanyEventConsumer>();
            services.AddTransient<DeletedIndustryEventConsumer>();
            services.AddTransient<DeletedSectorEventConsumer>();
            services.AddTransient<DeletedMarketEventConsumer>();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<NewFilingEvent, NewFilingEventConsumer>();
            eventBus.Subscribe<NewCompanyEvent, NewCompanyEventConsumer>();
            eventBus.Subscribe<NewIndustryEvent, NewIndustryEventConsumer>();
            eventBus.Subscribe<NewSectorEvent, NewSectorEventConsumer>();
            eventBus.Subscribe<NewMarketEvent, NewMarketEventConsumer>();

            eventBus.Subscribe<UpdatedCompanyEvent, UpdatedCompanyEventConsumer>();
            eventBus.Subscribe<UpdatedFilingEvent, UpdatedFilingEventConsumer>();

            eventBus.Subscribe<DeletedCompanyEvent, DeletedCompanyEventConsumer>();
            eventBus.Subscribe<DeletedIndustryEvent, DeletedIndustryEventConsumer>();
            eventBus.Subscribe<DeletedSectorEvent, DeletedSectorEventConsumer>();
            eventBus.Subscribe<DeletedMarketEvent, DeletedMarketEventConsumer>();
        }
    }
}

