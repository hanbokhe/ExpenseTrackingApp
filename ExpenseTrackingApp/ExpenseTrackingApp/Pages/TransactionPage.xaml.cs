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
            var carGroup = new GroupedTransactionModel() { CategoryName = "CAR", CategoryInitial = "C" };
            var entertainmentGroup = new GroupedTransactionModel() { CategoryName = "ENTERTAINMENT",CategoryInitial = "E" };
            var foodGroup = new GroupedTransactionModel() { CategoryName = "FOOD", CategoryInitial = "F" };
            var miscGroup = new GroupedTransactionModel() { CategoryName = "MISC", CategoryInitial = "M" };
            var shoppingGroup = new GroupedTransactionModel() { CategoryName = "SHOPPING", CategoryInitial = "S" };
            var rentGroup = new GroupedTransactionModel() { CategoryName = "RENT", CategoryInitial = "R" }; 
            foreach(var c in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Car).ToList())
            {
                carGroup.Add(new TransactionModel() { Name = c.Date.ToShortDateString() + "   " + c.Name, Amount = String.Format("{0:C2}",c.Amount), FileName=c.FileName});
                TotalAmountSpendByCategory["Car"] = (double)TotalAmountSpendByCategory["Car"] + c.Amount;
            }
            foreach (var e in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Entertainment).ToList())
            {
                entertainmentGroup.Add(new TransactionModel() { Name = e.Date.ToShortDateString() + "   " + e.Name, Amount = String.Format("{0:C2}", e.Amount), FileName = e.FileName });
                TotalAmountSpendByCategory["Entertainment"] = (double)TotalAmountSpendByCategory["Entertainment"] + e.Amount;
            }
            foreach (var f in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Food).ToList())
            {
                foodGroup.Add(new TransactionModel() { Name = f.Date.ToShortDateString() + "   " + f.Name, Amount = String.Format("{0:C2}", f.Amount), FileName = f.FileName });
                TotalAmountSpendByCategory["Food"] = (double)TotalAmountSpendByCategory["Food"] +f.Amount;
            }
            foreach (var m in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Misc).ToList())
            {
                miscGroup.Add(new TransactionModel() { Name = m.Date.ToShortDateString() + "   " + m.Name, Amount =  String.Format("{0:C2}", m.Amount), FileName = m.FileName });
                TotalAmountSpendByCategory["Misc"] = (double)TotalAmountSpendByCategory["Misc"] + m.Amount;
            }
            foreach (var s in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Shopping).ToList())
            {
                shoppingGroup.Add(new TransactionModel() { Name = s.Date.ToShortDateString() + "   " + s.Name, Amount = String.Format("{0:C2}", s.Amount), FileName = s.FileName });
                TotalAmountSpendByCategory["Shopping"] = (double)TotalAmountSpendByCategory["Shopping"] + s.Amount;
            }
            foreach (var r in filteredTransactionsByMonth.Where(t => t.Type == TransactionType.Rent).ToList())
            {
                rentGroup.Add(new TransactionModel() { Name = r.Date.ToShortDateString() + "   " + r.Name, Amount = String.Format("{0:C2}", r.Amount), FileName = r.FileName });
                TotalAmountSpendByCategory["Rent"] = (double)TotalAmountSpendByCategory["Rent"] + r.Amount;
            }
            GroupedTransactions.Clear();
            carGroup.AmountByCategory = "CAR                                                                                      " + String.Format("{0:C2}", TotalAmountSpendByCategory["Car"]);
            entertainmentGroup.AmountByCategory = "ENTERTAINMENT                                                              " + String.Format("{0:C2}", TotalAmountSpendByCategory["Entertainment"]);
            foodGroup.AmountByCategory = "FOOD                                                                                    " + String.Format("{0:C2}", TotalAmountSpendByCategory["Food"]);
            miscGroup.AmountByCategory = "MISC                                                                                     " + String.Format("{0:C2}", TotalAmountSpendByCategory["Misc"]);
            shoppingGroup.AmountByCategory = "SHOPPING                                                                           " + String.Format("{0:C2}", TotalAmountSpendByCategory["Shopping"]);
            rentGroup.AmountByCategory = "RENT                                                                                     " + String.Format("{0:C2}", TotalAmountSpendByCategory["Rent"]);

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

            TotalsLabel.Text = String.Format("{0:C2}", TotalSpend) + "     |     " + String.Format("{0:C2}", TotalBudget - TotalSpend);
        }
    

    private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                TransactionModel mt = (TransactionModel)e.SelectedItem;
                string allText = File.ReadAllText(mt.FileName);
                string[] separator = new string[] { "\n" };
                string[] lines = allText.Split(separator, StringSplitOptions.None);
                Transaction currentTransaction = new Transaction
                {
                    Amount = double.Parse(lines[1]),
                    Date = DateTime.Today,
                    Name = lines[2],
                    Month = lines[3],
                    Type = getTransactionType(lines[4]),
                    FileName = lines[0]
                };
                await Navigation.PushModalAsync(new TransactionDetailPage
                {
                    BindingContext = currentTransaction
                }) ;
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