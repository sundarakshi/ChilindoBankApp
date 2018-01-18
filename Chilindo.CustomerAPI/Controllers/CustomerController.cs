using Chilindo.CustomerAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chilindo.CustomerAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string APICurrency;


        [HttpGet]
        public string Get(int AccountNumber)
        {
            Chilindo.BAL.CustomerAccountManager objAccountDetails = new BAL.CustomerAccountManager();
            return objAccountDetails.AccountBalance(AccountNumber).ToString();
        }

        [HttpGet]
        public List<Chilindo.BAL.BALModels.CustomerTransactions> GetAccountDetails(int AccountNumber)
        {
            Chilindo.BAL.CustomerAccountManager objAccountDetails = new BAL.CustomerAccountManager();
            return objAccountDetails.GetAccountHistory(AccountNumber);
        }

                [HttpPost]
        [Route("api/Customer/Withdraw/")]
        [ActionName("Withdraw")]
        public Models.CustomerResponse Withdraw([FromBody] Models.WithdrawRequest withdrawRequest)
        {
            Models.CustomerResponse res = new CustomerResponse();
            Chilindo.BAL.BALModels.ProcessedResponse result = new BAL.BALModels.ProcessedResponse();
            try
            {
                Chilindo.BAL.CustomerAccountManager objAccountDetails = new BAL.CustomerAccountManager();
                APICurrency = ConfigurationManager.AppSettings["APICURRENCY"];
                CurrencyName curr = (CurrencyName)Enum.Parse(typeof(CurrencyName), withdrawRequest.Currency.ToString());
                result = objAccountDetails.Withdraw(withdrawRequest.AccountNumber, withdrawRequest.Amount, APICurrency, curr.ToString());
                res = new CustomerResponse { AccountNumber = result.AccountNumber, Balance = result.Balance, Currency = result.Currency, Message = result.Message, Successful = true };
            }catch(Exception ex)
            {
                res = new CustomerResponse { AccountNumber = result.AccountNumber, Balance = result.Balance, Currency = result.Currency, Message = string.Format("API Error :{0} / Stack Trace {1}", result.Message,ex.StackTrace), Successful = true };
            }
            return res;

        }


        [HttpPost]
        [Route("api/Customer/Deposit/")]
        [ActionName("Deposit")]
        public Models.CustomerResponse Deposit([FromBody] Models.WithdrawRequest withdrawRequest)
        {
            Models.CustomerResponse res = new CustomerResponse();
            Chilindo.BAL.BALModels.ProcessedResponse result = new BAL.BALModels.ProcessedResponse();
            try
            {
                Chilindo.BAL.CustomerAccountManager objAccountDetails = new BAL.CustomerAccountManager();
                APICurrency = ConfigurationManager.AppSettings["APICURRENCY"];
                CurrencyName curr = (CurrencyName)Enum.Parse(typeof(CurrencyName), withdrawRequest.Currency.ToString());
                result = objAccountDetails.Deposit(withdrawRequest.AccountNumber, withdrawRequest.Amount, APICurrency, curr.ToString());
                res = new CustomerResponse { AccountNumber = result.AccountNumber, Balance = result.Balance, Currency = result.Currency, Message = result.Message, Successful = true };
            }
            catch (Exception ex)
            {
                res = new CustomerResponse { AccountNumber = result.AccountNumber, Balance = result.Balance, Currency = result.Currency, Message = string.Format("API Error :{0} / Stack Trace {1}", result.Message, ex.StackTrace), Successful = true };
            }
            return res;

        }

    }
}