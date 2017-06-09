namespace PartnerNet.Providers.Stock
{
    public class StockProductTransaction : IStockProductTransaction
    {
        private string product;
        private int purchase;
        private int sale;
        private int stock;
        private int month;
        private int year;

        public string Product
        {
            get { return product; }
            set { product = value; }
        }

        public int Purchase
        {
            get { return purchase; }
            set { purchase = value; }
        }

        public int Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }
    }
}
