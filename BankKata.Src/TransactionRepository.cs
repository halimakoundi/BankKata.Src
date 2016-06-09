using System.Collections.Generic;

namespace BankKata.Src
{
    public interface ITransactionRepository
    {
        void RecordDeposit(int amount);
        void RecordWithdrawal(int amount);
        List<Transaction> GetTransactions();
    }

    public class TransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private readonly IDateProvider _dateProvider;

        public TransactionRepository(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public void RecordDeposit(int amount)
        {            
            _transactions.Add(new Transaction(amount, _dateProvider.Now()));
        }

        public void RecordWithdrawal(int amount)
        {
            _transactions.Add(new Transaction(-amount, _dateProvider.Now()));
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
    }
}