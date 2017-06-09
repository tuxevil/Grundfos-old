using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Providers.Stock
{
    public class StockProduct : IStockProduct
    {
        #region IStockProduct Members

        private string productCode;
        private string description;
        private string group;
        private string provider;

        #endregion

        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        public StockProduct(string productCode, string description)
        {
            this.productCode = productCode;
            this.description = description;
        }
    }
}
