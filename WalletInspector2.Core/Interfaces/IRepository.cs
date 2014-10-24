using System;
using System.Collections.Generic;
using System.Data.Entity;
using WalletInspector2.Core.Code.Data;

namespace WalletInspector2.Core.Interfaces
{
    public interface IRepository
    {
        ExpenseEntry AddEntry(ExpenseEntry entry);

        void RemoveEntryById(int id);

        void UpdateEntry(ExpenseEntry entry);

        ExpenseEntry GetEntryById(int id);

        IEnumerable<ExpenseEntry> GetAllEntriesByUserId(Guid userId);

        IEnumerable<ExpenseEntry> GetAllEntriesByDay(DateTime day, Guid userId);

        IEnumerable<ExpenseEntry> GetAllEntriesByWeek(DateTime week, Guid userId);

        IEnumerable<ExpenseEntry> GetAllEntriesByMonth(DateTime month, Guid userId);

        IEnumerable<ExpenseEntry> GetAllEntriesByYear(DateTime year, Guid userId);
    }
}
