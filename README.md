# BankSystem Project

## Overview
This project is a simple C# console application that simulates a basic bank system.  

---

## Project Structure

### BankClassLibrary
This folder contains the main logic of the program.

- **IAccount.cs**  
  Defines the interface for all account types.  
  Every account can `Credit()`, `Debit()`, `TryDebit()` and has `AccountName`, `AccountNumber`, and `Balance`.

- **Account.cs**  
  The base class that implements `IAccount`.  
  It contains common properties and methods shared by all accounts,  
  such as deposit, withdrawal, and validation of account data.

- **CurrentAccount.cs**  
  Inherits from `Account`.  
  Adds an `OverdraftLimit` and overrides the `Debit()` method to allow negative balances (within the limit).

- **SavingsAccount.cs**  
  Inherits from `Account`.  
  Adds an `AnnualInterestRate` and a method `ApplyMonthlyInterest()` to add monthly interest to the balance.

---

### BankUI
- **Program.cs**  
  This is the main program.  
  It creates several accounts (normal, current, savings), stores them in a `List<IAccount>`,  
  and performs operations like deposits, withdrawals, and displaying account information.  
  It also shows **polymorphism**, since different account types behave differently when using the same methods.

---

### BankUnitTests
- **AccountTests.cs**  
  Contains unit tests for different account behaviors, such as:
  - Depositing and withdrawing money
  - Handling insufficient funds and overdrafts
  - Applying monthly interest for savings
  - Checking interface-based operations work correctly

---

## How to Run
```bash
dotnet build
dotnet run --project BankUI
dotnet test
