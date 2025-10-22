namespace BankClassLibrary;

public class Account
{
    private string accountName = string.Empty;
    private string accountNumber = string.Empty;
    private decimal balance;

    public string AccountName
    {
        get { return accountName; }
        set { accountName = value.Length < 2 ? value + "*" : value; }
    }

    public string AccountNumber
    {
        get { return accountNumber; }
        set { accountNumber = value; }
    }

    public decimal Balance
    {
        get { return balance; }
        private set { balance = value < 0 ? 0 : value; }
    }

    public void Credit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
        }
    }

    public void Debit(decimal amount)
    {
        if (amount <= 0)
        throw new ArgumentException("Debit amount must be positive.");

        if (amount > Balance)
        throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount;
    }

    public override string ToString()
    {
        return $"Account Name: {AccountName} ｜ Account Number: {AccountNumber} ｜ Balance: £{Balance:F2}";
    }
}
