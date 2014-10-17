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
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }

        public double Value { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Date { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        public ExpenseEntry(string name, double value, List<string> tags, DateTime date, int userId)
        {
            this.Name = name;
            this.Value = value;
            this.Tags = tags;
            this.Date = date;
            this.UserId = userId;
        }

        public ExpenseEntry(int id, string name, double value, List<string> tags, DateTime date, int userId)
            : this(name, value, tags, date, userId)
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
            this.Tags = entry.Tags;
            this.Date = entry.Date;
        }

        public ExpenseEntry Clone()
        {
            return new ExpenseEntry(this.Id, this.Name, this.Value, this.Tags, this.Date, this.UserId);
        }
    }
}
