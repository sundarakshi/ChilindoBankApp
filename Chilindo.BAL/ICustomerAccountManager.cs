using Chilindo.BAL.BALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.BAL
{
    public interface ICustomerAccountManager
    {

        decimal AccountBalance(int accountNumber);
        Chilindo.DAL.Models.CurrencyBalance GetCurrencyAccountBalance(int accNo, string APICurrency, string ExpectedCurrency);

        List<CustomerTransactions> GetAccountHistory(int accNo);

        Chilindo.BAL.BALModels.ProcessedResponse Deposit(int accountNumber, decimal amount, string apiCurrency, string currency);


        Chilindo.BAL.BALModels.ProcessedResponse Withdraw(int accountNumber, decimal amount, string apiCurrency, string currency);
    }
}
