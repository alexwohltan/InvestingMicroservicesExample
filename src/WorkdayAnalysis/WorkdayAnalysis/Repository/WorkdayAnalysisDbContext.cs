namespace WorkdayAnalysis;

public class WorkdayAnalysisDbContext : DbContext
{
    public DbSet<WorkdayAdressableMarket> Markets { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountSharePrice> Prices { get; set; }
    public DbSet<AccountFiling> Filings { get; set; }

    public const string StandardAdressableMarketName = "AllWorkdayAccounts";

    private readonly IEventBus _eventBus;

    public WorkdayAnalysisDbContext(IEventBus eventBus = null) : base()
    {
        _eventBus = eventBus;
    }
    public WorkdayAnalysisDbContext(DbContextOptions<WorkdayAnalysisDbContext> options, IEventBus eventBus = null)
        : base(options)
    {
        _eventBus = eventBus;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkdayAdressableMarket>()
                .Property(e => e.AccountIds)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(e => int.Parse(e)).ToList());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public async Task<Account?> GetAccount(int accountId) => await Accounts.FindAsync(accountId);
    public async Task<Account?> GetAccount(string ticker) => await Accounts.FirstOrDefaultAsync(e => e.Ticker == ticker);

    public async void AddAccount(Account account)
    {
        AddAccount(account, new int[] { ((int)(await GetMarketIdFromName(StandardAdressableMarketName))) });
    }
    public async void AddAccount(Account account, IEnumerable<int> marketIds)
    {
        EnsureStandardMarketIsInDb();

        Accounts.Add(account);

        await SaveChangesAsync();

        var accountInDb = Accounts.FirstOrDefault(e => e.Ticker == account.Ticker);

        if (accountInDb == null)
            throw new Exception("Internal Error. Account was not saved to the database!");

        foreach (var marketId in marketIds)
        {
            var market = await Markets.FindAsync(marketId);

            if (market == null)
                continue;

            if (market.AccountIds == null)
                market.AccountIds = new List<int>();

            market.AccountIds.Add(accountInDb.Id);
        }

        await SaveChangesAsync();
    }
    public async void AddAccount(Account account, IEnumerable<string> marketNames)
    {
        var marketIds = await Task.WhenAll(marketNames.Select(async e => await GetMarketIdFromName(e)).Where(e => e != null).Select(async e => ((int)(await e))));
        AddAccount(account, marketIds);
    }

    public async void UpdateAccount(Account account)
    {
        var accountInDatabase = Accounts.FirstOrDefault(e => e.Ticker == account.Ticker);

        if (accountInDatabase == null)
            return;
            //throw new Exception("Account not available in database. Update not possible.");

        accountInDatabase.Name = account.Name;
        accountInDatabase.FundamentalDataId = account.FundamentalDataId;

        if (account.WorkdayHCMGoLive != null)
            accountInDatabase.WorkdayHCMGoLive = account.WorkdayHCMGoLive;
        if (account.WorkdayFINGoLive != null)
            accountInDatabase.WorkdayFINGoLive = account.WorkdayFINGoLive;
        if (account.WorkdayPLNGoLive != null)
            accountInDatabase.WorkdayPLNGoLive = account.WorkdayPLNGoLive;
        if (account.WorkdaySRCGoLive != null)
            accountInDatabase.WorkdaySRCGoLive = account.WorkdaySRCGoLive;
        if (account.WorkdayPKNGoLive != null)
            accountInDatabase.WorkdayPKNGoLive = account.WorkdayPKNGoLive;

        if (account.Prices != null && account.Prices.Count() > 0)
        {
            accountInDatabase.Prices = account.Prices;
        }
        if (account.Filings != null && account.Filings.Count() > 0)
        {
            accountInDatabase.Filings = account.Filings;
        }

        await SaveChangesAsync();
    }

    public async void AddMarket(WorkdayAdressableMarket market)
    {
        EnsureStandardMarketIsInDb();

        Markets.Add(market);

        await SaveChangesAsync();
    }

    public async Task<WorkdayAdressableMarket> GetStandardMarket()
    {
        EnsureStandardMarketIsInDb();
        return await GetMarketFromName(StandardAdressableMarketName);
    }

    public async Task<WorkdayAdressableMarket?> GetMarket(int marketId)
    {
        return await GetMarketWithAccounts(await GetMarketFromId(marketId));
    }
    public async Task<WorkdayAdressableMarket?> GetMarket(string marketName)
    {
        return await GetMarketWithAccounts(await GetMarketFromName(marketName));
    }

    private async Task<WorkdayAdressableMarket?> GetMarketFromName(string marketName)
    {
        return await Markets.FirstOrDefaultAsync(e => e.Name == marketName);
    }
    private async Task<WorkdayAdressableMarket?> GetMarketFromId(int marketId)
    {
        return await Markets.FindAsync(marketId);
    }

    private async Task<WorkdayAdressableMarket?> GetMarketWithAccounts(WorkdayAdressableMarket? market)
    {
        if (market == null)
            return null;

        market.Accounts = new List<Account>();

        foreach (var accountId in market.AccountIds)
        {
            market.Accounts.Add(await GetAccount(accountId));
        }

        return market;
    }

    private async Task<int?> GetMarketIdFromName(string marketName)
    {
        var market = await GetMarketFromName(marketName);
        return market != null ? market.Id : null;
    }

    private async void EnsureStandardMarketIsInDb()
    {
        if (Markets.Count() == 0)
            await Markets.AddAsync(new WorkdayAdressableMarket() { Name = StandardAdressableMarketName, Description = "All accounts." });
    }
}


