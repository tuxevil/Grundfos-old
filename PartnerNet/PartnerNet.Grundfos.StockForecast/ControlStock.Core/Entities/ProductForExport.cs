using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class ProductForExport
    {
        private string productCode;
        private string description;
        private int trimestralSale;
        private int anualSale;
        private int repLevel;
        private int repPoint;
        
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

        public virtual int TrimestralSale
        {
            get { return trimestralSale; }
            set { trimestralSale = value; }
        }

        public virtual int AnualSale
        {
            get { return anualSale; }
            set { anualSale = value; }
        }

        public int RepLevel
        {
            get { return repLevel; }
            set { repLevel = value; }
        }

        public int RepPoint
        {
            get { return repPoint; }
            set { repPoint = value; }
        }

        public ProductForExport() {}

        public ProductForExport(string productcode, string description, int trimestralSale, int anualSale, int repLevel, int repPoint)
        {
            this.productCode = productcode;
            this.description = description;
            this.trimestralSale = trimestralSale;
            this.anualSale = anualSale;
            this.repLevel = repLevel;
            this.repPoint = repPoint;
        }
    }
}
