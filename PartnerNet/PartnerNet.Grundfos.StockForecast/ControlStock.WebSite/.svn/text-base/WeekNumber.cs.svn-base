using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Grundfos.StockForecast
{
    public class WeekNumber
    {
        public static int WeekNumber_Entire4DayWeekRule(DateTime date)
        {
            // Updated 2004.09.27. Cleaned the code and fixed a bug. Compared the algorithm with
            // code published here . Tested code successfully against the other algorithm 
            // for all dates in all years between 1900 and 2100.
            // Thanks to Marcus Dahlberg for pointing out the deficient logic.

            // Calculates the ISO 8601 Week Number
            // In this scenario the first day of the week is monday, 
            // and the week rule states that:
            // [...] the first calendar week of a year is the one 
            // that includes the first Thursday of that year and 
            // [...] the last calendar week of a calendar year is 
            // the week immediately preceding the first 
            // calendar week of the next year.
            // The first week of the year may thus start in the 
            // preceding year

            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool ThursdayFlag = false;

            // Get the day number since the beginning of the year
            int DayOfYear = date.DayOfYear;

            // Get the numeric weekday of the first day of the 
            // year (using sunday as FirstDay)
            int StartWeekDayOfYear =
                 (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            int EndWeekDayOfYear =
                 (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

            // Compensate for the fact that we are using monday
            // as the first day of the week
            if (StartWeekDayOfYear == 0)
                StartWeekDayOfYear = 7;
            if (EndWeekDayOfYear == 0)
                EndWeekDayOfYear = 7;

            // Calculate the number of days in the first and last week
            int DaysInFirstWeek = 8 - (StartWeekDayOfYear);
            int DaysInLastWeek = 8 - (EndWeekDayOfYear);

            // If the year either starts or ends on a thursday it will have a 53rd week
            if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
                ThursdayFlag = true;

            // We begin by calculating the number of FULL weeks between the start of the year and
            // our date. The number is rounded up, so the smallest possible value is 0.
            int FullWeeks = (int)Math.Ceiling((DayOfYear - (DaysInFirstWeek)) / 7.0);

            int WeekNumber = FullWeeks;

            // If the first week of the year has at least four days, then the actual week number for our date
            // can be incremented by one.
            if (DaysInFirstWeek >= THURSDAY)
                WeekNumber = WeekNumber + 1;

            // If week number is larger than week 52 (and the year doesn't either start or end on a thursday)
            // then the correct week number is 1.
            if (WeekNumber > 53 && !ThursdayFlag)
                WeekNumber = 1;

            // If week number is still 0, it means that we are trying to evaluate the week number for a
            // week that belongs in the previous year (since that week has 3 days or less in our date's year).
            // We therefore make a recursive call using the last day of the previous year.
            if (WeekNumber == 0)
                WeekNumber = WeekNumber_Entire4DayWeekRule(
                     new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
            return WeekNumber;
        }
    }
}
