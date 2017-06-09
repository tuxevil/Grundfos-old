using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate.Mapping;
using PartnerNet.Domain;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Expression;

namespace PartnerNet.Business
{
    public class PurchaseOrderViewScalaController : AbstractNHibernateDao<PurchaseOrderViewScala, int>
    {
        public PurchaseOrderViewScalaController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public PurchaseOrderViewScala GetLastPurchaseOrder()
        {
            ICriteria crit = GetCriteria();
            return crit.UniqueResult<PurchaseOrderViewScala>();
        }
    }
}
