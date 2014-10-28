using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Data
{
    public class Day
    {
        public DateTime Date { get; set; }

        public List<FullExpenseData> Expenses { get; set; }

        public Day(DateTime date)
            : this(date, new List<FullExpenseData>())
        {
        }

        public Day(DateTime date, IEnumerable<FullExpenseData> expenses)
        {
            this.Date = date;
            this.Expenses = expenses.ToList();
        }
    }
}
