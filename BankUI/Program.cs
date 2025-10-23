using BankClassLibrary;

var acc = new Account
{
    AccountName = "Alice",
    AccountNumber = "00123"
};

acc.Credit(1500m);
acc.Debit(300m);

Console.WriteLine(acc.ToString());


var cur = new CurrentAccount
{
    AccountName = "Bob",
    AccountNumber = "9876543210",
    OverdraftLimit = 300m
};
cur.Credit(100m);
cur.Debit(350m);

var sav = new SavingsAccount
{
    AccountName = "Cathy",
    AccountNumber = "555",
    AnnualInterestRate = 0.035m
};
sav.Credit(1000m);
sav.ApplyMonthlyInterest(); 


var accounts = new List<Account> { acc, cur, sav };

Console.WriteLine("\n--- Statements ---");
foreach (var a in accounts)
{
    Console.WriteLine(a); 
}


try
{
    cur.Debit(200m);
}
catch (Exception ex)
{
    Console.WriteLine($"\nExpected error: {ex.Message}");
}