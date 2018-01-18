using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chilindo.DAL.Models;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Chilindo.DAL.BusinessContract;

namespace Chilindo.DAL.UnitTest
{
    [TestClass]
    public class AccountShouldReturn
    {

        public readonly ICustomerAccountInfo MockCustomerAccountRepo;
        public readonly ICurrency MockCurrencyRepo;


        Mock<ICustomerAccountInfo> mockCustomer;
        Mock<ICurrency> mockCurrency;


        IList<Models.Account> tblAccount;
        IList<Models.CurrencyConversion> tblCurrency;
        IList<Models.AccountHistory> tblAccountHistory;



        public AccountShouldReturn()
        {

            tblAccount = new List<Models.Account>
            {
                 new Account { AccountNumber=10001, Balance=453, LastUpdatedOn = DateTime.Now},
                 new Account { AccountNumber=10002, Balance=345, LastUpdatedOn = DateTime.Now},
                 new Account { AccountNumber=10003, Balance=234, LastUpdatedOn = DateTime.Now},
                 new Account { AccountNumber=10004, Balance=678, LastUpdatedOn = DateTime.Now},
                 new Account { AccountNumber=10005, Balance=54, LastUpdatedOn = DateTime.Now},
                 new Account { AccountNumber=10006, Balance=34, LastUpdatedOn = DateTime.Now},
                 new Account { AccountNumber=100, Balance=100, LastUpdatedOn = DateTime.Now }
            };

            tblAccountHistory = new List<Models.AccountHistory>
            {
                new AccountHistory { AccountId=1, AccountNumber=100, Amount=100, Currency="USD", LastUpdateOn=DateTime.Now, TransactionType="Deposit"}
            };


            tblCurrency = new List<Models.CurrencyConversion>
            {
                new CurrencyConversion { CurrencyConversionID=1, APICurrencyDim =1, ExpectedCurrency="INR", CurrencyValue=0.0156979m, APICurrencyType="USD"},
                new CurrencyConversion { CurrencyConversionID=2, APICurrencyDim =1, ExpectedCurrency="TBH", CurrencyValue=0.0156979m, APICurrencyType="USD"},
                new CurrencyConversion { CurrencyConversionID=3, APICurrencyDim =1, ExpectedCurrency="GDB", CurrencyValue=0.0156979m, APICurrencyType="USD"},
                new CurrencyConversion { CurrencyConversionID=4, APICurrencyDim =1, ExpectedCurrency="USD", CurrencyValue=1, APICurrencyType="USD"}



            };

            mockCustomer = new Mock<ICustomerAccountInfo>();
            mockCurrency = new Mock<ICurrency>();
            this.MockCustomerAccountRepo = mockCustomer.Object;
            this.MockCurrencyRepo = mockCurrency.Object;


        }

        [TestMethod]
        public void GetAccountBalance()
        {
            decimal expectedResult = 453;
            mockCustomer.Setup(x => x.GetAccountBalance(It.IsAny<int>())).Returns((int d) => tblAccount.Where(y => y.AccountNumber == d).Single().Balance);
            var result = this.MockCustomerAccountRepo.GetAccountBalance(10001).ToString();
            Assert.AreEqual(expectedResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void GetCurrencyValue()
        {
            decimal expectedResult = 1;
            mockCurrency.Setup(p => p.GetCurrency(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResult);
            var result = this.MockCurrencyRepo.GetCurrency("USD", "USD");
            Assert.AreEqual(expectedResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void InsertAccountHistory()
        {
            bool expectedResult = true;
            int expectedRowCount = 2;

            mockCustomer.Setup(x => x.InsertAccountHistory(It.IsAny<AccountHistory>())).Returns((AccountHistory his) =>
            {
                tblAccountHistory.Add(his);
                return true;
            });

            var result = this.MockCustomerAccountRepo.InsertAccountHistory(new AccountHistory
            {
                AccountId = 2,
                AccountNumber = 2,
                Amount = 200,
                Currency = "INR",
                TransactionType = "Deposit",
                LastUpdateOn = DateTime.Now
            });

            var count = tblAccountHistory.Count();
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedRowCount, count);
        }

        [TestMethod]
        public void UpdateBalance()
        {

            int expectedBalanceNow = 5000;
            int expectedBalanceBefore = 100;

            mockCustomer.Setup(x => x.UpdateBalance(It.IsAny<Account>())).Returns((Account acc) =>
            {
                tblAccount.Where(x => x.AccountNumber == acc.AccountNumber).FirstOrDefault().Balance = acc.Balance;
                return true;
            });

            var resultBalanceBefore = tblAccount.Where(x => x.AccountNumber == 100).First().Balance;

            var result = this.MockCustomerAccountRepo.UpdateBalance(new Account
            {
                 AccountNumber = 100, Balance =5000, LastUpdatedOn = DateTime.Now 
            });

            var resultBalanceAfter = tblAccount.Where(x => x.AccountNumber == 100).First().Balance;

            Assert.AreEqual(expectedBalanceBefore, resultBalanceBefore);
            Assert.AreEqual(expectedBalanceNow, resultBalanceAfter);

        }
    }
}
