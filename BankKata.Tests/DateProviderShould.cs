using System;
using BankKata.Src;
using NUnit.Framework;

namespace BankKata.Tests
{
    [TestFixture]
    public class DateProviderShould
    {
        [Test]
        public void ReturnTodaysDate()
        {
            var expected = DateTime.Now.ToString("d");
            var dateProvider = new DateProvider();

            var date = dateProvider.Now();

            Assert.That(date, Is.EqualTo(expected));
        }
    }
}
