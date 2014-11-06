using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var curYearDate = new DateTime(curYear, 1, 1);
            var yearStat = new YearStatisticData();
            yearStat.Name = curYear;
            yearStat.Data = this.DataProc.GetYearData(curYearDate);
            yearStat.TotalValue = yearStat.Data.TotalValue;
            var monthsData = this.DataProc.GetMontsPerYearData(curYearDate);

            for (int i = 0; i < 12; i++)
            {
                var monthNumber = i + 1;
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                var monthName = mfi.GetMonthName(monthNumber);
                yearStat.Months[i] = new YearStatisticData.MonthSimpleStatistic { Number = monthNumber, Name = monthName, TotalValue = monthsData[i].TotalValue };
            }

            return View(yearStat);
        }

        public ActionResult Month(int year, int month)
        {
            var monthStat = this.DataProc.GetMonthData(new DateTime(year, month, 1));
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            var monthName = mfi.GetMonthName(month);
            dynamic source = new ExpandoObject();
            source.Name = monthName;
            source.Data = monthStat;
            source.Year = year;
            source.Month = month;

            return View(source);
        }
    }
}