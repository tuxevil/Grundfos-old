using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class TransactionHistoryWeeklyCurrent : Identifier
    {
        private Product productID;
        private int week;
        private int year;
        private int sale;
        private int purchase;
        private int purchaseOrders;
        private int stock;
        //propiedad temporal, no considerar y borrar
        private string productCode;

        public virtual Product ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public virtual int Week
        {
            get { return week; }
            set { week = value; }
        }

        public virtual int Year
        {
            get { return year; }
            set { year = value; }
        }

        public virtual int Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public virtual int Purchase
        {
            get { return purchase; }
            set { purchase = value; }
        }
        public virtual int PurchaseOrders
        {
            get { return purchaseOrders; }
            set { purchaseOrders = value; }
        }

        public virtual int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public virtual string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        public TransactionHistoryWeeklyCurrent() { }

        public TransactionHistoryWeeklyCurrent(int sale, Product productid)
        {
            this.sale = sale;
            this.productID = productid;
        }
    }
}
