using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPage : ContentPage
    {
        private List<Transaction> Transactions;
        private System.Collections.Hashtable TotalAmountByCategory;
        private double totalSpend;

        public TransactionPage()
        {
            InitializeComponent();
            Transactions = new List<Transaction>();
            TotalAmountByCategory = new Hashtable();
        }
      
        protected override void OnAppearing()
        {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "*.transaction.txt");
            string allText;
            string transactionFileName = "", transactionName="";
            double transactionAmount;
            DateTime transactionDateTime;
            MonthBudget transactionMonth;
            TransactionType transactionType;
            string[] separator = new string[] { "\n"};
            string[] lines;
            totalSpend = 0;
            Transactions.Clear();
            TotalAmountByCategory.Clear();
            //insert categories
            TotalAmountByCategory.Add("Car", 0.00);
            TotalAmountByCategory.Add("Entertainment", 0.00);
            TotalAmountByCategory.Add("Food", 0.00);
            TotalAmountByCategory.Add("Misc", 0.00);
            TotalAmountByCategory.Add("Shopping", 0.00);
            TotalAmountByCategory.Add("Rent", 0.00);

            foreach (var filename in files)
            {
                allText = File.ReadAllText(filename);
                lines = allText.Split(separator, StringSplitOptions.None);
                transactionFileName = lines[0];
                transactionAmount = double.Parse(lines[1]);
                transactionName = lines[2];
                transactionMonth = getMonthValue(lines[3]);
                transactionType = getTransactionType(lines[4]);
                transactionDateTime = DateTime.Today; // we have to convert this property from file
                totalSpend += transactionAmount;
                Transactions.Add(new Transaction
                {
                    Amount = transactionAmount,
                    Date = transactionDateTime,
                    Name = transactionName,
                    Month = transactionMonth,
                    Type = transactionType,
                    FileName = transactionFileName

                });

                TotalAmountByCategory[lines[4].Trim()] = (double) TotalAmountByCategory[lines[4].Trim()] + transactionAmount;
            }

            MonthBudget getMonthValue ( string value )
            {
                if (value.CompareTo("January") == 0)
                    return MonthBudget.January;
                if (value.CompareTo("February") == 0)
                    return MonthBudget.February;
                if (value.CompareTo("March") == 0)
                    return MonthBudget.March;
                if (value.CompareTo("April") == 0)
                    return MonthBudget.April;
                if (value.CompareTo("May") == 0)
                    return MonthBudget.May;
                if (value.CompareTo("June") == 0)
                    return MonthBudget.June;
                if (value.CompareTo("July") == 0)
                    return MonthBudget.July;
                if (value.CompareTo("August") == 0)
                    return MonthBudget.August;
                if (value.CompareTo("September") == 0)
                    return MonthBudget.September;
                if (value.CompareTo("October") == 0)
                    return MonthBudget.October;
                if (value.CompareTo("November") == 0)
                    return MonthBudget.November;
                else
                    return MonthBudget.December;

            }

            TransactionType getTransactionType(string value)
            {
                if (value.CompareTo("Car")==0)
                    return TransactionType.Car;
                if (value.CompareTo("Entertainment") == 0)
                    return TransactionType.Entertainment;
                if (value.CompareTo("Food") == 0)
                    return TransactionType.Food;
                if (value.CompareTo("Misc") == 0)
                    return TransactionType.Misc;
                if (value.CompareTo("Shopping") == 0)
                    return TransactionType.Shopping;
                else
                    return TransactionType.Rent;

            }
            CarTransactionListView.ItemsSource= Transactions.Where(t => t.Type == TransactionType.Car).ToList();
            EntertainmentTransactionListView.ItemsSource = Transactions.Where(t => t.Type == TransactionType.Entertainment).ToList();
            FoodTransactionListView.ItemsSource= Transactions.Where(t => t.Type == TransactionType.Food).ToList();
            MiscTransactionListView.ItemsSource= Transactions.Where(t => t.Type == TransactionType.Misc).ToList();
            ShoppingTransactionListView.ItemsSource= Transactions.Where(t => t.Type == TransactionType.Shopping).ToList();
            RentTransactionListView.ItemsSource = Transactions.Where(t => t.Type == TransactionType.Rent).ToList();

            TotalsLabel.Text = "$" + totalSpend + "  |   $  XXXX";

            CarLabel.Text = "Car                      $" + TotalAmountByCategory["Car"];
            EntertainmentLabel.Text = "Entertainment             $" + TotalAmountByCategory["Entertainment"];
            FoodLabel.Text = "Food                     $" + TotalAmountByCategory["Food"];
            MiscLabel.Text = "Misc                     $" + TotalAmountByCategory["Misc"];
            ShoppingLabel.Text = "Shopping                 $" + TotalAmountByCategory["Shopping"];
            RentLabel.Text = "Rent                     $" + TotalAmountByCategory["Rent"];
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushModalAsync(new TransactionDetailPage
                {
                    BindingContext = (Transaction)e.SelectedItem
                });
            }
        }

        private async void AddTransactionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTransaction { BindingContext = new Transaction()});
        }
    
    }
}