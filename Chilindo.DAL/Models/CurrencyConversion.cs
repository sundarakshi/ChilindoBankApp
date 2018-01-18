using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.DAL.Models
{
    public class CurrencyConversion
    {
        public int CurrencyConversionID { get; set; }
        public int APICurrencyDim { get; set; }
        public string ExpectedCurrency { get; set; }

        public decimal CurrencyValue { get; set; }

        public string APICurrencyType { get; set; }
    }
}
