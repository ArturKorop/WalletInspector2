using System;
using System.Collections.Generic;
using System.Data.Entity;
using WalletInspector2.Core.Code.Data;

namespace WalletInspector2.Core.Interfaces
{
    public interface IRepository
    {
        FullExpenseData AddEntry(FullExpenseData entry);

        void RemoveEntryById(int id);

        void UpdateEntry(FullExpenseData entry);

        FullExpenseData GetEntryById(int id);

        IEnumerable<FullExpenseData> GetAllEntriesByUserId(Guid userId);

        IEnumerable<FullExpenseData> GetAllEntriesByDay(DateTime day, Guid userId);

        IEnumerable<FullExpenseData> GetAllEntriesByWeek(DateTime week, Guid userId);

        IEnumerable<FullExpenseData> GetAllEntriesByMonth(DateTime month, Guid userId);

        IEnumerable<FullExpenseData> GetAllEntriesByYear(DateTime year, Guid userId);

        IEnumerable<string> GetAllTagsByUserId(Guid userId);
    }
}
