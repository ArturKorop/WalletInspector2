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

        public int Tag { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public ExpenseEntry(string name, double value, int tag, DateTime date, Guid userId)
        {
            this.Name = name;
            this.Value = value;
            this.Tag = tag;
            this.Date = date;
            this.UserId = userId;
        }

        public ExpenseEntry(int id, string name, double value, int tag, DateTime date, Guid userId)
            : this(name, value, tag, date, userId)
        {
            this.Id = id;
        }

        public ExpenseEntry()
        {
        }

        public void Update(FullExpenseData data, int tag)
        {
            this.Name = data.Name;
            this.Value = data.Value;
            this.Tag = tag;
            this.Date = data.Date;
        }

        public ExpenseEntry Clone()
        {
            return new ExpenseEntry(this.Id, this.Name, this.Value, this.Tag, this.Date, this.UserId);
        }

        public FullExpenseData ToFullExpenseData(string tag)
        {
            return new FullExpenseData(this.Id, this.Name, this.Value, tag, this.Date, this.UserId);
        }
    }
}
