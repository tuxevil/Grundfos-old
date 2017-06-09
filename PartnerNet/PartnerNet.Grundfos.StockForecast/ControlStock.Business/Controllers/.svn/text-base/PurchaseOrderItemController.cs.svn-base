using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Common;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class PurchaseOrderItemController : AbstractNHibernateDao<PurchaseOrderItem, int>
    {
        public PurchaseOrderItemController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<PurchaseOrderItem> GetPurchaseOrderItemList(PurchaseOrder po)
        {
            ICriteria crit = GetCriteria();
                        
            crit.Add(new EqExpression("PurchaseOrder", po));
            
            return crit.List<PurchaseOrderItem>();
        }

        public List<PurchaseOrderItem> GetPurchaseOrderItemList()
        {
            ICriteria crit = GetCriteria();

            return crit.List<PurchaseOrderItem>() as List<PurchaseOrderItem>;
        }

        public void UpdatePOI(IList<PurchaseOrderItem> poilist, int quantity, PurchaseOrderItemStatus poistatus)
        {
            foreach (PurchaseOrderItem poi in poilist)
            {
                poi.Quantity = quantity;
                poi.PurchaseOrderItemStatus = poistatus;

                this.SaveOrUpdate(poi);
            }
        }

        public void ChangeStatus(IList<PurchaseOrderItem> selectedPOI, PurchaseOrderItemStatus status)
        {
            foreach (PurchaseOrderItem poi in selectedPOI)
            {
                poi.PurchaseOrderItemStatus = status;
            }
        }
    }
}
