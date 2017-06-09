using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class BreakDown:Identifier
    {
        private int product;
        private int part;
        private int quantity;

        public virtual int Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual int Part
        {
            get { return part; }
            set { part = value; }
        }

        public virtual int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public BreakDown() {}

        public BreakDown(int id, int quantity)
        {
            this.part = id;
            this.quantity = quantity;
        }

    }
}
