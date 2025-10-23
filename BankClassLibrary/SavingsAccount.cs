using System;

namespace BankClassLibrary
{
    public class SavingsAccount : Account
    {
        
        public decimal AnnualInterestRate { get; set; } = 0.035m;

        
        public void ApplyMonthlyInterest()
        {
            if (AnnualInterestRate <= 0 || Balance <= 0) return;
            var interest = Math.Round(Balance * (AnnualInterestRate / 12m), 2, MidpointRounding.ToEven);

            Credit(interest);
        }

        public override string ToString()
            => base.ToString() + $" | Type: Savings | Rate: {AnnualInterestRate:P2}";
    }
}
