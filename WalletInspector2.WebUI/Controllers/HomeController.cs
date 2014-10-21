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

        public Period CurrentPeriod
        {
            get
            {
                return this.Session["CurrentPeriod"] as Period;
            }
            set
            {
                this.Session["CurrentPeriod"] = value;
            }
        }

        public HomeController()
        {
            this.db = ServiceLocator.Get<IRepository>();
        }

        public ActionResult Index()
        {
            var temp = this.db.All(1);
            var week = new Period();
            week.Days = new List<Day>()
            {
                new Day(DateTime.Now.AddDays(-2), temp.Where(x=>x.Date.ToShortDateString() == DateTime.Now.AddDays(-2).ToShortDateString())),
                new Day(DateTime.Now.AddDays(-1), temp.Where(x=>x.Date.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString())),
                new Day(DateTime.Now, temp.Where(x=>x.Date.ToShortDateString() == DateTime.Now.ToShortDateString()))
            };

            this.CurrentPeriod = week;

            return View(week);
        }

        [HttpPost]
        public ActionResult Previous()
        {
            var currentDate = this.CurrentPeriod.Days.First().Date;
            ModelState.Remove("Date");
            var prevDate = currentDate.AddDays(-1);
            var temp = this.db.All(1);
            var week = new Period();
            week.Days = new List<Day>()
            {
                new Day(prevDate.AddDays(-2), temp.Where(x=>x.Date.ToShortDateString() == prevDate.AddDays(-2).ToShortDateString())),
                new Day(prevDate.AddDays(-1), temp.Where(x=>x.Date.ToShortDateString() == prevDate.AddDays(-1).ToShortDateString())),
                new Day(prevDate, temp.Where(x=>x.Date.ToShortDateString() == prevDate.ToShortDateString()))
            };

            this.CurrentPeriod = week;

            return View("~/Views/Home/DaysView.cshtml", week);
        }

        [HttpPost]
        public ActionResult Next()
        {
            var currentDate = this.CurrentPeriod.Days.Last().Date;
            ModelState.Remove("Date");
            var nextDate = currentDate.AddDays(1);
            var temp = this.db.All(1);
            var week = new Period();
            week.Days = new List<Day>()
            {
                new Day(nextDate.AddDays(2), temp.Where(x=>x.Date.ToShortDateString() == nextDate.AddDays(2).ToShortDateString())),
                new Day(nextDate.AddDays(1), temp.Where(x=>x.Date.ToShortDateString() == nextDate.AddDays(1).ToShortDateString())),
                new Day(nextDate, temp.Where(x=>x.Date.ToShortDateString() == nextDate.ToShortDateString()))
            };

            week.Days.Reverse();

            this.CurrentPeriod = week;

            return View("~/Views/Home/DaysView.cshtml", week);
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