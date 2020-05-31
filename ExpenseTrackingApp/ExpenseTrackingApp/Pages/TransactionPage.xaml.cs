using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTrackingApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPage : ContentPage
    {
        private List<Transaction> transactions;

        public TransactionPage()
        {
            InitializeComponent();
            transactions = new List<Transaction>();
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
            transactions.Clear();
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
                transactions.Add(new Transaction
                {
                    Amount = transactionAmount,
                    Date = transactionDateTime,
                    Name = transactionName,
                    Month = transactionMonth,
                    Type = transactionType,
                    FileName = transactionFileName

                });


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
            CarTransactionListView.ItemsSource= transactions.Where(t => t.Type == TransactionType.Car).ToList();
            EntertainmentTransactionListView.ItemsSource = transactions.Where(t => t.Type == TransactionType.Entertainment).ToList();
            FoodTransactionListView.ItemsSource= transactions.Where(t => t.Type == TransactionType.Food).ToList();
            MiscTransactionListView.ItemsSource= transactions.Where(t => t.Type == TransactionType.Misc).ToList();
            ShoppingTransactionListView.ItemsSource= transactions.Where(t => t.Type == TransactionType.Shopping).ToList();
            RentTransactionListView.ItemsSource = transactions.Where(t => t.Type == TransactionType.Rent).ToList();

            
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