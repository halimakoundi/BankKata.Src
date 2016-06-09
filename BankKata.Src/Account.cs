using System.Linq;

namespace BankKata.Src
{
    public class Account
    {
        private readonly IConsole _console;
        private readonly ITransactionRepository _transactionRepository;

        public Account(IConsole console, ITransactionRepository transactionRepository)
        {
            _console = console;
            _transactionRepository = transactionRepository;
        }

        public void Deposit(int amount)
        {
            _transactionRepository.RecordDeposit(amount);
        }

        public void Withdraw(int amount)
        {
            _transactionRepository.RecordWithdrawal(amount);
        }

        public void Print()
        {
            _console.PrintLine("date || credit || debit || balance");
            var transactions = _transactionRepository.GetTransactions();
            transactions.Reverse();

            for (var i = 0; i <transactions.Count; i++)
            {
                var transaction = transactions[i];
                var solde = transactions.Skip(i).Sum(x => x.GetAmount());
                _console.PrintLine(transaction + $"|| {solde.ToString("0.00")}");
            }
        }
    }
}