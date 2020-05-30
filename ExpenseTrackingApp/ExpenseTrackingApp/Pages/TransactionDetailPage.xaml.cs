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
    public partial class TransactionDetailPage : ContentPage
    {
        public TransactionDetailPage()
        {
            InitializeComponent();
         }

        private async void  OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var transaction = (Transaction)BindingContext;
            if (File.Exists(transaction.FileName))
            {
                File.Delete(transaction.FileName);
            }
            await Navigation.PopModalAsync();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}