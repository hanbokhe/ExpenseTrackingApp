using ExpenseTrackingApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackingApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBudget : ContentPage
    {
        public AddBudget()
        {
            InitializeComponent();


        }
        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
            private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            //if (File.Exists(Budget.Type))
            //{
            //    File.Delete(Budget.Name);
            //}
            await Navigation.PopModalAsync();
        }
    }
}