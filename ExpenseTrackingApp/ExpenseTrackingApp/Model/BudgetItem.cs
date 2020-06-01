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
        Total,
    }

    public class BudgetItem
    {
        public BudgetItemCategory BudgetItemCategory { get; set; }

        public int TotalAmount { get; set; }
      
    }
}
