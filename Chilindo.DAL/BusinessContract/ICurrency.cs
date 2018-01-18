using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.DAL.BusinessContract
{
    public interface ICurrency
    {
        decimal GetCurrency(string APICurrency, string ExpectedCurrency);
    }
}
