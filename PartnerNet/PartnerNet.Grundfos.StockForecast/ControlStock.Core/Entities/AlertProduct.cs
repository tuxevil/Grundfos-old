using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class AlertProduct : Identifier
    {
        private string productCode;
        private double standardCost;
        private double subTotal;
        private int quantity;
        private DateTime negativeDate;
        private int type;

        public virtual int Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        public virtual double StandardCost
        {
            get { return standardCost; }
            set { standardCost = value; }
        }

        public virtual double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }

        public virtual int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public virtual DateTime NegativeDate
        {
            get { return negativeDate; }
            set { negativeDate = value; }
        }

    }
}
