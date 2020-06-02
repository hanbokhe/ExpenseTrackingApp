using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Data
{
    public static class BudgetData
    {
        public static double BudgetAmount { get; private set; }

        static BudgetData()
        {
            BudgetAmount = 1234;
        }
    }
}
