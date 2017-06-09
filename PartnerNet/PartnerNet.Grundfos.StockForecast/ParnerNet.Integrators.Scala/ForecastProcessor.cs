using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using Grundfos.ScalaConnector;
using PartnerNet.Common;
using PartnerNet.Domain;
using ControllerManager=PartnerNet.Business.ControllerManager;
using Product=Grundfos.ScalaConnector.Product;
using PurchaseOrderItem=Grundfos.ScalaConnector.PurchaseOrderItem;

namespace ParnerNet.Integrators.Scala
{
    public class ForecastProcessor : IProcessor
    {
        public string Name
        {
            get { return "Forecast Processor"; }
        }

        public bool Execute(out string errors)
        {
            errors = "";

            try
            {
                #region Check if needs to be executed

                if (Config.CurrentDate.DayOfWeek ==
                    (DayOfWeek)Convert.ToInt32(ConfigurationManager.AppSettings["ExecuteDayOfWeek"])
                    && DateTime.Now.Hour >= Convert.ToInt32(ConfigurationManager.AppSettings["ExecuteHour"]))
                {
                    //Review if the forecast is executing in the log
                    //If not start the process, otherwise returns true with no errors.
                    if (ControllerManager.Log.IsExecuting(Name, ExecutionStatus.Start))
                        return true;
                    else
                        ControllerManager.Log.Add(Name, ExecutionStatus.Start, string.Empty);
                }
                else
                    return true;

                #endregion

                


                DateTime endDate = Config.CurrentDate;
                DateTime startDate = endDate.AddDays(-7);

                #region Provider Refresh
                IList<Grundfos.ScalaConnector.Provider> proveedoresScala = Grundfos.ScalaConnector.ControllerManager.Provider.GetProviderList();
                List<PartnerNet.Domain.Provider> proveedores = ControllerManager.Provider.GetFullProviderList();
                try
                {
                    foreach (Grundfos.ScalaConnector.Provider provider in proveedoresScala)
                    {
                        bool updated = false;
                        PartnerNet.Domain.Provider prov = null;
                        prov = proveedores.Find(delegate(PartnerNet.Domain.Provider record)
                                                    {
                                                        if (record.ProviderCode != provider.Id)
                                                        {
                                                            return false;
                                                        }
                                                        return true;
                                                    });

                        if (prov != null)
                        {
                            prov.Name = provider.Name;
                            prov.CountryCode = provider.CountryCode;
                            updated = true;
                        }

                        if (updated == false)
                        {
                            PartnerNet.Domain.Provider nuevo = new PartnerNet.Domain.Provider();
                            nuevo.Name = provider.Name;
                            nuevo.ProviderCode = provider.Id;
                            //cambiar y resubir
                            nuevo.CountryCode = provider.CountryCode;
                            ControllerManager.Provider.SaveOrUpdate(nuevo);
                            updated = true;
                        }
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Provider refresh successfully");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Provider Refresh";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }
                #endregion

                #region Product Refresh
                IList<Grundfos.ScalaConnector.Product> productosScala = Grundfos.ScalaConnector.ControllerManager.Product.GetProductList();
                List<PartnerNet.Domain.Product> productos = ControllerManager.Product.GetFullProductList();
                try
                {
                    foreach (Grundfos.ScalaConnector.Product product in productosScala)
                    {
                        PartnerNet.Domain.Product producto = productos.Find(delegate(PartnerNet.Domain.Product record)
                                                      {
                                                          if (record.ProductCode != product.Id)
                                                          {
                                                              return false;
                                                          }
                                                          return true;
                                                      });

                        if (producto == null)
                        {
                            producto = new PartnerNet.Domain.Product();
                            producto.ProductCode = product.Id;
                            producto.Safety = 6;
                        }

                        producto.Description = product.Description;
                        producto.Group = product.Group;
                        producto.LeadTime = product.Detail[0].Leadtime;
                        producto.CountryCode = product.CountryCode;
                        if(producto.RepositionLevel != product.Detail[0].RepPoint)
                        {
                            producto.RepositionLevel = product.Detail[0].RepPoint;
                            //ProductRepositionLevelHistory prlh = new ProductRepositionLevelHistory();
                            //prlh.Product = producto;
                            //prlh.RepositionLevel = producto.RepositionLevel;
                            //ControllerManager.Product.GenericSave(prlh);
                        }
                        producto.RepositionPoint = product.Detail[0].PurchaseMod;
                        producto.AlternativeProduct = product.AlternativeProduct;
                        producto.AlternativeDate = product.AlternativeDate;
                        PartnerNet.Domain.Provider provtemp = proveedores.Find(delegate(PartnerNet.Domain.Provider record)
                                                                 {
                                                                     if (record.ProviderCode != product.Provider.Id)
                                                                     {
                                                                         return false;
                                                                     }
                                                                     return true;
                                                                 });
                        if (provtemp != null)
                            producto.Provider = provtemp;
                        else if (provtemp == null)
                            producto.Provider = proveedores.Find(delegate(PartnerNet.Domain.Provider record)
                                                                 {
                                                                     if (record.ProviderCode != "999999")
                                                                     {
                                                                         return false;
                                                                     }
                                                                     return true;
                                                                 });

                        ControllerManager.Product.Save(producto);
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Product refresh successfully");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Product Refresh";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }
                #endregion

                #region Generate History

                #region Limpieza de datos procesador de esta semana (si los hubiera)

                ControllerManager.TransactionHistoryWeekly.CleanData(Config.CurrentWeek, Config.CurrentDate.Year);

                #endregion

                IList<Product> prodlist = Grundfos.ScalaConnector.ControllerManager.Product.GetProductList();
                List<PartnerNet.Domain.Product> productlist = ControllerManager.Product.GetProductListAlt();
                try
                {
                    List<Transactions> transcomp = Grundfos.ScalaConnector.ControllerManager.Transactions.GetTransaction(startDate, endDate, 0);
                    List<Transactions> transvent = Grundfos.ScalaConnector.ControllerManager.Transactions.GetTransaction(startDate, endDate, 1);
                    List<PurchaseOrderItem> poil = Grundfos.ScalaConnector.ControllerManager.PurchaseOrderItem.GetWeeklyTransaction(startDate, endDate);

                    foreach (Product prod in prodlist)
                    {
                        PartnerNet.Domain.Product producto = null;
                        producto = productlist.Find(delegate(PartnerNet.Domain.Product record)
                                                        {
                                                            if (record.ProductCode != prod.Id)
                                                            {
                                                                return false;
                                                            }
                                                            return true;
                                                        });

                        if (producto != null)
                        {
                            #region Generate TransactionHistoryWeekly

                            TransactionHistoryWeekly transax = new TransactionHistoryWeekly();

                            int stock = 0;
                            int purchases = 0;
                            int sales = 0;
                            int purchaseorders = 0;

                            stock = prod.StockQ;

                            Transactions subList = null;

                            subList = transcomp.Find(delegate(Transactions record)
                                                         {
                                                             if (record.Product != prod)
                                                             {
                                                                 return false;
                                                             }
                                                             return true;
                                                         });

                            if (subList != null)
                                purchases = subList.Quantity;

                            subList = transvent.Find(delegate(Transactions record)
                                                         {
                                                             if (record.Product != prod)
                                                             {
                                                                 return false;
                                                             }
                                                             return true;
                                                         });
                            if (subList != null)
                                sales = -subList.Quantity;

                            PurchaseOrderItem subList2 = null;
                            subList2 = poil.Find(delegate(PurchaseOrderItem record)
                                                     {
                                                         if (record.Product != prod)
                                                         {
                                                             return false;
                                                         }
                                                         return true;
                                                     });

                            if (subList2 != null)
                                purchaseorders = subList2.QuantityOrdered;


                            transax.ProductID = producto;
                            transax.Purchase = purchases;
                            transax.Sale = sales;
                            transax.PurchaseOrders = purchaseorders;
                            transax.Stock = stock;
                            transax.Year = endDate.Year;
                            transax.Week =
                                Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(endDate,
                                                                                           CalendarWeekRule.
                                                                                               FirstFourDayWeek,
                                                                                           DayOfWeek.Sunday);
                            transax.ProductCode = prod.Id;

                            ControllerManager.TransactionHistoryWeekly.Save(transax);

                            #endregion
                        }
                    }

                    ControllerManager.TransactionHistoryWeekly.Copy();

                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Weekly history created successfully");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Weekly History";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }
                #endregion

                #region Generate Weekly Statitics
                try
                {
                    ControllerManager.TransactionHistoryWeekly.CalculateFullStatistic(Config.CurrentWeek,
                                                                                      Config.CurrentDate.Year);

                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Weekly statistics created successfully");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Weekly Statistics";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }
                #endregion

                if (Config.CurrentDate.Day <= 7)
                {
                    #region Limpieza de datos procesador de este mes (si los hubiera)

                    ControllerManager.TransactionHistoryMonthly.CleanData(Config.CurrentDate.Month, Config.CurrentDate.Year);

                    #endregion

                    #region Generate Monthly History
                    try
                    {
                        DateTime endDateM = GetLastDayOfMonth(Config.CurrentDate.AddMonths(-1));
                        DateTime startDateM = GetFirstDayOfMonth(Config.CurrentDate.AddMonths(-1));

                        List<Transactions> transcompm = Grundfos.ScalaConnector.ControllerManager.Transactions.GetTransaction(startDateM, endDateM, 0);
                        List<Transactions> transventm = Grundfos.ScalaConnector.ControllerManager.Transactions.GetTransaction(startDateM, endDateM, 1);
                        List<PurchaseOrderItem> poilm = Grundfos.ScalaConnector.ControllerManager.PurchaseOrderItem.GetWeeklyTransaction(startDateM, endDateM);

                        foreach (Product prod in prodlist)
                        {
                            PartnerNet.Domain.Product producto = null;
                            producto = productlist.Find(delegate(PartnerNet.Domain.Product record)
                                                            {
                                                                if (record.ProductCode != prod.Id)
                                                                {
                                                                    return false;
                                                                }
                                                                return true;
                                                            });

                            if (producto != null)
                            {
                                #region Generate TransactionHistoryMonthly

                                TransactionHistoryMonthly transax = new TransactionHistoryMonthly();

                                int stock = 0;
                                int purchases = 0;
                                int sales = 0;
                                int purchaseorders = 0;

                                stock = prod.StockQ;

                                Transactions subList = null;

                                subList = transcompm.Find(delegate(Transactions record)
                                                              {
                                                                  if (record.Product != prod)
                                                                  {
                                                                      return false;
                                                                  }
                                                                  return true;
                                                              });

                                if (subList != null)
                                    purchases = subList.Quantity;

                                subList = transventm.Find(delegate(Transactions record)
                                                              {
                                                                  if (record.Product != prod)
                                                                  {
                                                                      return false;
                                                                  }
                                                                  return true;
                                                              });
                                if (subList != null)
                                    sales = -subList.Quantity;

                                PurchaseOrderItem subList2 = null;
                                subList2 = poilm.Find(delegate(PurchaseOrderItem record)
                                                          {
                                                              if (record.Product != prod)
                                                              {
                                                                  return false;
                                                              }
                                                              return true;
                                                          });

                                if (subList2 != null)
                                    purchaseorders = subList2.QuantityOrdered;


                                transax.ProductID = producto;
                                transax.Purchase = purchases;
                                transax.Sale = sales;
                                transax.PurchaseOrders = purchaseorders;
                                transax.Stock = stock;
                                transax.Year = endDateM.Year;
                                transax.Month =
                                    Thread.CurrentThread.CurrentCulture.Calendar.GetMonth(endDateM);
                                transax.ProductCode = prod.Id;

                                ControllerManager.TransactionHistoryMonthly.Save(transax);

                                #endregion
                            }
                        }

                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Montly history created successfully");
                    }
                    catch (Exception ex)
                    {
                        errors = ex.ToString();

                        try
                        {
                            string process = "Monthly History";
                            ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                            SendErrorEmail(process, errors);
                        }
                        catch
                        {
                        }

                        return false;
                    }

                    #endregion

                    #region Generate Monthly Statitics

                    //try
                    //{
                    //    ControllerManager.TransactionHistoryMonthly.CalculateFullStatistic(Config.CurrentDate.Month - 1, Config.CurrentDate.Year);

                    //    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Monthly statistics created successfully");
                    //}
                    //catch (Exception ex)
                    //{
                    //    errors = ex.ToString();

                    //    try
                    //    {
                    //        string process = "Monthly Statistics";
                    //        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                    //        SendErrorEmail(process, errors);
                    //    }
                    //    catch
                    //    {
                    //    }

                    //    return false;
                    //}

                    #endregion
                }


                #region Generate Forecast

                try
                {
                    IList<PartnerNet.Domain.Product> tempprodlist = new List<PartnerNet.Domain.Product>();
                    ControllerManager.Forecast.CalculateFullForecast(Config.CurrentWeek, Config.CurrentDate.Year, tempprodlist, true);

                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Forecast created successfully");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Forecast";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }
                #endregion

                #region Generate Purchase Orders

                #region Limpieza de datos procesador de esta semana (si los hubiera)

                ControllerManager.PurchaseOrder.CleanData(Config.CurrentDate);

                #endregion

                try
                {
                    ControllerManager.PurchaseOrder.GenerateFullPO(Config.CurrentWeek, Config.CurrentDate.Year, PurchaseOrderType.Forecast);

                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Purchase orders created successfully");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Purchase Orders";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }
                #endregion

                #region Proceso de Forecast a Productos Faltantes
                //List<string> prodlistfalt = PartnerNet.Business.ControllerManager.Product.GetLeftProductList();
                //List<PartnerNet.Domain.Product> prodlisttemp = PartnerNet.Business.ControllerManager.Product.GetFullProductList();
                //List<string> leftproducts = new List<string>();

                //foreach (PartnerNet.Domain.Product product in prodlisttemp)
                //{
                //    string temp = prodlistfalt.Find(delegate(string record)
                //                                                          {
                //                                                              if (record == product.ProductCode)
                //                                                              {
                //                                                                  return true;
                //                                                              }
                //                                                              return false;
                //                                                          });

                //    if (temp == null)
                //    {
                //        leftproducts.Add(product.ProductCode);
                //    }
                //}




                //foreach (string product in leftproducts)
                //{
                //    PartnerNet.Domain.Product producto = PartnerNet.Business.ControllerManager.Product.GetProductInfo(product);
                //    PartnerNet.Business.ControllerManager.Forecast.CalculateIndividualForecast(Config.CurrentWeek, Config.CurrentDate.Year, producto);
                //}
                #endregion

                ControllerManager.Log.Add(Name, ExecutionStatus.Finished, "Process finished successfully");

            }
            catch (Exception ex)
            {
                errors = ex.ToString();

                try
                {
                    string process = "Proceso de Forecast";
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                    SendErrorEmail(process, errors);
                }
                catch
                {
                }

                return false;
            }

            return true;
        }

        private DateTime GetFirstDayOfMonth(DateTime dtDate)
        {
            DateTime dtFrom = dtDate;
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
            return dtFrom;
        }

        private DateTime GetLastDayOfMonth(DateTime dtDate)
        {
            DateTime dtTo = dtDate;
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            return dtTo;
        }

        private void SendErrorEmail(string process, string errors)
        {
            SmtpClient mailclient = new SmtpClient();
            
            MailMessage mm = new MailMessage();
            mm.To.Add(ConfigurationManager.AppSettings["SupportEmail"]);
            mm.Subject = "Error in " + process;
            mm.Body = errors;

            mailclient.Send(mm);
        }
    }
}