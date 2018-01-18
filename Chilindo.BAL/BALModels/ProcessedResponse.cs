using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.BAL.BALModels
{
    public class ProcessedResponse
    {
        public int AccountNumber { get; set; }
        public bool Sucessful { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public string Message { get; set; }
    }
}
