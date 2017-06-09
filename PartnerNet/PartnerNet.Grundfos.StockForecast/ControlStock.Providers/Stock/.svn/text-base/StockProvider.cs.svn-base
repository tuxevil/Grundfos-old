using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace PartnerNet.Providers.Stock
{
    #region Stock Provider Abstract Class

    /// <summary>
    /// This class will allow direct communication with Stock databases.
    /// </summary>
    public abstract class StockProvider : ProviderBase
    {
        #region Properties
        public abstract string ApplicationName { get; }
        #endregion

        #region Functional Methods
        public abstract bool UpdateStock(string productCode, int quantity);
        public abstract IList<IStockProductTransaction> GetWeekHistoricTransaction(int week, int year);
        public abstract IList<IStockProductTransaction> GetMonthHistoricTransaction(int month, int year);
        public abstract string GeneratePurchaseOrder(string productCode, int quantity, DateTime creationDate, DateTime orderDate);
        public abstract IStockProduct GetProduct(string productCode);
        public abstract IStockProvider GetProvider(string providerCode);
        public abstract IList<IStockProvider> GetProviders();
        public abstract IList<IStockProduct> GetProducts();
        //public abstract IList<IProductForecast> CalculateForecast(int weekNumber);
        //public abstract IList<IProductForecast> CalculateForecast(int weekNumber, string productCode);
        //public abstract bool UpdateStatistics();
        //public abstract bool UpdateStatistics(string productCode);
        #endregion
    }

    #endregion

    #region Stock Provider Collection

    /// <summary>
    /// This class will allow to have a collection of <see cref="StockProvider<T>">Stock Provider</see> available.
    /// </summary>
    public class StockProviderCollection : ProviderCollection
    {
        public new StockProvider this[string name]
        {
            get { return (StockProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("No provider specified");

            if (!(provider is StockProvider))
                throw new ArgumentException
                    ("Invalid provider type", "StockProvider");

            base.Add(provider);
        }
    }

    #endregion
}
