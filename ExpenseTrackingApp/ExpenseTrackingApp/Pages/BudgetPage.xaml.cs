using ExpenseTrackingApp.Model;
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
        public double TotalBudget;
        public double BudgetRemaining;
        public double BudgetSpent;

        public BudgetPage()
        {
            InitializeComponent();
            this.InitializeBudgetItems();
            this.InitializeBudgetChart();
        }

        private void InitializeBudgetItems()
        {
            BudgetItemsView.ItemsSource = BudgetItems;
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Total, TotalAmount = 1000 });
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Car, TotalAmount = 100 });
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Entertainment, TotalAmount = 40 });
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Food, TotalAmount = 200 });
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Gas, TotalAmount = 80 });
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Rent, TotalAmount = 400 });
            this.BudgetItems.Add(new BudgetItem() { BudgetItemCategory = BudgetItemCategory.Shopping, TotalAmount = 60 });
        }

        private void InitializeBudgetChart()
        {
            var totalBudget = (float) BudgetManager.GetTotalBudget();
            var budgetRemaining = (float)BudgetManager.GetBudgetRemaining();
            var budgetSpent = (float) BudgetManager.GetBudgetSpent();


            entries.Add(new Entry(budgetSpent) { Color = SKColor.Parse(Color.Yellow.ToHex())});
            entries.Add(new Entry(budgetRemaining) {  Color = SKColor.Parse(Color.Green.ToHex())});
            BudgetChart.Chart = new Microcharts.DonutChart { Entries = entries };

            lblRemaining.Text = $"Remaining = ${budgetRemaining}";
            lblSpent.Text = $"Spent = ${budgetSpent}";
        }

        private async void BudgetItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ListTransactionPage());
        }


    }
}