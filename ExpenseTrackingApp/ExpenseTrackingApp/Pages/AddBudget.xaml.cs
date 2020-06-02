using ExpenseTrackingApp.Model;
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
        public AddBudget()
        {
            InitializeComponent();

        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            var budgetDetails = "";
            if (string.IsNullOrWhiteSpace(budget.Filename))
            {
                //create and save
                var filename = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.budget.txt");
                budgetDetails += filename + "\n";
                var amountText = TotalBudget.Text;
                double amount;
                if (string.IsNullOrWhiteSpace(amountText) || !Double.TryParse(amountText, out amount))//if is not a number we show an alert
                {
                    await DisplayAlert("Alert", "Please enter a valid number", "OK");
                }
                else
                {
                    budgetDetails += BudgetType.SelectedItem += "\n";
                    budgetDetails += amount + "\n";
                    budgetDetails += BudgetMonth.SelectedItem + "\n";
                    File.WriteAllText(filename, budgetDetails);
                }
            }
            else
            {
                //Message 

            }

            await Navigation.PopModalAsync();
        }

        private  void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            //var budget = (Budget)BindingContext;
            //if (File.Exists(Budget.Type))
            //{
            //    File.Delete(Budget.Name);
            //}
            //await Navigation.PopModalAsync();
        }
    }
}