using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Common;
using ProjectBase.Data;

namespace Grundfos.ScalaConnector.Controllers
{
    public class SaleOrderController : AbstractNHibernateDao<SaleOrder, string>
    {
        public SaleOrderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<SaleOrder> FilterByMonth(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            ICriteria crit = GetCriteria();
            crit.Add(new LtExpression("Date", endDate));
            crit.Add(new GeExpression("Date", startDate));

            return crit.List<SaleOrder>();
        }

        public IList<SaleOrder> FilterByWeek(int year, DateTime endDate, DateTime startDate)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new LeExpression("Date", endDate));
            crit.Add(new GtExpression("Date", startDate));

            return crit.List<SaleOrder>();
        }
    }
}
