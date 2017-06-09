using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate.Mapping;
using PartnerNet.Common;
using PartnerNet.Domain;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Expression;


namespace PartnerNet.Business
{
    public class ProductController : AbstractNHibernateDao<Product, int>
    {
        public ProductController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Product> GetProductList()
        {
            ICriteria crit = GetCriteria();
            return crit.List<Product>();
        }

        public IList<Product> GetProductList(int cant)
        {
            ICriteria crit = GetCriteria();
            crit.SetMaxResults(cant);
            return crit.List<Product>();
        }

        public List<Product> GetFullProductList()
        {
            ICriteria crit = GetCriteria();
            return crit.List<Product>() as List<Product>;
        }

        public List<Product> GetProductListAlt()
        {
            ICriteria crit = GetCriteria();
            return crit.List<Product>() as List<Product>;
        }

        public Product GetProductInfo(string ProductCode)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new LikeExpression("ProductCode", ProductCode, MatchMode.Exact));
            return crit.UniqueResult<Product>();
        }

        public IList<string> GetGroups()
        {
            ICriteria crit = GetCriteria();
            crit.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.GroupProperty("Group"))
                );
            crit.AddOrder(new Order("Group", true));
            
            IList lst = crit.List();

            return crit.List<string>();
        }

        public IList<Product> GetProductList(string nameCode, string group, int selection)
        {
            ProductSet prodset = ControllerManager.ProductSet.GetProductSet(selection);
            ICriteria crit = GetCriteria();

            if (nameCode != null)
                crit.Add(new OrExpression(new LikeExpression("ProductCode", nameCode, MatchMode.Anywhere), new LikeExpression("Description", nameCode, MatchMode.Anywhere)));
            if (group != null)
                crit.Add(new EqExpression("Group", group));
            if (selection > 0)
                crit.Add(new EqExpression("ProductSets", prodset));
            
            return crit.List<Product>();
        }

        public IList<ProductInformation> GetProductInformation(string nameCode, string group, int selection, int provider, int page, int pagesize, out int recordCount, bool exclude)
        {
            string querystring = "Count(P.Id)";
            IQuery q = GetInfo(nameCode, group, selection, provider, 0, 0, querystring, exclude);
            recordCount = Convert.ToInt32(q.UniqueResult());

            if (recordCount == 0)
                return new List<ProductInformation>();

            querystring = "P.Id, P.ProductCode, P.Description, PV.Name, VS.CurrentStock, VS.ReservedStock, VS.OrderedStock, P.RepositionLevel, P.RepositionPoint, PSW.Sale, P.LeadTime, P.Safety";
            q = GetInfo(nameCode, group, selection, provider, page, pagesize, querystring, exclude);
            q.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(ProductInformation).GetConstructors()[1]));
            return q.List<ProductInformation>();
        }

        public IQuery GetInfo(string nameCode, string group, int selection, int provider, int page, int pagesize, string querystring, bool exclude)
        {
            string query = "select " + querystring + " from Product P";
            query += " join P.ProductStatisticsWeeklys PSW";
            query += " join P.ViewScala VS";
            if (selection > 0)
                query += " join P.ProductSets PS";
            //if (provider > 0)
                query += " join P.Provider PV";
            query += " where PSW.Period = :Period";
            query += " and AlternativeDate >= :Date";
            //query += " where AlternativeDate >= :Date";
            if (nameCode != "")
                query += " AND (P.ProductCode LIKE :namecode OR P.Description LIKE :namecode)";
            if (group != "N/A")
                query += " AND P.Group = :Group";
            if (selection > 0)
                query += " AND PS.Id = :Selection";
            if (provider > 0)
                query += " AND PV.Id = :Provider";
            if (exclude)
                query += " AND PSW.Sale > 0";
            //query += " GROUP BY P.Id, P.ProductCode, P.Description, PV.Name, VS.CurrentStock, VS.ReservedStock, VS.OrderedStock, P.RepositionLevel, P.RepositionPoint, P.LeadTime, P.Safety";
            if (page != 0 && pagesize != 0)
                query += " order by P.ProductCode";

            IQuery q = NHibernateSession.CreateQuery(query);

            q.SetEnum("Period", Period.Yearly);
            q.SetDateTime("Date", Config.CurrentDate);
            //q.SetEnum("Period", Period.Bimonthly);
            if (nameCode != "")
                q.SetString("namecode", "%" + nameCode + "%");
            if (group != "N/A")
                q.SetString("Group", group);
            if (selection > 0)
                q.SetInt32("Selection", selection);
            if (provider > 0)
                q.SetInt32("Provider", provider);

            if (pagesize != 0)
            {
                q.SetMaxResults(pagesize);
                if (page == 1)
                    q.SetFirstResult(0);
                else
                    q.SetFirstResult((page - 1)*pagesize);
            }
            
            return q;
        }
        
        public void AddProductToProductSet(IList<Product> productlist, IList<ProductSet> productsetlist)
        {
            foreach(Product prod in productlist)
            {
                prod.ProductSets.Clear();

                foreach (ProductSet productset in productsetlist)
                    prod.ProductSets.Add(productset);

                this.SaveOrUpdate(prod);    
            }
            
        }

        public void UpdateProductSafety(IList<Product> productlist, int safety)
        {
            foreach (Product prod in productlist)
            {
                prod.Safety = safety;

                this.SaveOrUpdate(prod);
            }
        }

        public void UpdateProductLeadTime(IList<Product> productlist, int leadtime)
        {
            foreach (Product prod in productlist)
            {
                prod.LeadTime = leadtime;

                this.SaveOrUpdate(prod);
            }
        }

        public void UpdateProductRepositionPoint(IList<Product> productlist, int repPoint)
        {
            foreach (Product prod in productlist)
            {
                prod.RepositionPoint = repPoint;

                this.SaveOrUpdate(prod);
            }
        }

        public List<Product> GetAlertNegativeFutureStock()
        {
            ICriteria crit = GetCriteria();
            ICriteria forecast = crit.CreateCriteria("Forecasts");
            forecast.Add(new AndExpression(new AndExpression(new GeExpression(("Week"), Config.CurrentWeek), new GeExpression("Year", Config.CurrentDate.Year)), new LtPropertyExpression("FinalStock", "Safety")));
            crit.AddOrder(new Order("Id", true));
            forecast.AddOrder(new Order("Year", true));
            forecast.AddOrder(new Order("Week", true));
            return crit.List<Product>() as List<Product>;
        }

        public List<string> GetLeftProductList()
        {
            ICriteria crit = GetCriteria();
            ICriteria forecast = crit.CreateCriteria("Forecasts");
            forecast.SetProjection(Projections.ProjectionList()
                                   .Add(Projections.GroupProperty("ProductCode"))
                );

            return crit.List<string>() as List<string>;
        }

        //public List<ProductForExport> GetProductForExport(string nameCode, string group, int selection, int provider, bool exclude)
        //{
        //    string query = "select P.ProductCode, P.Description, PSW.Sale from Product P";
        //    query += " join P.ProductStatisticsWeeklys PSW";
        //    if (selection > 0)
        //        query += " join P.ProductSets PS";
        //    if (provider > 0)
        //        query += " join P.Provider PV";
        //    query += " where PSW.Period = :Period";
        //    query += " and AlternativeDate >= :Date";
        //    if (nameCode != "")
        //        query += " AND (P.ProductCode LIKE :namecode OR P.Description LIKE :namecode)";
        //    if (group != "N/A")
        //        query += " AND P.Group = :Group";
        //    if (selection > 0)
        //        query += " AND PS.Id = :Selection";
        //    if (provider > 0)
        //        query += " AND PV.Id = :Provider";
        //    if (exclude)
        //        query += " AND PSW.Sale > 0";
        //    query += " order by P.ProductCode";

        //    IQuery q = NHibernateSession.CreateQuery(query);

        //    q.SetEnum("Period", Period.Yearly);
        //    q.SetDateTime("Date", Config.CurrentDate);
            
        //    if (nameCode != "")
        //        q.SetString("namecode", "%" + nameCode + "%");
        //    if (group != "N/A")
        //        q.SetString("Group", group);
        //    if (selection > 0)
        //        q.SetInt32("Selection", selection);
        //    if (provider > 0)
        //        q.SetInt32("Provider", provider);

        //    q.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(ProductForExport).GetConstructors()[1]));

        //    return q.List<ProductForExport>() as List<ProductForExport>;
        //}

        public List<ProductForExport> GetProductForExport(string nameCode, string group, int selection, int provider, bool exclude, int week, int year)
        {
            int actualweek;
            int initialweek3;
            int initialweek53;
            int actualyear;
            int pastyear=0;
            
            int interval = 13;
            int weekDif = (week - interval);

            string sql = "select P.ProductCode as Code, ";
            sql += " P.Description as Description, ";
            sql += " THW3.Trimestral as VentaTrimestral,";
            sql += " THW12.Anual as VentaAnual,";
            sql += " P.RepositionPoint as PuntoRep,";
            sql += " P.RepositionLevel as ModCompra";
            sql += " from Product P ";
            sql += " inner join (select ProductID as ID, sum(Sale) as Trimestral ";
            sql += " from TransactionHistoryWeekly THW";

            if (weekDif <= 0)
            {
                sql += " where (Year = :ActualYear and Week between 1 and :ActualWeek) or (Year = :PastYear and Week between :InitialWeek3 and 53)";
                actualweek = week;
                initialweek3 = 53 + weekDif;
                actualyear = year;
                pastyear = year - 1;
            }
            else
            {
                sql += " where Year = :ActualYear and Week between :InitialWeek3 and :ActualWeek";
                actualweek = week;
                initialweek3 = week - interval;
                actualyear = year;
            }
            
            sql += " group by ProductID";
            sql += " ) THW3";
            sql += " on THW3.ID = P.Id";
            sql += " inner join (select ProductID as ID, sum(Sale) as Anual";
            sql += " from TransactionHistoryWeekly THW";

            interval = 53;
            weekDif = (week - interval);

            if (weekDif <= 0)
            {
                sql += " where (Year = :ActualYear and Week between 1 and :ActualWeek) or (Year = :PastYear and Week between :InitialWeek53 and 53)";
                actualweek = week;
                initialweek53 = 53 + weekDif;
                actualyear = year;
                pastyear = year - 1;
            }
            else
            {
                sql += " where Year = :ActualYear and Week between :InitialWeek53 and :ActualWeek";
                actualweek = week;
                initialweek53 = week - interval;
                actualyear = year;
            }
            
            sql += " group by ProductID";
            sql += " ) THW12";
            sql += " on THW12.ID = P.Id";
            sql += " inner join ProductStatisticWeekly PSW ";
            sql += " on P.Id=PSW.ProductID ";
            if (selection > 0)
            {
                sql += " inner join ProductSetRelation PSR";
                sql += " on P.Id=PSR.ProductID ";
                sql += " inner join ProductSet PS ";
                sql += " on PSR.ProductSetID=PS.Id ";
            }
            if (provider > 0)
            {
                sql += " inner join Provider PV";
                sql += " on P.ProviderID=PV.Id ";
            }

            sql += " where PSW.Period = :Period";
            sql += " and AlternativeDate >= :Date";
            if (nameCode != "")
                sql += " AND (P.ProductCode LIKE :namecode OR P.Description LIKE :namecode)";
            if (group != "N/A")
                sql += " AND P.Group = :Group";
            if (selection > 0)
                sql += " AND PS.Id = :Selection";
            if (provider > 0)
                sql += " AND PV.Id = :Provider";
            if (exclude)
                sql += " AND PSW.Sale > 0";
            sql += " order by P.ProductCode";

            IQuery iq = NHibernateSession.CreateSQLQuery(sql).AddScalar("Code", NHibernateUtil.String).AddScalar("Description", NHibernateUtil.String).AddScalar("VentaTrimestral", NHibernateUtil.Int32).AddScalar("VentaAnual", NHibernateUtil.Int32).AddScalar("PuntoRep", NHibernateUtil.Int32).AddScalar("ModCompra", NHibernateUtil.Int32);

            iq.SetEnum("Period", Period.Yearly);
            iq.SetDateTime("Date", Config.CurrentDate);
            iq.SetInt32("ActualWeek", actualweek);
            iq.SetInt32("InitialWeek3", initialweek3);
            iq.SetInt32("InitialWeek53", initialweek53);
            iq.SetInt32("ActualYear", actualyear);
            iq.SetInt32("PastYear", pastyear);

            if (nameCode != "")
                iq.SetString("namecode", "%" + nameCode + "%");
            if (group != "N/A")
                iq.SetString("Group", group);
            if (selection > 0)
                iq.SetInt32("Selection", selection);
            if (provider > 0)
                iq.SetInt32("Provider", provider);

            iq.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(ProductForExport).GetConstructors()[1]));

            return iq.List<ProductForExport>() as List<ProductForExport>;
        }

        public Product GetAlternativeTo(Product product)
        {
            if(!string.IsNullOrEmpty(product.AlternativeProduct))
            {
                ICriteria crit = GetCriteria();
                crit.Add(Expression.Eq("ProductCode", product.AlternativeProduct));
                return crit.UniqueResult<Product>();
            }
            return null;
        }

        public Product GetAlternativeFrom(Product product)
        {
            if (!string.IsNullOrEmpty(product.AlternativeProduct))
            {
                ICriteria crit = GetCriteria();
                crit.Add(Expression.Eq("AlternativeProduct", product.ProductCode));
                return crit.UniqueResult<Product>();
            }
            return null;
        }
       
        public object GenericSave(object o)
        {
            return NHibernateSession.Save(o);
        }
    }
}