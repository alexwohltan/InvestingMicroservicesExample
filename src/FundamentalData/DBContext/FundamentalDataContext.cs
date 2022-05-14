using System;
using Microsoft.EntityFrameworkCore;
using DataStructures;


namespace FundamentalData
{
    public class FundamentalDataContext : DbContext
    {
        public FundamentalDataContext() : base()
        {

        }
        public FundamentalDataContext(DbContextOptions<FundamentalDataContext> options)
            : base(options)
        {

        }

        public DbSet<Market> Markets { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Filing> Filings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
