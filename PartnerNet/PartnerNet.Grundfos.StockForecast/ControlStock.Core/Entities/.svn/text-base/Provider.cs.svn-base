using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class Provider : Identifier
    {
        private string providerCode;
        private string name;
        private Country country;
        private IList<Product> products;
        private string countryCode;
        
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual Country Country
        {
            get { return country; }
            set { country = value; }
        }
        public virtual IList<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        public virtual string ProviderCode
        {
            get { return providerCode; }
            set { providerCode = value; }
        }

        public virtual string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        public Provider() {}

        public Provider(string providerCode, string name)
        {
            this.providerCode = providerCode;
            this.name = name;
        }
    }
}
