using System;
using ProjectBase.Data;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;

namespace Grundfos.ScalaConnector.Controllers
{
    public class PurchaseOrderController : AbstractNHibernateDao<PurchaseOrder, string>
    {
        public PurchaseOrderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<PurchaseOrder> FilterByMonth(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            ICriteria crit = GetCriteria();
            crit.Add(new LtExpression("Date", endDate));
            crit.Add(new GeExpression("Date", startDate));

            return crit.List<PurchaseOrder>();
        }
                
        public IList<PurchaseOrder> FilterByWeek(DateTime startDate, DateTime endDate)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new LeExpression("Date", endDate));
            crit.Add(new GtExpression("Date", startDate));

            return crit.List<PurchaseOrder>();
        }
    }
}
