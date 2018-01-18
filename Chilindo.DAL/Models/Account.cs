using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chilindo.DAL.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }

        [ConcurrencyCheck]
        public DateTime LastUpdatedOn { get; set; }
    }
}
