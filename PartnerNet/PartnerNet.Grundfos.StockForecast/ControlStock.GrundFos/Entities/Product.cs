using System;
using System.Collections.Generic;
using System.Text;
using PartnerNet.Providers.Stock;

namespace Grundfos.ScalaConnector
{
    public class Product
    {
        private string id;
        private string description;
        private string description2;
        private string group;
        private double purchasePrice;
        private string purchaseCurrency;
        private double salePrice;
        private int overCost;
        private double standardCost;
        private Provider provider;
        private IList<ProductDetail> detail;
        private int stockQ;
        private string alternativeProduct;
        private DateTime alternativeDate;
        private string countryCode;
        private IList<ProductParts> parts;

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual string Group
        {
            get { return group; }
            set { group = value; }
        }

        public virtual Provider Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        public virtual string Description2
        {
            get { return description2; }
            set { description2 = value; }
        }

        public virtual double PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; }
        }

        public virtual string PurchaseCurrency
        {
            get { return purchaseCurrency; }
            set { purchaseCurrency = value; }
        }

        public virtual double SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }

        public virtual int OverCost
        {
            get { return overCost; }
            set { overCost = value; }
        }

        public virtual IList<ProductDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        public virtual int StockQ
        {
            get { return stockQ; }
            set { stockQ = value; }
        }

        public virtual string AlternativeProduct
        {
            get { return alternativeProduct; }
            set { alternativeProduct = value; }
        }

        public virtual DateTime AlternativeDate
        {
            get { return alternativeDate; }
            set { alternativeDate = value; }
        }

        public virtual double StandardCost
        {
            get { return standardCost; }
            set { standardCost = value; }
        }

        public virtual string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        public virtual IList<ProductParts> Parts
        {
            get { return parts; }
            set { parts = value; }
        }
    }
}
