using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(budget.Month)) 
            {
                //create and save
                var filename = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.budget.txt");
                budgetDetails += filename + "\n";
                var budgetAmount = BudgetLimitEditor.Text;
                double amount;
                if (string.IsNullOrWhiteSpace(budgetAmount) || !Double.TryParse(budgetAmount, out amount))//if is not a number we show an alert
                {
                    await DisplayAlert("Alert", "Please enter a valid number", "OK");
                }
                else
                {
                        budgetDetails += BudgetType.SelectedItem+ "\n";
                        budgetDetails += budgetAmount + "\n";
                        budgetDetails += BudgetMonth.SelectedItem += "\n";
                        budgetDetails += "100\n";
                        budgetDetails +=  "100\n";
                        budgetDetails += "10\n";
                        File.WriteAllText(filename, budgetDetails);
                    }
                await DisplayAlert("Alert", filename, "OK");
            }
            else
            {
                await DisplayAlert("Alert", budget.Month, "OK");
                //Message 

            }

            await Navigation.PopModalAsync();
        }
            private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            //var budget = (Budget)BindingContext;
            //if (File.Exists(Budget.Type))
            //{
            //    File.Delete(Budget.Name);
            //}
            await Navigation.PopModalAsync();
        }
    }
}