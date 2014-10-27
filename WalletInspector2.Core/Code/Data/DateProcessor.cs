using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletInspector2.Core.Interfaces;

namespace WalletInspector2.Core.Code.Data
{
    public class DateProcessor
    {
        private IRepository repository;

        private Guid userId;

        public DateProcessor(IRepository repository, Guid userId)
        {
            this.repository = repository;
            this.userId = userId;
        }

        public Period Now
        {
            get
            {
                return this.GetData(DateTime.Now);
            }
        }

        public Period Previous(DateTime date)
        {
            return this.GetData(date.AddDays(-1));
        }

        public Period Next(DateTime date)
        {
            return this.GetData(date.AddDays(1), false);
        }

        public Statistics GetDayData(DateTime date)
        {
            return new Statistics(this.repository.GetAllEntriesByDay(date, this.userId).Select(x => x.ToExpenseData()));
        }

        public Statistics GetWeekData(DateTime date)
        {
            return new Statistics(this.repository.GetAllEntriesByWeek(date, this.userId).Select(x => x.ToExpenseData()));
        }

        public Statistics GetMonthData(DateTime date)
        {
            return new Statistics(this.repository.GetAllEntriesByMonth(date, this.userId).Select(x => x.ToExpenseData()));
        }

        public Statistics GetYearData(DateTime date)
        {
            return new Statistics(this.repository.GetAllEntriesByYear(date, this.userId).Select(x => x.ToExpenseData()));
        }

        public Statistics GetTotalData()
        {
            return new Statistics(this.repository.GetAllEntriesByUserId(this.userId).Select(x => x.ToExpenseData()));
        }

        private Period GetData(DateTime date, bool prev = true)
        {
            var period = new Period();

            for (int i = 0; i <= 2; i++)
            {
                var curDate = date.AddDays(prev ? -i : i);
                var expenses = this.repository.GetAllEntriesByDay(curDate, this.userId);
                var day = new Day(curDate, expenses);

                period.Days.Add(day);
            }

            if (prev)
            {
                period.Days.Reverse();
            }

            return period;
        }
    }
}
