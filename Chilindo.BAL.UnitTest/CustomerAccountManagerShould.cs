using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chilindo.DAL.BusinessContract;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace Chilindo.BAL.UnitTest
{
    [TestClass]
    public class CustomerAccountManagerShould
    {
        public readonly ICustomerAccountInfo MockCustomerAccountRepo;
        public readonly ICurrency MockCurrencyRepo;
        public readonly ICustomerAccountManager MockCustomerAccountManagerRepo;



        Mock<ICustomerAccountInfo> mockCustomer;
        Mock<ICurrency> mockCurrency;
        Mock<ICustomerAccountManager> mockCustomerAccountManager;

        IList<DAL.Models.Account> tblAccount;
        IList<DAL.Models.CurrencyConversion> tblCurrency;
        IList<DAL.Models.AccountHistory> tblAccountHistory;


        public CustomerAccountManagerShould()
        {
            tblAccount = new List<DAL.Models.Account>
            {
                 new DAL.Models.Account { AccountNumber=10001, Balance=4000, LastUpdatedOn = DateTime.Now},
                 new DAL.Models.Account { AccountNumber=10002, Balance=345, LastUpdatedOn = DateTime.Now},
                 new DAL.Models.Account { AccountNumber=10003, Balance=234, LastUpdatedOn = DateTime.Now},
                 new DAL.Models.Account { AccountNumber=10004, Balance=678, LastUpdatedOn = DateTime.Now},
                 new DAL.Models.Account { AccountNumber=10005, Balance=54, LastUpdatedOn = DateTime.Now},
                 new DAL.Models.Account { AccountNumber=10006, Balance=34, LastUpdatedOn = DateTime.Now},
                 new DAL.Models.Account { AccountNumber=100, Balance=100, LastUpdatedOn = DateTime.Now }
            };

            tblAccountHistory = new List<DAL.Models.AccountHistory>
            {
                new DAL.Models.AccountHistory { AccountId=1, AccountNumber=100, Amount=100, Currency="USD", LastUpdateOn=DateTime.Now, TransactionType="Deposit"}
            };


            tblCurrency = new List<DAL.Models.CurrencyConversion>
            {
                new DAL.Models.CurrencyConversion { CurrencyConversionID=1, APICurrencyDim =1, ExpectedCurrency="INR", CurrencyValue=0.0156979m, APICurrencyType="USD"},
                new DAL.Models.CurrencyConversion { CurrencyConversionID=2, APICurrencyDim =1, ExpectedCurrency="TBH", CurrencyValue=0.0156979m, APICurrencyType="USD"},
                new DAL.Models.CurrencyConversion { CurrencyConversionID=3, APICurrencyDim =1, ExpectedCurrency="GDB", CurrencyValue=0.0156979m, APICurrencyType="USD"},
                new DAL.Models.CurrencyConversion { CurrencyConversionID=4, APICurrencyDim =1, ExpectedCurrency="USD", CurrencyValue=1, APICurrencyType="USD"},
                 new DAL.Models.CurrencyConversion { CurrencyConversionID=1, APICurrencyDim =1, ExpectedCurrency="USD", CurrencyValue=67, APICurrencyType="INR"}
            };

            mockCustomer = new Mock<ICustomerAccountInfo>();
            mockCurrency = new Mock<ICurrency>();
            this.MockCustomerAccountRepo = mockCustomer.Object;
            this.MockCurrencyRepo = mockCurrency.Object;


         


            mockCustomerAccountManager = new Mock<ICustomerAccountManager>();
            this.MockCustomerAccountManagerRepo = mockCustomerAccountManager.Object;

        }


        [TestMethod]
        public void ShowBalanceinCongiguredCurrency()
        {
            mockCustomer.Setup(x => x.GetAccountBalance(It.IsAny<int>())).Returns((int d) => tblAccount.Where(y => y.AccountNumber == d).Single().Balance);

            mockCurrency.Setup(p => p.GetCurrency(It.IsAny<string>(), It.IsAny<string>())).Returns((string a, string b) => 
            {
                return tblCurrency.Where(x => x.APICurrencyType == a && x.ExpectedCurrency == b).Single().CurrencyValue;
            });

            CustomerAccountManager obj = new CustomerAccountManager(MockCustomerAccountRepo, MockCurrencyRepo);
            var result = obj.GetCurrencyAccountBalance(10001, "USD", "USD");
            var ExpectedResult = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;
            Assert.AreEqual(ExpectedResult, result.Balance);
           
        }


        [TestMethod]
        public void ShowBalanceinINRCurrency()
        {
            mockCustomer.Setup(x => x.GetAccountBalance(It.IsAny<int>())).Returns((int d) => tblAccount.Where(y => y.AccountNumber == d).Single().Balance);

            mockCurrency.Setup(p => p.GetCurrency(It.IsAny<string>(), It.IsAny<string>())).Returns((string a, string b) =>
            {
                return tblCurrency.Where(x => x.APICurrencyType == a && x.ExpectedCurrency == b).Single().CurrencyValue;
            });

            CustomerAccountManager obj = new CustomerAccountManager(MockCustomerAccountRepo, MockCurrencyRepo);
            var result = obj.GetCurrencyAccountBalance(10001, "USD", "INR");
            var ExpectedResult = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;
            Assert.AreNotEqual(ExpectedResult, result.Balance);

        }

        [TestMethod]
        public void DepositValidSameCurrency()
        {
            mockCustomer.Setup(x => x.GetAccountBalance(It.IsAny<int>())).Returns((int d) => tblAccount.Where(y => y.AccountNumber == d).Single().Balance);

            mockCurrency.Setup(p => p.GetCurrency(It.IsAny<string>(), It.IsAny<string>())).Returns((string a, string b) =>
            {
                return tblCurrency.Where(x => x.APICurrencyType == a && x.ExpectedCurrency == b).Single().CurrencyValue;
            });

            mockCustomer.Setup(p => p.UpdateBalance(It.IsAny<DAL.Models.Account>())).Returns((DAL.Models.Account acc) => 
            {
                tblAccount.Where(x => x.AccountNumber == acc.AccountNumber).FirstOrDefault().Balance = acc.Balance;
                return true;
            });

 
            var balanceBeforeUpdate = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;

            CustomerAccountManager obj = new CustomerAccountManager(MockCustomerAccountRepo, MockCurrencyRepo);
            var result = obj.Deposit(10001,100, "USD", "USD");

            var balanceAfterUpdate = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;

            Assert.IsTrue(balanceAfterUpdate > balanceBeforeUpdate);
            Assert.AreEqual(result.Balance, balanceAfterUpdate);
            Assert.AreEqual(result.Message, "Processed Succcessful");
        }

        [TestMethod]
        public void WithdrawValidMoney()
        {
            mockCustomer.Setup(x => x.GetAccountBalance(It.IsAny<int>())).Returns((int d) => tblAccount.Where(y => y.AccountNumber == d).Single().Balance);

            mockCurrency.Setup(p => p.GetCurrency(It.IsAny<string>(), It.IsAny<string>())).Returns((string a, string b) =>
            {
                return tblCurrency.Where(x => x.APICurrencyType == a && x.ExpectedCurrency == b).Single().CurrencyValue;
            });

            mockCustomer.Setup(p => p.UpdateBalance(It.IsAny<DAL.Models.Account>())).Returns((DAL.Models.Account acc) =>
            {
                tblAccount.Where(x => x.AccountNumber == acc.AccountNumber).FirstOrDefault().Balance = acc.Balance;
                return true;
            });


            var balanceBeforeUpdate = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;

            CustomerAccountManager obj = new CustomerAccountManager(MockCustomerAccountRepo, MockCurrencyRepo);
            var result = obj.Withdraw(10001, 100, "USD", "USD");

            var balanceAfterUpdate = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;
            Assert.AreEqual(result.Message, "Processed Succcessful");
        }


        [TestMethod]
        public void WithdrawInvalidMoney()
        {
            mockCustomer.Setup(x => x.GetAccountBalance(It.IsAny<int>())).Returns((int d) => tblAccount.Where(y => y.AccountNumber == d).Single().Balance);

            mockCurrency.Setup(p => p.GetCurrency(It.IsAny<string>(), It.IsAny<string>())).Returns((string a, string b) =>
            {
                return tblCurrency.Where(x => x.APICurrencyType == a && x.ExpectedCurrency == b).Single().CurrencyValue;
            });

            mockCustomer.Setup(p => p.UpdateBalance(It.IsAny<DAL.Models.Account>())).Returns((DAL.Models.Account acc) =>
            {
                tblAccount.Where(x => x.AccountNumber == acc.AccountNumber).FirstOrDefault().Balance = acc.Balance;
                return true;
            });


            var balanceBeforeUpdate = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;

            CustomerAccountManager obj = new CustomerAccountManager(MockCustomerAccountRepo, MockCurrencyRepo);
            var result = obj.Withdraw(10001, 4000, "USD", "USD");

            var balanceAfterUpdate = tblAccount.Where(x => x.AccountNumber == 10001).FirstOrDefault().Balance;
            Assert.AreEqual(result.Message, "Insufficient Money in your account");
        }
    }
}
