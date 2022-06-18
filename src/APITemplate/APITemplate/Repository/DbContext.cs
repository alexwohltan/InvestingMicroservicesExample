namespace APITemplate;

public class MyDbContext : DbContext
{
    // TODO: Add DbSets

    private readonly IEventBus _eventBus;

    public MyDbContext(IEventBus eventBus = null) : base()
    {
        _eventBus = eventBus;
    }
    public MyDbContext(DbContextOptions<MyDbContext> options, IEventBus eventBus = null)
        : base(options)
    {
        _eventBus = eventBus;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    // TODO: Add CRUD Operations
}

