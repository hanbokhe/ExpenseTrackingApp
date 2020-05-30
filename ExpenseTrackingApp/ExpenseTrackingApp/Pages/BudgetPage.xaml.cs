using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public ObservableCollection<BudgetItem> BudgetItems { get; set; } = new ObservableCollection<BudgetItem>();

        public BudgetPage()
        {
            InitializeComponent();
            this.InitializeBudgetItems();
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

        private void BudgetItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}