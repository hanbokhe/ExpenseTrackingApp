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
            entries.Add(new Entry(200) { Label = "Total", Color = SKColor.Parse(Color.Red.ToHex()), ValueLabel= "200" });
            entries.Add(new Entry(100) { Label = "Total", Color = SKColor.Parse(Color.Green.ToHex()), ValueLabel = "100" }); ;
            entries.Add(new Entry(50) { Label = "Total", Color = SKColor.Parse(Color.Blue.ToHex()), ValueLabel= "50" });
            entries.Add(new Entry(500) { Label = "Total", Color = SKColor.Parse(Color.Orange.ToHex()), ValueLabel= "500" });
            BudgetChart.Chart = new Microcharts.DonutChart { Entries = entries };
        }

        private async void BudgetItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ListTransactionPage());
        }
    }
}