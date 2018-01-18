using System;
using System.Data.Entity;

namespace Chilindo.DAL
{
    public class SqlDBContext : DbContext
    {
        public SqlDBContext() : base()
        {
            Database.SetInitializer<SqlDBContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ModelMapping.MappingAccount());
            modelBuilder.Configurations.Add(new ModelMapping.MappingAccountHistory());
            modelBuilder.Configurations.Add(new ModelMapping.MappingCurrencyConversion());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.AccountHistory> AccountHistorys { get; set; }

        public DbSet<Models.CurrencyConversion> CurrencyConversions { get; set; }



    }
}
