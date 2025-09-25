using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BankAccTest
{
    [TestClass]
    public sealed class BankAccountTest
    {
        // Debit Test
        [TestMethod]
        public void Debit_WithValidAmount_UpdateBalance()
        {
            decimal beginningBalance = 11.99m;
            decimal debitAmount = 4.55m;
            decimal expected = 7.44m;

            BankAccount account = new BankAccount("Bilal Burton", beginningBalance);
            account.Withdraw(debitAmount);

            decimal actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001m, "Account not debited correctly");
        }

        // 1. Constructor Test
        [TestMethod]
        public void Constructor_InitializesCorrectly()
        {
            decimal beginningBalance = 500.00m;
            BankAccount account = new BankAccount("Bilal Burton", beginningBalance);

            Assert.AreEqual("Bilal Burton", account.CustomerName);
            Assert.AreEqual(beginningBalance, account.Balance);
            Assert.IsTrue(account.AccountId > 0, "Account ID should be positive.");
            Assert.IsTrue(account.Transactions[0].Contains("Account created"));
        }

        // 2. Deposit Method Tests
        [TestMethod]
        public void Deposit_PositiveAmount_UpdatesBalance()
        {
            BankAccount account = new BankAccount("Bilal Burton", 100.00m);
            account.Deposit(50.00m);

            Assert.AreEqual(150.00m, account.Balance);
            Assert.IsTrue(account.Transactions.Last().Contains("Deposited"));
        }

        [TestMethod]
        public void Deposit_InvalidAmount_DoesNotChangeBalance()
        {
            BankAccount account = new BankAccount("Bilal Burton", 100.00m);
            account.Deposit(-25.00m);

            Assert.AreEqual(100.00m, account.Balance, "Balance should not change for invalid deposit.");
            Assert.IsTrue(account.Transactions.Last().Contains("Failed deposit"));
        }

        // 3. Withdraw Method Tests
        [TestMethod]
        public void Withdraw_ValidAmount_UpdatesBalance()
        {
            BankAccount account = new BankAccount("Bilal Burton", 200.00m);
            account.Withdraw(75.00m);

            Assert.AreEqual(125.00m, account.Balance);
            Assert.IsTrue(account.Transactions.Last().Contains("Withdrew"));
        }

        [TestMethod]
        public void Withdraw_InvalidAmount_DoesNotChangeBalance()
        {
            BankAccount account = new BankAccount("Bilal Burton", 100.00m);
            account.Withdraw(-10.00m);

            Assert.AreEqual(100.00m, account.Balance, "Balance should not change for invalid withdraw.");
            Assert.IsTrue(account.Transactions.Last().Contains("Failed withdrawal"));
        }

        [TestMethod]
        public void Withdraw_ExceedsBalance_DoesNotChangeBalance()
        {
            BankAccount account = new BankAccount("Bilal Burton", 100.00m);
            account.Withdraw(150.00m);

            Assert.AreEqual(100.00m, account.Balance, "Balance should not change if withdrawal exceeds balance.");
            Assert.IsTrue(account.Transactions.Last().Contains("Failed withdrawal"));
        }

        // 4. Transaction Recording Tests
        [TestMethod]
        public void Transactions_AreRecordedInOrder()
        {
            BankAccount account = new BankAccount("Bilal Burton", 500.00m);
            account.Deposit(200.00m);
            account.Withdraw(100.00m);

            Assert.AreEqual(3, account.Transactions.Count);
            Assert.IsTrue(account.Transactions[1].Contains("Deposited"));
            Assert.IsTrue(account.Transactions[2].Contains("Withdrew"));
        }

        // 5. Account Balance Tests
        [TestMethod]
        public void Balance_IsAccurateAfterSequence()
        {
            BankAccount account = new BankAccount("Bilal Burton", 300.00m);
            account.Deposit(200.00m);   
            account.Withdraw(150.00m); 
            account.Deposit(50.00m);   

            Assert.AreEqual(400.00m, account.Balance);
        }
    }
}
