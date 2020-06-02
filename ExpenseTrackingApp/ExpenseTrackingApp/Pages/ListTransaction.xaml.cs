using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTransactionPage : ContentPage
    {
        public ObservableCollection<ExpenseTrackingApp.Model.Transaction> Transactions { get; set; } = new ObservableCollection<ExpenseTrackingApp.Model.Transaction>();

        private MonthBudget monthBudget;

        public ListTransactionPage(MonthBudget monthBudget, string transactionType)
        {
            InitializeComponent();
            this.monthBudget = monthBudget;

            this.InitializeTransactionItems(transactionType);
        }

        private void InitializeTransactionItems(string transactionTypeStr)
        {
            var transactionType = (TransactionType)Enum.Parse(typeof(TransactionType), transactionTypeStr);
            TransactionItemsView.ItemsSource = this.Transactions;
            this.Transactions.Clear();
            var transactions = BudgetManager.GetTransactions(transactionType, monthBudget);
            if (transactions == null || transactions.Count == 0)
            {
                lblMessage.Text = $"No Transactions of type {transactionTypeStr} for month {monthBudget}";
            }
            else
            {
                lblMessage.Text = $"Transactions of type {transactionTypeStr} for month {monthBudget}";
                transactions.ForEach(transaction => Transactions.Add(transaction));
            }
        }
    }
}