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
            transactions.Add(new Transaction(144.09, DateTime.Today, MonthBudget.May, TransactionType.Entertainment, "Cable"));
            transactions.Add(new Transaction(27.69, DateTime.Today, MonthBudget.May, TransactionType.Food, "Dinner"));
            transactions.Add(new Transaction(56.09, DateTime.Today, MonthBudget.May, TransactionType.Food, "Grocery store"));
            transactions.Add(new Transaction(19.90, DateTime.Today, MonthBudget.May, TransactionType.Rent, "Rent"));
            transactions.Add(new Transaction(60.00, DateTime.Today, MonthBudget.May, TransactionType.Entertainment, "Movie night"));

        }
        //This method should read the file and create the lists
        protected override void OnAppearing()
        {


            var notes = new List<Transaction>();

            //var files = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //  "*.notes.txt");
            // var files = Directory.GetFiles("ms-appx:///Assets/transactions.txt");

            //await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///textfile.txt"));
            //foreach (var filename in files)
            //{

            //RentImage.Source = ImageSource.FromFile("ms-appx:///Images/rent.png");
     
            RentTransactionsListView.ItemsSource = transactions.Where(t => t.Type == TransactionType.Rent).ToList();

            EntertainmentTransactionListView.ItemsSource = transactions.Where(t => t.Type == TransactionType.Entertainment).ToList();
        }
            private void AddTransactionButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem != null)
            //{
            //    await Navigation.PushModalAsync(new NoteEntryPage
            //    {
            //        BindingContext = (Note)e.SelectedItem
            //    });
            //}
        }
    }
}