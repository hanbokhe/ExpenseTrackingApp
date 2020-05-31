using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public enum TransactionType
    {
        Car,
        Entertainment,
        Food,
        Misc,
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
    public class Transaction
    {
        public string Name { get; set; }
        public MonthBudget Month { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string FileName { get; set; }
        public Transaction(double amount, DateTime date, MonthBudget month, TransactionType type, string name)
        {
            this.Amount = amount;
            this.Date = DateTime.Now;
            this.Name = name;
            this.Month = month;
            this.Type = type;
            FileName = null;
        }

        public Transaction()
        {
        }
    }

}
