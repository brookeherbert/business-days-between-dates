using DaysBetweenDates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaysBetweenDates.Services
{
    public interface IBusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate,
            IList<DateTime> publicHolidays);

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate,
            IList<PublicHoliday> publicHolidays);
    }
}
