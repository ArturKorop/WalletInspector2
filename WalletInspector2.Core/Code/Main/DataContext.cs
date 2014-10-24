using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Interfaces;
using WalletInspector2.Core.Code.Extensions;

namespace WalletInspector2.Core.Code.Main
{
    public class DataContext : DbContext, IRepository
    {
        public DbSet<ExpenseEntry> Entries { get; set; }

        public DataContext()
            : base("DefaultConnection")
        {
        }

        public ExpenseEntry AddEntry(ExpenseEntry entry)
        {
            this.Entries.Add(entry);
            this.SaveChanges();

            return entry.Clone();
        }

        public void UpdateEntry(ExpenseEntry entry)
        {
            var updatingEntry = this.Entries.Single(x => x.Id == entry.Id);
            updatingEntry.Update(entry);
            this.SaveChanges();
        }

        public void RemoveEntryById(int id)
        {
            var removingEntry = this.Entries.SingleOrDefault(x => x.Id == id);
            if (removingEntry != null)
            {
                this.Entries.Remove(removingEntry);
                this.SaveChanges();
            }
        }

        public ExpenseEntry GetEntryById(int id)
        {
            return this.Entries.Single(x => x.Id == id);
        }

        public IEnumerable<ExpenseEntry> GetAllEntriesByUserId(Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId).ToList();
        }

        public IEnumerable<ExpenseEntry> GetAllEntriesByDay(DateTime day, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId && x.Date.IsSameDay(day));
        }

        public IEnumerable<ExpenseEntry> GetAllEntriesByWeek(DateTime week, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId && x.Date.IsSameWeek(week));
        }

        public IEnumerable<ExpenseEntry> GetAllEntriesByMonth(DateTime month, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId && x.Date.IsSameMonth(month));
        }

        public IEnumerable<ExpenseEntry> GetAllEntriesByYear(DateTime year, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId && x.Date.IsSameYear(year));
        }
    }
}
