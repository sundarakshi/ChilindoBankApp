using Chilindo.DAL.BusinessContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chilindo.DAL.BusinessImpl
{
    public class Currency : ICurrency
    {

        private SqlDBContext dbContext;

        public Currency()
        {
            dbContext = new SqlDBContext();
        }
        public decimal GetCurrency(string APICurrency, string ExpectedCurrency)
        {
            var result = dbContext.CurrencyConversions.Single(x => x.APICurrencyType == APICurrency && x.ExpectedCurrency == ExpectedCurrency).CurrencyValue;

            return result;

        }
    }
}
