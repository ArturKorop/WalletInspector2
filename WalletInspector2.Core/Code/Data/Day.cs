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

        public List<ExpenseEntry> Expenses { get; set; }
    }
}
