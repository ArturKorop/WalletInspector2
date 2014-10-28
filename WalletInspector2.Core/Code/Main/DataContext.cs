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

        public DbSet<TagEntry> Tags { get; set; }

        public DataContext()
            : base("DefaultConnection")
        {
        }

        public FullExpenseData AddEntry(FullExpenseData expenseData)
        {
            var tag = this.GetTagId(expenseData.Tag);
            var expenseEntry = expenseData.ToExpenseEntry(tag);
            this.Entries.Add(expenseEntry);
            this.SaveChanges();
            expenseData.Id = expenseEntry.Id;

            return expenseData;
        }

        public void UpdateEntry(FullExpenseData expenseData)
        {
            var updatingEntry = this.Entries.Single(x => x.Id == expenseData.Id);
            var tagId = this.GetTagId(expenseData.Tag);

            updatingEntry.Update(expenseData, tagId);
            this.SaveChanges();

            if (tagId != updatingEntry.Tag)
            {
                var isRemoveTag = this.Entries.Any(x => x.Tag == tagId);
                if (isRemoveTag)
                {
                    this.Tags.Remove(this.Tags.Single(x => x.Id == tagId));
                    this.SaveChanges();
                }
            }
        }

        public void RemoveEntryById(int id)
        {
            var removingEntry = this.Entries.SingleOrDefault(x => x.Id == id);
            if (removingEntry != null)
            {
                var tagId = removingEntry.Tag;
                this.Entries.Remove(removingEntry);
                this.SaveChanges();

                var isRemoveTag = this.Entries.Any(x => x.Tag == tagId);
                if(isRemoveTag)
                {
                    this.Tags.Remove(this.Tags.Single(x=>x.Id == tagId));
                    this.SaveChanges();
                }
            }
        }

        public FullExpenseData GetEntryById(int id)
        {
            var entry = this.Entries.Single(x => x.Id == id);
            var tag = this.GetTagName(entry.Tag);

            return entry.ToFullExpenseData(tag);
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByUserId(Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId).Select(x=> x.ToFullExpenseData(this.GetTagName(x.Tag))).ToList();
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByDay(DateTime day, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId).ToList().Where(x => x.Date.IsSameDay(day)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag)));
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByWeek(DateTime week, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId).ToList().Where(x => x.Date.IsSameWeek(week)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag)));
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByMonth(DateTime month, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId).ToList().Where(x => x.Date.IsSameMonth(month)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag)));
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByYear(DateTime year, Guid userId)
        {
            return this.Entries.Where(x => x.UserId == userId).ToList().Where(x => x.Date.IsSameYear(year)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag)));
        }

        private string GetTagName(int id)
        {
            var tag = this.Tags.Single(x => x.Id == id);

            return tag.Name;
        }

        private int GetTagId(string name)
        {
            var tag = this.Tags.SingleOrDefault(x => x.Name == name);
            if(tag == null)
            {
                tag = this.Tags.Add(new TagEntry(name));
                this.SaveChanges();
            }

            return tag.Id;
        }
    }
}
