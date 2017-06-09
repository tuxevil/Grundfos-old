using ProjectBase.Data;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;

namespace Grundfos.ScalaConnector.Controllers
{
    public class ProductDetailController : AbstractNHibernateDao<ProductDetail, string>
    {
        public ProductDetailController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void UpdateStock()
        {
            throw new System.NotImplementedException();
        }

        public IList<ProductDetail> FilterByLocation(string location)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new LikeExpression("Location", location, MatchMode.Exact));
            return crit.List<ProductDetail>();
        }
    }
}
