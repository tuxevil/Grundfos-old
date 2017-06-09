using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Grundfos.ScalaConnector
{
    public class SaleOrder
    {
        private string id;
        private DateTime date;
        private string customerCode;
        private string location;

        public virtual DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public virtual int Month
        {
            get { return Date.Month; }
        }

        public virtual int Week
        {
            get { return Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday); }
        }

        public virtual int DateYear
        {
            get { return date.Year; }
        }

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }

        public virtual string Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
