using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankClassLibrary;
namespace BankUnitTests;

[TestClass]
public class AccountTests
{
    private Account account = null!;

    [TestInitialize]
    public void Setup()
    {
        account = new Account();
    }

    [TestMethod]
    public void Credit_AddsToBalance()
    {
        account.Credit(100m);
        Assert.AreEqual(100m, account.Balance);
    }

    [TestMethod]
    public void Debit_RemovesFromBalance()
    {
        account.Credit(200m);
        account.Debit(50m);
        Assert.AreEqual(150m, account.Balance);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Debit_Throws_WhenOverdrawn()
    {
        account.Debit(10m);
    }
}
