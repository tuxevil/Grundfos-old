using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Common;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class TransactionHistoryWeeklyController : AbstractNHibernateDao<TransactionHistoryWeekly, int>
    {
        public TransactionHistoryWeeklyController(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        {
        }


        public IList<TransactionHistoryWeekly> GetInfo(int productID, int week, int year, int period)
        {
            ICriteria crit = GetCriteria();

            crit.Add(Expression.Eq("ProductID", new Product(productID)));


            int weekDif = (week - period);

            if (weekDif <= 0)
            {
                Disjunction dis = new Disjunction();

                dis.Add(
                    new AndExpression(new BetweenExpression("Week", 53 + weekDif, 53),
                                      new EqExpression("Year", year - 1)));

                dis.Add(new AndExpression(new BetweenExpression("Week", 1, week), new EqExpression("Year", year)));

                crit.Add(dis);
            }

            else

                crit.Add(
                    new AndExpression(new BetweenExpression("Week", week - period, week), new EqExpression("Year", year)));


            crit.AddOrder(new Order("Year", false));

            crit.AddOrder(new Order("Week", false));


            return crit.List<TransactionHistoryWeekly>();
        }

        private IList<TransactionHistoryWeekly> GetByProduct(Product product)
        { 
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("ProductID", product));
            return crit.List<TransactionHistoryWeekly>();
        }

        public List<TransactionHistoryWeekly> GetInfo(int week, int year, int period)
        {
            ICriteria crit = GetCriteria();

            int weekDif = (week - period);
            if (weekDif <= 0)
            {
                Disjunction dis = new Disjunction();
                dis.Add(new AndExpression(new BetweenExpression("Week", 53 + weekDif, 53), new EqExpression("Year", year - 1)));
                dis.Add(new AndExpression(new BetweenExpression("Week", 1, week), new EqExpression("Year", year)));
                crit.Add(dis);
            }
            else
                crit.Add(new AndExpression(new BetweenExpression("Week", week - period, week), new EqExpression("Year", year)));

            crit.AddOrder(new Order("Year", false));
            crit.AddOrder(new Order("Week", false));

            return crit.List<TransactionHistoryWeekly>() as List<TransactionHistoryWeekly>;
        }



        public TransactionHistoryWeekly GetIndividualInfo(int productID, int week, int year)
        {
            if (productID == 2008)

                return new TransactionHistoryWeekly(0, ControllerManager.Product.GetById(productID));

            ICriteria crit = GetCriteria();

            crit.Add(Expression.Eq("ProductID", new Product(productID)));

            crit.Add(new AndExpression(new EqExpression("Week", week), new EqExpression("Year", year)));

            crit.SetMaxResults(1);

            return crit.UniqueResult<TransactionHistoryWeekly>();
        }


        public IList<TransactionHistoryWeekly> GetSalesTotal(Product productID, int week, int year)
        {
            List<TransactionHistoryWeekly> info = new List<TransactionHistoryWeekly>();


            for (int interval = 4; interval <= 53; )
            {
                ICriteria crit = GetCriteria();

                crit.Add(Expression.Eq("ProductID", productID));


                int weekDif = (week - interval);

                if (weekDif <= 0)
                {
                    Disjunction dis = new Disjunction();

                    dis.Add(
                        new AndExpression(new BetweenExpression("Week", 53 + weekDif, 53),
                                          new EqExpression("Year", year - 1)));

                    dis.Add(new AndExpression(new BetweenExpression("Week", 1, week), new EqExpression("Year", year)));

                    crit.Add(dis);
                }

                else

                    crit.Add(
                        new AndExpression(new BetweenExpression("Week", week - interval, week),
                                          new EqExpression("Year", year)));


                crit.SetProjection(Projections.ProjectionList()
                                       .Add(Projections.Sum("Sale"))
                                       .Add(Projections.GroupProperty("ProductID"))
                    );


                crit.SetResultTransformer(
                    new AliasToBeanConstructorResultTransformer(typeof(TransactionHistoryWeekly).GetConstructors()[1]));


                TransactionHistoryWeekly res = crit.UniqueResult<TransactionHistoryWeekly>();

                if (res == null)
                {
                    res = new TransactionHistoryWeekly();

                    res.Sale = 0;
                }


                info.Add(res);


                switch (interval)
                {
                    case 4:

                        interval = 13;

                        break;

                    case 13:

                        interval = 26;

                        break;

                    case 26:

                        interval = 53;

                        break;

                    case 53:

                        interval = 60;

                        break;
                }
            }

            return info;
        }

        public IList<AlertReposition> GetAlert6(int week, int year)
        {
            return GetAlert6(week, year, "");
        }

        private IList<AlertReposition> GetAlert6(int week, int year, string where)
        {
            int interval = 53;

            int weekDif = (week - interval);
            
            if (weekDif <= 0)
                where += "((((THW.Week > " + (53 + weekDif) + " and THW.Week < 53) and (THW.Year = " + (year - 1) + ")) or ((THW.Week > 1 and THW.Week < " + week + ") and (THW.Year = " + year + ")))";

            else
                where += "(((THW.Week > " + (week - interval) + " and THW.Week < " + week + ") and (THW.Year = " + year + "))";

            where += " and P.CountryCode not in ('" + Config.ArgentineCountryCode1 + "', '" + Config.ArgentineCountryCode2 + "'))";

            string query = "select P.Id as Id, P.ProductCode as ProductCode, P.AlternativeProduct as AlternativeProduct, P.AlternativeDate as AlternativeDate,";
            query += " sum(THW.Sale) as Sales,";
            query += " count(THW.Id) as Months,";
            query += " CASE WHEN (";
            query += " CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END) > 0";
            query += " THEN (P.RepositionLevel * 100 / (CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END)) - 100";
            query += " ELSE 0 END as Result";
            query += " from TransactionHistoryWeekly THW ";
            query += " inner join Product P ";
            query += " on THW.ProductID=P.Id ";
            query += " where " + where;
            query += " group by  P.Id, P.RepositionLevel, P.ProductCode, P.AlternativeProduct, P.AlternativeDate";
            query += " having sum(THW.Sale) > 0 and P.RepositionLevel >= 0 and ((CASE WHEN (";
            query += " CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END) > 0";
            query += " THEN (P.RepositionLevel * 100 / (CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END)) - 100";
            query += " ELSE 0 END > 20) or (CASE WHEN (";
            query += " CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END) > 0";
            query += " THEN (P.RepositionLevel * 100 / (CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END)) - 100";
            query += " ELSE 0 END < -20)) and CASE WHEN (";
            query += " CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END) > 0";
            query += " THEN (P.RepositionLevel * 100 / (CASE WHEN (";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float) > 1 ";
            query += " THEN sum(THW.Sale) / ((";
            query += " CASE WHEN round(count(THW.Id)/CAST(4 as float), 0) > 12 THEN 12 ";
            query += " WHEN round(count(THW.Id)/CAST(4 as float), 0) = 0 THEN 1 ";
            query += " ELSE round(count(THW.Id)/CAST(4 as float), 0) END";
            query += " ) * 3 / CAST(12 as float))";
            query += " ELSE sum(THW.Sale) END)) - 100";
            query += " ELSE 0 END <> 0";

            IQuery q = NHibernateSession.CreateSQLQuery(query).AddScalar("Id", NHibernateUtil.Int32).AddScalar("ProductCode", NHibernateUtil.String).AddScalar("AlternativeProduct", NHibernateUtil.String).AddScalar("AlternativeDate", NHibernateUtil.DateTime).AddScalar("Sales", NHibernateUtil.Int32).AddScalar("Months", NHibernateUtil.Int32).AddScalar("Result", NHibernateUtil.Decimal);
            
            q.SetResultTransformer(new AliasToBeanConstructorResultTransformer(typeof(AlertReposition).GetConstructors()[1]));

            return q.List<AlertReposition>();
        }

        public IList<AlertReposition> GetAlert6(int week, int year, List<string> alternativeProducts)
        {
            string where = "P.ProductCode in ('";

            foreach (string alternativeProduct in alternativeProducts)
                where += alternativeProduct + "','";

            if (where.Length > 17)
                where = where.Substring(0, where.Length - 2);

            where += ") and ";

            return GetAlert6(week, year, where);
        }



        public List<TransactionHistoryWeekly> GetAnualSales(int week, int year)
        {
            ICriteria crit = GetCriteria();

            int weekDif = (week - 53);

            if (weekDif <= 0)
            {
                Disjunction dis = new Disjunction();

                dis.Add(
                    new AndExpression(new BetweenExpression("Week", 53 + weekDif, 53),
                                      new EqExpression("Year", year - 1)));

                dis.Add(new AndExpression(new BetweenExpression("Week", 1, week), new EqExpression("Year", year)));

                crit.Add(dis);
            }

            else

                crit.Add(
                    new AndExpression(new BetweenExpression("Week", weekDif, week),
                                      new EqExpression("Year", year)));


            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("Sale"))
                                   .Add(Projections.GroupProperty("ProductID"))
                );


            crit.SetResultTransformer(new AliasToBeanConstructorResultTransformer(typeof(TransactionHistoryWeekly).GetConstructors()[1]));

            return crit.List<TransactionHistoryWeekly>() as List<TransactionHistoryWeekly>;
        }

        public void CalculateFullStatistic(int week, int year)
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_calculo_promedios_semanales");
            q.SetParameter("year", year);
            q.SetParameter("week", week);
            q.UniqueResult();

            NHibernateSession.Flush();
        }

        public void CalculateFullStatistic(int week, int year, Period period)
        {
            IList<Product> lst = new ProductController(this.SessionFactoryConfigPath).GetProductList();
            List<ProductStatisticWeekly> lstStats = CalculateStatistic(week, year, period);

            foreach (Product p in lst)
            {
                ProductStatisticWeekly stat = lstStats.Find(delegate(ProductStatisticWeekly record)
                                              {
                                                  if (record.Product != p)
                                                      return false;
                                                  else
                                                      return true;
                                              });

                if (stat == null)
                {
                    stat = new ProductStatisticWeekly();
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

        public List<ProductStatisticWeekly> CalculateStatistic(int week, int year, Period period)
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

            int weekDif = (week - interval);

            if (weekDif <= 0)
            {
                Disjunction dis = new Disjunction();
                dis.Add(
                    new AndExpression(new BetweenExpression("Week", 53 + weekDif, 53),
                                      new EqExpression("Year", year - 1)));
                dis.Add(new AndExpression(new BetweenExpression("Week", 1, week), new EqExpression("Year", year)));
                crit.Add(dis);
            }
            else
                crit.Add(
                    new AndExpression(new BetweenExpression("Week", week - interval, week),
                                      new EqExpression("Year", year)));

            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Avg("Purchase"))
                                   .Add(Projections.Avg("Sale"))
                                   .Add(Projections.GroupProperty("ProductID"))
                );

            crit.SetResultTransformer(
                new AliasToBeanConstructorResultTransformer(typeof(ProductStatisticWeekly).GetConstructors()[1]));

            return crit.List<ProductStatisticWeekly>() as List<ProductStatisticWeekly>;
        }

        public void Copy()
        {
            NHibernateSession.Flush();
            IQuery q = NHibernateSession.GetNamedQuery("sp_trans_copy");
            q.UniqueResult();
        }

        public void CleanData(int week, int year)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new AndExpression(new EqExpression("Week", week), new EqExpression("Year", year)));

            IList<TransactionHistoryWeekly> templist = crit.List<TransactionHistoryWeekly>();

            foreach (TransactionHistoryWeekly transactionHistoryWeekly in templist)
            {
                Delete(transactionHistoryWeekly);    
            }
        }

        public int GetActiveProducts(int week, int year)
        {
            ICriteria crit = GetCriteria();

            int weekDif = (week - 53);

            if (weekDif <= 0)
            {
                Disjunction dis = new Disjunction();
                dis.Add(new AndExpression(new BetweenExpression("Week", 53 + weekDif, 53), new EqExpression("Year", year - 1)));
                dis.Add(new AndExpression(new BetweenExpression("Week", 1, week), new EqExpression("Year", year)));
                crit.Add(dis);
            }

            else
                crit.Add(new AndExpression(new BetweenExpression("Week", weekDif, week), new EqExpression("Year", year)));


            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum("Sale"))
                                   .Add(Projections.GroupProperty("ProductID"))
                );


            crit.SetResultTransformer(new AliasToBeanConstructorResultTransformer(typeof(TransactionHistoryWeekly).GetConstructors()[1]));

            List<TransactionHistoryWeekly> templist = crit.List<TransactionHistoryWeekly>() as List<TransactionHistoryWeekly>;

            List<TransactionHistoryWeekly> countprod = templist.FindAll(delegate(TransactionHistoryWeekly record)
                                              {
                                                  if (record.Sale > 0)
                                                      return true;
                                                  else
                                                      return false;
                                              });

            return countprod.Count;
        }

        public void UpdateProductSaleLife(IList<AlertReposition> aRLst)
        {
            foreach (AlertReposition ar in aRLst)
            {
                ar.ProductSaleLife = GetCountByProduct(ar.Product.Id);
                ControllerManager.AlertReposition.Save(ar);
            }
        }

        private Int64 GetCountByProduct(int productId)
        {

            string hql = "Select Count (THW.Id) FROM TransactionHistoryWeekly THW ";
                   hql += "JOIN THW.ProductID P Where P.Id = :productId";

            IQuery q = NHibernateSession.CreateQuery(hql);
            q.SetInt32("productId", productId);

            return q.UniqueResult<Int64>();
        }
    }
}