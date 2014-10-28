using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Code.Main;
using WalletInspector2.Core.Interfaces;
using Microsoft.AspNet.Identity;
using WalletInspector2.WebUI.Code;

namespace WalletInspector2.WebUI.Controllers
{
    public class DayController : Controller
    {
        private IRepository db = ServiceLocator.Get<IRepository>();

        private Guid UserId
        {
            get
            {
                if(this.User != null)
                {
                    return new Guid(this.User.Identity.GetUserId());
                }

                return Guid.Empty;
            }
        }

        public ActionResult Add(string inputName, double inputValue, string inputTag, DateTime date)
        {
            var expense = new FullExpenseData(inputName, inputValue, inputTag ?? string.Empty, date, this.User.Id());
            var result = this.db.AddEntry(expense);

            return PartialView("~/Views/Expense/ExpenseView.cshtml", result);
        }
    }
}