using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace PartnerNet.Providers.Stock
{
    public class StockManager
    {
        //Initialization related variables and logic
        private static bool isInitialized = false;
        private static Exception initializationException;

        private static object initializationLock = new object();

        static StockManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            try
            {
                //Get the feature's configuration info
                StockProviderConfiguration qc =
                    (StockProviderConfiguration)ConfigurationManager.GetSection("stockProvider");

                if (qc.DefaultProvider == null || qc.Providers == null || qc.Providers.Count < 1)
                    throw new ProviderException("You must specify a valid default provider.");

                //Instantiate the providers
                providerCollection = new StockProviderCollection();
                ProvidersHelper.InstantiateProviders(qc.Providers, providerCollection, typeof(StockProvider));
                providerCollection.SetReadOnly();
                defaultProvider = providerCollection[qc.DefaultProvider];
                if (defaultProvider == null)
                {
                    throw new ConfigurationErrorsException(
                        "You must specify a default provider for the feature.",
                        qc.ElementInformation.Properties["defaultProvider"].Source,
                        qc.ElementInformation.Properties["defaultProvider"].LineNumber);
                }
            }
            catch (Exception ex)
            {
                initializationException = ex;
                isInitialized = true;
                throw ex;
            }

            isInitialized = true; //error-free initialization
        }

        //Public feature API
        private static StockProvider defaultProvider;
        private static StockProviderCollection providerCollection;

        public static StockProvider Provider
        {
            get
            {
                return defaultProvider;
            }
        }

        public static StockProviderCollection Providers
        {
            get
            {
                return providerCollection;
            }
        }

        public static IList<IStockProductTransaction> GetWeekHistoricTransaction(int week, int year)
        {
            return Provider.GetWeekHistoricTransaction(week, year);
        }

        public static IList<IStockProductTransaction> GetMonthHistoricTransaction(int month, int year)
        {
            return Provider.GetMonthHistoricTransaction(month, year);
        }

        public static IStockProduct GetProduct(string productCode)
        {
            return Provider.GetProduct(productCode);
        }

        public static IStockProvider GetProvider(string providerCode)
        {
            return Provider.GetProvider(providerCode);
        }

        // TODO: Complete with all the provider methods to simplify access;
    }
}
