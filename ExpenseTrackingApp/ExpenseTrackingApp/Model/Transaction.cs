using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public enum TransactionType
    {
        Car,
        Entertainment,
        Food,
        Gas,
        Shopping,
        Rent,
    }
    public enum MonthBudget
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }
    class Transaction
    {
        public string Name { get; set; }
        public MonthBudget Month { get; }
        public TransactionType Type { get; }
        public DateTime Date { get; }
        public double Amount { get; }
        public Transaction(double amount, DateTime date, MonthBudget month, TransactionType type, string name)
        {
            this.Amount = amount;
            this.Date = date;
            this.Name = name;
            this.Month = month;
            this.Type = type;
        }

    }

}
