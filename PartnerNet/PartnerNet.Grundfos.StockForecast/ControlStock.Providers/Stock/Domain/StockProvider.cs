using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Providers.Stock.Domain
{
    public class StockProvider : IStockProvider
    {
        private string providerCode;
        private string name;

        public string ProviderCode
        {
            get { return providerCode; }
            set { providerCode = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
