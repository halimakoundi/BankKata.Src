using System.Collections.Generic;
using BankKata.Src;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests
{
    [TestFixture]
    public class AccountShould
    {
        private Mock<IConsole> _console;
        private Mock<ITransactionRepository> _repo;
        private Account _account;
        private Mock<IDateProvider> _dateProvider;

        [SetUp]
        public void SetUp()
        {
            _console = new Mock<IConsole>();
            _dateProvider = new Mock<IDateProvider>();
            _repo = new Mock<ITransactionRepository>();
            _account = new Account(
                _console.Object, _repo.Object);
        }

        [Test]
        public void MakeDeposit()
        {
            var amount = 10;

            _account.Deposit(amount);

            _repo.Verify(repo => repo.RecordDeposit(amount));
        }

        [Test]
        public void MakeWithdrawal()
        {
            var amount = 10;

            _account.Withdraw(amount);

            _repo.Verify(repo => repo.RecordWithdrawal(amount));
        }

        [Test]
        public void PrintHeaderWhenThereAreNoTransaction()
        {
            _repo.Setup(r => r.GetTransactions())
               .Returns(new List<Transaction>());

            _account.Print();

            _console.Verify(c => c.PrintLine("date || credit || debit || balance"));
        }

        [Test]
        public void PrintCreditLineWhenThereIsADeposit()
        {
            var printedLines = new List<string>();
            _console.Setup(c => c.PrintLine(It.IsAny<string>()))
                .Callback<string>(r => printedLines.Add(r));

            _repo.Setup(r => r.GetTransactions())
                .Returns(new List<Transaction> { new Transaction(1000, "10/01/2012") });

            _account.Print();

            Assert.That(printedLines[0], Is.EqualTo("date || credit || debit || balance"));
            Assert.That(printedLines[1], Is.EqualTo("10/01/2012 || 1000.00 || || 1000.00"));
        }

        [Test]
        public void PrintrightBalanceWhenTereManyDeposits()
        {
            var printedLines = new List<string>();
            _console.Setup(c => c.PrintLine(It.IsAny<string>()))
                .Callback<string>(r => printedLines.Add(r));

            _repo.Setup(r => r.GetTransactions())
                .Returns(new List<Transaction> {
                    new Transaction(1000, "10/01/2012"),
                    new Transaction(2000, "13/01/2012")
                });

            _account.Print();

            Assert.That(printedLines[0], Is.EqualTo("date || credit || debit || balance"));
            Assert.That(printedLines[1], Is.EqualTo("13/01/2012 || 2000.00 || || 3000.00"));
            Assert.That(printedLines[2], Is.EqualTo("10/01/2012 || 1000.00 || || 1000.00"));
        }

        [Test]
        public void PrintDebitLineWhenWithdrawalInTransactionStore()
        {
            var printedLines    = new List<string>();
            _console.Setup(c => c.PrintLine(It.IsAny<string>()))
                .Callback<string>(r => printedLines.Add(r));
            _repo.Setup(r => r.GetTransactions()).Returns(new List<Transaction>
            {
                new Transaction(-500, "14/01/2012")
            });

            _account.Print();

            Assert.That(printedLines[0], Is.EqualTo("date || credit || debit || balance"));
            Assert.That(printedLines[1], Is.EqualTo("14/01/2012 || || 500.00 || -500.00"));
        }
    }
}
