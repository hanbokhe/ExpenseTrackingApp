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
        private static List<BudgetItem> BudgetItems = new List<BudgetItem>();
        private static Budget budget = new Budget (BudgetData.BudgetAmount);
        static BudgetManager()
        {
            InitializeBudgetItems();
        }

        public static void GetBudgetItems(ObservableCollection<BudgetItem> budgetItems)
        {
            budgetItems.Clear();
            BudgetItems.ForEach(budgetItem => budgetItems.Add(budgetItem));
        }

        public static List<Transaction> GetTransactions(TransactionType transactionType)
        {
            return budget.GetTransactions(transactionType);
        }

        public static void AddTransaction(
            TransactionType transactionType,
            double transactionAmount,
            string description,
            DateTime dateTime)
        {

        }
        private static void InitializeBudgetItems()
        {
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Total, TotalAmount = 1000 });
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Car, TotalAmount = 100 });
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Entertainment, TotalAmount = 40 });
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Food, TotalAmount = 200 });
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Gas, TotalAmount = 80 });
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Rent, TotalAmount = 400 });
            BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Shopping, TotalAmount = 60 });
        }
    }
}
