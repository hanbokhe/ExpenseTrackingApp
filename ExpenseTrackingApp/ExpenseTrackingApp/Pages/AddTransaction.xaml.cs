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
    public partial class AddTransaction : ContentPage
    {
        public AddTransaction()
        {
            InitializeComponent();
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var transaction = (Transaction)BindingContext;
            var transactionDetails= "";
            if (string.IsNullOrWhiteSpace(transaction.Name))
            {
                //create and save
                var filename = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.transaction.txt");
                transactionDetails += filename+"\n";
                var amountText = TransactionAmount.Text;
                double amount;
                if (string.IsNullOrWhiteSpace(amountText) || !Double.TryParse(amountText, out amount))//if is not a number we show an alert
                {
                    await DisplayAlert("Alert", "Please enter a valid number", "OK");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(TransactionDescription.Text))
                    {
                        await DisplayAlert("Alert", "Write a description", "OK");
                    }
                    else
                    {
                        transactionDetails += amountText + "\n";
                        transactionDetails += TransactionDescription.Text + "\n";
                        transactionDetails += TransactionMonth.SelectedItem.ToString() + "\n";
                        transactionDetails += TransactionType.SelectedItem.ToString() + "\n";
                        transactionDetails += DateTime.Today.ToString() + "\n";
                        File.WriteAllText(filename, transactionDetails);
                    }
                }
            }
            else
            {
                //Message 
               
            }
            await Navigation.PopModalAsync();
        }
        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PopModalAsync();
        }
    }
}