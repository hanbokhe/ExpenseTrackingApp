using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExpenseTrackingApp.Model
{
    public class TransactionModel
    {
		public string Name { get; set; }
		public string Amount { get; set; }
		public string FileName { get; set; }
		public TransactionModel()
			{
			}
		}

		public class GroupedTransactionModel : ObservableCollection<TransactionModel>
		{
			public string CategoryName { get; set; }
			public string CategoryInitial { get; set; }
		    public string AmountByCategory { get; set; }
		}
}
