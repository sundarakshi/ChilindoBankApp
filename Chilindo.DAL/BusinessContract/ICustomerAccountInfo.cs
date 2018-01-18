using Chilindo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.DAL.BusinessContract
{
    public interface ICustomerAccountInfo
    {
        decimal GetAccountBalance(int accountNumber);
        bool InsertAccountHistory(Models.AccountHistory accounHistory);
        bool UpdateBalance(Models.Account account);
        void Save();

        IList<AccountHistory> AccountHistory(int accountNumber);
    }
}
