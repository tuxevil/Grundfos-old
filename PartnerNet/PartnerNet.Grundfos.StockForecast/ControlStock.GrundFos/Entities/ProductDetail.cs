using System;
using System.Collections.Generic;
using System.Text;

namespace Grundfos.ScalaConnector
{
    public class ProductDetail
    {
        private string location;
        private int stock;
        private int sales1;
        private int sales2;
        private int purchases;
        private int leadtime;
        private int purchaseMod;
        private int repPoint;
        private Product product;
        private double standardCost;

        public virtual string Location
        {
            get { return location; }
            set { location = value; }
        }

        public virtual int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public virtual int Sales1
        {
            get { return sales1; }
            set { sales1 = value; }
        }

        public virtual int Sales2
        {
            get { return sales2; }
            set { sales2 = value; }
        }

        public virtual int Purchases
        {
            get { return purchases; }
            set { purchases = value; }
        }

        public virtual int Leadtime
        {
            get { return leadtime; }
            set { leadtime = value; }
        }

        public virtual int PurchaseMod
        {
            get { return purchaseMod; }
            set { purchaseMod = value; }
        }

        public virtual int RepPoint
        {
            get { return repPoint; }
            set { repPoint = value; }
        }

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public virtual double StandardCost
        {
            get { return standardCost; }
            set { standardCost = value; }
        }

        public override bool Equals(object obj)
        {
            ProductDetail item = (ProductDetail)obj;
            return item.Product.Id == this.Product.Id && item.Location == this.Location;
        }

        public override int GetHashCode()
        {
            return this.Product.Id.GetHashCode() + this.location.GetHashCode();
        }
    }
}
