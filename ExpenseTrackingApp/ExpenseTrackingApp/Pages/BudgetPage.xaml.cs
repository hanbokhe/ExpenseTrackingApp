using ExpenseTrackingApp.Model;
using ExpenseTrackingApp.ViewModel;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry=Microcharts.Entry;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public ObservableCollection<BudgetItem> BudgetItems { get; set; } = new ObservableCollection<BudgetItem>();

        private List<Entry> entries = new List<Entry>();

        public BudgetPage()
        {
            InitializeComponent();
            this.InitializeBudgetItems();
            this.InitializeBudgetChart();
        }

        private void InitializeBudgetItems()
        {
            BudgetItemsView.ItemsSource = BudgetItems;

            var transactionTypes = BudgetManager.GetAllTransactionTypes();
            foreach (var transactionType in transactionTypes)
            {
                var amountSpent = BudgetManager.GetAmountSpent(transactionType);
                this.BudgetItems.Add(new BudgetItem() { TransactionType = transactionType.ToString(), AmountSpent = amountSpent });
            }
        }

        private void InitializeBudgetChart()
        {
            var totalBudget = (float) BudgetManager.GetTotalBudget();
            var budgetRemaining = (float)BudgetManager.GetBudgetRemaining();
            var budgetSpent = (float) BudgetManager.GetBudgetSpent();

            entries.Add(new Entry(budgetSpent) { Color = SKColor.Parse(Color.Blue.ToHex())});
            lblSpent.Text = $"Spent = ${budgetSpent}";
            lblSpent.TextColor = Color.Blue;

            entries.Add(new Entry(budgetRemaining) {  Color = SKColor.Parse(Color.Green.ToHex())});
            lblRemaining.Text = $"Remaining = ${budgetRemaining}";
            lblRemaining.TextColor = Color.Green;

            BudgetChart.Chart = new Microcharts.DonutChart { Entries = entries };
        }

        private async void BudgetItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ListTransactionPage());
        }

        private void OnEditButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}