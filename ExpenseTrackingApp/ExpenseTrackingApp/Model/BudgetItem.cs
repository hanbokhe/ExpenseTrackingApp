using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public enum BudgetItemCategory
    {
        Car,
        Entertainment,
        Food,
        Gas,
        Rent,
        Shopping,
        Misc,
        Total,
    }

    public class BudgetItem
    {
        public BudgetItemCategory BudgetItemCategory { get; set; }

        public double TotalAmount { get; set; }

        public string Filename { get; set; }

        public string Month { get; set; }
      
    }
}
