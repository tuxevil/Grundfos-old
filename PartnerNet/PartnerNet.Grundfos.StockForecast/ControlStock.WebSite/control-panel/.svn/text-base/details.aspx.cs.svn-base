using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PartnerNet.Business;
using PartnerNet.Domain;
using System.Data.SqlClient;
using System.Drawing;

namespace Grundfos.StockForecast.control_panel
{
    public partial class details : System.Web.UI.Page
    {
        public string OrderField
        {
            get
            {
                if (ViewState["OrderField"] != null)
                    return ViewState["OrderField"].ToString();
                return string.Empty;
            }
            set { ViewState["OrderField"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";
            }

            if (Request.QueryString["alert"] != null)
            {
                List<Provider> providers = ControllerManager.Provider.GetFullProviderList();
                switch(Request.QueryString["alert"])
                {
                    case "1":
                        {
                            lblAlertName.Text = "OC Confirmadas y No Entregadas";
                            List<AlertPurchaseOrder> lstConfNoEntregadas = ControllerManager.AlertPurchaseOrder.ShowAlert1();
                            foreach (AlertPurchaseOrder order in lstConfNoEntregadas)
                            {
                                Provider provtemp = providers.Find(delegate(Provider record)
                                                                             {
                                                                                 if (record.ProviderCode == order.PurchaseOrderProviderCode)
                                                                                 {
                                                                                     return true;
                                                                                 }
                                                                                 return false;
                                                                             });
                                if (provtemp != null)
                                    order.PurchaseOrderProviderName = provtemp.Name;
                            }
                            GVOcConfirmadasNoEntregadas.DataSource = lstConfNoEntregadas;
                            GVOcConfirmadasNoEntregadas.DataBind();
                            GVOcConfirmadasNoEntregadas.Enabled = true;
                            btnExportToExcel.Visible = (lstConfNoEntregadas.Count > 0);
                            break;
                        }
                    case "2":
                        {
                            lblAlertName.Text = "OC No Confirmadas";
                            List<AlertPurchaseOrder> lstNoConfirmadas = ControllerManager.AlertPurchaseOrder.ShowAlert2();
                            foreach (AlertPurchaseOrder order in lstNoConfirmadas)
                            {
                                Provider provtemp = providers.Find(delegate(Provider record)
                                                                             {
                                                                                 if (record.ProviderCode == order.PurchaseOrderProviderCode)
                                                                                 {
                                                                                     return true;
                                                                                 }
                                                                                 return false;
                                                                             });
                                if (provtemp != null)
                                    order.PurchaseOrderProviderName = provtemp.Name;
                            }
                            GVOcNoConfirmadas.DataSource = lstNoConfirmadas;
                            GVOcNoConfirmadas.DataBind();
                            GVOcNoConfirmadas.Enabled = true;
                            btnExportToExcel.Visible = (lstNoConfirmadas.Count > 0);
                            break;
                        }
                    case "3":
                        {
                            lblAlertName.Text = "Productos con Stock Negativo";
                            List<AlertProduct> lstStockNegativo = ControllerManager.AlertProduct.ShowAlert3();
                            GVStockNegativo.DataSource = lstStockNegativo;
                            GVStockNegativo.DataBind();
                            GVStockNegativo.Enabled = true;
                            btnExportToExcel.Visible = (lstStockNegativo.Count > 0);
                            break;
                        }
                    case "4":
                        {
                            lblAlertName.Text = "OV no Cumplimentadas";
                            List<AlertSaleOrder> lstNoCumplimentadas = ControllerManager.AlertSaleOrder.ShowAlert4();
                            GVNoCumplimentadas.DataSource = lstNoCumplimentadas;
                            GVNoCumplimentadas.DataBind();
                            GVNoCumplimentadas.Enabled = true;
                            btnExportToExcel.Visible = (lstNoCumplimentadas.Count > 0);
                            break;
                        }
                    case "5":
                        {
                            lblAlertName.Text = "Stock que caera del Safety";
                            List<AlertProduct> lstStockMenorSafety = ControllerManager.AlertProduct.ShowAlert5();
                            GVStockMenorSafety.DataSource = lstStockMenorSafety;
                            GVStockMenorSafety.DataBind();
                            GVStockMenorSafety.Enabled = true;
                            btnExportToExcel.Visible = (lstStockMenorSafety.Count > 0);
                            break;
                        }
                    case "6":
                        {
                            lblAlertName.Text = "Diferencia en Nivel de Reposición";
                            List<AlertReposition> lstReposicionDife = ControllerManager.AlertReposition.ShowAlert(OrderField, sortOrder);
                            GVReposicionDiferente.DataSource = lstReposicionDife;
                            GVReposicionDiferente.DataBind();
                            GVReposicionDiferente.Enabled = true;
                            btnExportToExcel.Visible = (lstReposicionDife.Count > 0);
                            break;
                        }
                }
            }
        }

        #region SortingGVOcConfirmadasNoEntregadas
        protected void GVOcConfirmadasNoEntregadas_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (OrderField == e.SortExpression)
                ChangeOrder();
            OrderField = e.SortExpression;
            List<Provider> providers = ControllerManager.Provider.GetFullProviderList();
            List<AlertPurchaseOrder> lstConfNoEntregadas = ControllerManager.AlertPurchaseOrder.ShowAlert(OrderField, sortOrder, 1);
            foreach (AlertPurchaseOrder order in lstConfNoEntregadas)
            {
                Provider provtemp = providers.Find(delegate(Provider record)
                                                             {
                                                                 if (record.ProviderCode == order.PurchaseOrderProviderCode)
                                                                 {
                                                                     return true;
                                                                 }
                                                                 return false;
                                                             });
                if (provtemp != null)
                    order.PurchaseOrderProviderName = provtemp.Name;
            }
            GVOcConfirmadasNoEntregadas.DataSource = lstConfNoEntregadas;
            GVOcConfirmadasNoEntregadas.DataBind();
            GVOcConfirmadasNoEntregadas.Enabled = true;
        }
        #endregion

        #region SortingGVOcNoConfirmadas
        protected void GVOcNoConfirmadas_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (OrderField == e.SortExpression)
                ChangeOrder();
            OrderField = e.SortExpression;
            List<Provider> providers = ControllerManager.Provider.GetFullProviderList();
            List<AlertPurchaseOrder> lstNoConfirmadas = ControllerManager.AlertPurchaseOrder.ShowAlert(OrderField, sortOrder, 2);
            foreach (AlertPurchaseOrder order in lstNoConfirmadas)
            {
                Provider provtemp = providers.Find(delegate(Provider record)
                                                             {
                                                                 if (record.ProviderCode == order.PurchaseOrderProviderCode)
                                                                 {
                                                                     return true;
                                                                 }
                                                                 return false;
                                                             });
                if (provtemp != null)
                    order.PurchaseOrderProviderName = provtemp.Name;
            }
            GVOcNoConfirmadas.DataSource = lstNoConfirmadas;
            GVOcNoConfirmadas.DataBind();
            GVOcNoConfirmadas.Enabled = true;
        }
        #endregion

        #region SortingGVStockNegativo
        protected void GVStockNegativo_Sorting(object sender, GridViewSortEventArgs e)
         {
             if (OrderField == e.SortExpression)
                 ChangeOrder();
             OrderField = e.SortExpression;
             List<AlertProduct> lstStockNegativo = ControllerManager.AlertProduct.ShowAlert(OrderField, sortOrder, 1);
             GVStockNegativo.DataSource = lstStockNegativo;
             GVStockNegativo.DataBind();
             GVStockNegativo.Enabled = true;
          }
        #endregion

        #region SortingGVNoCumplimentadas
        protected void GVNoCumplimentadas_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (OrderField == e.SortExpression)
                ChangeOrder();
            OrderField = e.SortExpression;
            List<AlertSaleOrder> lstNoCumplimentadas = ControllerManager.AlertSaleOrder.ShowAlert(OrderField, sortOrder);
            GVNoCumplimentadas.DataSource = lstNoCumplimentadas;
            GVNoCumplimentadas.DataBind();
            GVNoCumplimentadas.Enabled = true;
        }

        #endregion

        #region SortingGVStockMenorSafety
        protected void GVStockMenorSafety_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (OrderField == e.SortExpression)
                ChangeOrder();
            OrderField = e.SortExpression;
            List<AlertProduct> lstStockMenorSafety = ControllerManager.AlertProduct.ShowAlert(OrderField, sortOrder, 2);
            GVStockMenorSafety.DataSource = lstStockMenorSafety;
            GVStockMenorSafety.DataBind();
            GVStockMenorSafety.Enabled = true;
        }
        #endregion

        #region SortingGVReposicionDiferente
        
        protected void GVReposicionDiferente_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (OrderField == e.SortExpression)
                ChangeOrder();
            OrderField = e.SortExpression;
            List<AlertReposition> lstReposicionDife = ControllerManager.AlertReposition.ShowAlert(OrderField, sortOrder);
            GVReposicionDiferente.DataSource = lstReposicionDife;
            GVReposicionDiferente.DataBind();
            GVReposicionDiferente.Enabled = true;
        }

        #endregion

        #region DataGridSortOrder
        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"] == null)
                    ViewState["sortOrder"] = "asc";
                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        public void ChangeOrder()
        {
            if (ViewState["sortOrder"].ToString() == "desc")
                ViewState["sortOrder"] = "asc";
            else
                ViewState["sortOrder"] = "desc";
        }
        #endregion

        #region IndexChanged

        protected void GVOcConfirmadasNoEntregadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVOcConfirmadasNoEntregadas.PageIndex = e.NewPageIndex;
            GVOcConfirmadasNoEntregadas.DataBind();
        }

        protected void GVOcNoConfirmadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVOcNoConfirmadas.PageIndex = e.NewPageIndex;
            GVOcNoConfirmadas.DataBind();
        }

        protected void GVStockNegativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVStockNegativo.PageIndex = e.NewPageIndex;
            GVStockNegativo.DataBind();
        }

        protected void GVNoCumplimentadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVNoCumplimentadas.PageIndex = e.NewPageIndex;
            GVNoCumplimentadas.DataBind();
        }

        protected void GVStockMenorSafety_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVStockMenorSafety.PageIndex = e.NewPageIndex;
            GVStockMenorSafety.DataBind();
        }

        protected void GVReposicionDiferente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVReposicionDiferente.PageIndex = e.NewPageIndex;
            GVReposicionDiferente.DataBind();
        }
        #endregion

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            switch (Request.QueryString["alert"])
            {
                case "1":
                    ControllerManager.AlertPurchaseOrder.ExportToExcel(OrderField, sortOrder, 1);
                    break;
                case "2":
                    ControllerManager.AlertPurchaseOrder.ExportToExcel(OrderField, sortOrder, 2);
                    break;
                case "3":
                    ControllerManager.AlertProduct.ExportToExcel(OrderField, sortOrder, 1);
                    break;
                case "4":
                    ControllerManager.AlertSaleOrder.ExportToExcel(OrderField, sortOrder);
                    break;
                case "5":
                    ControllerManager.AlertProduct.ExportToExcel(OrderField, sortOrder, 2);
                    break;
                case "6":
                    ControllerManager.AlertReposition.ExportToExcel(OrderField, sortOrder);
                    break;
            }

        }

        protected void GVReposicionDiferente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                AlertReposition ar = (e.Row.DataItem as AlertReposition);
                if (ar != null)
                {
                    if (ar.IsConflicted)
                        e.Row.BackColor = Color.FromArgb(250, 128, 114);
                }
            }
        }

        
        
    }
}
