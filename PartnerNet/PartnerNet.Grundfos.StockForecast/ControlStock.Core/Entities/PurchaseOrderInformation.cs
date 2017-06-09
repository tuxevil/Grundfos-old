using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class PurchaseOrderInformation
    {
        private int id;
        private int leadtime;
        private DateTime orderdate;
        private string provider;
        private int totalcount;
        private double amount;
        private string currency;
        private DateTime arrivaldate;
        private string type;
        private WayOfDelivery wOD;

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual DateTime Orderdate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }

        public virtual string Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        public virtual int Totalcount
        {
            get { return totalcount; }
            set { totalcount = value; }
        }

        public virtual double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public virtual DateTime Arrivaldate
        {
            //get { return arrivaldate; }

            get
            {
                int templeadtime = 0;
                switch (WayOfDelivery)
                {
                    case WayOfDelivery.Maritimo:
                        templeadtime = 7 * Leadtime;
                        break;
                    case WayOfDelivery.Aereo:
                        templeadtime = 15;
                        break;
                    case WayOfDelivery.Courrier:
                        templeadtime = 7;
                        break;
                }

                arrivaldate = Orderdate.AddDays(templeadtime);
                return arrivaldate;
            }
        }

        public virtual string Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual string Currency
        {
            get
            {
                switch (currency)
                {
                    case "00":
                        currency = "$";
                        break;
                    case "01":
                        currency = "U$S";
                        break;
                    case "02":
                        currency = "€";
                        break;
                }
                return currency;
            }
            set { currency = value; }
        }

        public virtual WayOfDelivery WayOfDelivery
        {
            get { return wOD; }
            set { wOD = value; }
        }

        public virtual int Leadtime
        {
            get { return leadtime; }
            set { leadtime = value; }
        }

        public PurchaseOrderInformation() {}

        public PurchaseOrderInformation(int id, string provider, DateTime orderdate, PurchaseOrderType type, WayOfDelivery wod, double amount, double quantity, string currency, double leadtime)
        {
            this.id = id;
            this.orderdate= orderdate;
            this.provider = provider;
            this.type= type.ToString();
            this.wOD = wod;
            this.amount = amount;
            this.totalcount = Convert.ToInt32(quantity);
            this.currency = currency;
            this.leadtime = Convert.ToInt32(leadtime);
        }
    }
}
