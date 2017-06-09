using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Common;
using PartnerNet.Domain;
using ProjectBase.Data;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using PartnerNet.Business;
using PartnerNet.Common;
using PartnerNet.Domain;

namespace PartnerNet.Business
{
    public class PurchaseOrderController : AbstractNHibernateDao<PurchaseOrder, int>
    {
        public PurchaseOrderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void ChangeStatus(IList<PurchaseOrder> selectedOC, PurchaseOrderStatus status)
        {
            foreach(PurchaseOrder oc in selectedOC)
            {
                oc.PurchaseOrderStatus = status;
            }
        }

        public PurchaseOrder GetPurchaseOrder(Provider provider, PurchaseOrderType pot, PurchaseOrderStatus pos)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Provider", provider));
            crit.Add(new EqExpression("Date", Config.CurrentDate));
            crit.Add(new EqExpression("PurchaseOrderType", pot));
            crit.Add(new EqExpression("PurchaseOrderStatus", pos));

            PurchaseOrder po = crit.UniqueResult<PurchaseOrder>();

            if (po == null)
            {
                po = new PurchaseOrder();
                po.Provider = provider;
                po.Date = Config.CurrentDate;
                po.PurchaseOrderStatus = 0;
                po.PurchaseOrderType = pot;
                po.WOD = WayOfDelivery.Maritimo;
                
            }
            return po;
        }

        public void GenerateFullPO(int week, int year, PurchaseOrderType orderType)
        {
            PurchaseOrderStatus orderStatus = PurchaseOrderStatus.Open;

            IList<Forecast> lst = ControllerManager.Forecast.GetPOList((Config.CurrentWeek == 53) ? 1 : Config.CurrentWeek + 1, (Config.CurrentWeek == 53) ? Config.CurrentDate.Year + 1 : Config.CurrentDate.Year);
            List<PurchaseOrder> purchaseOrders = GetByCriteria(new EqExpression("PurchaseOrderType", orderType), new EqExpression("PurchaseOrderStatus", orderStatus), new EqExpression("Date", Config.CurrentDate)) as List<PurchaseOrder>;

            foreach (Forecast forecast in lst)
            {
                PurchaseOrder po = purchaseOrders.Find(delegate(PartnerNet.Domain.PurchaseOrder record)
                                                 {
                                                     if (record.Provider == forecast.Product.Provider)
                                                         return true;
                                                     else
                                                        return false;
                                                 });

                if (po == null)
                {
                    po = new PurchaseOrder();
                    po.Provider = forecast.Product.Provider;
                    po.Date = Config.CurrentDate;
                    po.PurchaseOrderStatus = orderStatus;
                    po.PurchaseOrderType = orderType;
                    po.WOD = WayOfDelivery.Maritimo;
                    purchaseOrders.Add(po);
                }

                PurchaseOrderItem poi = new PurchaseOrderItem();
                poi.PurchaseOrder = po;
                poi.Product = forecast.Product;
                poi.Forecast = forecast;

                if (orderType == PurchaseOrderType.Forecast)
                {
                    poi.QuantitySuggested = poi.Forecast.QuantitySuggested;
                    poi.Quantity = poi.Forecast.QuantitySuggested;
                }

                po.PurchaseOrderItems.Add(poi);

                Save(po);
            }

            NHibernateSession.Flush();
        }

        public void GeneratePO(Product prod, int week, int year, PurchaseOrderType pot, int repositionpoint)
        {
            PurchaseOrderStatus pos = PurchaseOrderStatus.Open;

            // Busco si ya existe una orden de compra abierta para este proveedor
            PurchaseOrder po = GetPurchaseOrder(prod.Provider, pot, pos);
                        
            PurchaseOrderItem poi = new PurchaseOrderItem();
            poi.PurchaseOrder = po;
            poi.Product = prod;
            poi.Forecast = ControllerManager.Forecast.GetProductInfo(prod, Config.CurrentWeek + 1, Config.CurrentDate.Year);
            if (pot == PurchaseOrderType.Forecast)
            {
                poi.QuantitySuggested = poi.Forecast.QuantitySuggested;
                poi.Quantity = poi.Forecast.QuantitySuggested;
            }
            else if (pot == PurchaseOrderType.Manual)
            {
                poi.QuantitySuggested = repositionpoint;
                poi.Quantity = repositionpoint;
            }

            po.PurchaseOrderItems.Add(poi);
            this.Save(po);
        }

        public void FilterByDate()
        {
            throw new System.NotImplementedException();
        }

        public void FilterByProduct()
        {
            throw new System.NotImplementedException();
        }

        public IList<PurchaseOrderInformation> GetPurchaseOrders(int poid, DateTime date, int provider, int status, int page, int pagesize, int POtype)
        {
            string querystring = "PO.Id, PO.Provider, PO.Date, PO.PurchaseOrderType, PO.WOD";
            IQuery q = GetInfo(poid, date, provider, status, page, pagesize, querystring, POtype);

            IList lst = q.List();

            q.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(PurchaseOrderInformation).GetConstructors()[1]));

            IList<PurchaseOrderInformation> poinfo = q.List<PurchaseOrderInformation>();

            return poinfo;
        }

        public IList<PurchaseOrderInformation> GetPurchaseOrdersBetweenDates(int poid, DateTime date, DateTime dateEnd, int provider, int status, int page, int pagesize, int POtype, out int recordCount)
        {
            string querystring = "Count(PO.Id)";
            IQuery q = GetInfoBetweenDates(poid, date, dateEnd, provider, status, 0, 0, querystring, POtype, 1);
            recordCount = Convert.ToInt32(q.UniqueResult());

            if (recordCount == 0)
                return new List<PurchaseOrderInformation>();


            querystring = "PO.Id, PV.Name, PO.Date, PO.PurchaseOrderType, PO.WOD, SUM(POI.Quantity * VS.Price), SUM(POI.Quantity), MAX(VS.Currency), AVG(P.LeadTime)";
            q = GetInfoBetweenDates(poid, date, dateEnd, provider, status, page, pagesize, querystring, POtype, 2);

            q.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(PurchaseOrderInformation).GetConstructors()[1]));

            IList<PurchaseOrderInformation> poinfo = q.List<PurchaseOrderInformation>();

            return poinfo;
        }

        public int GetProductInformationCount(int poid, DateTime date, int provider, int status, int page, int pagesize, int POtype)
        {
            string querystring = "Count(PO.Id)";
            IQuery q = GetInfo(poid, date, provider, status, 0, 0, querystring, POtype);

            long prueba = q.UniqueResult<Int64>();

            return Convert.ToInt32(prueba);
        }

        public IQuery GetInfo(int poid, DateTime date, int provider, int status, int page, int pagesize, string querystring, int POtype)
        {
            string query = "select " + querystring + " from PurchaseOrder PO";
            query += " join PO.Provider PV";
            query += " where PO.PurchaseOrderStatus = :Status";
            if(poid > 0)
                query += " AND PO.Id = :Id";
            if(provider > 0)
                query += " AND PV.Id = :Provider";
            if (date > Convert.ToDateTime("1/1/1900"))
                query += " AND PO.Date = :Date";
            if (POtype > 0)
                query += " AND PO.PurchaseOrderType = :POtype";
            query += " order by PO.Id";

            IQuery q = NHibernateSession.CreateQuery(query);

            q.SetString("Status", status.ToString());

            if (poid > 0)
                q.SetInt32("Id", poid);
            if (provider > 0)
                q.SetInt32("Provider", provider);
            if (date > Convert.ToDateTime("1/1/1900"))
                q.SetDateTime("Date", date);          
            if (POtype > 0)
                q.SetEnum("POtype", (PurchaseOrderType)POtype);

            if (pagesize != 0)
            {
                q.SetMaxResults(pagesize);
                if (page == 1)
                    q.SetFirstResult(0);
                else
                    q.SetFirstResult((page - 1) * pagesize);
            }

            return q;
        }

        public IQuery GetInfoBetweenDates(int poid, DateTime date, DateTime dateEnd, int provider, int status, int page, int pagesize, string querystring, int POtype, int type)
        {
            string query = "select " + querystring + " from PurchaseOrder PO";
            query += " join PO.Provider PV";
            if (type == 2)
            {
                query += " join PO.PurchaseOrderItems POI";
                query += " join POI.Product P";
                query += " join P.ViewScala VS";
            }
            query += " where PO.PurchaseOrderStatus = :Status";
            if (poid > 0)
                query += " AND PO.Id = :Id";
            if (provider > 0)
                query += " AND PV.Id = :Provider";
            if (date > Convert.ToDateTime("1/1/1900"))
                query += " AND PO.Date >= :Date And PO.Date <= :DateEnd";
            if (POtype > 0)
                query += " AND PO.PurchaseOrderType = :POtype";

            if (type == 2)
                query += " GROUP BY PO.Id, PV.Name, PO.Date, PO.PurchaseOrderType, PO.WOD";

            if (page != 0 && pagesize != 0)
                query += " order by PO.Id";

            IQuery q = NHibernateSession.CreateQuery(query);

            q.SetString("Status", status.ToString());

            if (poid > 0)
                q.SetInt32("Id", poid);
            if (provider > 0)
                q.SetInt32("Provider", provider);
            if (date > Convert.ToDateTime("1/1/1900"))
                q.SetDateTime("Date", date);
            if (date > Convert.ToDateTime("1/1/1900"))
                q.SetDateTime("DateEnd", dateEnd);
            if (POtype > 0)
                q.SetEnum("POtype", (PurchaseOrderType)POtype);

            if (pagesize != 0)
            {
                q.SetMaxResults(pagesize);
                if (page == 1)
                    q.SetFirstResult(0);
                else
                    q.SetFirstResult((page - 1) * pagesize);
            }

            return q;
        }

        public void CleanTable()
        {
            foreach (PurchaseOrder o in GetAll())
                this.Delete(o);
        }

        public void CleanData(DateTime date)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Date", date));

            IList<PurchaseOrder> templist = crit.List<PurchaseOrder>();

            foreach (PurchaseOrder purchaseOrder in templist)
            {
                Delete(purchaseOrder);
            }
        }

    }
}
