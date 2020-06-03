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
		public string DateTransaction { get; set; }	
		public TransactionModel()
			{
			}
		}

		public class GroupedTransactionModel : ObservableCollection<TransactionModel>
		{
			public string LongName { get; set; }
			public string ShortName { get; set; }
		}
}
