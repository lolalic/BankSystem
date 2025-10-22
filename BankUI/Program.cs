using BankClassLibrary;

var acc = new Account();
acc.AccountName = "Alice";
acc.AccountNumber = "00123";
acc.Credit(1500);
acc.Debit(300);

Console.WriteLine(acc.ToString());



