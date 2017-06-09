using System;
using System.Collections;
using System.Collections.Generic;
using PartnerNet.Domain;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Expression;

namespace PartnerNet.Business
{
    public class CountryController : AbstractNHibernateDao<Country, int>
    {
        public CountryController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
    
        public IList<Country> GetCountryList()
        {
            ICriteria crit = GetCriteria();
            return crit.List<Country>();
        }
    }
}
