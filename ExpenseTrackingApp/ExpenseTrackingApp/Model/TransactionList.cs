using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    class TransactionList
    {
        public string Category { get; set; }
        public double SpendAmount { get; set; }

        public List<Transaction> TransactionsByCategory { get; set; }
    }
}
