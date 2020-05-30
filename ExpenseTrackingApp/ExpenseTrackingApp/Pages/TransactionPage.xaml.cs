using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private async void AddTransactionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTransaction { BindingContext = new Transaction()});
        }
    
    }
}