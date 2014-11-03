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

        public double TotalValue { get; set; }

        public Statistics(IEnumerable<SimpleExpenseData> data)
        {
            this.Tags = new List<TagStatistics>();
            this.Expenses = new List<ExpenseStatistics>();

            foreach (var expense in data)
            {
                this.UpdateTags(expense);
                this.UpdateExpenses(expense);
            }

            this.Tags.Sort((a, b) => a.Name.CompareTo(b.Name));
            this.Expenses.Sort((a, b) => a.Name.CompareTo(b.Name));
            this.TotalValue = this.Expenses.Sum(x => x.TotalAmount); ;
        }

        private void UpdateTags(SimpleExpenseData expense)
        {
            var tag = this.Tags.SingleOrDefault(x => x.Name == expense.Tag);
            if (tag == null)
            {
                tag = new TagStatistics { Name = expense.Tag };
                this.Tags.Add(tag);
            }

            var curExpense = tag.Expenses.SingleOrDefault(x => x.Name == expense.Name);
            if(curExpense == null)
            {
                curExpense = new ExpenseStatistics { Name = expense.Name };
                tag.Expenses.Add(curExpense);
            }

            curExpense.TotalAmount += expense.Value;
            tag.TotalAmount += expense.Value;
        }

        private void UpdateExpenses(SimpleExpenseData expense)
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

            public List<ExpenseStatistics> Expenses { get; set; }

            public double TotalAmount { get; set; }

            public TagStatistics()
            {
                this.Expenses = new List<ExpenseStatistics>();
            }
        }

        public class ExpenseStatistics
        {
            public string Name { get; set; }

            public double TotalAmount { get; set; }
        }
    }
}
