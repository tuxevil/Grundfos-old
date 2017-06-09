using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using ProjectBase.Data;

namespace Grundfos.ScalaConnector.Controllers
{
    public class ProviderController : AbstractNHibernateDao<Provider, string>
    {
        public ProviderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Provider> GetProviderList ()
        {
            ICriteria crit = GetCriteria();
            crit.AddOrder(new Order("Name", true));
            return crit.List<Provider>();
        }

        //public void GetProviderList()
        //{
        //    ICriteria crit = GetCriteria();

        //    crit.SetProjection(Projections.ProjectionList()
        //                           .Add(Projections.Property("Id"))
        //                           .Add(Projections.Property("Name"))
        //        );

        //    IList lst = crit.List();

        //    crit.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(PartnerNet.Domain.Provider).GetConstructors()[1]));

        //    IList lst1 = crit.List();

        //    IList<PartnerNet.Domain.Provider> providers = crit.List<PartnerNet.Domain.Provider>();

        //    foreach (PartnerNet.Domain.Provider provider in providers)
        //    {
        //        this.NHibernateSession.SaveOrUpdate(provider);
        //    }
        //}
    }
}
