namespace BankClassLibrary
{
    public interface IAccount
    {
        string AccountName { get; set; }
        string AccountNumber { get; set; }

        decimal Balance { get; }

        void Credit(decimal amount);
        void Debit(decimal amount);
        bool TryDebit(decimal amount);
    }
}
