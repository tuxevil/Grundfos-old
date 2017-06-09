using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class ProductViewScala : Identifier
    {
        private double price;
        private string currency;
        private int currentStock;
        private int reservedStock;
        private int orderedStock;

        public virtual double Price
        {
            get { return price; }
            set { price = value; }
        }

        public virtual string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public virtual int CurrentStock
        {
            get { return currentStock; }
            set { currentStock = value; }
        }

        public virtual int ReservedStock
        {
            get { return reservedStock; }
            set { reservedStock = value; }
        }

        public virtual int OrderedStock
        {
            get { return orderedStock; }
            set { orderedStock = value; }
        }
    }
}
