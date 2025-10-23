using BankClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankUnitTests;

[TestClass]
public class AccountTests
{
    private Account acc = null!;

    [TestInitialize]
    public void Setup() => acc = new Account { AccountName = "A*", AccountNumber = "123" };

    [DataTestMethod]
    [DataRow(0.01)]
    [DataRow(10)]
    [DataRow(9999.99)]
    public void Credit_IncreasesBalance(double amt)
    {
        var amount = (decimal)amt;
        var start = acc.Balance;
        acc.Credit(amount);
        Assert.AreEqual(start + amount, acc.Balance);
    }

    [TestMethod]
    public void Debit_RemovesFromBalance()
    {
        acc.Credit(200m);
        acc.Debit(50m);
        Assert.AreEqual(150m, acc.Balance);
    }

    [TestMethod]
    public void Debit_Throws_WhenOverdrawn()
    {
        acc.Credit(100m);
        Assert.ThrowsException<InvalidOperationException>(() => acc.Debit(101m));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Credit_Throws_On_NonPositive(int amount)
    {
        Assert.ThrowsException<ArgumentException>(() => acc.Credit((decimal)amount));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-5)]
    public void Debit_Throws_On_NonPositive(int amount)
    {
        acc.Credit(10m);
        Assert.ThrowsException<ArgumentException>(() => acc.Debit((decimal)amount));
    }

    [TestMethod]
    public void AccountNumber_Normalizes_To_10_Digits()
    {
        acc.AccountNumber = "123";
        Assert.AreEqual("0000000123", acc.AccountNumber);

        acc.AccountNumber = "1234567890123";
        Assert.AreEqual("1234567890", acc.AccountNumber);
    }


}

[TestClass]
public class DerivedAccountTests
{
    [TestMethod]
    public void CurrentAccount_Allows_Overdraft_Within_Limit()
    {
        var cur = new CurrentAccount { AccountName = "X", AccountNumber = "1", OverdraftLimit = 300m };
        cur.Credit(100m);
        cur.Debit(350m); // -> -250
        Assert.AreEqual(-250m, cur.Balance);
    }

    [TestMethod]
    public void CurrentAccount_Throws_When_Exceeding_Limit()
    {
        var cur = new CurrentAccount { AccountName = "X", AccountNumber = "1", OverdraftLimit = 200m };
        cur.Credit(50m);
        Assert.ThrowsException<InvalidOperationException>(() => cur.Debit(300m)); // 50-300 = -250 < -200
    }

    [TestMethod]
    public void SavingsAccount_Accrues_Monthly_Interest()
    {
        var sav = new SavingsAccount { AccountName = "Y", AccountNumber = "2", AnnualInterestRate = 0.12m }; 
        sav.Credit(1000m);
        sav.ApplyMonthlyInterest();
        Assert.AreEqual(1010m, sav.Balance); // 1000 * 1% = 10
    }
}
