using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class PurchaseOrder : Identifier
    {
        private Provider provider;
        private PurchaseOrderType purchaseOrderType;
        private PurchaseOrderStatus purchaseOrderStatus;
        private DateTime date;
        private IList<PurchaseOrderItem> purchaseOrderItems = new List<PurchaseOrderItem>();

        private WayOfDelivery wOD;


        public virtual Provider Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        public virtual PurchaseOrderType PurchaseOrderType
        {
            get { return purchaseOrderType; }
            set { purchaseOrderType = value; }
        }

        public virtual PurchaseOrderStatus PurchaseOrderStatus
        {
            get { return purchaseOrderStatus; }
            set { purchaseOrderStatus = value; }
        }

        public virtual DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public virtual IList<PurchaseOrderItem> PurchaseOrderItems
        {
            get { return purchaseOrderItems; }
            set { purchaseOrderItems = value; }
        }

        public virtual WayOfDelivery WOD
        {
            get { return wOD; }
            set { wOD = value; }
        }

        public PurchaseOrder() {}

        public PurchaseOrder(int id, Provider provider, DateTime date, PurchaseOrderStatus status)
        {
            this.id = id;
            this.provider = provider;
            this.date = date;
            this.purchaseOrderStatus = status;
        }

    }
}
