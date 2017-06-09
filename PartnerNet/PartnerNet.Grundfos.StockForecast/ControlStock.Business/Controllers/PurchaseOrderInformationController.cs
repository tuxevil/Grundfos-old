using System;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class PurchaseOrderInformationController : AbstractNHibernateDao<PurchaseOrderInformationController, int>
    {
         public PurchaseOrderInformationController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
    }
}
