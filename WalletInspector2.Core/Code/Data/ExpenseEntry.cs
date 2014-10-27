using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WalletInspector2.Core.Code.Data
{
    public class ExpenseEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public ExpenseEntry(string name, double value, string tag, DateTime date, Guid userId)
        {
            this.Name = name;
            this.Value = value;
            this.Tag = tag;
            this.Date = date;
            this.UserId = userId;
        }

        public ExpenseEntry(int id, string name, double value, string tag, DateTime date, Guid userId)
            : this(name, value, tag, date, userId)
        {
            this.Id = id;
        }

        public ExpenseEntry()
        {
        }

        public void Update(ExpenseEntry entry)
        {
            this.Name = entry.Name;
            this.Value = entry.Value;
            this.Tag = entry.Tag;
            this.Date = entry.Date;
        }

        public ExpenseEntry Clone()
        {
            return new ExpenseEntry(this.Id, this.Name, this.Value, this.Tag, this.Date, this.UserId);
        }

        public ExpenseData ToExpenseData()
        {
            return new ExpenseData { Name = this.Name, Value = this.Value, Tag = this.Tag };
        }
    }
}
