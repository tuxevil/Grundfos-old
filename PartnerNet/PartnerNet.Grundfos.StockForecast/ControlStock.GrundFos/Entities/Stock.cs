using System;
using System.Collections.Generic;
using System.Text;

namespace Grundfos.ScalaConnector
{
    public class Stock
    {
        private string id;
        private Product product;
        private int quantity;
        private string location;

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public virtual string Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
