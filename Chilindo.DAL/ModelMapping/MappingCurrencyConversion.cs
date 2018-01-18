using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Chilindo.DAL.ModelMapping
{
    public class MappingCurrencyConversion : EntityTypeConfiguration<Models.CurrencyConversion>
    {
        public MappingCurrencyConversion()
        {
            this.HasKey(t => t.CurrencyConversionID);
            this.ToTable("CurrencyConversion");
        }
    }
}
