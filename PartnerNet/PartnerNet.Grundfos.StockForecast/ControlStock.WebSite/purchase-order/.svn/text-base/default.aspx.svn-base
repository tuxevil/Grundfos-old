<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Grundfos.StockForecast.purchase_order._default" Title="Ordenes de Compras Generadas" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="../Templates/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="../Templates/DetalleOC.ascx" TagName="Detalle" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div style="text-align: center">
        <table width="100%" border="0" cellspacing="7" cellpadding="0">
        <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr class="fdo_tit">
                <td>
                    <asp:Label ID="lblOrdenesCompras" runat="server" CssClass="text2" Font-Bold="true" Font-Size="Medium" Text="Ordenes de Compras Generadas"></asp:Label></td>
                </tr>
                <tr bgcolor="#e3e3e3">
                <td align="center" valign="middle">
                <table width="90%" border="0" cellspacing="3" cellpadding="0">
                    <tr>
                        <td align="right" class="text1" style="height: 24px; width: 90px;">
                            Código OC.</td>
                        <td class="text1" style="height: 24px"><asp:TextBox style="Z-INDEX: 102;" id="txtCodigoOc" runat="server" Font-Size="Small" Width="235px" MaxLength="5"></asp:TextBox></td>
                        <td align="right" style="height: 24px; width: 150px;"><span class="text1">Fecha desde OC.&nbsp;</span></td>
                        <td style="height: 24px"><asp:TextBox ID="txtFechaDesde" runat="server" Width="80px"></asp:TextBox></td>
                        
                        <td style="height: 24px; width: 60px;"></td>
                        <td colspan="2" rowspan="2">
                        <asp:Table ID="Table1" runat="server">
                        <asp:TableRow>
                        <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnExportarOc','','../images/btn_imp_oc_s2-07.gif',1)"><asp:ImageButton ID="btnExportarOc" runat="server" ImageUrl="../images/btn_imp_oc_s-07.gif" AlternateText="Generar Ordenes de Compra Seleccionadas" Width="131" Height="21" BorderWidth="0" OnClick="btnExportarOc_Click" /></a>
                        </asp:TableCell>
                        <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnImprimirOc','','../images/btn_imp_oc_s2.gif',1)"><asp:ImageButton ID="btnImprimirOc" runat="server" ImageUrl="../images/btn_imp_oc_s.gif" AlternateText="Imprimir Ordenes de Compra Seleccionadas" Width="131" Height="21" BorderWidth="0" OnClick="btnImprimirOc_Click" /></a>
                        </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                        <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnCancelar','','../images/btn_canc_oc_s2.gif',1)"><asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="../images/btn_canc_oc_s.gif" AlternateText="Cancelar Ordenes de Compra Seleccionadas" Width="131" Height="21" BorderWidth="0" OnClick="btnCancelar_Click" /></a>
                        </asp:TableCell>
                        </asp:TableRow>
                        
                            </asp:Table>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td align="right" class="text1" style="width: 90px">
                            Proveedor</td>
                        <td class="text1">
                        <asp:DropDownList style="Z-INDEX: 104;" id="ddlProveedor" runat="server" Font-Size="Small" Width="246px">
                            <asp:ListItem Value="0">--Proveedor--</asp:ListItem>
                        </asp:DropDownList></td>
                        <td align="right" style="height: 24px; width: 150px;"><span class="text1">Fecha hasta OC.&nbsp;</span></td>
                        <td style="height: 24px"><asp:TextBox ID="txtFechaHasta" runat="server" Width="80px"></asp:TextBox></td>
                        
                        
                        <td style="width: 60px"></td>
                    </tr>
                    
                    <tr>
                        <td align="right" class="text1" style="text-align: right">
                            Origen</td>
                        <td>
                            <asp:DropDownList ID="ddlOrigen" runat="server" Width="246px">
                                <asp:ListItem Value="0">Indistinto</asp:ListItem>
                                <asp:ListItem Value="1">Forecast</asp:ListItem>
                                <asp:ListItem Value="2">Manual</asp:ListItem>
                            </asp:DropDownList></td>
                        
                        <td align="right" style="width: 94px"><span class="text1">Estado</span></td>
                        <td><span class="text1">
                        <asp:DropDownList style="Z-INDEX: 108;" id="ddlEstados" runat="server" Font-Size="Small" Width="80px">
                            <asp:ListItem Value="0">--Estado--</asp:ListItem>
                        </asp:DropDownList>
                        </span></td>
                        
                    </tr>
                    
                    <tr>
                        <td align="right" class="text1" style="text-align: center;" colspan="2">
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCodigoOc"
                                CssClass="text2" ErrorMessage="Por favor, ingrese un número para buscar." MaximumValue="99999"
                                MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtCodigoOc"
                            ErrorMessage="Seleccione un Filtro!" Font-Bold="True" Font-Size="Small" OnServerValidate="CustomValidator1_ServerValidate" CssClass="text4"></asp:CustomValidator></td>
                        <td align="right" style="width: 94px">
                        </td>
                        
                        <td>
                            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="../images/btn_buscar.gif" AlternateText="Buscar" Width="59" Height="21" BorderWidth="0" OnClick="btnBuscar_Click" /></td>
                            <td style="width: 60px">
                            </td>
                        <td colspan="2"><asp:Label ID="lblCompraError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                            Text="Por favor seleccione al menos 1 orden de compra!"
                            Visible="False" CssClass="text2"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
                            <ajaxToolkit:CalendarExtender ID="CalendarDesde" runat="server" TargetControlID="txtFechaDesde" Format="MM/dd/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:CalendarExtender ID="CalendarHasta" runat="server" TargetControlID="txtFechaHasta" Format="MM/dd/yyyy">
                            </ajaxToolkit:CalendarExtender>
            </td>
            </tr>
            <tr>
            
            <td><img src="../images/fin_tabla_busq.gif" width="986" height="6" ></td>
            </tr>
        </table>
        <br/>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td class="none" align="center">
            
            <uc2:Pager ID="Pager2" runat="server" />
            
        </td>
        </tr>
        <tr>
        <td align="center"><img src="../images/linea_arriba.gif" width="819" height="5"><br>
            <br>
            <asp:Label ID="lblBuscarError" runat="server" CssClass="text4" Text="No se han encontrado resultados que cumplan con el criterio seleccionado"
                Visible="False"></asp:Label></td>
        </tr>
        <tr>
        <td><asp:ScriptManager ID="ScriptManager1" runat="server" /><asp:UpdatePanel ID="upOC" runat="server" ChildrenAsTriggers="true">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
        </Triggers>
        <ContentTemplate>
            <asp:Repeater ID="repItems" runat="server" OnItemCommand="repItems_ItemCommand" OnItemDataBound="repItems_ItemDataBound"  >
            <HeaderTemplate>
            <table border="0" cellspacing="1" cellpadding="2">
                <tr bgcolor="#0e4b88" class="text2">
                <td width=20></td>
                <td width=35>Cód</td>
                <td width=100>Fecha de OC</td>
                <td width=280>Proveedor</td>
                <td width=80>Cant. Art</td>
                <td width=100>Importe</td>
                <td width=100>Fecha Arribo</td>
                <td width=75>Envio</td>
                <td width=80>Origen</td>
                <td width=100><asp:LinkButton ID="lnbMarcarTodos" Text="Marcar Todos" OnClick="lnbMarcarTodos_Click" runat="server" CssClass="text2"></asp:LinkButton></td>
                </tr>
            </table>
            </HeaderTemplate>
            <ItemTemplate>
                    <table border="0" cellspacing="1" cellpadding="2">
                        <tr runat="server" id="rowLine" class="paginado">
                        <td width=20 align="center"><asp:ImageButton CommandName="Expand" ID="imgExpand" runat="server" imageurl="~/images/whiteExpandIcon.gif" /></td>               
                        <td width=35><asp:Label ID="lblCodigoOC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'></asp:Label></td>
                        <td width=100><%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Orderdate")).ToShortDateString()%></td>
                        <td width=280><%# DataBinder.Eval(Container, "DataItem.Provider") %></td>
                        <td width=80 align="right"><%# DataBinder.Eval(Container, "DataItem.Totalcount") %></td>
                        <td width=100 align="right"><%# DataBinder.Eval(Container, "DataItem.Currency").ToString() + " " + Convert.ToDouble(DataBinder.Eval(Container, "DataItem.Amount")).ToString("#,##0.00") %></td>
                        <td width=100><%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Arrivaldate")).ToShortDateString()%></td>
                        <td width=75><%# DataBinder.Eval(Container, "DataItem.WayOfDelivery") %></td>
                        <td width=80><%# DataBinder.Eval(Container, "DataItem.Type") %></td>
                        <td width=100><asp:CheckBox ID="chkbItemStatus" runat="server"></asp:CheckBox></td>
                        </tr>
                    </table> 
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="False" runat="server">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgExpand" />
                </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="C" runat="server" BackColor="#c7dbe9" >
                            <uc1:Detalle ID="ucDetalle" OnItemChanged="ucDetalle_ItemChanged" Visible="false" runat="server" />
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CPE" runat="server" TargetControlID="C"
                            ExpandControlID="imgExpand" CollapseControlID="imgExpand" Collapsed="True" ImageControlID="imgExpand"
                            ExpandedImage="~/Images/whiteCollapseIcon.gif"
                            CollapsedImage="~/Images/whiteExpandIcon.gif"
                        />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ItemTemplate>
            <FooterTemplate>
            <table>
                <tr>
                <td width=30></td>
                <td width=80></td>
                <td width=300></td>
                <td width=175></td>
                <td width=70></td>
                <td width=70></td>
                <td width=70></td>
                <td width=50></td>
                <td width=50></td>
                <td width=70></td>
                <td width=100 class="text2"><asp:LinkButton ID="lnbDesamarcarTodo" Text="Desmarcar Todos" OnClick="lnbDesamarcarTodo_Click" runat="server" CssClass="text2" ForeColor="blue"></asp:LinkButton></td>
                </tr>
            </table>
            </FooterTemplate>
            </asp:Repeater></ContentTemplate></asp:UpdatePanel>
        </td>
        </tr>
        <tr>
        <td align="center"><br>
            <img src="../images/linea_abajo.gif" width="819" height="5">
        </td>
        </tr>
        <tr>
        <td align="center">
            
        <uc2:Pager ID="Pager1" runat="server" Visible="true" />
        </td>
        </tr>
        </table>
    <p>&nbsp;</p></td>
    </tr>
    </table>
<%---------------------------------------------------------------------------%>
        <div id="modalQuitarSeleccion" style="z-index: 1000">
        <div class="modalBackground">
        </div>
        <div class="modalContainer" style="left: 50%; top: 50%">
            <div class="modal" style="left: -148px; top: -148px">
                <div class="modalTop">Cancelar Orden de Compra</div>
                <div class="modalBody">
                    <asp:Button runat="server" ID="btnCancelarOC" Text="Si" CausesValidation="false" Width="68px" />
                    <input id="Button16" type="button" value="No" onclick="hideModal('modalQuitarSeleccion')" Style="width: 68px;" /></div>
                </div>
            </div>
        </div>
    </div>
    
    <div>
        &nbsp;</div>
    
</asp:Content>
