using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class BreakDown2
    {
        private Product product;
        private Product part;
        private int quantity;

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual Product Part
        {
            get { return part; }
            set { part = value; }
        }

        public virtual int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
