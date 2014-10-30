using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Data
{
    public class MonthStatistic
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string Name { get; set; }

        public List<Day> Days { get; set; }

        public double TotalValue { get; set; }
    }
}
