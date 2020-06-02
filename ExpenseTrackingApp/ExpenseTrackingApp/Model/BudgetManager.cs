using ExpenseTrackingApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public static class BudgetManager
    {
        private static Dictionary<MonthBudget, Budget> budgetDictionary;
        private static MonthBudget currentMonth;

        static BudgetManager()
        {
            budgetDictionary = new Dictionary<MonthBudget, Budget>();
        }

        public static bool BudgetExists(MonthBudget monthBudget)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                return true;
            }

            return false;
        }

        public static void CreateBudget(MonthBudget monthBudget, double budgetLimit)
        {
            // Create Budget if it does not exit
            if (!BudgetManager.BudgetExists(monthBudget))
            {
                var budget = new Budget(budgetLimit);
                budgetDictionary.Add(monthBudget, budget);
            }
        }

        public static List<Transaction> GetTransactions(TransactionType transactionType, MonthBudget monthBudget)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                var budget = budgetDictionary[monthBudget];
                return budget.GetTransactions(transactionType);
            }

            return null;            
        }

        public static double GetAmountSpent(TransactionType transactionType, MonthBudget monthBudget)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                var budget = budgetDictionary[monthBudget];
                return budget.GetAmountSpent(transactionType);
            }

            return -1;
        }

        public static void AddTransaction(
            TransactionType transactionType,
            double transactionAmount,
            string description,
            DateTime dateTime)
        {

        }

        public static List<TransactionType> GetAllTransactionTypes(MonthBudget monthBudget)
        {
            return Budget.GetAllTransactionTypes();
        }

        public static double GetBudgetSpent(MonthBudget monthBudget)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                var budget = budgetDictionary[monthBudget];
                return budget.BudgetSpent;
            }

            return -1;
        }

        public static double GetTotalBudget(MonthBudget monthBudget)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                var budget = budgetDictionary[monthBudget];
                return budget.BudgetLimit;
            }

            return -1;
        }

        public static void UpdateBudgetLimit(MonthBudget monthBudget, double budgetLimit)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                var budget = budgetDictionary[monthBudget];
                budget.UpdateBudgetLimit(budgetLimit);
            }
        }

        public static double GetBudgetRemaining(MonthBudget monthBudget)
        {
            if (budgetDictionary.ContainsKey(monthBudget))
            {
                var budget = budgetDictionary[monthBudget];
                return budget.BudgetRemaining;
            }

            return -1;
        }
    }
}
