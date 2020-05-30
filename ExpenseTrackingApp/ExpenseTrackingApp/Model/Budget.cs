using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExpenseTrackingApp.Model
{

    public class Budget
    {
        public double TotalBudget { get; set; }
        public double Balance { 
            get 
            {
                double balance = 0;
                foreach (var transaction in allTransactions)
                {
                    balance += transaction.Amount;
                    
                }
                return balance;
            } 
        }
        public decimal BudgetLimit { get; set; }

        private List<Transaction> allTransactions = new List<Transaction>();
        public Budget(decimal budgetLimit)
        {
            BudgetLimit = budgetLimit;
            if (BudgetLimit >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(budgetLimit), "Enter a larger budget");
            }
        }

        public void Spend(double amount, DateTime date, MonthBudget month, TransactionType type, string name)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Enter a positive number");
            }
            var spent = new Transaction(amount, date, month, type, name);
            allTransactions.Add(spent);

        }
        public void Save(double amount, DateTime date, MonthBudget month, TransactionType type, string name)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Enter a positive number");
            }

            var save = new Transaction(-amount, date, month, type, name);
            allTransactions.Add(save);

            //alert for exceeding budget


        }
        //public string ShowAllTransaction()
        //{
        //    var report = new System.Text.StringBuilder();
        //    decimal balance = 0;
        //}
        //public string ShowTransactionByType()
        //{
        //    var report = new System.Text.StringBuilder();
        //    decimal balance = 0;
        //}
        //public string ShowTransactionByMonth()
        //{
        //    var report = new System.Text.StringBuilder();
        //    decimal balance = 0;
        //}
    }
}
