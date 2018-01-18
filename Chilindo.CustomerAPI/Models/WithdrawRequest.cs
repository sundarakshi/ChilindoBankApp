
using Chilindo.CustomerAPI.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chilindo.CustomerAPI.Models
{
    [ValidateModel]
    public class WithdrawRequest
    {
        [JsonProperty("AccountNumber")]
        [Required]
        public int AccountNumber { get; set; }

        [JsonProperty("Currency")]
        [Required]
        public CurrencyName Currency { get; set; }

        [JsonProperty("Amount")]
        [Required]
        public decimal Amount { get; set; }
    }

    public enum CurrencyName
    {
        USD,
        THB,
        INR,
        GDB
    }
}