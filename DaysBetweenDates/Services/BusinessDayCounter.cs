using DaysBetweenDates.Helpers;
using DaysBetweenDates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaysBetweenDates.Services
{
    public class BusinessDayCounter : IBusinessDayCounter
    {
        private const int WEEK_MODULO_OPERATOR = 6;
        private const int SUBSTITUTE_HOL_DAY_THRESHOLD = 2;

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate)
                return 0;

            var days = 0;

            for (DateTime tempDate = secondDate.Date.AddDays(-1); tempDate > firstDate; tempDate = tempDate.AddDays(-1))
            {
                if (IsWeekday(tempDate))
                    days++;
            }
            return days;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate,
            IList<DateTime> publicHolidays)
        {
            if (secondDate <= firstDate)
                return 0;

            var days = 0;

            for (DateTime tempDate = secondDate.Date.AddDays(-1); tempDate > firstDate; tempDate = tempDate.AddDays(-1))
            {
                if (IsWeekday(tempDate) && !publicHolidays.Where(x => x.Date == tempDate.Date).Any())
                    days++;
            }
            return days;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate,
          IList<PublicHoliday> publicHolidays)
        {
            if (secondDate <= firstDate)
                return 0;

            var days = 0;

            for (DateTime tempDate = secondDate.Date.AddDays(-1); tempDate > firstDate; tempDate = tempDate.AddDays(-1))
            {
                if (IsWeekday(tempDate) && !publicHolidays.Any(x => IsPublicHoliday(x, tempDate)))
                    days++;
            }
            return days;
        }

        private bool IsWeekday(DateTime date)
        {
            // If sat(6) or sun(0), modulo will be zero, therefore weekend
            return ((int)date.DayOfWeek + WEEK_MODULO_OPERATOR) % WEEK_MODULO_OPERATOR > 0;
        }

        private bool IsPublicHoliday(PublicHoliday publicHoliday, DateTime date)
        {
            return publicHoliday.Month == date.Month &&
                ((publicHoliday.FixedDate && publicHoliday.DayInMonth == date.Day) ||
                    (publicHoliday.VariableDayInMonth.DayOfWeek == (int)date.DayOfWeek &&
                       publicHoliday.VariableDayInMonth.WeekOfMonth == DateTimeHelper.GetWeekNumberOfMonth(date)) ||
                     IsSubstituteHoliday(publicHoliday, date));
        }

        private bool IsSubstituteHoliday(PublicHoliday publicHoliday, DateTime date)
        {
            if (publicHoliday.DayInMonth == null)
                return false;

            var publicHolidayDate = new DateTime(date.Year, publicHoliday.Month, publicHoliday.DayInMonth.Value);

            return publicHoliday.SubstituteHoliday &&
                (publicHolidayDate.DayOfWeek == DayOfWeek.Saturday || publicHolidayDate.DayOfWeek == DayOfWeek.Sunday) &&
                (date - publicHolidayDate).TotalDays < SUBSTITUTE_HOL_DAY_THRESHOLD &&
                date.DayOfWeek == DayOfWeek.Monday;
        }
    }
}
