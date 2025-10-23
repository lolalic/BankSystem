using System.Globalization;
using System.Text.RegularExpressions;

namespace BankClassLibrary;

public class Account : IAccount
{
    public const int AccountNumberLength = 10;
    public static string BankName = "Nationwide Demo Bank";

    private string accountName = "Anon";
    private string accountNumber = "0000000000";
    public decimal Balance { get; protected set; } = 0m;

    public string AccountName
    {
        get { return accountName;}
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Account name cannot be empty.", nameof(value));

            accountName = value.Length < 2 ? value.PadRight(2, '*') : value;
        }
    }

    public string AccountNumber
    {
        get => accountNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Account number cannot be empty.", nameof(value));

            // 仅数字
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
                throw new ArgumentException("Account number must contain digits only.", nameof(value));

            // 规范固定位长
            if (value.Length < AccountNumberLength)
                value = value.PadLeft(AccountNumberLength, '0');
            else if (value.Length > AccountNumberLength)
                value = value[..AccountNumberLength];

            accountNumber = value;
        }
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.", nameof(amount));

        Balance += amount;
    }

    public virtual void Debit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Debit amount must be positive.", nameof(amount));

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount;
    }

    public bool TryDebit(decimal amount)
    {
        if (amount <= 0 || amount > Balance) return false;
        Balance -= amount;
        return true;
    }

    public override string ToString()
    {
        var money = Balance.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-GB"));
        return $"{BankName} | Account Name: {AccountName} | Account Number: {AccountNumber} | Balance: {money}";
    }
}
