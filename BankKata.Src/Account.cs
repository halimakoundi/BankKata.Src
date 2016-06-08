namespace BankKata.Src
{
    public class Account
    {
        private IConsole _console;
        private TransactionRepository _transactionRepository;

        public Account(IConsole console, TransactionRepository transactionRepository)
        {
            this._console = console;
            this._transactionRepository = transactionRepository;
        }

        public void Deposit(int amount)
        {
            _transactionRepository.RecordDeposit(amount);
        }

        public void Withdraw(int amount)
        {
            throw new System.NotImplementedException();
        }

        public void Print()
        {
            throw new System.NotImplementedException();
        }
    }
}