using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Grundfos.ScalaConnector
{
    public class PurchaseOrderDelivery
    {
        private Product product;
        private PurchaseOrder purchaseOrder;
        private DateTime deliveryDate;
        private string temp;
        private Provider provider;
        private string providerStatus;
        private string orderLine;
        private string deliveryType;

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual PurchaseOrder PurchaseOrder
        {
            get { return purchaseOrder; }
            set { purchaseOrder = value; }
        }

        public virtual DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }

        public virtual string Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        public virtual Provider Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        public virtual string ProviderStatus
        {
            get { return providerStatus; }
            set { providerStatus = value; }
        }

        public virtual string DeliveryType
        {
            get { return deliveryType; }
            set { deliveryType = value; }
        }

        public virtual string OrderLine
        {
            get { return orderLine; }
            set { orderLine = value; }
        }

        public override bool Equals(object obj)
        {
            PurchaseOrderDelivery item = (PurchaseOrderDelivery)obj;
            return item.Product.Id == this.Product.Id && item.PurchaseOrder.Id == this.PurchaseOrder.Id;
        }

        public override int GetHashCode()
        {
            return this.Product.Id.GetHashCode() + this.PurchaseOrder.Id.GetHashCode();
        }
    }
}
