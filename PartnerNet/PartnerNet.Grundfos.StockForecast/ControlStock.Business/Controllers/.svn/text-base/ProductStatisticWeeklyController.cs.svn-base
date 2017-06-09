using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class ProductStatisticWeeklyController : AbstractNHibernateDao<ProductStatisticWeekly, int>
    {
        public ProductStatisticWeeklyController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void FilterByProduct(int week, int year)
        {
            throw new System.NotImplementedException();
        }

        public void FilterByPeriod()
        {
            throw new System.NotImplementedException();
        }
        public List<ProductStatisticWeekly> GetStatistics()
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Period", Period.Bimonthly));
            return crit.List<ProductStatisticWeekly>() as List<ProductStatisticWeekly>;
        }

        public ProductStatisticWeekly GetProductInfo(int productID)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Product", new Product(productID)));
            crit.Add(new EqExpression("Period", Period.Bimonthly));
            return crit.UniqueResult<ProductStatisticWeekly>();
        }
        public IList<ProductStatisticWeekly> GetProductFullInfo(int productID)
        {
            List<ProductStatisticWeekly> list = new List<ProductStatisticWeekly>();
            
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Product", new Product(productID)));

            IList lst = crit.List();

            if (lst.Count == 0)
            {
                list.Add(new ProductStatisticWeekly(0, 0, new Product(productID)));
                list.Add(new ProductStatisticWeekly(0, 0, new Product(productID)));
                list.Add(new ProductStatisticWeekly(0, 0, new Product(productID)));
                list.Add(new ProductStatisticWeekly(0, 0, new Product(productID)));
                list.Add(new ProductStatisticWeekly(0, 0, new Product(productID)));
            }
            else return crit.List<ProductStatisticWeekly>();

            return list;
        }

        public void CleanTable()
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_stats_clean");
            q.UniqueResult();
        }
    }
}
