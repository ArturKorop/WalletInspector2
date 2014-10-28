using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Data
{
    public class FullExpenseData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public FullExpenseData()
        {
        }

        public FullExpenseData(string name, double value, string tag, DateTime date, Guid userId) : this()
        {
            this.Name = name;
            this.Value = value;
            this.Tag = tag;
            this.UserId = userId;
            this.Date = date;
        }

        public FullExpenseData(int id, string name, double value, string tag, DateTime date, Guid userId) : this(name, value, tag, date, userId)
        {
            this.Id = id;
        }

        public ExpenseEntry ToExpenseEntry (int tag)
        {
            return new ExpenseEntry(this.Id, this.Name, this.Value, tag, this.Date, this.UserId);
        }

        public SimpleExpenseData ToSimpleExpenseData()
        {
            return new SimpleExpenseData { Name = this.Name, Tag = this.Tag, Value = this.Value };
        }
    }
}
