using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PartnerNet.Business;
using PartnerNet.Common;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.Templates
{
    #region Pager Event Definition

    public delegate void ItemModificationEventHandler(
            object sender, ItemModificatedEventArgs e);

    public class ItemModificatedEventArgs : EventArgs
    {
        public ItemModificatedEventArgs(bool modificated)
        {
            _modificated = modificated;
        }

        private bool _modificated;
        public bool ModificationStatus
        {
            get { return _modificated; }
            set { _modificated = value; }
        }
    }

    #endregion



    public partial class DetalleOC : System.Web.UI.UserControl, IPostBackEventHandler
    {
        #region Exposed Event for PageChanged

        public event ItemModificationEventHandler ItemChanged;

        protected virtual void OnItemChanged(ItemModificatedEventArgs e)
        {
            if (ItemChanged != null)
                ItemChanged(this, e);
       }

        #endregion

        public int POId
        {
            get { return (ViewState["POId"] != null) ? (int)ViewState["POId"] : 0; }
            set { ViewState["POId"] = value; }
        }

        public int POS
        {
            get { return (ViewState["POS"] != null) ? (int)ViewState["POS"] : 0; }
            set { ViewState["POS"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole(UserType.Lectores.ToString()))
            {
                rblWOD.Enabled = false;
            }
        }

        public void LoadInformation()
        {
            LoadInformation(POId, POS);
        }

        private void LoadInformation(int id, int pos)
        {
            PurchaseOrder oc = ControllerManager.PurchaseOrder.GetById(id);
            switch(oc.WOD)
            {
                case WayOfDelivery.Maritimo:
                    rblWOD.SelectedValue = "1";
                    break;
                case WayOfDelivery.Aereo:
                    rblWOD.SelectedValue = "2";
                    break;
                case WayOfDelivery.Courrier:
                    rblWOD.SelectedValue = "3";
                    break;
            }

            IList<PurchaseOrderItem> ocList = ControllerManager.PurchaseOrderItem.GetPurchaseOrderItemList(oc);

            IList<PurchaseOrderItemInformation> poiinfo = new List<PurchaseOrderItemInformation>();

            foreach (PurchaseOrderItem item in ocList)
            {
                Grundfos.ScalaConnector.Product prodscala = Grundfos.ScalaConnector.ControllerManager.Product.GetProductInfo(item.Product.ProductCode);
                PurchaseOrderItemInformation temp = new PurchaseOrderItemInformation();
                temp.Id = item.Id;
                temp.ProductName = item.Product.Description;
                temp.Quantity = item.Quantity;
                temp.Price = prodscala.PurchasePrice;
                temp.TotalPrice = temp.Price * temp.Quantity;
                temp.Stock = prodscala.StockQ;
                temp.ProductCode = item.Product.ProductCode;
                temp.Status = Convert.ToInt32(item.PurchaseOrderItemStatus);
                temp.QuantitySuggested = item.QuantitySuggested;
                switch (prodscala.PurchaseCurrency)
                {
                    case "00":
                        temp.Currency = "$";
                        break;
                    case "01":
                        temp.Currency = "U$S";
                        break;
                    case "02":
                        temp.Currency = "€";
                        break;
                }

                poiinfo.Add(temp);
            }
            Label1.Text = id.ToString();
            Label3.Text = pos.ToString();

            repItems.DataSource = poiinfo;
            repItems.DataBind();
        }

        protected void repItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                IList<PurchaseOrderItem> selectedpoi = new List<PurchaseOrderItem>();
                PurchaseOrderItem poi = ControllerManager.PurchaseOrderItem.GetById(Convert.ToInt32(e.CommandArgument));
                selectedpoi.Add(poi);

                int quantity = Convert.ToInt32(((TextBox)e.Item.FindControl("TextBox1")).Text);
                CheckBox chkstatus = (CheckBox)e.Item.FindControl("CheckBox1");
                int status = 0;
                if(chkstatus.Checked == false)
                    status = 1;
                ControllerManager.PurchaseOrderItem.UpdatePOI(selectedpoi, quantity, (PurchaseOrderItemStatus)status);
                Image save = (Image) e.Item.FindControl("Image1");
                save.Visible = true;
                Label2.Visible = true;
                LoadInformation(POId, POS);
                OnItemChanged(new ItemModificatedEventArgs(true));
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            IList<PurchaseOrder> selectedpo = new List<PurchaseOrder>();

            PurchaseOrder po = ControllerManager.PurchaseOrder.GetById(Convert.ToInt32(Label1.Text));
            selectedpo.Add(po);
                
            ControllerManager.PurchaseOrder.ChangeStatus(selectedpo, (PurchaseOrderStatus)3);
        }

        protected void repItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            if (Roles.IsUserInRole(UserType.Lectores.ToString()))
            {
                TextBox tbtemp = (TextBox)e.Item.FindControl("TextBox1");
                tbtemp.Enabled = false;
                ImageButton imtemp = (ImageButton)e.Item.FindControl("btnGuardarInd");
                imtemp.Enabled = false;
                CheckBox cbtemp = (CheckBox)e.Item.FindControl("CheckBox1");
                cbtemp.Enabled = false;
            }

            if (((PurchaseOrderItemInformation)e.Item.DataItem).Status == 0)
                ((CheckBox)e.Item.FindControl("CheckBox1")).Checked = true;
            else 
                ((CheckBox) e.Item.FindControl("CheckBox1")).Checked = false;
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            IList<PurchaseOrder> selectedpo = new List<PurchaseOrder>();

            PurchaseOrder po = ControllerManager.PurchaseOrder.GetById(Convert.ToInt32(Label1.Text));
            selectedpo.Add(po);

            BorrarArchivoEsportacion();
            int numordcomp = Convert.ToInt32((ControllerManager.PurchaseOrderViewScala.GetLastPurchaseOrder()).PurchaseOrderCode);
              
            foreach (PurchaseOrder order in selectedpo)
            {
                if ((order.PurchaseOrderStatus == PurchaseOrderStatus.Open) || (order.PurchaseOrderStatus == PurchaseOrderStatus.Processed))
                {
                    numordcomp++;
                    int contcant = 5;
                    foreach (PurchaseOrderItem poi in order.PurchaseOrderItems)
                    {
                        if ((poi.PurchaseOrderItemStatus == PurchaseOrderItemStatus.Open) && (contcant < 100))
                        {
                            AgregarLineaArchivo(poi, numordcomp, contcant);
                            contcant = contcant + 5;
                        }
                        else if (poi.PurchaseOrderItemStatus == PurchaseOrderItemStatus.Open)
                        {
                            AgregarLineaArchivo(poi, numordcomp, contcant);
                            contcant = 5;
                            numordcomp++;
                        }
                    }
                    order.PurchaseOrderStatus = PurchaseOrderStatus.Processed;
                    ControllerManager.PurchaseOrder.SaveOrUpdate(order);
                }
            }
            
            Response.Clear();
            Response.AppendHeader("content-disposition", "attachment; filename=genocompra.prn");
            Response.WriteFile(Server.MapPath("~/res/genocompra.prn"));
            Response.End();
        }

        public static String Left(String strParam, int iLen)
        {
            if (iLen > 0)
                return strParam.Substring(0, iLen);
            else
                return strParam;
        }

        public static String Right(String strParam, int iLen)
        {
            if (iLen > 0)
                return strParam.Substring(strParam.Length - iLen, iLen);
            else
                return strParam;
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            IList<PurchaseOrderInformation> poinfo = new List<PurchaseOrderInformation>();
            int recordcount = 0;
            IList<PurchaseOrderInformation> poii = ControllerManager.PurchaseOrder.GetPurchaseOrdersBetweenDates(Convert.ToInt32(Label1.Text), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), 0, Convert.ToInt32(Label3.Text), 0, 0, 0, out recordcount);

            poinfo.Add(poii[0]);

            if (poinfo.Count > 0)
            {
                DataSet objDataSet = new DataSet();
                DataTable objPOT = new DataTable("rptPO");
                DataTable objItems = new DataTable("rptPOI");

                objPOT.Columns.Add("Codigo");
                objPOT.Columns.Add("FechaPedido");
                objPOT.Columns.Add("Proveedor");
                objPOT.Columns.Add("CantArt");
                objPOT.Columns.Add("Importe");
                objPOT.Columns.Add("FechaArribo");
                objPOT.Columns.Add("Origen");

                objItems.Columns.Add("CodOC");
                objItems.Columns.Add("Codigo");
                objItems.Columns.Add("Descripcion");
                objItems.Columns.Add("Cantidad");
                objItems.Columns.Add("PrecioUnitario");
                objItems.Columns.Add("Total");
                objItems.Columns.Add("Stock");
                objItems.Columns.Add("CantidadSugerida");


                foreach (PurchaseOrderInformation information in poinfo)
                {
                    IList<PurchaseOrderItem> poi = ControllerManager.PurchaseOrderItem.GetPurchaseOrderItemList(ControllerManager.PurchaseOrder.GetById(information.Id));
                    information.Amount = 0;
                    information.Totalcount = 0;
                    foreach (PurchaseOrderItem item in poi)
                    {
                        Grundfos.ScalaConnector.Product prodscala = Grundfos.ScalaConnector.ControllerManager.Product.GetProductInfo(item.Product.ProductCode);
                        information.Amount = information.Amount + (item.Quantity * prodscala.PurchasePrice);
                        information.Totalcount = information.Totalcount + Convert.ToInt32(item.Quantity);
                        

                        DataRow drItems;
                        drItems = objItems.NewRow();
                        drItems["CodOC"] = information.Id;
                        drItems["Codigo"] = item.Product.ProductCode;
                        drItems["Descripcion"] = item.Product.Description;
                        drItems["Cantidad"] = item.Quantity;
                        drItems["PrecioUnitario"] = prodscala.PurchasePrice;
                        drItems["Total"] = prodscala.PurchasePrice * item.Quantity;
                        drItems["Stock"] = prodscala.StockQ;
                        drItems["CantidadSugerida"] = item.QuantitySuggested;

                        objItems.Rows.Add(drItems);
                    }
                    DataRow myRow;
                    myRow = objPOT.NewRow();
                    myRow[0] = information.Id;
                    myRow[1] = information.Orderdate.ToShortDateString();
                    myRow[2] = information.Provider;
                    myRow[3] = information.Totalcount;
                    myRow[4] = information.Amount;
                    myRow[5] = information.Arrivaldate.ToShortDateString();
                    myRow[6] = information.Type;

                    objPOT.Rows.Add(myRow);
                }
                objDataSet.Tables.Add(objPOT);
                objDataSet.Tables.Add(objItems);

                objDataSet.Relations.Add("PO_POI", objDataSet.Tables["rptPO"].Columns["Codigo"], objDataSet.Tables["rptPOI"].Columns["CodOC"]);

                //objDataSet.WriteXmlSchema("c:/esquema.xsd");
                //objDataSet.WriteXml("c:/dataset.xml");
                Session["dsOrdCompras"] = objDataSet;
                Response.Redirect("/purchase-order/report.aspx");
            }
        }

        protected void rblWOD_SelectedIndexChanged(object sender, EventArgs e)

        {
            PurchaseOrder po = ControllerManager.PurchaseOrder.GetById(Convert.ToInt32(Label1.Text));
            po.WOD = (WayOfDelivery) Convert.ToInt32(rblWOD.SelectedValue);
            ControllerManager.PurchaseOrder.SaveOrUpdate(po);
            Label2.Visible = true;
            LoadInformation(POId, POS);
            OnItemChanged(new ItemModificatedEventArgs(true));
            //chekear como hacerlo para q no relodee toda la pantalla
            //Response.Redirect("~/purchase-order/default.aspx");
        }

        protected void BorrarArchivoEsportacion()
        {
            try
            {
                FileInfo exportacion = new FileInfo(Server.MapPath("~/res/genocompra.prn"));
                if (exportacion.Exists)
                {
                    File.Delete(Server.MapPath("~/res/genocompra.prn"));
                }
            }

            catch (Exception ex)
            {

            }

        }

        protected void AgregarLineaArchivo(PurchaseOrderItem poi, int numeroOrden, int linea)
        {
            int delay = 0;
            int wod = 0;
            switch (poi.PurchaseOrder.WOD)
            {
                case WayOfDelivery.Maritimo:
                    delay = 44;
                    wod = 1;
                    break;
                case WayOfDelivery.Aereo:
                    delay = 12;
                    wod = 2;
                    break;
                case WayOfDelivery.Courrier:
                    delay = 4;
                    wod = 3;
                    break;
            }

            FileStream objFile = new FileStream(Server.MapPath("~/res/genocompra.prn"), FileMode.Append);

            StreamWriter objWriter = new StreamWriter(objFile, Encoding.Default);

            int tipoorden = 1;

            objWriter.WriteLine(Left("0000000000", 10 - numeroOrden.ToString().Length) + numeroOrden.ToString() + tipoorden + Left("000000", 6 - linea.ToString().Length) + linea.ToString() + poi.Product.ProductCode +
                                Right("                                  0000000",
                                      41 - poi.Product.ProductCode.ToString().Length) +
                                Left("00000000", 8 - poi.Quantity.ToString().Length) +
                                poi.Quantity +
                                Left("00000000000",
                                     11 - poi.PurchaseOrder.Provider.ProviderCode.ToString().Length) +
                                poi.PurchaseOrder.Provider.ProviderCode + "    " +
                                Config.CurrentDate.ToString("ddMMyyyy") +
                                Config.CurrentDate.AddDays(delay).ToString("ddMMyyyy") +
                                "0" + wod + "01");

            objWriter.Flush();
            objWriter.Close();
            objFile.Close();
        }
        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string eventArgument)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    
}