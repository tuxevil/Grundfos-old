using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class BreakDownController : AbstractNHibernateDao<BreakDown, int>
    {
        public BreakDownController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<BreakDown> GetBreakDown(Product prod)
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_despiece");
            //q.NamedParameters.SetValue(prod.Id, 0);
            //q.NamedParameters.SetValue(1, 1);
            q.SetInt32("ProductID", prod.Id);
            q.SetInt32("Quantity", 1);

            q.SetResultTransformer(new AliasToBeanConstructorResultTransformer(typeof(BreakDown).GetConstructors()[1]));

            return q.List<BreakDown>();

            //ICriteria crit = GetCriteria();
            //crit.Add(new EqExpression("Product", prod));
            //return crit.List<BreakDown>();

        }
    }
}
