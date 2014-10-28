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
        private DateProcessor dateProcessor;

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
            this.dateProcessor = new DateProcessor(ServiceLocator.Get<IRepository>(), this.UserId);
        }

        public ActionResult Index()
        {
            var period = this.dateProcessor.Now;
            this.CurrentPeriod = period;

            return View(period);
        }

        [HttpPost]
        public ActionResult Previous()
        {
            var currentDate = this.CurrentPeriod.Days.First().Date;
            ModelState.Remove("Date");

            var period = this.dateProcessor.Previous(currentDate);
            this.CurrentPeriod = period;

            return View("~/Views/Home/DaysView.cshtml", period);
        }

        [HttpPost]
        public ActionResult Next()
        {
            var currentDate = this.CurrentPeriod.Days.Last().Date;
            ModelState.Remove("Date");

            var period = this.dateProcessor.Next(currentDate);
            this.CurrentPeriod = period;

            return View("~/Views/Home/DaysView.cshtml", period);
        }

        [HttpPost]
        public JsonResult GetWeekData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var data = this.dateProcessor.GetWeekData(curDate);
            var jsonData = data.Expenses.Select(x => new { name = x.Name, y = x.TotalAmount });

            return new JsonResult() { Data = jsonData };
        }

        [HttpPost]
        public JsonResult GetMonthData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var data = this.dateProcessor.GetMonthData(curDate);
            var jsonData = data.Tags;//.Expenses.Select(x => new { name = x.Name, y = x.TotalAmount });

            return new JsonResult() { Data = jsonData };
        }

        [HttpPost]
        public JsonResult GetYearData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var data = this.dateProcessor.GetYearData(curDate);
            var jsonData = data.Tags.Select(x => new { name = x.Name, y = x.TotalAmount });

            return new JsonResult() { Data = jsonData };
        }

        [HttpPost]
        public JsonResult GetTotalData()
        {
            var data = this.dateProcessor.GetTotalData();
            var jsonData = data.Tags.Select(x => new { name = x.Name, y = x.TotalAmount });

            return new JsonResult() { Data = jsonData };
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