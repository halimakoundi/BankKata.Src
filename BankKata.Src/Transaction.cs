using System;

namespace BankKata.Src
{
    public class Transaction
    {
        private readonly int _amount;
        private readonly string _dateCreated;

        public Transaction(int amount, string dateCreated)
        {
            this._amount = amount;
            _dateCreated = dateCreated;
        }

        public override string ToString()
        {
            return $"{this._dateCreated} {this.GetAmountAsStatementString()} ";
        }

        private string GetAmountAsStatementString()
        {
            if (this._amount < 0)
            {
                return $"|| || {Math.Abs(this._amount).ToString("0.00")}";
            }
            return $"|| {this._amount.ToString("0.00")} ||";
        }

        public int GetAmount()
        {
            return this._amount;
        }

        protected bool Equals(Transaction other)
        {
            return _amount == other._amount && string.Equals(_dateCreated, other._dateCreated);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Transaction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_amount*397) ^ (_dateCreated != null ? _dateCreated.GetHashCode() : 0);
            }
        }
    }
}