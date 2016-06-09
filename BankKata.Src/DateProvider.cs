using System;

namespace BankKata.Src
{
    public class DateProvider:IDateProvider
    {
        public string Now()
        {
            return DateTime.Now.ToString("d");
        }
    }
}