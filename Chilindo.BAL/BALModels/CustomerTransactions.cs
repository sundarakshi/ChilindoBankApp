using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.BAL.BALModels
{

    public class CustomerTransactions
    {
        public int AccountId { get; set; }
        public long AccountNumber { get; set; }
        public String Currency { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }

        public DateTime LastUpdateOn { get; set; }

    }

    
}
