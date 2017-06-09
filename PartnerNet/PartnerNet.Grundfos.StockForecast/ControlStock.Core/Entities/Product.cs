using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class Product:Identifier
    {
        private string productCode;
        private string description;
        private string group;
        private int leadTime;
        private int safety;
        private int repositionPoint;
        private int repositionLevel;
        private Provider provider;
        private IList<ProductStatisticWeekly> productStatisticsWeeklys;
        private IList<TransactionHistoryWeeklyCurrent> transactionHistoryWeeklys;
        private IList<ProductStatisticMonthly> productStatisticsMonthlys;
        private IList<Forecast> forecasts;
        private IList<ProductSet> productSets;
        private ProductViewScala viewScala;
        private string alternativeProduct;
        private DateTime alternativeDate;
        private string countryCode;

        public virtual string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
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

        public virtual int LeadTime
        {
            get { return leadTime; }
            set { leadTime = value; }
        }

        public virtual int Safety
        {
            get { return safety; }
            set { safety = value; }
        }

        public virtual int RepositionPoint
        {
            get { return repositionPoint; }
            set { repositionPoint = value; }
        }

        public virtual int RepositionLevel
        {
            get { return repositionLevel; }
            set { repositionLevel = value; }
        }

        public virtual Provider Provider
        {
            get { return provider; }
            set { provider = value; }
        }
        public virtual IList<ProductStatisticWeekly> ProductStatisticsWeeklys
        {
            get { return productStatisticsWeeklys; }
            set { productStatisticsWeeklys = value; }
        }
        public virtual IList<ProductStatisticMonthly> ProductStatisticsMonthlys
        {
            get { return productStatisticsMonthlys; }
            set { productStatisticsMonthlys = value; }
        }
        public virtual IList<Forecast> Forecasts
        {
            get { return forecasts; }
            set { forecasts = value; }
        }

        public virtual IList<ProductSet> ProductSets
        {
            get { return productSets; }
            set { productSets = value; }
        }

        public virtual IList<TransactionHistoryWeeklyCurrent> TransactionHistoryWeeklys
        {
            get { return transactionHistoryWeeklys; }
            set { transactionHistoryWeeklys = value; }
        }

        public virtual ProductViewScala ViewScala
        {
            get { return viewScala; }
            set { viewScala = value; }
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

        public virtual string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        public Product() {}

        public Product(string productCode)
        {
            this.productCode = productCode;
        }

        public Product(int id)
        {
            this.id = id;
        }

        public Product(string temp, string group)
        {
            this.group = group;
        }

    }
}
