using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Code.Main;
using WalletInspector2.Core.Interfaces;

namespace WalletInspector2.WebUI.Controllers
{
    public class DayController : Controller
    {
        private IRepository db = ServiceLocator.Get<IRepository>();

        public ActionResult Add(string inputName, int inputValue, string inputTags, DateTime date)
        {
            var expense = new ExpenseEntry(inputName, inputValue, null, date, 1);
            var result = this.db.Add(expense);

            return PartialView("~/Views/Expense/ExpenseView.cshtml", result);
        }
    }
}