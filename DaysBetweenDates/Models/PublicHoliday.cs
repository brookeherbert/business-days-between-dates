using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaysBetweenDates.Models
{
    public class PublicHoliday
    {
        public PublicHoliday()
        {
        }

        public string HolidayName { get; set; }
        public int Month { get; set; }
        public bool FixedDate { get; set; }
        public int? DayInMonth { get; set; }
        public VariableDayInMonth VariableDayInMonth { get; set; }
        public bool SubstituteHoliday { get; set; }
    }

    public class VariableDayInMonth
    {
        public int DayOfWeek { get; set; }
        public int WeekOfMonth { get; set; }
    }
}
