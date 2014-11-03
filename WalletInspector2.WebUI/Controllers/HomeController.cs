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
        private DataProcessor DataProc
        {
            get
            {
                return new DataProcessor(ServiceLocator.Get<IRepository>(), this.UserId);
            }
        }

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

        public ActionResult Index()
        {
            var period = this.DataProc.Now;
            this.CurrentPeriod = period;
            this.UpdateUsefulTagsAndItems();

            return View(period);
        }

        [HttpPost]
        public ActionResult Previous()
        {
            var currentDate = this.CurrentPeriod.Days.First().Date;
            ModelState.Remove("Date");

            var period = this.DataProc.Previous(currentDate);
            this.CurrentPeriod = period;
            this.UpdateUsefulTagsAndItems();

            return View("~/Views/Home/DaysView.cshtml", period);
        }

        [HttpPost]
        public ActionResult Next()
        {
            var currentDate = this.CurrentPeriod.Days.Last().Date;
            ModelState.Remove("Date");

            var period = this.DataProc.Next(currentDate);
            this.CurrentPeriod = period;
            this.UpdateUsefulTagsAndItems();

            return View("~/Views/Home/DaysView.cshtml", period);
        }

        [HttpPost]
        public JsonResult GetWeekData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var data = this.DataProc.GetWeekData(curDate);
            var jsonData = data.Expenses.Select(x => new { name = x.Name, y = x.TotalAmount });

            return new JsonResult() { Data = jsonData };
        }

        [HttpPost]
        public JsonResult GetMonthData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var data = this.DataProc.GetMonthData(curDate);
            var jsonData = data.Tags;

            return new JsonResult() { Data = jsonData };
        }

        [HttpPost]
        public JsonResult GetMonthAndWeekData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var weekData = this.DataProc.GetWeekData(curDate);
            var jsonWeekData = weekData.Expenses.Select(x => new { name = x.Name, y = x.TotalAmount });
            var totalWeekAmount = weekData.Expenses.Sum(x => x.TotalAmount);

            var monthData = this.DataProc.GetMonthData(curDate);
            var jsonMonthData = monthData.Tags;
            var totalMonthAmount = monthData.Expenses.Sum(x => x.TotalAmount);

            return new JsonResult() { Data = new { week = new { data = jsonWeekData, value = totalWeekAmount }, month = new { data = jsonMonthData, value = totalMonthAmount } } };
        }

        [HttpPost]
        public JsonResult GetYearData()
        {
            var curDate = this.CurrentPeriod.Days.Last().Date;
            var data = this.DataProc.GetYearData(curDate);
            var jsonData = data.Tags.Select(x => new { name = x.Name, y = x.TotalAmount });

            return new JsonResult() { Data = jsonData };
        }

        [HttpPost]
        public JsonResult GetTotalData()
        {
            var data = this.DataProc.GetTotalData();
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

        private void UpdateUsefulTagsAndItems()
        {
            this.ViewBag.Tags = this.DataProc.GetTags();
        }
    }
}