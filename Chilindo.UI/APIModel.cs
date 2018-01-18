using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.UI
{
    public class WithdrawRequest
    {
        [JsonProperty("AccountNumber")]
 
        public int AccountNumber { get; set; }

        [JsonProperty("Currency")]
        public CurrencyName Currency { get; set; }

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }
    }

    public enum CurrencyName
    {
        USD,
        THB,
        INR,
        GDB
    }


    public class AccountHistory
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public String Currency { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }

        public DateTime LastUpdateOn { get; set; }

    }
}
