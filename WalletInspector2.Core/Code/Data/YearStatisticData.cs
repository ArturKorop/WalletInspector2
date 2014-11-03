using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Data
{
    public class YearStatisticData
    {
        public int Name { get; set; }

        public double TotalValue { get; set; }

        public MonthSimpleStatistic[] Months = new MonthSimpleStatistic[12];

        public Statistics Data { get; set; }

        public class MonthSimpleStatistic
        {
            public int Number { get; set; }

            public string Name { get; set; }

            public double TotalValue { get; set; }
        }
    }
}
