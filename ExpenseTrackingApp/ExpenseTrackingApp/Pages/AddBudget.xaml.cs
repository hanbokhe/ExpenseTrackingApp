using ExpenseTrackingApp.Model;
using ExpenseTrackingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBudget : ContentPage
    {
        private MonthBudget monthBudget;
        private ObservableCollection<BudgetItem> BudgetItems;

        public AddBudget(MonthBudget monthBudget, ObservableCollection<BudgetItem> budgetItems)
        {
            InitializeComponent();

            this.monthBudget = monthBudget;
            this.BudgetItems = budgetItems;

            BudgetItemsView.ItemsSource = budgetItems;
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {

            await Navigation.PopModalAsync();
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            //if (File.Exists(Budget.Type))
            //{
            //    File.Delete(Budget.Name);
            //}
            await Navigation.PopModalAsync();
        }
    }
}