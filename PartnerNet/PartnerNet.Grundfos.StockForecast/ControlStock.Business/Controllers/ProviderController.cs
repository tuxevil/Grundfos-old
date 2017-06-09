using System;
using System.Collections;
using System.Collections.Generic;
using PartnerNet.Domain;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Expression;

namespace PartnerNet.Business
{
    public class ProviderController : AbstractNHibernateDao<Provider, int>
    {
         public ProviderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
    
        public IList<Provider> GetProviderList()
        {
            ICriteria crit = GetCriteria();
            crit.AddOrder(new Order("Name", true));
            return crit.List<Provider>();
        }

        public List<Provider> GetFullProviderList()
        {
            ICriteria crit = GetCriteria();
            crit.AddOrder(new Order("Name", true));
            return crit.List<Provider>() as List<Provider>;
        }

        public Provider GetProvider(string providerCode)
        {
            Provider resultado = new Provider();

            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("ProviderCode", providerCode));

            if (crit.List().Count > 0)
                resultado = crit.UniqueResult<Provider>();
            
            return resultado;
        }
    }

    
}
