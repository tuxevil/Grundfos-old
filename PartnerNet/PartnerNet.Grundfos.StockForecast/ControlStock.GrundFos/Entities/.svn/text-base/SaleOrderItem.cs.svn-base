using System;
using System.Collections.Generic;
using System.Text;

namespace Grundfos.ScalaConnector
{
    public class SaleOrderItem
    {
        private SaleOrder saleOrder;
        private Product product;
        private double quantity;
        private double quantityDelivery;
        private DateTime deliveryDate;
        private string saleOrderId;

        public virtual SaleOrder SaleOrder
        {
            get { return saleOrder; }
            set { saleOrder = value; }
        }

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public virtual double QuantityDelivery
        {
            get { return quantityDelivery; }
            set { quantityDelivery = value; }
        }

        public virtual DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }

        public virtual string SaleOrderId
        {
            get { return saleOrderId; }
            set { saleOrderId = value; }
        }

        public SaleOrderItem() {}

        public SaleOrderItem(int quantity, Product product)
        {
            this.quantity = quantity;
            this.product = product;
        }

        public SaleOrderItem(double quantity, double quantitydelivery, Product product)
        {
            this.quantity = quantity;
            this.quantityDelivery = quantitydelivery;
            this.product = product;
        }

        public override bool Equals(object obj)
        {
            SaleOrderItem item = (SaleOrderItem)obj;
            return item.Product.Id == this.Product.Id && item.SaleOrder.Id == this.SaleOrder.Id;
        }

        public override int GetHashCode()
        {
            return this.Product.Id.GetHashCode() + this.SaleOrder.Id.GetHashCode();
        }
    }
}
