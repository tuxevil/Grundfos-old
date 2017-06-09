using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using ProjectBase.Data;

namespace Grundfos.ScalaConnector.Controllers
{
    public class TransactionsController : AbstractNHibernateDao<Transactions, string>
    {
        public TransactionsController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
        
        public IList<Transactions> GetTransaction(DateTime startDate, DateTime endDate)
        {
            ICriteria crit = GetCriteria();

            crit.Add(new LeExpression("Date", endDate));
            crit.Add(new GtExpression("Date", startDate));
            //crit.Add(new GtExpression("Quantity", 0));
            
            return crit.List<Transactions>();
        }
        public List<Transactions> GetTransaction(DateTime startDate, DateTime endDate, int tipo)
        {
            ICriteria crit = GetCriteria();

            if(tipo == 0)
            {
                
                crit.Add(new LeExpression("Date", endDate));
                crit.Add(new GtExpression("Date", startDate));
                crit.Add(new LikeExpression("OrderNumber", "00000", MatchMode.Start));
                crit.Add(new OrExpression(new EqExpression("Location", "01"), new EqExpression("Location", "09")));

                crit.SetProjection(Projections.ProjectionList()
                                       .Add(Projections.Sum("Quantity"))
                                       .Add(Projections.GroupProperty("Product")));
            }
            else if(tipo == 1)
            {

                crit.Add(new LeExpression("Date", endDate));
                crit.Add(new GtExpression("Date", startDate));
                crit.Add(new LikeExpression("OrderNumber", "0000", MatchMode.Start));
                crit.Add(new NotExpression(new LikeExpression("OrderNumber", "00000", MatchMode.Start)));
                crit.Add(new OrExpression(new EqExpression("Location", "01"), new EqExpression("Location", "09")));

                crit.SetProjection(Projections.ProjectionList()
                                       .Add(Projections.Sum("Quantity"))
                                       .Add(Projections.GroupProperty("Product")));
            }
            crit.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(Transactions).GetConstructors()[1]));
            return crit.List<Transactions>() as List<Transactions>;
        }
        public IList<Transactions> GetTransaction(DateTime startDate, DateTime endDate, Product prod)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new LeExpression("Date", endDate));
            crit.Add(new GtExpression("Date", startDate));
            crit.Add(new EqExpression("Id", prod));


            return crit.List<Transactions>();
        }

    }
}
