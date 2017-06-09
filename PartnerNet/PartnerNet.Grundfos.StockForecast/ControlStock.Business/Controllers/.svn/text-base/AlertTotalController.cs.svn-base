using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class AlertTotalController : AbstractNHibernateDao<AlertTotal, int>
    {
        public AlertTotalController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public int GetTotalForAlert(int alert)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Alert", alert));
            return crit.UniqueResult<AlertTotal>().Total;
        }

        public AlertTotal GetAlertTotal(int alert)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Alert", alert));
            return crit.UniqueResult<AlertTotal>();
        }
    }
}
