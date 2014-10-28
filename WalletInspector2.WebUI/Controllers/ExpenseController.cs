using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Code.Main;
using WalletInspector2.Core.Interfaces;
using WalletInspector2.WebUI.Models;

namespace WalletInspector2.WebUI.Controllers
{
    public class ExpenseController : Controller
    {
        IRepository db;

        public ExpenseController()
        {
            this.db = ServiceLocator.Get<IRepository>();
        }

        [HttpPost]
        public void Update(FullExpenseData entry)
        {
            this.db.UpdateEntry(entry);
        }

        [HttpPost]
        public void Remove(int id)
        {
            this.db.RemoveEntryById(id);
        }
    }
}