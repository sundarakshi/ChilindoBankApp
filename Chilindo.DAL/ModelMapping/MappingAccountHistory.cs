using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Chilindo.DAL.ModelMapping
{
    public class MappingAccountHistory : EntityTypeConfiguration<Models.AccountHistory>
    {
        public MappingAccountHistory()
        {
            this.HasKey(t => t.AccountId);
            this.Property(t => t.AccountNumber).IsRequired();
            this.Property(t => t.Amount).IsRequired();
            this.Property(t => t.Currency).IsRequired();
            this.ToTable("AccountHistory");

        }

    }
}
