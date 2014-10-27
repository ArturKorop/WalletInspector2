using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace WalletInspector2.Core.Code.Data
{
    public class Statistics
    {
        public List<TagStatistics> Tags { get; private set; }

        public List<ExpenseStatistics> Expenses { get; private set; }

        public Statistics(IEnumerable<ExpenseData> data)
        {
            this.Tags = new List<TagStatistics>();
            this.Expenses = new List<ExpenseStatistics>();

            foreach (var expense in data)
            {
                this.UpdateTags(expense);
                this.UpdateExpenses(expense);
            }
        }

        private void UpdateTags(ExpenseData expense)
        {
            var tag = this.Tags.SingleOrDefault(x => x.Name == expense.Tag);
            if (tag == null)
            {
                tag = new TagStatistics { Name = expense.Tag };
                this.Tags.Add(tag);
            }

            tag.ExpenseNames.Add(expense.Name);
            tag.TotalAmount += expense.Value;
        }

        private void UpdateExpenses(ExpenseData expense)
        {
            var exp = this.Expenses.SingleOrDefault(x => x.Name == expense.Name);
            if (exp == null)
            {
                exp = new ExpenseStatistics { Name = expense.Name };
                this.Expenses.Add(exp);
            }

            exp.TotalAmount += expense.Value;
        }

        public class TagStatistics
        {
            public string Name { get; set; }

            public List<string> ExpenseNames { get; set; }

            public double TotalAmount { get; set; }

            public TagStatistics()
            {
                this.ExpenseNames = new List<string>();
            }
        }

        public class ExpenseStatistics
        {
            public string Name { get; set; }

            public double TotalAmount { get; set; }
        }
    }
}
