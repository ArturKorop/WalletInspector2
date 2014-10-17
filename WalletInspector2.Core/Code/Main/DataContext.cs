using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Interfaces;

namespace WalletInspector2.Core.Code.Main
{
    public class DataContext : DbContext, IRepository
    {
        public DbSet<ExpenseEntry> Entries { get; set; }

        public DataContext()
            : base("DefaultConnection")
        {
        }

        public ExpenseEntry Add(ExpenseEntry entry)
        {
            this.Entries.Add(entry);
            this.SaveChanges();

            return entry.Clone();
        }

        public void Update(ExpenseEntry entry)
        {
            var updatingEntry = this.Entries.Single(x => x.Id == entry.Id);
            updatingEntry.Update(entry);
            this.SaveChanges();
        }

        public void Remove(int id)
        {
            var removingEntry = this.Entries.SingleOrDefault(x => x.Id == id);
            if (removingEntry != null)
            {
                this.Entries.Remove(removingEntry);
                this.SaveChanges();
            }
        }

        public ExpenseEntry Get(int id)
        {
            return this.Entries.Single(x => x.Id == id);
        }

        public IEnumerable<ExpenseEntry> All(int userId)
        {
            return this.Entries.Where(x => x.UserId == userId).ToList();
        }
    }
}
