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
        public TransactionPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var notes = new List<Transaction>();

            //var files = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //  "*.notes.txt");
           // var files = Directory.GetFiles("ms-appx:///Assets/transactions.txt");
                
                //await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///textfile.txt"));
            //foreach (var filename in files)
            //{
                
                notes.Add(new Transaction(144.09, DateTime.Today, MonthBudget.May, TransactionType.Entertainment,"Cable"));
            notes.Add(new Transaction(27.69, DateTime.Today, MonthBudget.May, TransactionType.Food, "Dinner"));
            notes.Add(new Transaction(56.09, DateTime.Today, MonthBudget.May, TransactionType.Food, "Grocery store"));
            notes.Add(new Transaction(19.90, DateTime.Today, MonthBudget.May, TransactionType.Rent, "Rent"));
            notes.Add(new Transaction(144.09, DateTime.Today, MonthBudget.May, TransactionType.Entertainment, "Movie night"));

            //  }

            transactionListView.ItemsSource = notes.OrderBy(n => n.Date).ToList();
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