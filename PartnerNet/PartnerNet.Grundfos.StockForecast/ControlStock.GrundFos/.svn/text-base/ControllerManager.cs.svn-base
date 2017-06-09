using System;
using System.Collections.Generic;
using System.Text;
using Grundfos.ScalaConnector.Controllers;
using PartnerNet.Common;

namespace Grundfos.ScalaConnector
{
    public class ControllerManager
    {
        public static ProductController Product
        {
            get { return new ProductController(Config.ScalaFactoryConfigPath); }
        }
        public static ProductDetailController ProductDetail
        {
            get { return new ProductDetailController(Config.ScalaFactoryConfigPath); }
        }
        public static ProviderController Provider
        {
            get { return new ProviderController(Config.ScalaFactoryConfigPath); }
        }
        public static PurchaseOrderController PurchaseOrder
        {
            get { return new PurchaseOrderController(Config.ScalaFactoryConfigPath); }
        }
        public static PurchaseOrderItemController PurchaseOrderItem
        {
            get { return new PurchaseOrderItemController(Config.ScalaFactoryConfigPath); }
        }
        public static SaleOrderController SaleOrder
        {
            get { return new SaleOrderController(Config.ScalaFactoryConfigPath); }
        }
        public static SaleOrderItemController SaleOrderItem
        {
            get { return new SaleOrderItemController(Config.ScalaFactoryConfigPath); }
        }
        public static TransactionsController Transactions
        {
            get { return new TransactionsController(Config.ScalaFactoryConfigPath); }
        }
    }
}
