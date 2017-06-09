using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;
using System.Threading;
using Grundfos.ScalaConnector;
using PartnerNet.Common;
using PartnerNet.Domain;
using ControllerManager = PartnerNet.Business.ControllerManager;
using Product = Grundfos.ScalaConnector.Product;
using PurchaseOrder=Grundfos.ScalaConnector.PurchaseOrder;
using PurchaseOrderItem = Grundfos.ScalaConnector.PurchaseOrderItem;

namespace ParnerNet.Integrators.Scala
{
    public class AlertsProcessor : IProcessor
    {
        public string Name
        {
            get { return "Alerts Processor"; }
        }

        public bool Execute(out string errors)
        {
            errors = "";

            try
            {
                #region Check if needs to be executed

                if (DateTime.Now.Hour >= Convert.ToInt32(ConfigurationManager.AppSettings["ExecuteHourAlerts"]))
                {
                    // Review if the forecast is executing in the log
                    // If not start the process, otherwise returns true with no errors.
                    if (ControllerManager.Log.IsExecuting(Name, ExecutionStatus.Start))
                        return true;
                    else
                        ControllerManager.Log.Add(Name, ExecutionStatus.Start, string.Empty);
                }
                else
                    return true;

                #endregion

                //------------------------------------------------------------------------------------------------------------------

                List<PurchaseOrderItem> POList = Grundfos.ScalaConnector.ControllerManager.PurchaseOrderItem.GetAlerts();
                List<SaleOrderItem> saleorderitemsgrouped = Grundfos.ScalaConnector.ControllerManager.SaleOrderItem.PendingSaleOrderQuantity(Config.CurrentDate.AddDays(-70));

                # region Alerta de Ordenes de Compras Confirmadas y no Despachadas
                try
                {
                    List<PurchaseOrderItem> POListAlert1 = POList.FindAll(delegate(PurchaseOrderItem record)
                                                                              {
                                                                                  double daysOfWOF = 0;
                                                                                  switch (record.PurchaseOrder.WayOfDelivery)
                                                                                  {
                                                                                      case 1:
                                                                                          daysOfWOF = 44;
                                                                                          break;
                                                                                      case 2:
                                                                                          daysOfWOF = 12;
                                                                                          break;
                                                                                      case 3:
                                                                                          daysOfWOF = 4;
                                                                                          break;
                                                                                  }
                                                                                  if ((record.Confirmed == "1") && (record.QuantityOrdered > record.Quantity) &&
                                                                                      (record.PurchaseOrder.Location =="01") &&(record.PurchaseOrder.Id.StartsWith("00000")))
                                                                                  {
                                                                                      if (record.PurchaseOrder.PurchaseOrderDelivery.Count > 0)
                                                                                      {
                                                                                          if ((record.PurchaseOrder.PurchaseOrderDelivery[0].Provider.Id =="900900" ||
                                                                                              record.PurchaseOrder.PurchaseOrderDelivery[0].Provider.Id =="900928"))
                                                                                          {
                                                                                              if (!record.PurchaseOrder.PurchaseOrderDelivery[0].ProviderStatus.EndsWith("#") &&
                                                                                                 record.PurchaseOrder.PurchaseOrderDelivery[0].DeliveryDate.AddDays(-daysOfWOF) > Config.CurrentDate)
                                                                                              {
                                                                                                  return true;
                                                                                              }
                                                                                          }
                                                                                          else
                                                                                          {
                                                                                              return true;
                                                                                          }
                                                                                      }
                                                                                      else
                                                                                      {
                                                                                          return true;
                                                                                      }
                                                                                  }
                                                                                  return false;
                                                                              });

                    ControllerManager.AlertPurchaseOrder.CleanAlertPurchaseOrder();

                    foreach (PurchaseOrderItem item in POListAlert1)
                    {
                        AlertPurchaseOrder alert = new AlertPurchaseOrder();

                        alert.Destination = SearchSaleOrderDestination(saleorderitemsgrouped, item);

                        alert.PurchaseOrderCode = item.PurchaseOrder.Id;
                        alert.PurchaseOrderItemCode = item.Product.Id;
                        alert.Quantity = item.QuantityOrdered - item.Quantity;
                        alert.Type = AlertPurchaseOrderType.Alert1;
                        alert.GAP = 0;
                        alert.WayOfDelivery = (WayOfDelivery) item.PurchaseOrder.WayOfDelivery;
                        alert.ArrivalDate = item.ArrivalDate;
                        if (item.Product.Provider == null)
                        {
                            alert.PurchaseOrderProviderCode =
                                ControllerManager.Provider.GetProvider("999999").ProviderCode;
                            alert.PurchaseOrderProviderName = ControllerManager.Provider.GetProvider("999999").Name;
                        }
                        else if (!string.IsNullOrEmpty(item.Product.Provider.Id.Trim()))
                        {
                            alert.PurchaseOrderProviderCode = item.Product.Provider.Id;
                            alert.PurchaseOrderProviderName = item.Product.Provider.Name;
                        }
                        else
                        {
                            alert.PurchaseOrderProviderCode = string.Empty;
                            alert.PurchaseOrderProviderName = string.Empty;
                        }

                        double days = 0;
                        switch (item.PurchaseOrder.WayOfDelivery)
                        {
                            case 1:
                                days = 44;
                                break;
                            case 2:
                                days = 12;
                                break;
                            case 3:
                                days = 4;
                                break;
                        }

                        if (item.PurchaseOrder.PurchaseOrderDelivery.Count > 0)
                            alert.CalculatedArrivalDate = item.PurchaseOrder.PurchaseOrderDelivery[0].DeliveryDate.AddDays(-days);
                        else
                            alert.CalculatedArrivalDate = DateTime.MaxValue;

                        ControllerManager.AlertPurchaseOrder.Save(alert);
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Alerta de Ordenes de Compras Confirmadas y no Despachadas: Completadas");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Alerta de Ordenes de Compras Confirmadas y no Despachadas";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }

                #endregion

                #region Alerta de Ordenes de Compras No Confirmadas
                try
                {
                    List<PurchaseOrderItem> POListAlert2 = POList.FindAll(delegate(PurchaseOrderItem record)
                                                                              {
                                                                                  if ((record.Confirmed != "1") &&
                                                                                      (record.PurchaseOrder.Date <
                                                                                       Config.CurrentDate.AddDays(-4)) &&
                                                                                      (record.PurchaseOrder.Location ==
                                                                                       "01") &&
                                                                                      (record.QuantityOrdered >
                                                                                       record.Quantity) &&
                                                                                      (record.PurchaseOrder.Id.
                                                                                          StartsWith("00000")))
                                                                                  {
                                                                                      return true;
                                                                                  }
                                                                                  return false;
                                                                              });

                    foreach (PurchaseOrderItem item in POListAlert2)
                    {
                        AlertPurchaseOrder alert = new AlertPurchaseOrder();

                        alert.Destination = SearchSaleOrderDestination(saleorderitemsgrouped, item);

                        alert.PurchaseOrderCode = item.PurchaseOrder.Id;
                        alert.PurchaseOrderItemCode = item.Product.Id;
                        alert.Quantity = item.QuantityOrdered - item.Quantity;
                        alert.Type = AlertPurchaseOrderType.Alert2;
                        alert.GAP = Convert.ToInt32((Config.CurrentDate - item.PurchaseOrder.Date).TotalDays);
                        alert.WayOfDelivery = (WayOfDelivery) item.PurchaseOrder.WayOfDelivery;
                        alert.ArrivalDate = item.ArrivalDate;
                        if ((item.Product.Provider == null) || (item.Product.Provider.Id == " "))
                        {
                            alert.PurchaseOrderProviderCode =
                                ControllerManager.Provider.GetProvider("999999").ProviderCode;
                            alert.PurchaseOrderProviderName = ControllerManager.Provider.GetProvider("999999").Name;
                        }
                        else if (!string.IsNullOrEmpty(item.Product.Provider.Id.Trim()))
                        {
                            alert.PurchaseOrderProviderCode = item.Product.Provider.Id;
                            alert.PurchaseOrderProviderName = item.Product.Provider.Name;
                        }
                        else
                        {
                            alert.PurchaseOrderProviderCode = string.Empty;
                            alert.PurchaseOrderProviderName = string.Empty;
                        }

                        double days = 0;
                        switch (item.PurchaseOrder.WayOfDelivery)
                        {
                            case 1:
                                days = 44;
                                break;
                            case 2:
                                days = 12;
                                break;
                            case 3:
                                days = 4;
                                break;
                        }
                        if (item.PurchaseOrder.PurchaseOrderDelivery.Count > 0)
                            alert.CalculatedArrivalDate = item.PurchaseOrder.PurchaseOrderDelivery[0].DeliveryDate.AddDays(-days);
                        else
                            alert.CalculatedArrivalDate = DateTime.MaxValue; 
                        
                        ControllerManager.AlertPurchaseOrder.Save(alert);
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Alerta de Ordenes de Compras No Confirmadas: Completadas");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Alerta de Ordenes de Compras No Confirmadas";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }

                #endregion

                #region Alerta de Stock Negativos
                try
                {
                    List<Product> ProdList = Grundfos.ScalaConnector.ControllerManager.Product.GetAlert3();

                    ControllerManager.AlertProduct.CleanAlertProduct();

                    foreach (Product product in ProdList)
                    {
                        AlertProduct alert = new AlertProduct();
                        alert.ProductCode = product.Id;
                        alert.StandardCost = product.StandardCost;
                        alert.SubTotal = product.StandardCost*Convert.ToDouble(product.StockQ);
                        alert.Quantity = product.StockQ;
                        alert.Type = 1;
                        alert.NegativeDate = DateTime.Today;

                        ControllerManager.AlertProduct.Save(alert);
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Alertas de Stock Negativo: Completadas");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Alertas de Stock Negativo";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }

                #endregion

                #region Alerta de Ventas no Cumplimentadas
                try
                {
                    List<SaleOrderItem> saleorderitems =
                        Grundfos.ScalaConnector.ControllerManager.SaleOrderItem.PendingSaleOrder(
                            Config.CurrentDate.AddDays(-44));

                    List<PurchaseOrderItem> POListAlert3 = POList.FindAll(delegate(PurchaseOrderItem record)
                                                                              {
                                                                                  if ((record.Confirmed == "1") &&
                                                                                      (record.PurchaseOrder.Location ==
                                                                                       "01") &&
                                                                                      (record.PurchaseOrder.Id.
                                                                                          StartsWith("00000")))
                                                                                  {
                                                                                      return true;
                                                                                  }
                                                                                  return false;
                                                                              });

                    ControllerManager.AlertSaleOrder.CleanAlertSaleOrder();

                    foreach (PurchaseOrderItem item in POListAlert3)
                    {
                        AlertSaleOrder alert = new AlertSaleOrder();

                        AlertPurchaseOrderDestination destination =
                            SearchSaleOrderDestination(saleorderitemsgrouped, item);

                        if (destination == AlertPurchaseOrderDestination.Venta)
                        {
                            List<SaleOrderItem> saleorderitem = SearchSaleOrder(saleorderitems, item);

                            foreach (SaleOrderItem orderItem in saleorderitem)
                            {
                                if (item.ArrivalDate > orderItem.DeliveryDate)
                                {
                                    alert.PurchaseOrderCode = item.PurchaseOrder.Id;
                                    alert.PurchaseOrderItemCode = item.Product.Id;
                                    alert.SaleOrderCode = orderItem.SaleOrder.Id;
                                    alert.CustomerCode = orderItem.SaleOrder.CustomerCode;
                                    alert.Quantity = (int) orderItem.Quantity;
                                    alert.GAP = (item.ArrivalDate - orderItem.DeliveryDate).Days;
                                    alert.WayOfDelivery = (WayOfDelivery) item.PurchaseOrder.WayOfDelivery;
                                    alert.PurchaseOrderArrivalDate = item.ArrivalDate;
                                    alert.SaleOrderDeliveryDate = orderItem.DeliveryDate;
                                    alert.OrderDate = orderItem.SaleOrder.Date;
                                    ControllerManager.AlertSaleOrder.Save(alert);
                                }
                            }
                        }
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Alertas de Ventas no Cumplimentadas: Completadas");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Alertas de Ventas no Cumplimentadas";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }

                #endregion

                #region Alerta de Stock Futuro Menor al Safety
                //PENDIENTE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                try
                {
                    List<PartnerNet.Domain.Product> ProdList = ControllerManager.Product.GetAlertNegativeFutureStock();
                    PartnerNet.Domain.Product prodtemp = null;

                    foreach (PartnerNet.Domain.Product product in ProdList)
                    {
                        if (prodtemp != product)
                        {
                            AlertProduct alert = new AlertProduct();
                            alert.ProductCode = product.ProductCode;
                            alert.StandardCost = 0;
                            alert.SubTotal = 0;
                            alert.Quantity = product.Forecasts[0].FinalStock;
                            DateTime fechatemp = Convert.ToDateTime("01/01/" + Config.CurrentDate.Year.ToString());
                            fechatemp = Thread.CurrentThread.CurrentCulture.Calendar.AddWeeks(fechatemp, product.Forecasts[0].Week);
                            alert.NegativeDate = fechatemp.AddDays(-7 + (int)fechatemp.DayOfWeek);
                            alert.Type = 2;

                            ControllerManager.AlertProduct.Save(alert);

                            prodtemp = product;
                        }
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Stock Futuro Menor al Safety: Completadas");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Stock Futuro Menor al Safety";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }

                //PENDIENTE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                #endregion

                #region Alerta de Nivel de Reposición
                try
                {
                    List<AlertReposition> lstrepotmp = ControllerManager.TransactionHistoryWeekly.GetAlert6(Config.CurrentWeek, Config.CurrentDate.Year) as List<AlertReposition>;
                    ControllerManager.AlertReposition.CleanAlertReposition();

                    List<AlertReposition> aRLst = lstrepotmp.FindAll(delegate(AlertReposition record)
                                                                              {
                                                                                  if (record.Product.AlternativeProduct == " "
                                                                                      && record.Product.AlternativeDate == new DateTime(9999, 12, 31)

                                                                                      )
                                                                                      return true;
                                                                                  else
                                                                                      return false;
                                                                              });

                    List<AlertReposition> aRLst2 = lstrepotmp.FindAll(delegate(AlertReposition record)
                                                                          {
                                                                              if (record.Product.AlternativeProduct != " "
                                                                                  && record.Product.AlternativeDate != new DateTime(9999, 12, 31)
                                                                                  )
                                                                                  return true;
                                                                              else
                                                                                  return false;
                                                                          });

                    List<string> alternativeProducts = new List<string>();
                    foreach (AlertReposition alertReposition in aRLst2)
                        alternativeProducts.Add(alertReposition.Product.AlternativeProduct);

                    List<AlertReposition> lstprodalt = ControllerManager.TransactionHistoryWeekly.GetAlert6(Config.CurrentWeek, Config.CurrentDate.Year, alternativeProducts) as List<AlertReposition>;

                    List<AlertReposition> aRLst3 = lstprodalt.FindAll(delegate(AlertReposition record)
                                                                              {
                                                                                  if (record.Product.AlternativeProduct == " "
                                                                                      && record.Product.AlternativeDate == new DateTime(9999, 12, 31)

                                                                                      )
                                                                                      return true;
                                                                                  else
                                                                                      return false;
                                                                              });

                    List<SaleOrderItem> soFullLst = Grundfos.ScalaConnector.ControllerManager.SaleOrderItem.SaleOrdersByProduct();
                    foreach (AlertReposition ar in aRLst)
                    {
                        List<SaleOrderItem> soLst = soFullLst.FindAll(delegate(SaleOrderItem record)
                                                                          {
                                                                              if (record.Product.Id == ar.Product.ProductCode)
                                                                                  return true;
                                                                              else
                                                                                  return false;
                                                                          });

                        double maximumQuantity = 0;
                        double total = 0;
                        bool haveParts = false;
                        string maximumOrderCode = "Sin Orden";
                        foreach (SaleOrderItem sOI in soLst)
                        {
                            if (sOI.Quantity > maximumQuantity)
                            {
                                maximumQuantity = sOI.Quantity;
                                maximumOrderCode = sOI.SaleOrderId;
                            }
                            total = total + sOI.Quantity;

                            if (sOI.Product.Parts.Count > 0 && sOI.Product.Parts[0].Type == "B")
                            {
                                haveParts = true;
                                break;
                            }
                        }
                        if (!haveParts)
                        {
                            if (((maximumQuantity * total) / 100) > 40)
                                ar.IsConflicted = true;
                            else
                                ar.IsConflicted = false;

                            ar.OrderInfo = soLst.Count + "/" + ((maximumQuantity * total) / 100) + "/" + maximumOrderCode;

                            if (!aRLst3.Exists(delegate(AlertReposition record)
                                                                      {
                                                                          if (record.ProductCode == ar.ProductCode)
                                                                              return true;
                                                                          else
                                                                              return false;
                                                                      }))
                                ControllerManager.AlertReposition.Save(ar);
                        }
                    }

                    foreach (AlertReposition ar in aRLst3)
                    {
                        AlertReposition pastproduct = aRLst2.Find(delegate(AlertReposition record)
                                                                      {
                                                                          if (record.Product.AlternativeProduct == ar.Product.ProductCode)
                                                                              return true;
                                                                          else
                                                                              return false;
                                                                      });

                        List<SaleOrderItem> soLst = soFullLst.FindAll(delegate(SaleOrderItem record)
                                                                          {
                                                                              if (record.Product.Id == ar.Product.ProductCode)
                                                                                  return true;
                                                                              else
                                                                                  return false;
                                                                          });


                        List<SaleOrderItem> soLstAlt = soFullLst.FindAll(delegate(SaleOrderItem record)
                                                                          {
                                                                              if (record.Product.Id == pastproduct.Product.ProductCode)
                                                                                  return true;
                                                                              else
                                                                                  return false;
                                                                          });

                        ar.Sales += pastproduct.Sales;

                        int saleMonts = Convert.ToInt32(Math.Round(Convert.ToDecimal(ar.ProductSaleLife + pastproduct.ProductSaleLife) / 4, MidpointRounding.AwayFromZero));
                        if (saleMonts > 12)
                            saleMonts = 12;
                        if (saleMonts == 0)
                            saleMonts = 1;


                        decimal cuatrimestralSales;
                        decimal divider = Convert.ToDecimal((saleMonts * 3)) / 12;
                        if (divider > 1)
                            cuatrimestralSales = Math.Round(ar.Sales / divider);
                        else
                            cuatrimestralSales = ar.Sales;

                        if (cuatrimestralSales > 0)
                            ar.Result = (ar.Product.RepositionLevel * 100 / cuatrimestralSales) - 100;

                        double maximumQuantity = 0;
                        double total = 0;
                        bool haveParts = false;
                        string maximumOrderCode = "Sin Orden";
                        foreach (SaleOrderItem sOI in soLst)
                        {
                            if (sOI.Quantity > maximumQuantity)
                            {
                                maximumQuantity = sOI.Quantity;
                                maximumOrderCode = sOI.SaleOrderId;
                            }
                            total = total + sOI.Quantity;

                            if (sOI.Product.Parts.Count > 0 && sOI.Product.Parts[0].Type == "B")
                            {
                                haveParts = true;
                                break;
                            }
                        }
                        foreach (SaleOrderItem sOI in soLstAlt)
                        {
                            if (sOI.Quantity > maximumQuantity)
                            {
                                maximumQuantity = sOI.Quantity;
                                maximumOrderCode = sOI.SaleOrderId;
                            }
                            total = total + sOI.Quantity;

                            if (sOI.Product.Parts.Count > 0 && sOI.Product.Parts[0].Type == "B")
                            {
                                haveParts = true;
                                break;
                            }
                        }
                        if (!haveParts)
                        {
                            if (((maximumQuantity * total) / 100) > 40)
                                ar.IsConflicted = true;
                            else
                                ar.IsConflicted = false;

                            ar.OrderInfo = soLst.Count + "/" + ((maximumQuantity * total) / 100) + "/" + maximumOrderCode;

                            ControllerManager.AlertReposition.Save(ar);
                        }
                    }
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Alertas de Nivel de Reposición: Completadas");
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();

                    try
                    {
                        string process = "Alertas de Nivel de Reposición";
                        ControllerManager.Log.Add(Name, ExecutionStatus.Running, process + ": " + errors);
                        SendErrorEmail(process, errors);
                    }
                    catch
                    {
                    }

                    return false;
                }

                #endregion

                #region Obtener Totales

                AlertTotal alerttotal;

                for (int i = 1; i <= 6; i++)
                {
                    alerttotal = ControllerManager.AlertTotal.GetAlertTotal(i);

                    switch (i)
                    {
                        case 1:
                            List<PurchaseOrderItem> POListAlertTotal1 = POList.FindAll(delegate(PurchaseOrderItem record)
                                                                              {
                                                                                  if ((record.QuantityOrdered >
                                                                                       record.Quantity) &&
                                                                                      (record.PurchaseOrder.Location ==
                                                                                       "01") &&
                                                                                      (record.PurchaseOrder.Id.
                                                                                          StartsWith("00000")))
                                                                                  {
                                                                                      return true;
                                                                                  }
                                                                                  return false;
                                                                              });
                            alerttotal.Total = POListAlertTotal1.Count;
                            break;
                        case 2:
                            List<PurchaseOrderItem> POListAlertTotal2 = POList.FindAll(delegate(PurchaseOrderItem record)
                                                                              {
                                                                                  if ((record.QuantityOrdered >
                                                                                       record.Quantity) &&
                                                                                      (record.PurchaseOrder.Location ==
                                                                                       "01") &&
                                                                                      (record.PurchaseOrder.Id.
                                                                                          StartsWith("00000")))
                                                                                  {
                                                                                      return true;
                                                                                  }
                                                                                  return false;
                                                                              });
                            alerttotal.Total = POListAlertTotal2.Count;
                            break;
                        case 3:
                            alerttotal.Total = ControllerManager.TransactionHistoryWeekly.GetActiveProducts(Config.CurrentWeek, Config.CurrentDate.Year);
                            break;
                        case 4:
                            List<PurchaseOrderItem> POListAlertTotal3 = POList.FindAll(delegate(PurchaseOrderItem record)
                                                                                                          {
                                                                                                              if ((record.Confirmed == "1") &&
                                                                                                                  (record.PurchaseOrder.Location ==
                                                                                                                   "01"))
                                                                                                              {
                                                                                                                  return true;
                                                                                                              }
                                                                                                              return false;
                                                                                                          });
                            alerttotal.Total = POListAlertTotal3.Count;
                            break;
                        case 5:
                            alerttotal.Total = ControllerManager.TransactionHistoryWeekly.GetActiveProducts(Config.CurrentWeek, Config.CurrentDate.Year);
                            break;
                        case 6:
                            alerttotal.Total = ControllerManager.TransactionHistoryWeekly.GetActiveProducts(Config.CurrentWeek, Config.CurrentDate.Year);
                            break;
                    }
                }
                ControllerManager.Log.Add(Name, ExecutionStatus.Running, "Totales obtenidos para las alertas.");
                #endregion

                ControllerManager.Log.Add(Name, ExecutionStatus.Finished, "Alert process finished successfully");
            }
            catch (Exception ex)
            {
                errors = ex.ToString();

                try
                {
                    string process = "Proceso de Alertas";
                    ControllerManager.Log.Add(Name, ExecutionStatus.Running, errors);
                    SendErrorEmail(process, errors);
                }
                catch
                {
                }

                return false;
            }

            return true;
        }

        private AlertPurchaseOrderDestination SearchSaleOrderDestination(List<SaleOrderItem> saleorderitems, PurchaseOrderItem item)
        {
            SaleOrderItem saleorderitem = saleorderitems.Find(delegate(SaleOrderItem record)
                                                        {
                                                            if (record.Product == item.Product)
                                                            {
                                                                return true;
                                                            }
                                                            return false;
                                                        });
            if (saleorderitem != null)
            {
                if (saleorderitem.Product.StockQ <= saleorderitem.Quantity)
                {
                    return AlertPurchaseOrderDestination.Venta;
                }
                else return AlertPurchaseOrderDestination.Stock;
            }
            else return AlertPurchaseOrderDestination.Stock;
        }

        private List<SaleOrderItem> SearchSaleOrder(List<SaleOrderItem> saleorderitems, PurchaseOrderItem item)
        {
            List<SaleOrderItem> saleorderitem = saleorderitems.FindAll(delegate(SaleOrderItem record)
                                                        {
                                                            if (record.Product == item.Product)
                                                            {
                                                                return true;
                                                            }
                                                            return false;
                                                        });
            return saleorderitem;
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
