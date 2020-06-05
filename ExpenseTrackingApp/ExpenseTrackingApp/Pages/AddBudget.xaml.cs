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
                //var filename = Path.Combine(
                //    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                //    $"{Path.GetRandomFileName()}.budget.txt");
                //budgetDetails += filename + "\n";

                var carAmount = Convert.ToDouble(CarBudget.Text);
                var entertainmentAmount = Convert.ToDouble(EntertainmentBudget.Text);
                var foodAmount = Convert.ToDouble(FoodBudget.Text);
                var miscAmount = Convert.ToDouble(MiscBudget.Text);
                var shoppingAmount = Convert.ToDouble(ShoppingBudget.Text);
                var rentAmount = Convert.ToDouble(RentBudget.Text);
                var TotalAmount = carAmount + entertainmentAmount + foodAmount + miscAmount + shoppingAmount + rentAmount;

                double amount;

                if (MonthPicker.SelectedIndex == -1)
                {
                    await DisplayAlert("Alert", "Select a Month", "OK");
                }

                if (TotalAmount <= 0 
                    || !Double.TryParse(CarBudget.Text, out amount) 
                    || !Double.TryParse(EntertainmentBudget.Text, out amount)
                    || !Double.TryParse(FoodBudget.Text, out amount)
                    || !Double.TryParse(MiscBudget.Text, out amount)
                    || !Double.TryParse(ShoppingBudget.Text, out amount)
                    || !Double.TryParse(RentBudget.Text, out amount)) //if is not a number we show an alert
                {
                    await DisplayAlert("Alert", "Please enter a valid number", "OK");
                }
                else
                {
                    // Car Details
                    writeFile("Car", carAmount, MonthPicker.SelectedItem.ToString());
                    budgetDetails += carAmount + "\n";
                    budgetDetails += MonthPicker.SelectedItem.ToString() + "\n";

                    // Entertainment Details
                    writeFile("Entertainment", entertainmentAmount, MonthPicker.SelectedItem.ToString());
                    budgetDetails += entertainmentAmount + "\n";
                    budgetDetails += MonthPicker.SelectedItem.ToString() + "\n";

                    // Food Details
                    writeFile("Food", foodAmount, MonthPicker.SelectedItem.ToString());
                    budgetDetails += foodAmount + "\n";
                    budgetDetails += MonthPicker.SelectedItem.ToString() + "\n";

                    // Misc Details
                    writeFile("Miscellaneous", miscAmount, MonthPicker.SelectedItem.ToString());
                    budgetDetails += miscAmount + "\n";
                    budgetDetails += MonthPicker.SelectedItem.ToString() + "\n";

                    // Shopping Details
                    writeFile("Shopping", shoppingAmount, MonthPicker.SelectedItem.ToString());
                    budgetDetails += shoppingAmount + "\n";
                    budgetDetails += MonthPicker.SelectedItem.ToString() + "\n";

                    // Rent Details
                    writeFile("Rent", rentAmount, MonthPicker.SelectedItem.ToString());
                    budgetDetails += rentAmount + "\n";
                    budgetDetails += MonthPicker.SelectedItem.ToString() + "\n";
                }
            }
            await Navigation.PopModalAsync();
        }

        private void writeFile(string type, double amount, string month)
        {
            //create and save
            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.budget.txt");
            string budgetDetails = "";
            budgetDetails += filename + "\n";

            budgetDetails += type += "\n";
            budgetDetails += amount + "\n";
            budgetDetails += month + "\n";
            File.WriteAllText(filename, budgetDetails);
        }


        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {

            var budget = (Budget)BindingContext;
            if (File.Exists(budget.Filename))
            {
                File.Delete(budget.Filename);
            }
            await Navigation.PopModalAsync();
        }
    }
}




