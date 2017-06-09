using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Common;
using ProjectBase.Data;

namespace Grundfos.ScalaConnector.Controllers
{
    public class SaleOrderItemController : AbstractNHibernateDao<SaleOrderItem, string>
    {
        public SaleOrderItemController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<SaleOrderItem> GetMonthlyTransaction(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            ICriteria crit = GetCriteria();

            ICriteria sale = crit.CreateCriteria("SaleOrder");

            sale.Add(new LtExpression("Date", endDate));
            sale.Add(new GeExpression("Date", startDate));
            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("Quantity"))
                                   .Add(Projections.GroupProperty("Product"))
                );

            crit.SetResultTransformer(
                    new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(SaleOrderItem).GetConstructors()[1])
                );

            //IList obj = new ArrayList();
            //crit.List(obj);

            return crit.List<SaleOrderItem>();
        }
        
        public List<SaleOrderItem> PendingSaleOrderQuantity(DateTime startdate)
        {
            ICriteria crit = GetCriteria();
            
            crit.Add(new GtPropertyExpression("Quantity", "QuantityDelivery"));
            crit.CreateCriteria("SaleOrder").Add(new GeExpression("Date", startdate));

            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("Quantity"))
                                   .Add(Projections.Avg("QuantityDelivery"))
                                   .Add(Projections.GroupProperty("Product"))
                );

            crit.SetResultTransformer(
                new AliasToBeanConstructorResultTransformer(typeof(SaleOrderItem).GetConstructors()[2]));

            return crit.List<SaleOrderItem>() as List<SaleOrderItem>;
        }

        public List<SaleOrderItem> PendingSaleOrder(DateTime startdate)
        {
            ICriteria crit = GetCriteria();

            crit.Add(new GtPropertyExpression("Quantity", "QuantityDelivery"));
            crit.CreateCriteria("SaleOrder").Add(new GeExpression("Date", startdate));

            return crit.List<SaleOrderItem>() as List<SaleOrderItem>;
        }

        public List<SaleOrderItem> SaleOrdersByProduct(string productCode)
        {
            ICriteria crit = GetCriteria();
            ICriteria product = crit.CreateCriteria("Product");

            product.Add(Expression.Eq("ProductCode", productCode));
            return crit.List<SaleOrderItem>() as List<SaleOrderItem>;
        }

        public List<SaleOrderItem> SaleOrdersByProduct(List<string> products)
        {
            ICriteria crit = GetCriteria();
            ICriteria product = crit.CreateCriteria("Product");
            product.Add(Expression.In("Id", products));
            return crit.List<SaleOrderItem>() as List<SaleOrderItem>;
        }

        public List<SaleOrderItem> SaleOrdersByProduct()
        {
            ICriteria crit = GetCriteria();
            crit.SetFetchMode("Product", FetchMode.Join);
            return crit.List<SaleOrderItem>() as List<SaleOrderItem>;
        }
    }
}
