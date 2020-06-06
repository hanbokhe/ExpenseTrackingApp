using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBudget : ContentPage
    {
        List<Budget> BudgetList; //all budgets
        List<Budget> BudgetByMonth;
        public string MonthBudget;
        bool isEmpty;
        public AddBudget()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var myBudget = (Budget)BindingContext;
            //MonthPicker.SelectedIndex = DateTime.Now.Month - 1;
            MonthBudget = myBudget.Month;
            BudgetList = InitializeBudgetItems();
            BudgetByMonth = GetBudgetByMonth(MonthBudget);
            MonthLabel.Text = myBudget.Month;
            isEmpty = BudgetByMonth.Count == 0;
       }

        protected override void OnAppearing()
        {
            Reload();
        }

        private void Reload()
        {
            BudgetList = InitializeBudgetItems();
            BudgetByMonth = GetBudgetByMonth(MonthBudget);
            if (BudgetByMonth.Count > 0)
            {
                foreach(var b in BudgetByMonth)
                {
                    string type = b.Type;
                    if(type.Equals("Car"))
                        CarBudget.Text = b.BudgetLimit.ToString();
                    else if (type.Equals("Food"))
                        FoodBudget.Text = b.BudgetLimit.ToString();
                    else if (type.Equals("Misc"))
                        MiscBudget.Text = b.BudgetLimit.ToString();
                    else if (type.Equals("Shopping"))
                        ShoppingBudget.Text = b.BudgetLimit.ToString();
                    else if (type.Equals("Rent"))
                        RentBudget.Text = b.BudgetLimit.ToString();
                    else
                        EntertainmentBudget.Text = b.BudgetLimit.ToString();
                }
            }
        }

        private string GetMonth(int month)
        {
            string[] months = {"January", "February", "March","April","May", "June", "July", "August","September","October", "November", "December"};
            if(month<=12)
                return months[month-1];
            return null;
        }
        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var carAmount = Convert.ToDouble(CarBudget.Text);
            var entertainmentAmount = Convert.ToDouble(EntertainmentBudget.Text);
            var foodAmount = Convert.ToDouble(FoodBudget.Text);
            var miscAmount = Convert.ToDouble(MiscBudget.Text);
            var shoppingAmount = Convert.ToDouble(ShoppingBudget.Text);
            var rentAmount = Convert.ToDouble(RentBudget.Text);
            var TotalAmount = carAmount + entertainmentAmount + foodAmount + miscAmount + shoppingAmount + rentAmount;

            double amount;

            //if (MonthPicker.SelectedIndex == -1)
            //{
            //    await DisplayAlert("Alert", "Select a Month", "OK");
            //}
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
                if (!isEmpty)//delete old files
                {
                    foreach (var b in BudgetByMonth)
                    {
                        if (b.Type.Equals("Car"))
                            writeFile(b.Filename, "Car", carAmount, b.Month);
                        else if (b.Type.Equals("Food") )
                            writeFile(b.Filename, "Food", foodAmount, b.Month);
                        else if (b.Type.Equals("Misc"))
                            writeFile(b.Filename, "Misc", miscAmount, b.Month);
                        else if (b.Type.Equals("Shopping") )
                            writeFile(b.Filename, "Shopping", shoppingAmount, b.Month);
                        else if (b.Type.Equals("Rent") )
                            writeFile(b.Filename, "Rent", rentAmount, b.Month);
                        else 
                            writeFile(b.Filename, "Entertainment", entertainmentAmount, b.Month);
                    }                
                }
                else
                {
                    // Car Details
                    writeFile(null,"Car", carAmount, MonthBudget);
                    writeFile(null,"Entertainment", entertainmentAmount, MonthBudget);
                    writeFile(null,"Food", foodAmount, MonthBudget);
                    writeFile(null,"Misc", miscAmount, MonthBudget);
                    writeFile(null,"Shopping", shoppingAmount, MonthBudget);
                    writeFile(null,"Rent", rentAmount, MonthBudget);
                }
               
            }
            await Navigation.PopModalAsync();

        }

        private void writeFile(string fname,string type, double amount, string month)
        { 
            var filename=fname;
            if (string.IsNullOrEmpty(fname))
            {
                filename = Path.Combine(
                         Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         $"{Path.GetRandomFileName()}.budget.txt");
            }
           
            string budgetDetails = "";
            budgetDetails += filename + "\n";
            budgetDetails += type += "\n";
            budgetDetails += amount + "\n";
            budgetDetails += month + "\n";
            File.WriteAllText(filename, budgetDetails);
        }
        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BudgetList.Count > 0)//delete old files
            {
                foreach (var b in BudgetByMonth)
                {
                    File.Delete(b.Filename);

                }
            }
            BudgetList=InitializeBudgetItems();
            await Navigation.PopModalAsync();
        }

        private List<Budget> InitializeBudgetItems()
        {
            List<Budget> budgets = new List<Budget>();
            //Showing the budget for the month selected on monthpicker. By default is the current month
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "*.budget.txt");
            string allText = "";
            string budgetFilename, month;
            string budgetType;
            double budgetAmount, budgetLimit, budgetSpent, budgetRemaining;
            string[] separator = new string[] { "\n" };
            string[] lines;
            foreach (var filename in files)
            {
                allText = File.ReadAllText(filename);
                lines = allText.Split(separator, StringSplitOptions.None);
                budgetFilename = lines[0];
                budgetType = lines[1].Trim();
                budgetAmount = double.Parse(lines[2].Trim());
                month = lines[3];
                budgetLimit = double.Parse(lines[2].Trim());
                budgetSpent = double.Parse(lines[2].Trim());
                budgetRemaining = double.Parse(lines[2].Trim());
                budgets.Add(new Budget(budgetAmount)
                {
                   Filename = budgetFilename,
                   Type = budgetType,
                   BudgetLimit = budgetLimit,
                   TotalBudget = budgetLimit,
                   Month = month
                });
                
            }
            return budgets;
        }
        private List<Budget> GetBudgetByMonth(string month)
        {
            var filteredList = BudgetList.Where(b => b.Month.Equals(MonthBudget)).ToList();
            return filteredList;
        }

        private void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CarBudget.Text= MonthPicker.SelectedItem.ToString();
            BudgetByMonth.Clear();
            BudgetByMonth = GetBudgetByMonth(MonthBudget);
            Reload();
        }
    }
}




