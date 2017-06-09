using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class ProductDetailViewScala : Identifier
    {
        private int currentStock;
        private int reservedStock;
        private int orderedStock;
     
        public virtual int CurrentStock
        {
            get { return currentStock; }
            set { currentStock = value; }
        }

        public virtual int ReservedStock
        {
            get { return reservedStock; }
            set { reservedStock = value; }
        }

        public virtual int OrderedStock
        {
            get { return orderedStock; }
            set { orderedStock = value; }
        }
    }
}
