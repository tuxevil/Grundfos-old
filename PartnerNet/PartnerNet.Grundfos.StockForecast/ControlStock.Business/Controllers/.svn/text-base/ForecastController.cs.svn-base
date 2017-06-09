using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping;
using PartnerNet.Common;
using PartnerNet.Domain;
using ProjectBase.Data;
using PurchaseOrderItem=Grundfos.ScalaConnector.PurchaseOrderItem;

namespace PartnerNet.Business
{
    public class ForecastController : AbstractNHibernateDao<Forecast, int>
    {
        public ForecastController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath)
        {
        }

        public void CalculateFullForecast(int week, int year, IList<Product> productlist, bool clean)
        {
            int prodcont = 0;
            if(productlist.Count < 1)
                productlist = ControllerManager.Product.GetProductList();

            List<TransactionHistoryWeekly> fullhistory = ControllerManager.TransactionHistoryWeekly.GetInfo(week, year, 20);
            List<ProductStatisticWeekly> fullstatistics = ControllerManager.ProductStatisticWeekly.GetStatistics();
            List<PurchaseOrderItem> fullpoil = Grundfos.ScalaConnector.ControllerManager.PurchaseOrderItem.GetFullArrivalPO(Config.CurrentDate);

            if (clean)
            {
                IQuery q = NHibernateSession.GetNamedQuery("sp_forecast_clean");
                q.SetInt32("Week", week);
                q.SetInt32("Year", year);
                q.UniqueResult();
            }
            foreach (Product p in productlist)
            {
                prodcont++;
                List<TransactionHistoryWeekly> history = fullhistory.FindAll(delegate(TransactionHistoryWeekly record)
                                                                                 {
                                                                                     if (record.ProductID != p)
                                                                                     {
                                                                                         return false;
                                                                                     }
                                                                                     return true;
                                                                                 });

                ProductStatisticWeekly statistics = fullstatistics.Find(delegate(ProductStatisticWeekly record)
                                                                            {
                                                                                if (record.Product != p)
                                                                                {
                                                                                    return false;
                                                                                }
                                                                                return true;
                                                                            });

                List<PurchaseOrderItem> poil = fullpoil.FindAll(delegate(PurchaseOrderItem record)
                                                                            {
                                                                                if (record.Product.Id == p.ProductCode)
                                                                                {
                                                                                    return true;
                                                                                }
                                                                                return false;
                                                                            });

                if (history.Count < 20)
                {
                    List<TransactionHistoryWeekly> temphistory = new List<TransactionHistoryWeekly>();

                    int initialweek = 0;
                    int initialyear = 0;
                    if (week < 20)
                    {
                        initialweek = week - 20 + 54;
                        initialyear = year - 1;
                    }
                    else
                    {
                        initialweek = week - 20;
                    }
                    for (int cont = 1; cont <= 20; cont++)
                    {
                        TransactionHistoryWeekly tempTHW = history.Find(delegate(TransactionHistoryWeekly record)
                                                                            {
                                                                                if ((record.Week == initialweek) &&
                                                                                    (record.Year == initialyear))
                                                                                {
                                                                                    return true;
                                                                                }
                                                                                return false;
                                                                            });
                        if (tempTHW == null)
                        {
                            tempTHW = new TransactionHistoryWeekly(0, p);
                            tempTHW.Week = initialweek;
                            tempTHW.Year = initialyear;
                            tempTHW.Stock = 0;
                            tempTHW.PurchaseOrders = 0;
                            tempTHW.Purchase = 0;
                            tempTHW.Sale = 0;
                        }


                        temphistory.Add(tempTHW);

                        if (initialweek < 53)
                        {
                            initialweek++;
                        }
                        else
                        {
                            initialweek = 1;
                            initialyear++;
                        }
                    }
                    CalculateForecast(p, week, year, temphistory, statistics, poil);
                }
                else
                {
                    CalculateForecast(p, week, year, history, statistics, poil);
                }

                if(Convert.ToDouble(prodcont) % 1000 == 0)
                {
                    NHibernateSession.Flush();
                    ControllerManager.Log.Add("Forecast Processor", ExecutionStatus.Running, "Forecast: " + prodcont + " productos procesados");
                }
            }

            NHibernateSession.Flush();
            ControllerManager.Log.Add("Forecast Processor", ExecutionStatus.Running, "Forecast: " + prodcont + " productos procesados");
        }

        public void CalculateForecast(Product prod, int week, int year, List<TransactionHistoryWeekly> history,
                                      ProductStatisticWeekly statistics, List<PurchaseOrderItem> poil)
        {
            //VARIABLES
            int cont = 0;
            int cont2 = 0;
            int cont3 = 0;
            int todaysale = 0;
            int processedOn = 0;

            //CREACION DE COLECCION DE OBJETOS
            IList<Forecast> lista = new List<Forecast>();

            //INICIO DE CARGA DE TRANSACCIONES PASADAS (10)
            for (cont3 = 19; cont3 >= 0; cont3--)
            {
                Forecast past = new Forecast();
                past.Product = prod;
                if (week - cont3 > 0)
                {
                    past.Week = week - cont3;
                    past.Year = year;
                }
                else
                {
                    past.Week = 53 + (week - cont3);
                    past.Year = year - 1;
                }
                past.Stock = 0;
                past.Purchase = 0;
                past.Sale = 0;
                past.FinalStock = 0;
                past.Safety = 0;
                past.SafetyCoEf = 0;
                past.QuantitySuggested = 0;
                past.ProcessedOn = 0;

                TransactionHistoryWeekly THWpast = history.Find(delegate(TransactionHistoryWeekly record)
                                                                    {
                                                                        if ((record.Year == past.Year) &&
                                                                            (record.Week == past.Week))
                                                                        {
                                                                            return true;
                                                                        }
                                                                        return false;
                                                                    });
                if (THWpast != null)
                {
                    past.FinalStock = THWpast.Stock;
                    past.Purchase = THWpast.Purchase;
                    past.Sale = THWpast.Sale;
                }
              
                past.Stock = past.FinalStock + past.Sale - past.Purchase;

                lista.Add(past);
            }

            //PROCESO DE CALCULO DEL FUTURO (18)
            int saleaverage = statistics.Sale;
            int safety = statistics.Sale*prod.Safety;

            for (cont = 1; cont <= 53; cont++)
            {
                int stock = 0;
                int purchase = 0;
                int finalStock = 0;
                int quantitySuggested = 0;

                //if (cont <= prod.LeadTime)
                {
                    //int tempweek = week;
                    //int tempyear = year;
                    //if (53 - prod.LeadTime + cont + week < 54)
                    //{
                    //    tempweek = 53 - prod.LeadTime + cont + week;
                    //    tempyear = year - 1;
                    //}
                    //else if (prod.LeadTime - cont > 0)
                    //{
                    //    tempweek = prod.LeadTime - cont;
                    //}
                    //TransactionHistoryWeekly THWfut = history.Find(delegate(TransactionHistoryWeekly record)
                    //                                                   {
                    //                                                       if ((record.Week == tempweek) &&
                    //                                                           (record.Year == tempyear))
                    //                                                       {
                    //                                                           return true;
                    //                                                       }
                    //                                                       return false;
                    //                                                   });
                    int tempdays = 0;
                    if (cont == 1)
                        tempdays = 1;
                    else
                        tempdays = 1 + (7 * (cont-1));
                    DateTime startDate = Config.CurrentDate.AddDays(tempdays);
                    DateTime endDate = startDate.AddDays(7);
                    
                    List<Grundfos.ScalaConnector.PurchaseOrderItem> poilist = poil.FindAll(delegate(Grundfos.ScalaConnector.PurchaseOrderItem record)
                                                 {
                                                     if ((record.ArrivalDate >= startDate) && (record.ArrivalDate <= endDate))
                                                     {
                                                         return true;
                                                     }
                                                     return false;
                                                 });
                    if(poilist.Count == 0)
                    {
                        purchase = 0;
                    }
                    else
                    {
                        foreach (PurchaseOrderItem item in poilist)
                        {
                            purchase = purchase + item.QuantityOrdered;
                        }
                    }
                }

                stock = lista[18 + cont].FinalStock;
                finalStock = stock + purchase - saleaverage;

                Forecast info = new Forecast();

                info.Product = prod;
                if (week + cont <= 53)
                {
                    info.Week = week + cont;
                    info.Year = year;
                }
                else
                {
                    info.Week = week + cont - 53;
                    info.Year = year + 1;
                }

                if (cont > prod.LeadTime && finalStock < safety)
                {
                    double modulequantity = 1;
                    int purchasesuggested = 0;
                    if (prod.RepositionPoint > 0)
                    {
                        modulequantity = (Convert.ToDouble(safety) - Convert.ToDouble(finalStock)) / Convert.ToDouble(prod.RepositionPoint);
                        purchasesuggested = Convert.ToInt32(Math.Ceiling(modulequantity));
                    }

                    if(prod.LeadTime > 0)
                        lista[cont - prod.LeadTime + 19].QuantitySuggested = purchasesuggested*prod.RepositionPoint;
                    else
                        lista[cont - prod.LeadTime + 18].QuantitySuggested = purchasesuggested * prod.RepositionPoint;
                    purchase = purchasesuggested*prod.RepositionPoint + purchase;
                    finalStock = stock + purchase - saleaverage;
                }

                info.Stock = stock;
                info.Purchase = purchase;
                info.Sale = saleaverage;
                info.FinalStock = finalStock;
                info.Safety = safety;
                info.SafetyCoEf = prod.Safety;
                info.QuantitySuggested = quantitySuggested;
                info.ProcessedOn = processedOn;

                lista.Add(info);
            }
            for (cont = 20; cont < 73; cont++)
            {
                Forecast forecast = lista[cont];
                Save(forecast);
            }
        }

        public void CleanTable(int week, int year)
        {
            ICriteria crit = GetCriteria();

            crit.Add(
                new OrExpression(new AndExpression(new GtExpression("Week", week), new EqExpression("Year", year)),
                                 new AndExpression(new LtExpression("Week", week), new GtExpression("Year", year))));

            foreach (Forecast fore in crit.List<Forecast>())
            {
                Delete(fore);
            }
        }

        public Forecast GetProductInfo(Product product, int week, int year)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Product", product));
            crit.Add(new EqExpression("Week", week));
            crit.Add(new EqExpression("Year", year));
            return crit.UniqueResult<Forecast>();
        }

        public IList<Forecast> GetPOList(int week, int year)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new GtExpression("QuantitySuggested", 0));
            crit.Add(new EqExpression("Week", week));
            crit.Add(new EqExpression("Year", year));

            return crit.List<Forecast>();
        }

        public IList<Forecast> GetForecast(Product product, int week, int year)
        {
            //VARIABLES
            int cont = 0;
            int cont2 = 0;
            int cont3 = 0;
            int todaysale = 0;
            int processedOn = 0;

            //OBTENCION DE LISTA DE HISTORICOS Y CARGA DE ESTADISTICAS ACTUALES
            IList<TransactionHistoryWeekly> history =
                ControllerManager.TransactionHistoryWeekly.GetInfo(product.Id, week, year, 10);
            ProductStatisticWeekly statistics = ControllerManager.ProductStatisticWeekly.GetProductInfo(product.Id);

            //CREACION DE COLECCION DE OBJETOS
            IList<Forecast> lista = new List<Forecast>();

            //INICIO DE CARGA DE TRANSACCIONES PASADAS (10)
            for (cont3 = 10; cont3 > 0; cont3--)
            {
                Forecast past = new Forecast();
                past.Product = new Product(product.Id);
                if (week - cont3 > 0)
                {
                    past.Week = week - cont3;
                    past.Year = year;
                }
                else
                {
                    past.Week = 53 + (week - cont3);
                    past.Year = year - 1;
                }
                past.Stock = 0;
                past.Purchase = 0;
                past.Sale = 0;
                past.FinalStock = 0;
                past.Safety = 0;
                past.SafetyCoEf = 0;
                past.QuantitySuggested = 0;
                past.ProcessedOn = 0;
                for (int i = 0; i < history.Count; i++)
                {
                    if (history[i].Year == past.Year)
                    {
                        if (history[i].Week == past.Week)
                        {
                            past.FinalStock = history[i].Stock;
                            past.Purchase = history[i].Purchase;
                            past.Sale = history[i].Sale;
                        }
                    }
                }
                past.Stock = past.FinalStock + past.Sale - past.Purchase;

                lista.Add(past);
            }

            //OBTENCION DE DATOS FUTUROS PRECALCULADOS

            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Product", product));
            crit.Add(
                new OrExpression(new AndExpression(new GeExpression("Week", week), new GeExpression("Year", year)),
                                 new GtExpression("Year", year)));
            crit.AddOrder(new Order("Year", true));
            crit.AddOrder(new Order("Week", true));
            //crit.Add(new GtExpression("Week", week));
            //crit.Add(new GeExpression("Year", year));

            IList<Forecast> lst = crit.List<Forecast>();

            foreach (Forecast forecast in lst)
            {
                lista.Add(forecast);
            }

            return lista;
        }

    }
}