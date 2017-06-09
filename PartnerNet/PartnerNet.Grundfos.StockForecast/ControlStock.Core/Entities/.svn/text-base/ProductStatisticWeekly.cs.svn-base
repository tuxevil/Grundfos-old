using System;
using ProjectBase.Data;

namespace PartnerNet.Domain
{
    public class ProductStatisticWeekly : Identifier
    {
        private Product product;
        private Period period;
        private int sale;
        private int year;
        private bool status;
        private int purchase;

        public virtual Period Period
        {
            get { return period; }
            set { period = value; }
        }

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual int Year
        {
            get { return year; }
            set { year = value; }
        }

        public virtual int Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public virtual int Purchase
        {
            get { return purchase; }
            set { purchase = value; }
        }

        public virtual bool Status
        {
            get { return status; }
            set { status = value; }
        }


        public ProductStatisticWeekly() { }

        public ProductStatisticWeekly(double sale, double purchase, Product product)
        {
            this.product = product;
            this.sale = Convert.ToInt32(sale);
            this.purchase = Convert.ToInt32(purchase);
        }
    }
}
