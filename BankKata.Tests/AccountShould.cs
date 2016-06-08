using BankKata.Src;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests
{
    [TestFixture]
    public class AccountShould
    {
        [Test]
        public void MakeDeposit()
        {
            var mockConsole = new Mock<IConsole>();
            var mockedRepo = new Mock<TransactionRepository>();
            var amount = 10;
            var account = new Account(
                mockConsole.Object, mockedRepo.Object);

            account.Deposit(amount);

            mockedRepo.Verify(repo => repo.RecordDeposit(amount));
        }

        [Test]
        public void MakeWithdrawal()
        {
            
        }
    }
}
