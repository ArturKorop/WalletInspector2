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
            var tag = this.GetTagId(expenseData.Tag, expenseData.UserId);
            var expenseEntry = expenseData.ToExpenseEntry(tag);
            this.Entries.Add(expenseEntry);
            this.SaveChanges();
            expenseData.Id = expenseEntry.Id;

            return expenseData;
        }

        public void UpdateEntry(FullExpenseData expenseData)
        {
            var updatingEntry = this.Entries.Single(x => x.Id == expenseData.Id);
            var tagId = this.GetTagId(expenseData.Tag, updatingEntry.UserId);

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
                var userId = removingEntry.UserId;
                var tagId = removingEntry.Tag;
                this.Entries.Remove(removingEntry);
                this.SaveChanges();

                var isRemoveTag = !this.MyEntries(userId).Any(x => x.Tag == tagId);
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
            return this.MyEntries(userId).Select(x=> x.ToFullExpenseData(this.GetTagName(x.Tag))).ToList();
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByDay(DateTime day, Guid userId)
        {
            return this.MyEntries(userId).Where(x => x.Date.IsSameDay(day)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag))).ToList();
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByWeek(DateTime week, Guid userId)
        {
            return this.MyEntries(userId).Where(x => x.Date.IsSameWeek(week)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag))).ToList();
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByMonth(DateTime month, Guid userId)
        {
            return this.MyEntries(userId).Where(x => x.Date.IsSameMonth(month)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag))).ToList();
        }

        public IEnumerable<FullExpenseData> GetAllEntriesByYear(DateTime year, Guid userId)
        {
            return this.MyEntries(userId).Where(x => x.Date.IsSameYear(year)).Select(x => x.ToFullExpenseData(this.GetTagName(x.Tag))).ToList();
        }

        public IEnumerable<FullExpenseData>[] GetAllEntiresInMonthsPerYear(DateTime year, Guid userId)
        {
            var result = new IEnumerable<FullExpenseData>[12];
            var data = this.GetAllEntriesByYear(year, userId);

            for (int i = 1; i <= 12; i++)
            {
                result[i - 1] = data.Where(x => x.Date.Month == i).ToList();
            }

            return result;
        }

        private string GetTagName(int id)
        {
            var tag = this.Tags.Single(x => x.Id == id);

            return tag.Name;
        }

        private int GetTagId(string name, Guid userId)
        {
            var tag = this.MyTags(userId).SingleOrDefault(x => x.Name == name);
            if(tag == null)
            {
                tag = this.Tags.Add(new TagEntry(name, userId));
                this.SaveChanges();
            }

            return tag.Id;
        }

        public IEnumerable<string> GetAllTagsByUserId(Guid userId)
        {
            return this.Tags.Where(x => x.UserId == userId).Select(x=>x.Name);
        }

        private List<ExpenseEntry> MyEntries(Guid userID)
        {
            return this.Entries.Where(x => x.UserId == userID).ToList();
        }

        private List<TagEntry> MyTags(Guid userID)
        {
            return this.Tags.Where(x => x.UserId == userID).ToList();
        }
    }
}
