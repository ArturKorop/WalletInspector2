using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalletInspector2.Core.Code.Main;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Interfaces;
using WalletInspector2.WebUI.Models;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using Microsoft.AspNet.Identity;
using WalletInspector2.WebUI.Code;

namespace WalletInspector2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IRepository db;

        private Guid UserId
        {
            get
            {
                return this.User.Id();
            }
        }

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
            var test = new { Value = 15 };
            var test2 = new JavaScriptSerializer();
            var test3 = test2.Serialize(test);
            var test4 = test3.Count();

            var temp = this.db.GetAllEntriesByUserId(this.UserId);
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
            var temp = this.db.GetAllEntriesByUserId(this.UserId);
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
            var temp = this.db.GetAllEntriesByUserId(this.UserId);
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

        [HttpPost]
        public JsonResult GetWeekData()
        {
            var data = this.db.GetAllEntriesByUserId(this.UserId);
            var jsonData = data.Select(x => new { name = x.Name, y = x.Value });

            return new JsonResult() { Data = jsonData };
        }

        //[HttpPost]
        //public JsonResult GetMonthData()
        //{
        //    var data = this.db.All(1);
        //    var jsonData = data.Select(x => new { name = x.Name, y = x.Value });

        //    return new JsonResult() { Data = jsonData };
        //}

        //[HttpPost]
        //public JsonResult GetYearData()
        //{
        //    var data = this.db.All(1);
        //    var jsonData = data.Select(x => new { name = x.Name, y = x.Value });

        //    return new JsonResult() { Data = jsonData };
        //}

        //[HttpPost]
        //public JsonResult GetTotalData()
        //{
        //    var data = this.db.All(1);
        //    var jsonData = data.Select(x => new { name = x.Name, y = x.Value });

        //    return new JsonResult() { Data = jsonData };
        //}


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