using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class Forecast : Identifier
    {
        private Product product;
        private int week;
        private int year;
        private int stock;
        private int purchase;
        private int sale;
        private int finalStock;
        private int safety;
        private int safetyCoEf;
        private int quantitySuggested;
        private int processedOn;


        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
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

        public virtual int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public virtual int Purchase
        {
            get { return purchase; }
            set { purchase = value; }
        }

        public virtual int Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public virtual int FinalStock
        {
            get { return finalStock; }
            set { finalStock = value; }
        }

        public virtual int Safety
        {
            get { return safety; }
            set { safety = value; }
        }

        public virtual int SafetyCoEf
        {
            get { return safetyCoEf; }
            set { safetyCoEf = value; }
        }

        public virtual int QuantitySuggested
        {
            get { return quantitySuggested; }
            set { quantitySuggested = value; }
        }

        public virtual int ProcessedOn
        {
            get { return processedOn; }
            set { processedOn = value; }
        }
                
    }
}
