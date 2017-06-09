using System;
using System.Collections.Generic;
using System.Text;

namespace Grundfos.ScalaConnector
{
    public class Transactions
    {
        private string id;
        private Product product;
        private DateTime date;
        private int quantity;
        private string orderNumber;
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

        public virtual DateTime Date
        {
            get { return date; }
            set { date = value; }
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

        public virtual string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        public Transactions() {}

        public Transactions(int quantity, Product product)
        {
            this.quantity = quantity;
            this.product = product;
        }
    }
}
