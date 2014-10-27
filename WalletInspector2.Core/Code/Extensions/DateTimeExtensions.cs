using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsSameDay(this DateTime source, DateTime day)
        {
            return source.Year == day.Year && source.Month == day.Month && source.Day == day.Day;
        }

        public static bool IsSameWeek(this DateTime source, DateTime week)
        {
            var dayOfWeek = (int)week.DayOfWeek;
            var beginOfWeek = week.AddDays(1 - dayOfWeek);

            var fullWeek = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                fullWeek.Add(beginOfWeek.AddDays(i));
            }

            return fullWeek.Any(x => x.IsSameDay(source));
        }

        public static bool IsSameMonth(this DateTime source, DateTime month)
        {
            return source.Year == month.Year && source.Month == month.Month;
        }

        public static bool IsSameYear(this DateTime source, DateTime year)
        {
            return source.Year == year.Year;
        }

    }
}
