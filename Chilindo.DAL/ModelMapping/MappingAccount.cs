using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Chilindo.DAL.ModelMapping
{
    public class MappingAccount : EntityTypeConfiguration<Models.Account>
    {
        public MappingAccount()
        {
            this.HasKey(t => t.AccountNumber);
            this.Property(t => t.AccountNumber).IsRequired();
            this.Property(t => t.Balance).IsRequired();
            this.ToTable("Account");
        }
    }
}
