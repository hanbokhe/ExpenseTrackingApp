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
using System.Collections.ObjectModel;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPage : ContentPage
    {
        private List<Transaction> TransactionsList;
        private List<Budget> BudgetList;
        private string MonthTransactions;
        private System.Collections.Hashtable TotalAmountSpendByCategory;
        private ObservableCollection<GroupedTransactionModel> GroupedTransactions { get; set; }
        private double TotalSpend, TotalBudget;

        public TransactionPage()
        {
            InitializeComponent();
            TransactionsList = new List<Transaction>();
            BudgetList = new List<Budget>();
            MonthTransactions = DateTime.Now.Month.ToString("MMM");
            TotalAmountSpendByCategory = new Hashtable();
            GroupedTransactions = new ObservableCollection<GroupedTransactionModel>();
            TotalSpend = 0; TotalBudget = 0;
            
        }
      
        protected override void OnAppearing()
        {
            TransactionsList.Clear();
            GroupedTransactions.Clear();
            InitializeBudgetItems();
            InitializeTransactionsItems();
            MonthPicker.SelectedIndex = DateTime.Now.Month - 1;//current month selected by default
            MonthTransactions = MonthPicker.SelectedItem.ToString();
            ReloadTransactionsByMonth();
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
                        TotalBudget = budgetLimit,
                        Month = month
                    });
                }
            }

            protected void InitializeTransactionsItems()
            {
                var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "*.transaction.txt");
                string allText;
                string transactionFileName = "", transactionName = "";
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
                    transactionFileName = lines[0];
                    transactionAmount = double.Parse(lines[1]);
                    transactionName = lines[2];
                    transactionMonth = lines[3];
                    transactionType = getTransactionType(lines[4]);
                    transactionDateTime = DateTime.Today; // we have to convert this property from file
                    TransactionsList.Add(new Transaction
                    {
                        Amount = transactionAmount,
                        Date = transactionDateTime,
                        Name = transactionName,
                        Month = transactionMonth,
                        Type = transactionType,
                        FileName = transactionFileName

                    });
                }
            }

        TransactionType getTransactionType(string value)
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
        private List<Budget> GetBudgetByMonth(string month)
        {
            List<Budget> filteredList = new List<Budget>();
            filteredList = BudgetList.Where(b => b.Month.Equals(month)).ToList();
            return filteredList;
        }

        private List<Transaction> GetTransactionByMonth(string month)
        {
            List<Transaction> filteredList = new List<Transaction>();
            filteredList = TransactionsList.Where(t => t.Month.Equals(month)).ToList();
            return filteredList;
        }


        private void ReloadTransactionsByMonth()
        {
            var filteredBudgetListByMonth = GetBudgetByMonth(MonthTransactions);
            var filteredTransactionsByMonth = GetTransactionByMonth(MonthTransactions);
            //obtaining the total spend by month and category
            TotalAmountSpendByCategory.Clear();
            //insert categories
            TotalAmountSpendByCategory.Add("Car", 0.00);
            TotalAmountSpendByCategory.Add("Entertainment", 0.00);
            TotalAmountSpendByCategory.Add("Food", 0.00);
            TotalAmountSpendByCategory.Add("Misc", 0.00);
            TotalAmountSpendByCategory.Add("Shopping", 0.00);
            TotalAmountSpendByCategory.Add("Rent", 0.00);
            TotalSpend = 0; TotalBudget = 0;
            var carGroup = new GroupedTransactionModel() { LongName = "Car     "+ 
                String.Format("{0:C2}", TotalAmountSpendByCategory["Car"]), ShortName = "C" };
            var entertainmentGroup = new GroupedTransactionModel() { LongName = "Entertainment        "+
                String.Format("{0:C2}", TotalAmountSpendByCategory["Entertainment"]),ShortName = "E" };
            var foodGroup = new GroupedTransactionModel() { LongName = "Food        "+
                String.Format("{0:C2}", TotalAmountSpendByCategory["Food"]), ShortName = "F" };
            var miscGroup = new GroupedTransactionModel() { LongName = "Misc      "+
                String.Format("{0:C2}", TotalAmountSpendByCategory["Misc"]), ShortName = "M" };
            var shoppingGroup = new GroupedTransactionModel() { LongName = "Shopping      "+
                String.Format("{0:C2}", TotalAmountSpendByCategory["Shopping"]), ShortName = "S" };
            var rentGroup = new GroupedTransactionModel() { LongName = "Rent       "+
                String.Format("{0:C2}", TotalAmountSpendByCategory["Rent"]), ShortName = "R" };
            
            foreach(var c in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Car).ToList())
            {
                carGroup.Add(new TransactionModel() { Name = c.Name, Amount = c.Amount.ToString(), DateTransaction = c.Date.ToString() });
            }
            foreach (var e in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Entertainment).ToList())
            {
                entertainmentGroup.Add(new TransactionModel() { Name = e.Name, Amount = e.Amount.ToString(), DateTransaction = e.Date.ToString() });
            }
            foreach (var f in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Food).ToList())
            {
                foodGroup.Add(new TransactionModel() { Name = f.Name, Amount = f.Amount.ToString(), DateTransaction = f.Date.ToString() });
            }
            foreach (var m in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Misc).ToList())
            {
                miscGroup.Add(new TransactionModel() { Name = m.Name, Amount = m.Amount.ToString(), DateTransaction = m.Date.ToString() });
            }
            foreach (var s in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Shopping).ToList())
            {
                shoppingGroup.Add(new TransactionModel() { Name = s.Name, Amount = s.Amount.ToString(), DateTransaction = s.Date.ToString() });
            }
            foreach (var r in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Rent).ToList())
            {
                rentGroup.Add(new TransactionModel() { Name = r.Name, Amount = r.Amount.ToString(), DateTransaction = r.Date.ToString() });
            }
            GroupedTransactions.Clear();
            GroupedTransactions.Add(carGroup);
            GroupedTransactions.Add(entertainmentGroup);
            GroupedTransactions.Add(foodGroup);
            GroupedTransactions.Add(miscGroup);
            GroupedTransactions.Add(shoppingGroup);
            GroupedTransactions.Add(rentGroup);

            lstView.ItemsSource = GroupedTransactions;
            foreach (var t in filteredTransactionsByMonth){
                TotalAmountSpendByCategory[t.Type.ToString()] = t.Amount+(double)TotalAmountSpendByCategory[t.Type.ToString()];
                TotalSpend += t.Amount;}
            foreach(var b in filteredBudgetListByMonth) { TotalBudget += b.TotalBudget; }

            TotalsLabel.Text = String.Format("{0:C2}", TotalSpend) + "       |        " + String.Format("{0:C2}", TotalBudget - TotalSpend);
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

        private void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonthTransactions = MonthPicker.SelectedItem.ToString();
            ReloadTransactionsByMonth();
        }

    }
}