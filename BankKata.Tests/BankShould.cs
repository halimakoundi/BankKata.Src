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

            var mockedRepo  = new Mock<TransactionRepository>();
            var account = new Account(console.Object, mockedRepo.Object);
            account.Deposit(1000);
            account.Deposit(2000);
            account.Withdraw(2000);

            account.Print();

            Assert.That(printedLines[0], Is.EqualTo("date || credit || debit || balance"));
            Assert.That(printedLines[1], Is.EqualTo("14/01/2012 || || 500.00 || 2500.00"));
            Assert.That(printedLines[2], Is.EqualTo("13/01/2012 || 2000.00 || || 3000.00"));
            Assert.That(printedLines[3], Is.EqualTo("10/01/2012 || 1000.00 || || 1000.00"));

        }

    }
}
