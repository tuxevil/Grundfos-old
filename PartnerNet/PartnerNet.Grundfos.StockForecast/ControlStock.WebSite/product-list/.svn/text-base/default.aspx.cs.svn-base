using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Grundfos.StockForecast.Templates;
using PartnerNet.Business;
using PartnerNet.Common;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.product_list
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole(UserType.Lectores.ToString()))
            {
                Table1.Visible = false;
            }
            txtDescripcion.Focus();
            lblErrorProducto.Visible = false;
            Pager1.PageChanged += Pager1_PageChanged;
            Pager2.PageChanged += Pager1_PageChanged;
            if (!IsPostBack)
            {
                Cargar_Grupo();
                Cargar_Selecciones();
                Cargar_Proveedores();
                if (Request.QueryString["productcode"] != null)
                {
                    Page.Validate();
                    if (!IsValid)
                        return;

                    txtDescripcion.Text = Request.QueryString["productcode"];
                    ViewState["page"] = "1";
                    Cargar_Busqueda();
                }
                else
                {
                    Page.Validate();
                    if (!IsValid)
                        return;

                    ViewState["page"] = "1";
                    Cargar_Busqueda();
                }
            }
            Form.DefaultButton = btnBuscar.UniqueID;
            
        }

        protected void repItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            if(Roles.IsUserInRole(UserType.Lectores.ToString()))
            {
                TextBox tbtemp = (TextBox)e.Item.FindControl("txtPuntoReposicion");
                tbtemp.Enabled = false;
                tbtemp = (TextBox)e.Item.FindControl("txtLeadTime");
                tbtemp.Enabled = false;
                tbtemp = (TextBox)e.Item.FindControl("txtSafety");
                tbtemp.Enabled = false;
                ImageButton imtemp = (ImageButton) e.Item.FindControl("btnGuardarInd");
                imtemp.Enabled = false;
                CheckBox cbtemp = (CheckBox)e.Item.FindControl("chkGuardar");
                cbtemp.Enabled = false;
            }


            ProductInformation pi = (ProductInformation)e.Item.DataItem;

            Detalle det = (Detalle)e.Item.FindControl("ucDetalle");
            det.ProductId = pi.Id;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            IList<Product> selectedproduct = new List<Product>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkGuardar")).Checked)
                {
                    Product prod = ControllerManager.Product.GetProductInfo(((Label)rep.FindControl("lblCodigo")).Text);
                    selectedproduct.Add(prod);
                }
            }
            if (selectedproduct.Count > 0 && txtChangeSafety.Text != "")
            {
                ControllerManager.Product.UpdateProductSafety(selectedproduct, Convert.ToInt32(txtChangeSafety.Text));
            }
            else
            {
                lblErrorProducto.Visible = true;
            }
            Cargar_Busqueda();
        }

        protected void btnAceptarNueva_Click(object sender, EventArgs e)
        {
            string selection = ddlSeleccionPop.SelectedValue;
            if (txtNuevaPop.Text != "")
            {
                ControllerManager.ProductSet.CreateNewProductSet(txtNuevaPop.Text);
                IList<ProductSet> selections = ControllerManager.ProductSet.GetProductSetList();

                if (selections != null)
                {
                    ddlSeleccion.Items.Clear();
                    ddlSeleccionPop.Items.Clear();
                    ddlQuitarSeleccion.Items.Clear();
                    ListItem NA = new ListItem("--Selección--", "0");
                    ddlSeleccion.Items.Add(NA);
                    ddlSeleccionPop.Items.Add(NA);
                    ddlQuitarSeleccion.Items.Add(NA);
                    foreach (ProductSet sel in selections)
                    {
                        ListItem selectionlist = new ListItem(sel.Name, sel.Id.ToString());
                        ddlSeleccion.Items.Add(selectionlist);
                        ddlSeleccionPop.Items.Add(selectionlist);
                        ddlQuitarSeleccion.Items.Add(selectionlist);
                        if (sel.Name == txtNuevaPop.Text)
                            selection = sel.Id.ToString();
                    }
                }
            }

            IList<Product> selectedproduct = new List<Product>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkGuardar")).Checked)
                {
                    Product prod = ControllerManager.Product.GetProductInfo(((Label)rep.FindControl("lblCodigo")).Text);
                    selectedproduct.Add(prod);
                }
            }
            if (selectedproduct.Count > 0)
            {
                ControllerManager.ProductSet.AddProductToProductSet(Convert.ToInt32(selection), selectedproduct);

                Cargar_Busqueda();
                txtNuevaPop.Text = "";
            }
            else
            {
                lblErrorProducto.Visible = true;
                txtNuevaPop.Text = "";
            }
        }

        protected void btnAceptarQuitarSeleccion_Click(object sender, EventArgs e)
        {
            IList<Product> selectedproduct = new List<Product>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkGuardar")).Checked)
                {
                    Product prod = ControllerManager.Product.GetProductInfo(((Label)rep.FindControl("lblCodigo")).Text);
                    selectedproduct.Add(prod);
                }
            }
            if (selectedproduct.Count > 0)
            {
                ControllerManager.ProductSet.DelProductFromProductSet(Convert.ToInt32(ddlQuitarSeleccion.SelectedValue), selectedproduct);
                Cargar_Busqueda();
            }
            else
            {
                lblErrorProducto.Visible = true;
            }
        }

        protected void Cargar_Busqueda()
        {
            DisplayInfo(10, Convert.ToInt32(ViewState["page"]));
        }

        protected void Cargar_Grupo()
        {
            IList<string> groups = ControllerManager.Product.GetGroups();
            if (groups != null)
            {
                foreach (string group in groups)
                {
                    ddlCategoria.Items.Add(group);
                }
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
                    ddlEstado.Items.Add(LI);
                }
            }
        }

        protected void Cargar_Selecciones()
        {
            IList<ProductSet> selections = ControllerManager.ProductSet.GetProductSetList();

            if (selections != null)
            {
                ddlSeleccion.Items.Clear();
                ddlSeleccionPop.Items.Clear();
                ddlQuitarSeleccion.Items.Clear();
                ListItem NA = new ListItem("--Selección--", "0");
                ddlSeleccion.Items.Add(NA);
                ddlSeleccionPop.Items.Add(NA);
                ddlQuitarSeleccion.Items.Add(NA);
                foreach (ProductSet sel in selections)
                {
                    ListItem selectionlist = new ListItem(sel.Name, sel.Id.ToString());
                    ddlSeleccion.Items.Add(selectionlist);
                    ddlSeleccionPop.Items.Add(selectionlist);
                    ddlQuitarSeleccion.Items.Add(selectionlist);
                }
            }
        }

        protected void validatorDesc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args.Value != "" || (args.Value == "" && (Convert.ToInt32(ddlEstado.SelectedValue) > 0 || Convert.ToInt32(ddlCategoria.SelectedValue) > 0 || Convert.ToInt32(ddlSeleccion.SelectedValue) > 0)));

        }

        protected void lnkMarcarTodos_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rep in repItems.Items)
            {
                ((CheckBox)rep.FindControl("chkGuardar")).Checked = true;
            }
        }

        protected void lnkDesmarcarTodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rep in repItems.Items)
            {
                ((CheckBox)rep.FindControl("chkGuardar")).Checked = false;
            }
        }

        protected void repItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {


            if (e.CommandName == "Expand")
            {
                Detalle det = (Detalle)e.Item.FindControl("ucDetalle");
                det.Visible = true;
                det.LoadInformation();
            }
            else if (e.CommandName == "Save")
            {
                IList<Product> selectedproduct = new List<Product>();
                Product prod = ControllerManager.Product.GetById(Convert.ToInt32(e.CommandArgument));
                selectedproduct.Add(prod);

                ControllerManager.Product.UpdateProductSafety(selectedproduct, Convert.ToInt32(((TextBox)e.Item.FindControl("txtSafety")).Text));
                ControllerManager.Product.UpdateProductLeadTime(selectedproduct, Convert.ToInt32(((TextBox)e.Item.FindControl("txtLeadTime")).Text));
                ControllerManager.Product.UpdateProductRepositionPoint(selectedproduct, Convert.ToInt32(((TextBox)e.Item.FindControl("txtPuntoReposicion")).Text));
            }
        }

        private void Pager1_PageChanged(object sender, Controls_Pager.PageChangedEventArgs e)
        {
            DisplayInfo(10, e.NewPageNumber);
        }

        private void DisplayInfo(int pageSize, int currentPage)
        {
            txtDescripcion.Text = txtDescripcion.Text.Trim();
            int totalCount;
            IList<ProductInformation> lst = ControllerManager.Product.GetProductInformation(txtDescripcion.Text, ddlCategoria.SelectedValue, Convert.ToInt32(ddlSeleccion.SelectedValue), Convert.ToInt32(ddlEstado.SelectedValue), currentPage, pageSize, out totalCount, chbViejos.Checked);

            if (totalCount == 0)
                totalCount = pageSize;

            Pager1.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalCount) / Convert.ToDouble(pageSize)));
            Pager2.PageCount = Pager1.PageCount;
            Pager1.CurrentPage = currentPage;
            Pager2.CurrentPage = Pager1.CurrentPage;
            
            repItems.DataSource = lst;
            repItems.DataBind();

            Pager1.Step = 10;
            Pager2.Step = 10;
            Pager1.DataBind();
            Pager2.DataBind();
            if (lst.Count == 0)
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

            ViewState["page"] = "1";
            Cargar_Busqueda();
        }

        protected void btnGenerarOC_Click(object sender, ImageClickEventArgs e)
        {
            IList<Product> selectedproduct = new List<Product>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkGuardar")).Checked)
                {
                    Product prod = ControllerManager.Product.GetProductInfo(((Label)rep.FindControl("lblCodigo")).Text);
                    selectedproduct.Add(prod);
                }
            }
            if (selectedproduct.Count > 0)
            {
                foreach (Product prod in selectedproduct)
                    ControllerManager.PurchaseOrder.GeneratePO(prod, Config.CurrentWeek + 1, Config.CurrentDate.Year, PurchaseOrderType.Manual, prod.RepositionPoint);
                //cambiar la semana, quitarle el +1, es solo para pruebas
            }

        }

        protected void txtDescripcion_DataBinding(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            ViewState["page"] = "1";
            Cargar_Busqueda();
        }

        protected void btnImprimirLP_Click(object sender, ImageClickEventArgs e)
        {
            IList<Product> selectedproduct = new List<Product>();

            foreach (RepeaterItem rep in repItems.Items)
            {
                if (((CheckBox)rep.FindControl("chkGuardar")).Checked)
                {
                    Product prod = ControllerManager.Product.GetProductInfo(((Label)rep.FindControl("lblCodigo")).Text);
                    selectedproduct.Add(prod);
                }
            }
            if (selectedproduct.Count > 0)
            {
                DataSet objDataSet = new DataSet();
                DataTable objProdList = new DataTable("rptProdList");

                objProdList.Columns.Add("Codigo");
                objProdList.Columns.Add("Descripcion");
                objProdList.Columns.Add("Proveedor");
                objProdList.Columns.Add("Stock");
                objProdList.Columns.Add("NivelRep");
                objProdList.Columns.Add("ModuloComp");
                objProdList.Columns.Add("PromVentas");
                objProdList.Columns.Add("LeadTime");
                objProdList.Columns.Add("Safety");

                objProdList.Columns.Add("PrecioCompra");
                objProdList.Columns.Add("SobreCostos");
                objProdList.Columns.Add("PrecioVenta");
                objProdList.Columns.Add("VentaAnual");
                objProdList.Columns.Add("VentaSemestral");
                objProdList.Columns.Add("VentaTrimestral");
                objProdList.Columns.Add("VentaMensual");
                objProdList.Columns.Add("PromedioAnual");
                objProdList.Columns.Add("PromedioSemestral");
                objProdList.Columns.Add("PromedioTrimestral");
                objProdList.Columns.Add("PromedioMensual");

                foreach (Product prod in selectedproduct)
                {
                    int cuenta;
                    ProductInformation prodinfo = ControllerManager.Product.GetProductInformation(prod.ProductCode, "N/A", 0, 0, 0, 0, out cuenta, chbViejos.Checked)[0];
                    TransactionHistoryWeekly tran = ControllerManager.TransactionHistoryWeekly.GetIndividualInfo(prod.Id, Config.CurrentWeek, Config.CurrentDate.Year);
                    Grundfos.ScalaConnector.Product prodscala = Grundfos.ScalaConnector.ControllerManager.Product.GetProductInfo(prod.ProductCode);
                    IList<TransactionHistoryWeekly> sales = ControllerManager.TransactionHistoryWeekly.GetSalesTotal(prod, Config.CurrentWeek, Config.CurrentDate.Year);
                    IList<ProductStatisticWeekly> fullstats = ControllerManager.ProductStatisticWeekly.GetProductFullInfo(prod.Id);

                    DataRow myRow;
                    myRow = objProdList.NewRow();
                    myRow[0] = prodinfo.ProductCode;
                    myRow[1] = prodinfo.Description;
                    myRow[2] = prodinfo.Provider;
                    myRow[3] = prodinfo.Stock;
                    myRow[4] = prodinfo.RepositionLevel;
                    myRow[5] = prodinfo.RepositionPoint;
                    myRow[6] = prodinfo.Saleaverage;
                    myRow[7] = prodinfo.LeadTime;
                    myRow[8] = prodinfo.Safety;
                    myRow[9] = prodscala.PurchasePrice;
                    myRow[10] = prodscala.OverCost;
                    myRow[11] = prodscala.SalePrice;
                    myRow[12] = sales[3].Sale;
                    myRow[13] = sales[2].Sale;
                    myRow[14] = sales[1].Sale;
                    myRow[15] = sales[0].Sale;
                    myRow[16] = fullstats[4].Sale;
                    myRow[17] = fullstats[3].Sale;
                    myRow[18] = fullstats[2].Sale;
                    myRow[19] = fullstats[0].Sale;

                    objProdList.Rows.Add(myRow);
                }
                objDataSet.Tables.Add(objProdList);
                
                //objDataSet.Relations.Add("PO_POI", objDataSet.Tables["rptPO"].Columns["Codigo"], objDataSet.Tables["rptPOI"].Columns["CodOC"]);

                //objDataSet.WriteXmlSchema("c:/esquemaprod.xsd");
                //objDataSet.WriteXml("c:/dataset.xml");
                Session["dsListProductos"] = objDataSet;
                Response.Redirect("/product-list/report.aspx");
            }

            else
            {
                lblErrorProducto.Visible = true;
            }
        }


        protected void btnImprimirBusqueda_Click(object sender, EventArgs e)
        {
            List<ProductForExport> lst = ControllerManager.Product.GetProductForExport(txtDescripcion.Text, ddlCategoria.SelectedValue, Convert.ToInt32(ddlSeleccion.SelectedValue), Convert.ToInt32(ddlEstado.SelectedValue), chbViejos.Checked, Config.CurrentWeek, Config.CurrentDate.Year);
            if (lst.Count > 0)
            {
                Session["ProductForExport"] = lst;
                Response.Redirect("/product-list/export.aspx");
            }

            else
            {
                lblErrorProducto.Visible = true;
            }
        }


       
    }
}
