using System;
using System.Collections.Generic;
using System.Text;
using PartnerNet.Common;

namespace PartnerNet.Business
{
    public class ControllerManager
    {
        public static ProductStatisticWeeklyController ProductStatisticWeekly
        {
            get { return new ProductStatisticWeeklyController(Config.GrundfosFactoryConfigPath);}
        }
        public static ProductStatisticMonthlyController ProductStatisticMonthly
        {
            get { return new ProductStatisticMonthlyController(Config.GrundfosFactoryConfigPath); }
        }
        public static ForecastController Forecast
        {
            get { return new ForecastController(Config.GrundfosFactoryConfigPath);}
        }

        public static LogController Log
        {
            get { return new LogController(Config.GrundfosFactoryConfigPath); }
        }

        public static ProductController Product
        {
            get { return new ProductController(Config.GrundfosFactoryConfigPath);}
        }
        public static PurchaseOrderController PurchaseOrder
        {
            get { return new PurchaseOrderController(Config.GrundfosFactoryConfigPath);}
        }
        public static PurchaseOrderItemController PurchaseOrderItem
        {
            get { return new PurchaseOrderItemController(Config.GrundfosFactoryConfigPath); }
        }
        public static TransactionHistoryMonthlyController TransactionHistoryMonthly
        {
            get { return new TransactionHistoryMonthlyController(Config.GrundfosFactoryConfigPath);}
        }
        public static TransactionHistoryWeeklyController TransactionHistoryWeekly
        {
            get { return new TransactionHistoryWeeklyController(Config.GrundfosFactoryConfigPath);}
        }
        public static ProductSetController ProductSet
        {
            get { return new ProductSetController(Config.GrundfosFactoryConfigPath); }
        }
        public static ProductInformationController ProductInformation
        {
            get { return new ProductInformationController(Config.GrundfosFactoryConfigPath); }
        }
        public static ProviderController Provider
        {
            get { return new ProviderController(Config.GrundfosFactoryConfigPath); }
        }
        public static CountryController Country
        {
            get { return new CountryController(Config.GrundfosFactoryConfigPath); }
        }
        public static PurchaseOrderInformationController PurchaseOrderInformation
        {
            get { return new PurchaseOrderInformationController(Config.GrundfosFactoryConfigPath); }
        }
        public static PurchaseOrderItemInformationController PurchaseOrderItemInformation
        {
            get { return new PurchaseOrderItemInformationController(Config.GrundfosFactoryConfigPath); }
        }
        public static BreakDownController BreakDown
        {
            get { return new BreakDownController(Config.GrundfosFactoryConfigPath); }
        }
        public static PurchaseOrderViewScalaController PurchaseOrderViewScala
        {
            get { return new PurchaseOrderViewScalaController(Config.GrundfosFactoryConfigPath); }
        }
        public static AlertPurchaseOrderController AlertPurchaseOrder
        {
            get { return new AlertPurchaseOrderController(Config.GrundfosFactoryConfigPath); }
        }
        public static AlertProductController AlertProduct
        {
            get { return new AlertProductController(Config.GrundfosFactoryConfigPath); }
        }
        public static AlertSaleOrderController AlertSaleOrder
        {
            get { return new AlertSaleOrderController(Config.GrundfosFactoryConfigPath); }
        }
        public static AlertTotalController AlertTotal
        {
            get { return new AlertTotalController(Config.GrundfosFactoryConfigPath); }
        }

        public static AlertRepositionController AlertReposition
        {
            get { return new AlertRepositionController(Config.GrundfosFactoryConfigPath); }
        }

        public static ProductRepositionLevelHistoryController ProductRepositionLevelHistory
        {
            get { return new ProductRepositionLevelHistoryController(Config.GrundfosFactoryConfigPath); }
        }

    }
}
