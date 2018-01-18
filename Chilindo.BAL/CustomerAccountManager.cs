using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chilindo.DAL.BusinessContract;
using Chilindo.BAL.BALModels;

namespace Chilindo.BAL
{
    public class CustomerAccountManager : ICustomerAccountManager
    {
        private  ICustomerAccountInfo _customerAccountInfo;
        private  ICurrency _CurrencyConversion;

        public CustomerAccountManager()
        {
            _customerAccountInfo = new Chilindo.DAL.BusinessImpl.CustomerAccountInfo();
            _CurrencyConversion = new Chilindo.DAL.BusinessImpl.Currency();
        }

        public CustomerAccountManager(ICustomerAccountInfo custAcctInfo, ICurrency _currency )
        {
            _customerAccountInfo = custAcctInfo;
            _CurrencyConversion = _currency;
        }


        public decimal AccountBalance(int accountNumber)
        {
            decimal res = _customerAccountInfo.GetAccountBalance(accountNumber);
            return res;
        }

        public Chilindo.DAL.Models.CurrencyBalance GetCurrencyAccountBalance(int accNo, string APICurrency, string ExpectedCurrency)
        {
            var tempCurrentBalance = _customerAccountInfo.GetAccountBalance(accNo);
            var currentCoversion = _CurrencyConversion.GetCurrency(ExpectedCurrency, APICurrency);
            var res = currentCoversion * tempCurrentBalance;
            Chilindo.DAL.Models.CurrencyBalance result = new Chilindo.DAL.Models.CurrencyBalance { Balance = res, ExpectedCurrency = ExpectedCurrency };
            return result;
        }


        public List<CustomerTransactions> GetAccountHistory(int accNo)
        {
            IList<Chilindo.DAL.Models.AccountHistory> lstHistory = _customerAccountInfo.AccountHistory(accNo);
            List<CustomerTransactions> custTran = new List<CustomerTransactions>();
            CustomerTransactions objCustomerTran;
            foreach (Chilindo.DAL.Models.AccountHistory history in lstHistory)
            {
                objCustomerTran = new CustomerTransactions();
                objCustomerTran.TransactionType = history.TransactionType;
                objCustomerTran.Currency = history.Currency;
                objCustomerTran.Amount = history.Amount;
                objCustomerTran.LastUpdateOn = history.LastUpdateOn;
                objCustomerTran.AccountId = history.AccountId;
                objCustomerTran.AccountNumber = history.AccountNumber;
                custTran.Add(objCustomerTran);
            }
            return custTran;
        }

        public Chilindo.BAL.BALModels.ProcessedResponse Deposit(int accountNumber, decimal amount, string apiCurrency, string currency)
        {
            Chilindo.BAL.BALModels.ProcessedResponse objResponse = null;
            decimal newBalance = 0;
            try
            {
                var tempCurrentBalance = _customerAccountInfo.GetAccountBalance(accountNumber);

                var currentCoversion = _CurrencyConversion.GetCurrency(apiCurrency, currency);
                newBalance = (currentCoversion * amount) + tempCurrentBalance;

                // Update the Balance
                Chilindo.DAL.Models.Account tempAccount = new Chilindo.DAL.Models.Account();
                tempAccount.AccountNumber = accountNumber;
                tempAccount.Balance = newBalance;
                tempAccount.LastUpdatedOn = DateTime.UtcNow;
                _customerAccountInfo.UpdateBalance(tempAccount);

                // Update the AccountHistory Table
                Chilindo.DAL.Models.AccountHistory tempAccountHistory = new Chilindo.DAL.Models.AccountHistory();
                tempAccountHistory.AccountNumber = accountNumber;
                tempAccountHistory.Amount = amount;
                tempAccountHistory.Currency = currency;
                tempAccountHistory.TransactionType = "Deposit";
                tempAccountHistory.LastUpdateOn = DateTime.UtcNow;
                _customerAccountInfo.InsertAccountHistory(tempAccountHistory);
                _customerAccountInfo.Save();
                objResponse = ProcessResponse(accountNumber, newBalance, currency, true, "Processed Succcessful");
            }
            catch (Exception ex)
            {
                objResponse = ProcessResponse(accountNumber, newBalance, currency, false, ex.Message);
            }
            return objResponse;
        }


        public Chilindo.BAL.BALModels.ProcessedResponse Withdraw(int accountNumber, decimal amount, string apiCurrency, string currency)
        {
            Chilindo.BAL.BALModels.ProcessedResponse objResponse = null;
            decimal newBalance = 0;
            try
            {
                var tempCurrentBalance = _customerAccountInfo.GetAccountBalance(accountNumber);
                var currentCoversion = _CurrencyConversion.GetCurrency(apiCurrency, currency);
                newBalance = tempCurrentBalance - (currentCoversion * amount);
                if (newBalance > 0)
                {
                    // Update the Balance
                    Chilindo.DAL.Models.Account tempAccount = new Chilindo.DAL.Models.Account();
                    tempAccount.AccountNumber = accountNumber;
                    tempAccount.Balance = newBalance;
                    tempAccount.LastUpdatedOn = DateTime.UtcNow;
                    _customerAccountInfo.UpdateBalance(tempAccount);

                    // Update the AccountHistory Table
                    Chilindo.DAL.Models.AccountHistory tempAccountHistory = new Chilindo.DAL.Models.AccountHistory();
                    tempAccountHistory.AccountNumber = accountNumber;
                    tempAccountHistory.Amount = amount;
                    tempAccountHistory.Currency = currency;
                    tempAccountHistory.TransactionType = "Withdraw";
                    tempAccountHistory.LastUpdateOn = DateTime.UtcNow;
                    _customerAccountInfo.InsertAccountHistory(tempAccountHistory);
                    _customerAccountInfo.Save();
                    objResponse = ProcessResponse(accountNumber, newBalance, currency, true, "Processed Succcessful");
                }
                else
                {
                    objResponse = ProcessResponse(accountNumber, newBalance, currency, false, "Insufficient Money in your account");
                }
            }
            catch (Exception ex)
            {
                objResponse = ProcessResponse(accountNumber, newBalance, currency, false, ex.Message);
            }
            return objResponse;
        }

        private Chilindo.BAL.BALModels.ProcessedResponse ProcessResponse(int accNo, decimal bal, string curr, bool status, string err)
        {
            Chilindo.BAL.BALModels.ProcessedResponse result = new Chilindo.BAL.BALModels.ProcessedResponse();
            result.AccountNumber = accNo;
            result.Balance = bal;
            result.Currency = curr;
            result.Sucessful = status;
            result.Message = err;
            return result;
        }
    }
}
