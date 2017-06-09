using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Common;
using ProjectBase.Data;

namespace Grundfos.ScalaConnector.Controllers
{
    public class ProductPartsController : AbstractNHibernateDao<ProductParts, string>
    {
        public ProductPartsController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

    }
}
