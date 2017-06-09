<%@ Page Language="C#" MasterPageFile="../Base.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Grundfos.StockForecast.product_list._default" Title="Listado de Productos" %>
<%@ Import namespace="System.ComponentModel"%>
<%@ Register Src="../Templates/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="../Templates/Detalle.ascx" TagName="Detalle" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div style="text-align: center">
    <table width="100%" border="0" cellspacing="7" cellpadding="0">
        <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr class="fdo_tit">
                <td style="height: 27px"><asp:Label ID="lblTitulo" runat="server" CssClass="text2" Font-Bold="true" Font-Size="Medium" Text="Listado de Productos"></asp:Label></td>
                </tr>
                <tr bgcolor="#e3e3e3">
                <td align="center" valign="top">
                <table width="90%" border="0" cellspacing="3" cellpadding="0">
                    <tr>
                        <td align="right" class="text1" style="height: 24px">Descripción</td>
                        <td class="text1" style="height: 24px"><asp:TextBox style="Z-INDEX: 102;" id="txtDescripcion" runat="server" Font-Size="Small" OnDataBinding="txtDescripcion_DataBinding" Width="235px" ></asp:TextBox></td>
                        <td align="right" style="height: 24px"><span class="text1">Categoría</span></td>
                        <td style="height: 24px">
                        <asp:DropDownList style="Z-INDEX: 107;" id="ddlCategoria" runat="server" Font-Size="Small" Width="150px">
                            <asp:ListItem Value="N/A">--Grupo--</asp:ListItem>
                        </asp:DropDownList></td>
                        <td width="64" style="height: 24px">
                            </td>
                        <td colspan="2" rowspan="3">
                            <asp:Table ID="Table1" runat="server">
                            <asp:TableRow>
                            <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnAgregar','','../images/btn_agregar_sel2.gif',1)" onclick="revealModal('modalAgregarSeleccion')"><img src="../images/btn_agregar_sel.gif" alt="Agregar a Selección" name="btnAgregar" width="131" height="21" border="0"></a>
                            </asp:TableCell>
                            <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image13','','../images/btn_generar_com2.gif',1)"><asp:ImageButton ID="btnGenerarOC" runat="server" ImageUrl="../images/btn_generar_com.gif" AlternateText="Generar Orden de Compra" Width="131" Height="21" BorderWidth="0" OnClick="btnGenerarOC_Click" /></a>
                            </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                            <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnQuitarSelec','','../images/btn_quitar_selec2.gif',1)" onclick="revealModal('modalQuitarSeleccion')"><img src="../images/btn_quitar_selec.gif" alt="Quitar de Selección" name="btnQuitarSelec" width="131" height="21" border="0"></a>
                            </asp:TableCell>
                            <asp:TableCell><a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnModificarSafety','','../images/btn_modificar_saf2.gif',1)" onclick="revealModal('modalCambioSafety')"><img src="../images/btn_modificar_saf.gif" alt="Modificar Safety" name="btnModificarSafety" width="131" height="21" border="0"></a>
                            </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                            <asp:TableCell><asp:ImageButton ID="btnImprimirLP" runat="server" ImageUrl="~/Images/btn_imp_lp.gif"
                                OnClick="btnImprimirLP_Click" AlternateText="Imprimir Lista de Productos" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center"><asp:LinkButton ID="btnImprimirBusqueda" runat="server" OnClick="btnImprimirBusqueda_Click" Text="Imp. Busqueda Completa" Style=" font-family:Verdana; font-size:x-small" />
                            </asp:TableCell>
                            </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="text1">Estado</td>
                        <td class="text1">
                        <asp:DropDownList style="Z-INDEX: 104;" id="ddlEstado" runat="server" Font-Size="Small" Width="246px">
                            <asp:ListItem Value="0">--Proveedor--</asp:ListItem>
                        </asp:DropDownList></td>
                        <td align="right"><span class="text1">Selección</span></td>
                        <td><span class="text1">
                        <asp:DropDownList style="Z-INDEX: 108;" id="ddlSeleccion" runat="server" Font-Size="Small" Width="150px">
                            <asp:ListItem Value="0">--Selección--</asp:ListItem>
                        </asp:DropDownList>
                        </span></td>
                        <td width="64"></td>
                    </tr>
                    <tr>
                        <td align="right" class="text1" style="height: 19px">
                        </td>
                        <td class="text1" style="height: 19px">
                            <asp:CheckBox ID="chbViejos" runat="server" CssClass="text1" Text="Excluir articulos sin ventas" /></td>
                        <td align="right" style="height: 19px">
                        </td>
                        <td style="height: 19px">
                            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="../images/btn_buscar.gif" AlternateText="Buscar" Width="59" Height="21" BorderWidth="0" OnClick="btnBuscar_Click" /></td>
                        <td width="64" style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="text1">
                        </td>
                        <td class="text1">
                        <asp:CustomValidator ID="validatorDesc" runat="server" ControlToValidate="txtDescripcion"
            ErrorMessage="Seleccione un Filtro!" Font-Bold="True" Font-Size="Small" OnServerValidate="validatorDesc_ServerValidate" ></asp:CustomValidator></td>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                        <td width="64">
                        </td>
                        <td colspan="2"> <asp:Label ID="lblErrorProducto" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
            Text="Por favor seleccione al menos 1 producto!" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
            <td><img src="../images/fin_tabla_busq.gif" width="986" height="6"></td>
            </tr>
        </table>
        <br>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td align="center" style="height: 19px">
            
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
        <td><asp:ScriptManager ID="ScriptManager1" runat="server" />            &nbsp;
            <asp:Repeater ID="repItems" runat="server" OnItemCommand="repItems_ItemCommand" OnItemDataBound="repItems_ItemDataBound"  >
            <HeaderTemplate>
            <table border="0" cellspacing="1" cellpadding="2">
                <tr bgcolor="#0e4b88" class="text2">
                <td width=20></td>
                <td width=75>Código</td>
                <td width=275>Artículo</td>
                <td width=165>Proveedor</td>
                <td width=90>Stock <font size="1">St/Ov/Oc</font></td>
                <td width=50>Nivel Rep.</td>
                <td width=50>Modulo Comp.</td>
                <td width=50>Prom. Ventas</td>
                <td width=50>Lead Time</td>
                <td width=50>Safety</td>
                <td width=100><asp:LinkButton ID="lnkMarcarTodos" Text="Marcar Todos" OnClick="lnkMarcarTodos_Click" runat="server" CssClass="text2"></asp:LinkButton></td>
                </tr>
            </table>
            </HeaderTemplate>
            <ItemTemplate>
            
                    <table border="0" cellspacing="1" cellpadding="2">
                        <tr runat="server" id="rowLine" class="paginado">
                        <td width=20 align="center">
                        <asp:ImageButton CommandName="Expand" ID="btnExpandir" runat="server" imageurl="../images/whiteExpandIcon.gif" /></td>               
                        <td width=75><asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCode") %>'></asp:Label></td>
                        <td width=275><%# DataBinder.Eval(Container, "DataItem.Description") %></td>
                        <td width=165><%# DataBinder.Eval(Container, "DataItem.Provider") %></td>
                        <td width=90><%# DataBinder.Eval(Container, "DataItem.Stock").ToString() + "/" + DataBinder.Eval(Container, "DataItem.ReservedStock").ToString() + "/" + DataBinder.Eval(Container, "DataItem.OrderedStock").ToString() %></td>
                        <td width=50><%# DataBinder.Eval(Container, "DataItem.RepositionLevel") %></td>
                        <td width=50><asp:TextBox ID="txtPuntoReposicion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RepositionPoint") %>' Width=40></asp:TextBox></td>
                        <td width=50 align="center"><%# DataBinder.Eval(Container, "DataItem.SaleAverage") %></td>
                        <td width=50 align="center"><asp:TextBox ID="txtLeadTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LeadTime") %>' Width=40></asp:TextBox></td>
                        <td width=50><asp:TextBox ID="txtSafety" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Safety") %>' Width=40></asp:TextBox></td>
                        <td width=100><asp:ImageButton ID="btnGuardarInd" AlternateText="Save" CommandName="Save" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id") %>' Width="15px" Height="15px" runat="server" ImageUrl="../Images/3floppy_unmount.ico" /><asp:CheckBox ID="chkGuardar" runat="server"></asp:CheckBox></td>
                        </tr>
                    </table> 
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="conditional" ChildrenAsTriggers="False" runat="server">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnExpandir" />
                </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="C" runat="server" BackColor="#c7dbe9" >
                                <uc1:Detalle ID="ucDetalle" Visible="false" runat="server" />
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CPE" runat="server" TargetControlID="C"
                        ExpandControlID="btnExpandir" CollapseControlID="btnExpandir" Collapsed="True" ImageControlID="btnExpandir"
                        ExpandedImage="../Images/whiteCollapseIcon.gif"
                        CollapsedImage="../Images/whiteExpandIcon.gif"
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
                <td width=100 class="text2"><asp:LinkButton ID="lnkDesmarcarTodo" Text="Desmarcar Todos" OnClick="lnkDesmarcarTodo_Click" runat="server" CssClass="text2" ForeColor="blue"></asp:LinkButton></td>
                </tr>
            </table>
            </FooterTemplate>
            </asp:Repeater>
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
        <div id="modalCambioSafety" style="position: absolute; left: 50%; top: 50%; margin-left:auto; margin-right:auto; margin-bottom:auto; margin-top:auto; z-index: 1000">
        <div class="modalBackground">
        </div>
        <div class="modalContainer">
            <div class="modal">
                <div class="modalTop">Cambio de Safety</div>
                <div class="modalBody">
                    Valor:
				    <asp:TextBox ID="txtChangeSafety" runat="server" Columns="5" Rows="1" TextMode="SingleLine"></asp:TextBox><br />
				    <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click" />
				    <input id="btnCancelar" type="button" value="Cancelar" onclick="hideModal('modalCambioSafety')" Style="width: 68px;" /></div>
            </div>
        </div>
    </div>
        <div id="modalAgregarSeleccion" style="position: absolute; left: 50%; top: 50%; z-index: 1000">
        <div class="modalBackground">
        </div>
        <div class="modalContainer">
            <div class="modal">
                <div class="modalTop">Agregar a Selección</div>
                <div class="modalBody">
                <asp:DropDownList ID="ddlSeleccionPop" runat="server" Font-Size="Small" Width="136px" >
                    <asp:ListItem Value="0">--Selección--</asp:ListItem>
                </asp:DropDownList><br />
				    <asp:TextBox ID="txtNuevaPop" runat="server" Columns="5" Rows="1" TextMode="SingleLine" Width="94px" ></asp:TextBox>
                    <span style="font-size: 7pt">NUEVA
                    </span><br />
                    <asp:Button runat="server" ID="btnAceptarNueva" Text="Aceptar" OnClick="btnAceptarNueva_Click" />
                    <input id="btnCancelarNueva" type="button" value="Cancelar" onclick="hideModal('modalAgregarSeleccion')" Style="width: 68px;" /></div>
                </div>
            </div>
        </div>
        <div id="modalQuitarSeleccion" style="margin-left:auto; margin-right:auto; margin-bottom:auto; margin-top:auto; z-index: 1000">
        <div class="modalBackground">
        </div>
        <div class="modalContainer" style="left: 50%; top: 50%">
            <div class="modal" style="left: -148px; top: -148px">
                <div class="modalTop">Quitar de Selección</div>
                <div class="modalBody">
                <asp:DropDownList ID="ddlQuitarSeleccion" runat="server" Font-Size="Small" Width="136px" >
                    <asp:ListItem Value="0">--Selección--</asp:ListItem>
                </asp:DropDownList><br />
                    <asp:Button runat="server" ID="btnAceptarQuitarSeleccion" Text="Aceptar" CausesValidation="false" OnClick="btnAceptarQuitarSeleccion_Click" />
                    <input id="btnCancelarQuitarSeleccion" type="button" value="Cancelar" onclick="hideModal('modalQuitarSeleccion')" Style="width: 68px;" /></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
