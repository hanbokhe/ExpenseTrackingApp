using ExpenseTrackingApp.Model;
using ExpenseTrackingApp.ViewModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

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

            var currentDate = DateTime.Now;
            // Get month name from current date
            var currentMonth = currentDate.ToString("MMMM", CultureInfo.InvariantCulture);

            // Get the current month index
            this.MonthPicker.SelectedIndex = currentDate.Month - 1;

            // Convert the current month to MonthBudget
            var currentMonthBudget = (MonthBudget)Enum.Parse(typeof(MonthBudget), currentMonth);

            this.InitializeBudgetChart(currentMonthBudget);
            this.InitializeBudgetItems(currentMonthBudget);
        }

        private void InitializeBudgetChart(MonthBudget monthBudget)
        {
            var totalBudget = (float)BudgetManager.GetTotalBudget(monthBudget);
            var budgetRemaining = (float)BudgetManager.GetBudgetRemaining(monthBudget);
            var budgetSpent = (float)BudgetManager.GetBudgetSpent(monthBudget);

            entries.Add(new Entry(budgetSpent) { Color = SKColor.Parse(Color.Blue.ToHex()) });
            lblSpent.Text = $"Spent = ${budgetSpent}";
            lblSpent.TextColor = Color.Blue;

            entries.Add(new Entry(budgetRemaining) { Color = SKColor.Parse(Color.Green.ToHex()) });
            lblRemaining.Text = $"Remaining = ${budgetRemaining}";
            lblRemaining.TextColor = Color.Green;

            BudgetChart.Chart = new Microcharts.DonutChart { Entries = entries };

            var budgetExists = BudgetManager.BudgetExists(monthBudget);
            if (budgetExists)
            {
                btnAdd.Text = "Edit Budget";
            }
            else
            {
                btnAdd.Text = "Set Budget";
            }
        }

        private void InitializeBudgetItems(MonthBudget monthBudget)
        {
            BudgetItemsView.ItemsSource = BudgetItems;

            var transactionTypes = BudgetManager.GetAllTransactionTypes(monthBudget);
            foreach (var transactionType in transactionTypes)
            {
                var amountSpent = BudgetManager.GetAmountSpent(transactionType, monthBudget);
                this.BudgetItems.Add(new BudgetItem() { TransactionType = transactionType.ToString(), AmountSpent = amountSpent });
            }
        }

        private async void BudgetItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ListTransactionPage(this.GetSelectedMonthBudget()));
        }

        private async void OnAddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddBudget(this.GetSelectedMonthBudget(), this.BudgetItems));
        }

        private MonthBudget GetSelectedMonthBudget()
        {
            return (MonthBudget)Enum.Parse(typeof(MonthBudget), this.MonthPicker.SelectedItem.ToString());
        }
    }
}