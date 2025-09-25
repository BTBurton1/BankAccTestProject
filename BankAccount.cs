using System;
using System.Collections.Generic;

namespace BankAccTest
{
    public class BankAccount
    {
        private static int nextId = 1;
        private int accountId;
        private string customerName;
        private decimal balance;
        private List<string> transactions;

        public int AccountId => accountId;
        public string CustomerName => customerName;
        public decimal Balance => balance;
        public List<string> Transactions => transactions;

        public BankAccount(string customerName, decimal initialBalance)
        {
            accountId = nextId++;
            this.customerName = customerName;
            balance = initialBalance >= 0 ? initialBalance : 0;
            transactions = new List<string>();

            transactions.Add($"Account created for {customerName} with initial balance {balance:C}");
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
                transactions.Add($"Deposited {amount:C}. Balance: {balance:C}");
            }
            else
            {
                transactions.Add($"Failed deposit: {amount:C} (invalid)");
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                transactions.Add($"Withdrew {amount:C}. Balance: {balance:C}");
            }
            else
            {
                transactions.Add($"Failed withdrawal: {amount:C}. Balance: {balance:C}");
                Console.WriteLine("Insufficient funds or invalid withdrawal.");
            }
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account ID: {accountId}, Name: {customerName}, Balance: {balance:C}");
        }
    }
}
