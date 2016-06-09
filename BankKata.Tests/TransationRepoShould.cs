using System.Collections.Generic;
using BankKata.Src;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests
{
    [TestFixture]
    class TransationRepoShould
    {
        private string A_DATE = "2016/01/01";
        private Mock<IDateProvider> _dateProvider;
        private TransactionRepository _repo;
        private const int AN_AMOUNT = 1000;

        [SetUp]
        public void Setup()
        {
            _dateProvider = new Mock<IDateProvider>();
            _repo = new TransactionRepository(_dateProvider.Object);
        }

        [Test]
        public void RequireTodayDate_WhenRecordingADeposit()
        {
            _repo.RecordDeposit(AN_AMOUNT);

            _dateProvider.Verify(dp => dp.Now());
        }

        [Test]
        public void StoreADeposit()
        {
            _dateProvider.Setup(dp => dp.Now()).Returns(A_DATE);
            var deposit = new Transaction(AN_AMOUNT, A_DATE);
            var expectedTransactions = new List<Transaction> { deposit };

            _repo.RecordDeposit(AN_AMOUNT);

            CollectionAssert
                .AreEqual(_repo.GetTransactions(), expectedTransactions);

        }

        [Test]
        public void RequireTodayDate_WhenRecordingAWithdrawal()
        {
            _repo.RecordWithdrawal(AN_AMOUNT);

            _dateProvider.Verify(dp => dp.Now());
        }

        [Test]
        public void StoreAWithdrawal()
        {
            _dateProvider.Setup(dp => dp.Now()).Returns(A_DATE);
            var withdrawal = new Transaction(-AN_AMOUNT, A_DATE);
            var expectedTransactions = new List<Transaction> { withdrawal };

            _repo.RecordWithdrawal(AN_AMOUNT);

            CollectionAssert
                .AreEqual(_repo.GetTransactions(), expectedTransactions);
        }
    }
}
