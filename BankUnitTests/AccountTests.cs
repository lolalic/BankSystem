using BankClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    public void Credit_Throws_On_NonPositive(decimal amount)
    {
        Assert.ThrowsException<ArgumentException>(() => acc.Credit(amount));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-5)]
    public void Debit_Throws_On_NonPositive(decimal amount)
    {
        acc.Credit(10m);
        Assert.ThrowsException<ArgumentException>(() => acc.Debit(amount));
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
