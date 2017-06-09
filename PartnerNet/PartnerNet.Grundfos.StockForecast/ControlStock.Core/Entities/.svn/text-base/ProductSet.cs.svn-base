using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class ProductSet : Identifier
    {
        private string name;
        private IList<Product> products = new List<Product>();

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual IList<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        public ProductSet() {}

        public ProductSet(string name)
        {
            this.name = name;
        }
    }
}
