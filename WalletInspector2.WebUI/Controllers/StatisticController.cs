using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalletInspector2.Core.Code.Data;
using WalletInspector2.Core.Code.Main;
using WalletInspector2.Core.Interfaces;
using WalletInspector2.WebUI.Code;

namespace WalletInspector2.WebUI.Controllers
{
    public class StatisticController : Controller
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

        public ActionResult Year(int? year)
        {
            var curYear = year.HasValue ? year.Value : DateTime.Now.Year;

            var yearStat = new YearStatisticData();
            yearStat.Name = curYear;
            yearStat.Data = this.DataProc.GetYearData(DateTime.Now);
            yearStat.TotalValue = yearStat.Data.TotalValue;
            //for (int i = 1; i <= 12; i++)
            //{
            //    var monthSimpleStat = this.DataProc.GetMonthData(new DateTime(DateTime.Now.Year, i, 1));
            //    DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            //    var monthName = mfi.GetMonthName(i);
            //    yearStat.Months[i - 1] = new YearStatisticData.MonthSimpleStatistic { Number = i, Name = monthName, TotalValue = monthSimpleStat.TotalValue };
            //}
            var monthsData = this.DataProc.GetMontsPerYearData(DateTime.Now);
            for (int i = 0; i < 12; i++)
            {
                var monthNumber = i + 1;
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                var monthName = mfi.GetMonthName(monthNumber);
                yearStat.Months[i] = new YearStatisticData.MonthSimpleStatistic { Number = monthNumber, Name = monthName, TotalValue = monthsData[i].TotalValue };
            }

            return View(yearStat);
        }
    }
}