using System;
using System.Collections.Generic;
using System.Text;

namespace Grundfos.ScalaConnector
{
    public class PurchaseOrderItem
    {
        private PurchaseOrder purchaseOrder;
        private Product product;
        private int quantity;
        private string confirmed;
        private int quantityOrdered;
        private DateTime arrivalDate;
        private string temp1;
        private string temp2;

        public virtual PurchaseOrder PurchaseOrder
        {
            get { return purchaseOrder; }
            set { purchaseOrder = value; }
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

        public virtual string Confirmed
        {
            get { return confirmed; }
            set { confirmed = value; }
        }

        public virtual int QuantityOrdered
        {
            get { return quantityOrdered; }
            set { quantityOrdered = value; }
        }

        public virtual DateTime ArrivalDate
        {
            get { return arrivalDate; }
            set { arrivalDate = value; }
        }

        public virtual string Temp1
        {
            get { return temp1; }
            set { temp1 = value; }
        }

        public virtual string Temp2
        {
            get { return temp2; }
            set { temp2 = value; }
        }

        public PurchaseOrderItem() {}

        public PurchaseOrderItem(int quantityOrdered, Product product)
        {
            this.quantityOrdered = quantityOrdered;
            this.product = product;
        }

        public override bool Equals(object obj)
        {
            PurchaseOrderItem item = (PurchaseOrderItem) obj;
            return item.PurchaseOrder.Id == this.PurchaseOrder.Id;
        }

        public override int GetHashCode()
        {
            return this.PurchaseOrder.Id.GetHashCode();
        }
    }
}
