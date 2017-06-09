using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class PurchaseOrderViewScala : Identifier
    {
        private string purchaseOrderCode;

        public virtual string PurchaseOrderCode
        {
            get { return purchaseOrderCode; }
            set { purchaseOrderCode = value; }
        }
    }
}
