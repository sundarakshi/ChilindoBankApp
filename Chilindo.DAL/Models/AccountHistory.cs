using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.DAL.Models
{
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
