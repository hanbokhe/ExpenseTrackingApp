using ExpenseTrackingApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackingApp
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            this.Title = "TabbedPage";
            this.BarBackgroundColor = Color.Red;
            this.BarTextColor = Color.Black;

            InitializeComponent();
            Children.Add(new AccountPage());
            Children.Add(new BudgetPage());
            Children.Add(new TransactionPage());
            Children.Add(new TrendsPage());
        }
    }
}
