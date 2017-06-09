using System.Collections.Generic;

namespace PartnerNet.Domain
{
    public class TransactionHistoryMonthly : Identifier
    {
        private Product productID;
        private int purchase;
        private int purchaseOrders;
        private int sale;
        private int stock;
        private int month;
        private int year;
        private string productCode;

        public virtual Product ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public virtual int Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public virtual int Month
        {
            get { return month; }
            set { month = value; }
        }

        public virtual int Year
        {
            get { return year; }
            set { year = value; }
        }

        public virtual int Purchase
        {
            get { return purchase; }
            set { purchase = value; }
        }

        public virtual int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public virtual int PurchaseOrders
        {
            get { return purchaseOrders; }
            set { purchaseOrders = value; }
        }

        public virtual string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        public TransactionHistoryMonthly() {}

        public TransactionHistoryMonthly(int sale, Product productid)
        {
            this.sale = sale;
            this.productID = productid;
        }

    }
}