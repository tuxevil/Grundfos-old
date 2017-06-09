using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class TransactionHistoryMonthlyController : AbstractNHibernateDao<TransactionHistoryMonthly, int>
    {
        public TransactionHistoryMonthlyController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public List<TransactionHistoryMonthly> GetInfo(int productID)
        {
            ICriteria crit = GetCriteria();

            crit.Add(Expression.Eq("ProductID", new Product(productID)));

            crit.AddOrder(new Order("Year", false));

            crit.AddOrder(new Order("Month", false));

            crit.SetMaxResults(12);

            return crit.List<TransactionHistoryMonthly>() as List<TransactionHistoryMonthly>;
        }
        
        public void CalculateFullStatistic(int month, int year)
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_calculo_promedios_mensuales");
            q.SetParameter("year", year);
            q.SetParameter("month", month);
            q.UniqueResult();

            NHibernateSession.Flush();
        }

        public void CalculateFullStatistic(int month, int year, Period period)
        {
            IList<Product> lst = new ProductController(this.SessionFactoryConfigPath).GetProductList();
            List<ProductStatisticMonthly> lstStats = CalculateStatistic(month, year, period);

            foreach (Product p in lst)
            {
                ProductStatisticMonthly stat = lstStats.Find(delegate(ProductStatisticMonthly record)
                                              {
                                                  if (record.Product != p)
                                                      return false;
                                                  else
                                                      return true;
                                              });

                if (stat == null)
                {
                    stat = new ProductStatisticMonthly();
                    stat.Product = p;
                    stat.Sale = 0;
                    stat.Purchase = 0;
                    stat.Period = period;
                }
                else
                    stat.Period = period;

                NHibernateSession.Save(stat);
            }

            NHibernateSession.Flush();
        }

        public List<ProductStatisticMonthly> CalculateStatistic(int month, int year, Period period)
        {
            int interval = 0;
            if (Convert.ToInt32(period) == 1)
                interval = 4;
            else if (Convert.ToInt32(period) == 2)
                interval = 8;
            else if (Convert.ToInt32(period) == 3)
                interval = 13;
            else if (Convert.ToInt32(period) == 4)
                interval = 26;
            else if (Convert.ToInt32(period) == 5)
                interval = 53;

            ICriteria crit = GetCriteria();

            int weekDif = (month - interval);

            if (weekDif <= 0)
            {
                Disjunction dis = new Disjunction();
                dis.Add(
                    new AndExpression(new BetweenExpression("Month", 53 + weekDif, 53),
                                      new EqExpression("Year", year - 1)));
                dis.Add(new AndExpression(new BetweenExpression("Week", 1, month), new EqExpression("Year", year)));
                crit.Add(dis);
            }
            else
                crit.Add(
                    new AndExpression(new BetweenExpression("Week", month - interval, month),
                                      new EqExpression("Year", year)));

            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Avg("Purchase"))
                                   .Add(Projections.Avg("Sale"))
                                   .Add(Projections.GroupProperty("ProductID"))
                );

            crit.SetResultTransformer(
                new AliasToBeanConstructorResultTransformer(typeof(ProductStatisticMonthly).GetConstructors()[1]));

            return crit.List<ProductStatisticMonthly>() as List<ProductStatisticMonthly>;
        }

        public void CleanData(int month, int year)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new AndExpression(new EqExpression("Month", month), new EqExpression("Year", year)));

            IList<TransactionHistoryMonthly> templist = crit.List<TransactionHistoryMonthly>();

            foreach (TransactionHistoryMonthly transactionHistoryMonthly in templist)
            {
                Delete(transactionHistoryMonthly);
            }
        }
    }
}
