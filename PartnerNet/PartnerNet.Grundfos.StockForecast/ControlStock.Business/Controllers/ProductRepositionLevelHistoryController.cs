using System;
using System.Collections.Generic;
using System.Text;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class ProductRepositionLevelHistoryController : AbstractNHibernateDao<ProductRepositionLevelHistory, int>
    {
        public ProductRepositionLevelHistoryController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
    }
}
