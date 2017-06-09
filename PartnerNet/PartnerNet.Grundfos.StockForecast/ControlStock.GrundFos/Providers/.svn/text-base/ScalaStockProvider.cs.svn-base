using System;
using System.Collections.Generic;
using PartnerNet.Providers.Stock;

namespace Grundfos.ScalaConnector.Providers
{
    public class ScalaStockProvider : StockProvider
    {
        public override string ApplicationName
        {
            get
            {
                return "Scala";
            }
        }

        public override IStockProduct GetProduct(string productCode)
        {
            Product p =  ControllerManager.Product.GetById(productCode);
            return new StockProduct(p.Id, p.Description);
        }

        public override bool UpdateStock(string productCode, int quantity)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IList<IStockProductTransaction> GetWeekHistoricTransaction(int week, int year)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IList<IStockProductTransaction> GetMonthHistoricTransaction(int month, int year)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GeneratePurchaseOrder(string productCode, int quantity, DateTime creationDate, DateTime orderDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IList<IStockProvider> GetProviders()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IList<IStockProduct> GetProducts()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IStockProvider GetProvider(string providerCode)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
