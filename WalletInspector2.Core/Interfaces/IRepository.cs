using System.Collections.Generic;
using System.Data.Entity;
using WalletInspector2.Core.Code.Data;

namespace WalletInspector2.Core.Interfaces
{
    public interface IRepository
    {
        ExpenseEntry Add(ExpenseEntry entry);

        IEnumerable<ExpenseEntry> All(int userId);

        void Remove(int id);

        ExpenseEntry Get(int id);

        void Update(ExpenseEntry entry);
    }
}
