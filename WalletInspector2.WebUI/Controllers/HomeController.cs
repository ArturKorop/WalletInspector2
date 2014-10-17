using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalletInspector2.Core.Code.Main;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Interfaces;
using WalletInspector2.WebUI.Models;

namespace WalletInspector2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IRepository db;

        public HomeController()
        {
            this.db = ServiceLocator.Get<IRepository>();
        }

        public ActionResult Index()
        {
            var temp = this.db.All(1);
            var week = new Week();
            week.Days = new List<Day>()
            {
                new Day(){ Expenses = temp.ToList(), Date = DateTime.Now }
            };

            return View(week);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}