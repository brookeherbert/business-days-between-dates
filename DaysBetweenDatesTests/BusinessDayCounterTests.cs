using DaysBetweenDates.Models;
using DaysBetweenDates.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DaysBetweenDatesTests
{
    public class BusinessDayCounterTests
    {
        public BusinessDayCounter _businessDaysService;
        public IList<DateTime> sampleHolidays;

        [SetUp]
        public void Setup()
        {
            sampleHolidays = new List<DateTime>()
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 01, 01)
            };
        }

        public BusinessDayCounterTests()
        {
            _businessDaysService = new BusinessDayCounter();
        }

        [Test]
        public void WeekdaysBetweenTwoDatesStartDatetNotBeforeOrEqualToEndDateReturnsZero()
        {
            //Arrange
            var expected = 0;
            var startDate = new DateTime(2013, 10, 07);
            var endDate = new DateTime(2013, 10, 05);

            //Act
            var actual = _businessDaysService.WeekdaysBetweenTwoDates(startDate, endDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WeekdaysBetweenTwoDatesReturnsCorrectNumDays()
        {
            //Arrange
            var expected = 1;
            var startDate = new DateTime(2013, 10, 07);
            var endDate = new DateTime(2013, 10, 09);

            var expectedCase2 = 5;
            var startDateCase2 = new DateTime(2013, 10, 05);
            var endDateCase2 = new DateTime(2013, 10, 14);

            var expectedCase3 = 61;
            var startDateCase3 = new DateTime(2013, 10, 07);
            var endDateCase3 = new DateTime(2014, 01, 01);

            //Act
            var actual = _businessDaysService.WeekdaysBetweenTwoDates(startDate, endDate);
            var actualCase2 = _businessDaysService.WeekdaysBetweenTwoDates(startDateCase2, endDateCase2);
            var actualCase3 = _businessDaysService.WeekdaysBetweenTwoDates(startDateCase3, endDateCase3);

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedCase2, actualCase2);
            Assert.AreEqual(expectedCase3, actualCase3);
        }

        [Test]
        public void BusinessDaysBetweenTwoDatesReturnsCorrectNumDays()
        {
            //Arrange
            var expected = 1;
            var startDate = new DateTime(2013, 10, 07);
            var endDate = new DateTime(2013, 10, 09);

            var expectedCase2 = 0;
            var startDateCase2 = new DateTime(2013, 10, 24);
            var endDateCase2 = new DateTime(2013, 10, 27);

            var expectedCase3 = 59;
            var startDateCase3 = new DateTime(2013, 10, 07);
            var endDateCase3 = new DateTime(2014, 01, 01);

            //Act
            var actual = _businessDaysService.BusinessDaysBetweenTwoDates(startDate, endDate, sampleHolidays);
            var actualCase2 = _businessDaysService.BusinessDaysBetweenTwoDates(startDateCase2, endDateCase2, sampleHolidays);
            var actualCase3 = _businessDaysService.BusinessDaysBetweenTwoDates(startDateCase3, endDateCase3, sampleHolidays);


            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedCase2, actualCase2);
            Assert.AreEqual(expectedCase3, actualCase3);
        }

        [Test]
        public void BusinessDaysBetweenTwoDatesComplexReturnsCorrectNumDays()
        {
            //Arrange
            var holidays = new List<PublicHoliday>()
            {
                new PublicHoliday()
                {
                    HolidayName = "Holiday to test Substitution",
                    Month = 10,
                    DayInMonth = 26,
                    FixedDate = true,
                    SubstituteHoliday = true
                },
                new PublicHoliday()
                {
                    HolidayName = "Christmas Day",
                    Month = 12,
                    DayInMonth = 25,
                    FixedDate = true
                },
                new PublicHoliday()
                {
                    HolidayName = "Queens Birthday",
                    Month = 6,
                    VariableDayInMonth = new VariableDayInMonth()
                    {
                        DayOfWeek = (int)DayOfWeek.Monday,
                        WeekOfMonth = 2
                    }
                }
            };

            var expected = 1;
            var startDate = new DateTime(2013, 10, 24);
            var endDate = new DateTime(2013, 10, 29);

            var expectedCase2 = 3;
            var startDateCase2 = new DateTime(2013, 12, 24);
            var endDateCase2 = new DateTime(2013, 12, 29);

            var expectedCase3 = 29;
            var startDateCase3 = new DateTime(2013, 05, 31);
            var endDateCase3 = new DateTime(2014, 07, 01);

            //Act
            var actual = _businessDaysService.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);
            var actualCase2 = _businessDaysService.BusinessDaysBetweenTwoDates(startDateCase2, endDateCase2, holidays);
            var actualCase3 = _businessDaysService.BusinessDaysBetweenTwoDates(startDateCase3, endDateCase3, holidays);

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedCase2, actualCase2);
            Assert.AreEqual(expectedCase3, actualCase3);
        }
    }
}