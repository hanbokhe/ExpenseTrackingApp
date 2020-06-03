using ExpenseTrackingApp.Model;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExpenseTrackingApp.Model.Budget;
using Entry=Microcharts.Entry;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public ObservableCollection<BudgetItem> BudgetItems { get; set; } = new ObservableCollection<BudgetItem>();

        private List<Budget> BudgetList;
        private List<Transaction> TransactionsList;
        public double TotalBudget, TotalTransactions;
        public double BudgetRemaining;
        public double BudgetSpent;
        private string MonthBudget;

        public BudgetPage()
        {
            InitializeComponent();
            BudgetList = new List<Budget>();
            TransactionsList = new List<Transaction>();

            MonthPicker.SelectedIndex = DateTime.Now.Month - 1;//current month selected by default
            MonthBudget = DateTime.Now.Month.ToString("MMM");

            this.InitializeBudgetItems();
            this.InitializeBudgetChart();
        }

        private void InitializeBudgetItems()
        {
            //Showing the budget for the month selected on monthpicker. By default is the current month
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "*.budget.txt");
            string allText = "";
            string budgetFilename, month;
            string budgetType;
            double budgetAmount, budgetLimit, budgetSpent, budgetRemaining;
            BudgetList.Clear();
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
                BudgetList.Add(new Budget(budgetAmount)
                {
                    Filename = budgetFilename,
                    Type = budgetType,
                    BudgetLimit = budgetLimit,
                    TotalBudget=budgetLimit,
                    Month=month
                 });
            }          
      }

        protected void InitializeTransactionsItems()
        {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "*.transaction.txt");
            string allText;  
            double transactionAmount;
            DateTime transactionDateTime;
            string transactionMonth;
            TransactionType transactionType;
            string[] separator = new string[] { "\n" };
            string[] lines;
            foreach (var filename in files)
            {
                allText = File.ReadAllText(filename);
                lines = allText.Split(separator, StringSplitOptions.None);
                //transactionFileName = lines[0];
                transactionAmount = double.Parse(lines[1]);
                //transactionName = lines[2];
                transactionMonth = lines[3];
                transactionType = GetTransactionType(lines[4]);
                transactionDateTime = DateTime.Today; // we have to convert this property from file
                TransactionsList.Add(new Transaction
                {
                    Amount = transactionAmount,
                    Date = transactionDateTime,
                    Name = lines[2],
                    Month = transactionMonth,
                    Type = transactionType,
                    FileName = lines[0]
                }); ;
            }

        }
            TransactionType GetTransactionType(string value)
            {
                if (value.CompareTo("Car") == 0)
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

        protected override void OnAppearing()
        {
            BudgetItems.Clear();
            InitializeBudgetItems();
            InitializeTransactionsItems();
            BudgetItemsView.ItemsSource = BudgetItems;
            MonthBudget = MonthPicker.SelectedItem.ToString();
            ReloadCategoryBudgetList();
           
        }

        private List<Budget> GetBudgetByMonth(string month)
        {
            List<Budget> filteredList = new List<Budget>();
            filteredList=BudgetList.Where(b => b.Month.Equals(month)).ToList();
            return filteredList;
        }

        private List<Transaction> GetTransactionByMonth(string month)
        {
            List<Transaction> filteredList = new List<Transaction>();
            filteredList = TransactionsList.Where(t => t.Month.Equals(month)).ToList();
            return filteredList;
        }


        private void InitializeBudgetChart()
        {
            var monthBudget = (MonthBudget)Enum.Parse(typeof(MonthBudget), this.MonthPicker.SelectedItem.ToString());
            var totalBudget = (float)BudgetManager.GetTotalBudget(monthBudget);
            var budgetRemaining = (float)BudgetManager.GetBudgetRemaining(monthBudget);
            var budgetSpent = (float)BudgetManager.GetBudgetSpent(monthBudget);

            var entries = new List<Entry>();
            entries.Add(new Entry(budgetSpent) { Color = SKColor.Parse(Color.Blue.ToHex()) });
            entries.Add(new Entry(budgetRemaining) { Color = SKColor.Parse(Color.Green.ToHex()) });
            BudgetChart.Chart = new Microcharts.DonutChart { Entries = entries };

            lblRemaining.Text = $"Remaining = ${budgetRemaining}";
            lblSpent.Text = $"Spent = ${budgetSpent}";
        }                                                    

        //private async void BudgetItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    //await Navigation.PushModalAsync(new ListTransactionPage());
        //}

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new AddBudget { BindingContext = new Budget(100) });
        }

        private void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonthBudget=MonthPicker.SelectedItem.ToString();
            BudgetItems.Clear();
            ReloadCategoryBudgetList();
        }

        private void ReloadCategoryBudgetList()
        {
            var filteredBudgetListByMonth = GetBudgetByMonth(MonthBudget);
            var filteredTransactionsByMonth = GetTransactionByMonth(MonthBudget);
            double totalBudget=0, totalTransactions = 0;
            foreach (var b in filteredBudgetListByMonth)
            {
                totalBudget += b.BudgetLimit;
                this.BudgetItems.Add(new BudgetItem()
                {
                    TransactionType = b.Type,
                    AmountBudget = b.BudgetLimit,
                    AmountSpent=0,
                    Month = b.Month
                }); ;
            }
            this.BudgetItems.Add(new BudgetItem
            {
                TransactionType = "Total",
                AmountBudget = totalBudget,
                AmountSpent=0,
                Month = MonthBudget
            });
            TotalBudget = totalBudget;
            BudgetItemsView.ItemsSource = BudgetItems.Reverse(); //so total is going to appear in the beggining
            foreach(var t in filteredTransactionsByMonth)
            {
                totalTransactions += t.Amount;
            }
            TotalTransactions = totalTransactions;
            var spent = Convert.ToInt32(totalTransactions);
            var remaining = Convert.ToInt32(totalBudget - totalTransactions);
            lblSpent.Text = "  Spent "+ String.Format("{0:C2}", spent);
            lblRemaining.Text = "Remaining  "+ String.Format("{0:C2}", remaining);

            var entries = new List<Entry>();
            entries.Add(new Entry(spent) { Color = SKColor.Parse(Color.Blue.ToHex() )});
            entries.Add(new Entry(remaining) { Color = SKColor.Parse(Color.Green.ToHex()) });
            BudgetChart.Chart = new Microcharts.DonutChart { Entries = entries };
        }
    }
}