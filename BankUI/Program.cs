using BankClassLibrary;

var acc = new Account
{
    AccountName = "Alice",
    AccountNumber = "00123"
};

acc.Credit(1500m);
acc.Debit(300m);

Console.WriteLine(acc.ToString());
