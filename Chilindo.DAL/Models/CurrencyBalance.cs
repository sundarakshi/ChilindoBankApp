using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.DAL.Models
{
    public class CurrencyBalance
    {
        public decimal Balance { get; set; }
        public string ExpectedCurrency { get; set; }
    }
}
