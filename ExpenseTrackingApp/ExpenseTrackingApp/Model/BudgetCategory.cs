using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Model
{
   public class BudgetCategory
    {
        public String Name { get; set; }
        public Double Spent { get; set; }
        public Double Balance { get; set; }
        public Double Budget { get; set; }
        public Double Percentage { get; set; }
        private List<Transaction> transactions { get; set; }
    }
}
