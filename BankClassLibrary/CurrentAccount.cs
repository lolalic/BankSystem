using System;

namespace BankClassLibrary
{
    public class CurrentAccount : Account
    {
        public decimal OverdraftLimit { get; set; } = 200m;

        public override void Debit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Debit amount must be positive.", nameof(amount));

            if (Balance - amount < -OverdraftLimit)
                throw new InvalidOperationException("Exceeds overdraft limit.");

            Balance -= amount;
        }

        public override string ToString()
            => base.ToString() + $" | Type: Current | OD Limit: {OverdraftLimit:C0}";
    }
}
