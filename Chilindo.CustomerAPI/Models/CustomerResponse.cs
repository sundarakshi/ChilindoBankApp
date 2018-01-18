using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chilindo.CustomerAPI.Models
{
    public class CustomerResponse
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool Successful { get; set; }

        public string Currency { get; set; }

        public string Message { get; set; }
    }
}