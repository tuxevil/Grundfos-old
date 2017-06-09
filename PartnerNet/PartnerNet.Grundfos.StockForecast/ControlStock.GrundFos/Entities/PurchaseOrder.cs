using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Grundfos.ScalaConnector
{
    public class PurchaseOrder
    {
        private string id;
        private DateTime date;
        private int wayOfDelivery;
        private IList<PurchaseOrderDelivery> purchaseOrderDelivery;
        private string location;

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
            get { return Date.Year; }
        }

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public virtual int WayOfDelivery
        {
            get { return wayOfDelivery; }
            set { wayOfDelivery = value; }
        }

        public virtual IList<PurchaseOrderDelivery> PurchaseOrderDelivery
        {
            get { return purchaseOrderDelivery; }
            set { purchaseOrderDelivery = value; }
        }

        public virtual string Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
