using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Model
{

    public class Budget
    {
        public decimal TotalBudget { get; set; }
        public decimal Balance { 
            get 
            {
                decimal balance = 0;
                foreach (var transaction in allTransactions)
                {
                    balance += transaction.Amount;
                    
                }
                return balance;
            } 
        }
        private List<Transaction> allTransactions = new List<Transaction>();
        public Budget()
        {
            //Ana, I need help~
        }

        public void Spend(decimal amount, DateTime date, MonthBudget month, string name)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Enter a positive number");
            }
            var spent = new Transaction(amount, date, month, name);
            allTransactions.Add(spent);
        }
        public void Save(decimal amount, DateTime date, MonthBudget month, string name)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Enter a positive number");
            }

            var save = new Transaction(-amount, date, month, name);
            allTransactions.Add(save);

            //alert for exceeding budget
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Budget Exceeded");
            }

        }
        //public string GetTransaction History()
        //{
        //    var report = new System.Text.StringBuilder();
        //    decimal balance = 0;
        //}
    }
}
