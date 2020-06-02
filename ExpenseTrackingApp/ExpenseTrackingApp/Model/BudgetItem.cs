using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public class BudgetItem
    {
        public string TransactionType { get; set; }

        public double AmountSpent { get; set; }

        public double AmountBudget { get; set; }

        public string Month { get; set; }
    }
}
