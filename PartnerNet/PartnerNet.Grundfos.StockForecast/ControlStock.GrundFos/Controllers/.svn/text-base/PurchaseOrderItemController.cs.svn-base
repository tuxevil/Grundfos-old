using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Common;
using ProjectBase.Data;

namespace Grundfos.ScalaConnector.Controllers
{
    public class PurchaseOrderItemController : AbstractNHibernateDao<PurchaseOrderItem, string>
    {
        public PurchaseOrderItemController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<PurchaseOrderItem> GetMonthlyTransaction(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            ICriteria crit = GetCriteria();

            ICriteria purchase = crit.CreateCriteria("PurchaseOrder");

            purchase.Add(new LtExpression("Date", endDate));
            purchase.Add(new GeExpression("Date", startDate));
            purchase.Add(new OrExpression(new EqExpression("Location", "01"), new EqExpression("Location", "09")));
            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("QuantityOrdered"))
                                   .Add(Projections.GroupProperty("Product"))
                );

            crit.SetResultTransformer(
                    new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(PurchaseOrderItem).GetConstructors()[1])
                );

            //IList obj = new ArrayList();
            //crit.List(obj);

            return crit.List<PurchaseOrderItem>();
        }

        public List<PurchaseOrderItem> GetWeeklyTransaction(DateTime startDate, DateTime endDate)
        {
            ICriteria crit = GetCriteria();

            ICriteria purchase = crit.CreateCriteria("PurchaseOrder");

            purchase.Add(new LeExpression("Date", endDate));
            purchase.Add(new GeExpression("Date", startDate));
            purchase.Add(new OrExpression(new EqExpression("Location", "01"), new EqExpression("Location", "09")));
            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("QuantityOrdered"))
                                   .Add(Projections.GroupProperty("Product"))
                );
            
            crit.SetResultTransformer(
                    new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(PurchaseOrderItem).GetConstructors()[1])
                );

            return crit.List<PurchaseOrderItem>() as List<PurchaseOrderItem>;
        }

        public List<PurchaseOrderItem> GetArrivalPO(DateTime startDate, DateTime endDate)
        {
            ICriteria crit = GetCriteria();

            crit.Add(new LeExpression("ArrivalDate", endDate));
            crit.Add(new GeExpression("ArrivalDate", startDate));

            ICriteria purchase = crit.CreateCriteria("PurchaseOrder");
            
            purchase.Add(new OrExpression(new EqExpression("Location", "01"), new EqExpression("Location", "09")));
            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("QuantityOrdered"))
                                   .Add(Projections.GroupProperty("Product"))
                );

            crit.SetResultTransformer(
                    new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(PurchaseOrderItem).GetConstructors()[1])
                );

            return crit.List<PurchaseOrderItem>() as List<PurchaseOrderItem>;
        }

        public List<PurchaseOrderItem> GetFullArrivalPO(DateTime startDate)
        {
            ICriteria crit = GetCriteria();

            crit.Add(new GtExpression("ArrivalDate", startDate));

            ICriteria purchase = crit.CreateCriteria("PurchaseOrder");

            purchase.Add(new OrExpression(new EqExpression("Location", "01"), new EqExpression("Location", "09")));
            
            return crit.List<PurchaseOrderItem>() as List<PurchaseOrderItem>;
        }

        public PurchaseOrderItem GetPurchaseOrderInfo(Product product, PurchaseOrder po)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Product", product));
            crit.Add(new EqExpression("PurchaseOrder", po));
        	ICriteria critPurchaseOrder = crit.CreateCriteria("PurchaseOrder");
			//critPurchaseOrder.Add( new LtExpression( "Date" , endDate ) );
			//critPurchaseOrder.Add( new GeExpression( "Date" , startDate ) );


            return crit.UniqueResult<PurchaseOrderItem>();
        }

        public List<PurchaseOrderItem> GetAlerts()
        {
            ICriteria crit = GetCriteria();
            return crit.List<PurchaseOrderItem>() as List<PurchaseOrderItem>;
        }

    }
}
