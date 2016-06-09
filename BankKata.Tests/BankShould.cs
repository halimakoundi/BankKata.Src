using System.Collections.Generic;
using BankKata.Src;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests
{
    [TestFixture]
    public class BankShould
    {
        [Test]
        public void AcceptUserTest1()
        {
            var printedLines = new List<string>();

            var console = new Mock<IConsole>();

            console.Setup(c => c.PrintLine(It.IsAny<string>()))
                .Callback<string>(r => printedLines.Add(r));

            var dateProvider = new Mock<IDateProvider>();
            var account = new Account(
                console.Object, 
                new TransactionRepository(dateProvider.Object));

            dateProvider.Setup(dp => dp.Now())
                .Returns("10/01/2012");
            account.Deposit(1000);
            dateProvider.Setup(dp => dp.Now())
                .Returns("13/01/2012");
            account.Deposit(2000);
            dateProvider.Setup(dp => dp.Now())
                .Returns("14/01/2012");
            account.Withdraw(500);

            account.Print();

            Assert.That(printedLines[0], Is.EqualTo("date || credit || debit || balance"));
            Assert.That(printedLines[1], Is.EqualTo("14/01/2012 || || 500.00 || 2500.00"));
            Assert.That(printedLines[2], Is.EqualTo("13/01/2012 || 2000.00 || || 3000.00"));
            Assert.That(printedLines[3], Is.EqualTo("10/01/2012 || 1000.00 || || 1000.00"));

        }

    }
}
