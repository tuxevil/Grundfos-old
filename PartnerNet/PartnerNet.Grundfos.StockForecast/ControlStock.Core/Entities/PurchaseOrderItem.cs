using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class PurchaseOrderItem : Identifier
    {
        private double quantity;
        private Product product;
        private PurchaseOrder purchaseOrder;
        private int quantitySuggested;
        private Forecast forecast;
        private PurchaseOrderItemStatus purchaseOrderItemStatus;

        public virtual double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

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

        public virtual int QuantitySuggested
        {
            get { return quantitySuggested; }
            set { quantitySuggested = value; }
        }

        public virtual Forecast Forecast
        {
            get { return forecast; }
            set { forecast = value; }
        }

        public virtual PurchaseOrderItemStatus PurchaseOrderItemStatus
        {
            get { return purchaseOrderItemStatus; }
            set { purchaseOrderItemStatus = value; }
        }

    }
}
