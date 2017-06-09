using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using Grundfos.StockForecast.Templates;
using PartnerNet.Business;
using PartnerNet.Common;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.purchase_order
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole(UserType.Lectores.ToString()))
            {
                Table1.Visible = false;
            }
            txtCodigoOc.Focus();
            lblCompraError.Visible = false;
            Pager1.PageChanged += Pager1_PageChanged;
            Pager2.PageChanged += Pager1_PageChanged;
            

            if (!IsPostBack)
            {
                Cargar_Estados();
                Cargar_Proveedores();
                txtFechaDesde.Text = Config.CurrentDate.AddDays(-7).ToShortDateString();
                txtFechaHasta.Text = Config.CurrentDate.ToShortDateString();
                Cargar_PrimeraBusqueda();
                Session["RadioBtn"] = 0;
                ViewState["page"] = "1";
            }
            Form.DefaultButton = btnBuscar.UniqueID;
        }

        protected void repItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            if (Roles.IsUserInRole(UserType.Lectores.ToString()))
            {
                CheckBox cbtemp = (CheckBox)e.Item.FindControl("chkbItemStatus");
                cbtemp.Enabled = false;
            }

            PurchaseOrderInformation po = (PurchaseOrderInformation)e.Item.DataItem;

            DetalleOC det = (DetalleOC)e.Item.FindControl("ucDetalle");
            det.POId = po.Id;
            det.POS = Convert.ToInt32(ddlEstados.SelectedValue);

        }

        protected void Cargar_Busqueda()
        {
            DisplayInfo(10, Convert.ToInt32(ViewState["page"]));
        }

        protected void Cargar_PrimeraBusqueda()
        {
            DisplayInfo(10, 1);
        }

        protected void Cargar_Estados()
        {
            IList status = EnumHelper.GetList(typeof(PurchaseOrderStatus));
            for (int cont = 0; cont < status.Count; cont++)
            {
                ListItem LI = new ListItem(EnumHelper.GetDescription((PurchaseOrderStatus)cont), cont.ToString());
                ddlEstados.Items.Add(LI);
            }
        }

        protected void Cargar_Proveedores()
        {
            IList<Provider> providers = ControllerManager.Provider.GetProviderList();
            if (providers != null)
            {

                foreach (Provider prov in providers)
                {
                    ListItem LI = new ListItem(prov.Name, prov.Id.ToString());
                    ddlProveedor.Items.Add(LI);
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args.Value != "" || (args.Value == "" && (Convert.ToInt32(ddlProveedor.SelectedValue) > 0 || Convert.ToInt32(ddlEstados.SelectedValue) > 0)));

        }

        protected void lnbMarcarTodos_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rep in repItems.Items)
            {
                ((CheckBox)rep.FindControl("chkbItemStatus")).Checked = true;
            }
        }

        protected void lnbDesamarcarTodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rep in repItems.Items)
            {
                ((CheckBox)rep.FindControl("chkbItemStatus")).Checked = false;
            }
        }

        protected void repItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {


            if (e.CommandName == "Expand")
            {
                DetalleOC det = (DetalleOC)e.Item.FindControl("ucDetalle");
                det.Visible = true;
                det.LoadInformation();
            }
        }

        private void Pager1_PageChanged(object sender, Controls_Pager.PageChangedEventArgs e)
        {
            DisplayInfo(10, e.NewPageNumber);
        }

        public void ucDetalle_ItemChanged(object sender, Grundfos.StockForecast.Templates.ItemModificatedEventArgs e)
        {
            Page.Validate();
            if (!IsValid)
                return;

            Cargar_Busqueda();
        }

        private void DisplayInfo(int pageSize, int currentPage)
        {
            txtCodigoOc.Text = txtCodigoOc.Text.Trim();
            int cod = 0;
            DateTime date = new DateTime();
            DateTime dateEnd = new DateTime();
            if (txtCodigoOc.Text != "")
                cod = Convert.ToInt32(txtCodigoOc.Text);
            if (txtFechaDesde.Text != "")
                date = Convert.ToDateTime(txtFechaDesde.Text);
            else date = Convert.ToDateTime("01/01/1900");
            if (txtFechaHasta.Text != "")
                dateEnd = Convert.ToDateTime(txtFechaHasta.Text);
            else dateEnd = Convert.ToDateTime("01/01/1900");

            int totalCount;

            IList<PurchaseOrderInformation> poinfo = ControllerManager.PurchaseOrder.GetPurchaseOrdersBetweenDates(cod, date, dateEnd, Convert.ToInt32(ddlProveedor.SelectedValue), Convert.ToInt32(ddlEstados.SelectedValue), 0, 0, Convert.ToInt32(ddlOrigen.SelectedValue), out totalCount);
            
            if (totalCount == 0)
                totalCount = pageSize;
            
            Pager1.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalCount) / Convert.ToDouble(pageSize)));
            Pager2.PageCount = Pager1.PageCount;
            Pager1.CurrentPage = currentPage;
            Pager2.CurrentPage = Pager1.CurrentPage;
            repItems.DataSource = poinfo;
            repItems.DataBind();
            

            Pager1.Step = 4;
            Pager2.Step = 4;
            Pager1.DataBind();
            Pager2.DataBind();

            if (poinfo.Count == 0)
            {
                lblBuscarError.Visible = true;
                repItems.Visible = false;
            }
            else
            {
                lblBuscarError.Visible = false;
                repItems.Visible = true;
            }

        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid)
                return;

            //ViewState["page"] = "1";
            Cargar_Busqueda();
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            IList<PurchaseOrder> selectedpo = new List<PurchaseOrder>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkbItemStatus")).Checked)
                {
                    PurchaseOrder po = ControllerManager.PurchaseOrder.GetById(Convert.ToInt32(((Label)rep.FindControl("lblCodigoOC")).Text));
                    selectedpo.Add(po);
                }
            }
            if (selectedpo.Count > 0)
            {
                ControllerManager.PurchaseOrder.ChangeStatus(selectedpo, (PurchaseOrderStatus)3);
                Cargar_Busqueda();
            }
            else
            {
                lblCompraError.Visible = true;
                lblCompraError.Text = "Por favor seleccione al menos 1 orden de compra!";
            }
        }

        protected void btnExportarOc_Click(object sender, ImageClickEventArgs e)
        {
            IList<PurchaseOrder> selectedpo = new List<PurchaseOrder>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkbItemStatus")).Checked)
                {
                    PurchaseOrder po = ControllerManager.PurchaseOrder.GetById(Convert.ToInt32(((Label)rep.FindControl("lblCodigoOC")).Text));
                    selectedpo.Add(po);
                }
            }
            if (selectedpo.Count > 0)
            {
                BorrarArchivoEsportacion();
                int numordcomp = Convert.ToInt32((ControllerManager.PurchaseOrderViewScala.GetLastPurchaseOrder()).PurchaseOrderCode);
              
                foreach (PurchaseOrder order in selectedpo)
                {
                   
                    if ((order.PurchaseOrderStatus == PurchaseOrderStatus.Open)||(order.PurchaseOrderStatus == PurchaseOrderStatus.Processed))
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
                Cargar_Busqueda();
                if (File.Exists(Server.MapPath("~/res/genocompra.prn")))
                {
                    Response.Clear();
                    Response.AppendHeader("content-disposition", "attachment; filename=genocompra.prn");
                    Response.WriteFile(Server.MapPath("~/res/genocompra.prn"));
                    Response.End();
                }
                else
                {
                    lblCompraError.Visible = true;
                    lblCompraError.Text = "No se pueden exportar ordenes de compras canceladas!";
                }
            }
            else
            {
                lblCompraError.Visible = true;
                lblCompraError.Text = "Por favor seleccione al menos 1 orden de compra!";
            }
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

        protected void btnImprimirOc_Click(object sender, ImageClickEventArgs e)
        {
            IList<PurchaseOrderInformation> poinfo = new List<PurchaseOrderInformation>();
            int totalcount = 0;
            txtCodigoOc.Text = txtCodigoOc.Text.Trim();

            DateTime date = Convert.ToDateTime("01/01/1900");
            DateTime dateEnd = Convert.ToDateTime("01/01/1900");

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkbItemStatus")).Checked)
                {
                    IList<PurchaseOrderInformation> poii = ControllerManager.PurchaseOrder.GetPurchaseOrdersBetweenDates(Convert.ToInt32(((Label)rep.FindControl("lblCodigoOC")).Text), date, dateEnd, Convert.ToInt32(ddlProveedor.SelectedValue), Convert.ToInt32(ddlEstados.SelectedValue), 0, 0, Convert.ToInt32(ddlOrigen.SelectedValue), out totalcount);

                    poinfo.Add(poii[0]);
                }
            }
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
                 
                    foreach (PurchaseOrderItem item in poi)
                    {
                        Grundfos.ScalaConnector.Product prodscala = Grundfos.ScalaConnector.ControllerManager.Product.GetProductInfo(item.Product.ProductCode);

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
                Response.Write("<script>");
                Response.Write("window.open('/purchase-order/report.aspx','_blank')");
                Response.Write("</script>"); 
                //Response.Redirect("/purchase-order/report.aspx");
            }
            else
            {
                lblCompraError.Visible = true;
                lblCompraError.Text = "Por favor seleccione al menos 1 orden de compra!";
            }
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
            switch(poi.PurchaseOrder.WOD)
            {
                case WayOfDelivery.Maritimo:
                    delay = 7 * poi.Product.LeadTime;
                    wod = 1;
                    break;
                case WayOfDelivery.Aereo:
                    delay = 15;
                    wod = 2;
                    break;
                case WayOfDelivery.Courrier:
                    delay = 7;
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
                                Left("000000",
                                     6 - poi.PurchaseOrder.Provider.ProviderCode.ToString().Length) +
                                poi.PurchaseOrder.Provider.ProviderCode + "    " +
                                Config.CurrentDate.ToString("ddMMyyyy") +
                                Config.CurrentDate.AddDays(delay).ToString("ddMMyyyy") +
                                "0" + wod + "01" + "    " + "01");

            objWriter.Flush();
            objWriter.Close();
            objFile.Close();
        }

    }
}
