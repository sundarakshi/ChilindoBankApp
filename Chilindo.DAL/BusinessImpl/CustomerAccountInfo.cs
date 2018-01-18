using Chilindo.DAL.BusinessContract;
using Chilindo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chilindo.DAL.BusinessImpl
{

    public class CustomerAccountInfo : ICustomerAccountInfo
    {
        private SqlDBContext dbContext;

        public CustomerAccountInfo()
        {
            dbContext = new SqlDBContext();
        }

        public IList<AccountHistory> AccountHistory(int accountNumber)
        {
            return dbContext.AccountHistorys.Where(x => x.AccountNumber == accountNumber).ToList();
        }

        public decimal GetAccountBalance(int accountNumber)
        {
            try
            {
                if (dbContext.Accounts.Where(x => x.AccountNumber == accountNumber).Count() > 0)
                {
                    decimal res = dbContext.Accounts.Single(x => x.AccountNumber.Equals(accountNumber)).Balance;
                    return res;
                }
                else
                {
                    throw new Exception("Account Number Not Found Exception");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertAccountHistory(AccountHistory accounHistory)
        {
            dbContext.AccountHistorys.Add(accounHistory);
            return true;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public bool UpdateBalance(Account account)
        {
            try
            {
                Account objTemp = dbContext.Accounts.Where(x => x.AccountNumber == account.AccountNumber).FirstOrDefault();
                objTemp.Balance = account.Balance;
                objTemp.LastUpdatedOn = account.LastUpdatedOn;
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbupdate)
            {
                var entity = dbupdate.Entries.Single().GetDatabaseValues();
                if (entity == null)
                {
                    string error = string.Format("Account Number :{0} not exits in the Account table.", account.AccountNumber);
                    Exception ex = new Exception(error);
                    throw ex;
                }
                else
                {
                    var data = dbupdate.Entries.Single();
                    data.OriginalValues.SetValues(data.GetDatabaseValues());
                    dbContext.SaveChanges();

                }
            }
            catch (Exception e1)
            {
                throw e1;
            }
            return true;
        }
    }

}


