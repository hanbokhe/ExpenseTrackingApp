using ExpenseTrackingApp.Data;
using ExpenseTrackingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public static class BudgetManager
    {
        private static Budget budget = new Budget(BudgetData.BudgetAmount);
        static BudgetManager()
        {
        }

        public static List<Transaction> GetTransactions(TransactionType transactionType)
        {
            return budget.GetTransactions(transactionType);
        }

        public static double GetAmountSpent(TransactionType transactionType)
        {
            return budget.GetAmountSpent(transactionType);
        }

        public static void AddTransaction(
            TransactionType transactionType,
            double transactionAmount,
            string description,
            DateTime dateTime)
        {

        }

        public static List<TransactionType> GetAllTransactionTypes()
        {
            return budget.GetAllTransactionTypes();
        }

        public static double GetBudgetSpent()
        {
            return budget.BudgetSpent;
        }

        public static double GetTotalBudget()
        {
            return budget.TotalBudget;
        }

        public static double GetBudgetRemaining()
        {
            return budget.BudgetRemaining;
        }
    }
}
