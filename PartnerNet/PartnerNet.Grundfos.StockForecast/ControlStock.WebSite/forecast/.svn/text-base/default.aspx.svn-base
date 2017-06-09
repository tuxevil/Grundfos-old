<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Grundfos.StockForecast.forecast._default" Title="Forecast" %>
<%@ Register Assembly="netchartdir" Namespace="ChartDirector" TagPrefix="chart" %>
<%@ Register Assembly="dotnetCHARTING" Namespace="dotnetCHARTING" TagPrefix="dotnetCHARTING" %>
<%@ Import namespace="PartnerNet.Domain"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
        <table width="100%" border="0" cellspacing="7" cellpadding="0">
          <tr>
            <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="28" valign="top">
                <table width="100%">
                <tr>
                <td style=" width:33%">
                    <asp:Label ID="lblAlternativeFrom" runat="server" CssClass="text4" Text="Producto Sustituido: " Visible="false"></asp:Label> <asp:LinkButton ID="btnAlternativeFrom" runat="server" CssClass="textlink" OnClick="btnAlternativeFrom_Click" ></asp:LinkButton>
                </td>
                <td style=" width:33%">
                <a href="javascript:history.back()" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image9','','../images/btn_volver2.gif',1)"><img src="../images/btn_volver.gif" alt="Volver" name="Image9" width="60" height="21" hspace="5" border="0" /></a>
                </td>
                <td style=" width:33%">
                    <asp:Label ID="lblAlternativeTo" runat="server" CssClass="text4" Text="Producto Sustituto: " Visible="false"></asp:Label> <asp:LinkButton ID="btnAlternativeTo" runat="server" CssClass="textlink" OnClick="btnAlternativeTo_Click"></asp:LinkButton>
                </td>
                </tr>
                </table>
                </td>
              </tr>
              <tr class="fdo_tit">
                <td>
                    <img src="../images/tit_listado_articulos.gif" alt="Listado de artículos" width="172" height="27"/>
                </td>
              </tr>
              <tr>
                <td><p><br>
                </p>
                  <table width="100%" border="0" cellspacing="0" cellpadding="8">
                    <tr>
                      <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td style="width: 460px">
                          <asp:GridView ID="GridView1" runat="server" Width="460px" PageSize="12" AutoGenerateColumns="False" CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
                                <FooterStyle BackColor="#0E4B88" CssClass="text2" />
                                <RowStyle BackColor="#ECEBEB" ForeColor="#333333" CssClass="paginado"/>
                                <Columns>
                                    <asp:BoundField DataField="Year" HeaderText="A&#241;o" />
                                    <asp:BoundField DataField="Week" HeaderText="Semana" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                    <asp:BoundField DataField="Purchase" HeaderText="Compras" />
                                    <asp:BoundField DataField="Sale" HeaderText="Ventas" />
                                    <asp:BoundField DataField="FinalStock" HeaderText="Stock Final" />
                                    <asp:BoundField DataField="Safety" HeaderText="Safety" />
                                    <asp:BoundField DataField="SafetyCoEf" HeaderText="Safety CoEf" />
                                    <asp:BoundField DataField="QuantitySuggested" HeaderText="Cantidad Sugerida" />
                                </Columns>
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#0E4B88" CssClass="text2" Font-Size="X-Small" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="#E3E3E3" ForeColor="#284775" />
                        </asp:GridView>
                        </td>
                        </tr>
                      </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr class="fdo_controles">
                            <td height="45" align="center"></td>
                          </tr>
                        </table>                        </td>
                      <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                          <td><table width="100%" border="0" cellpadding="8" cellspacing="0">
                            <tr>
                                <td align="center" bgcolor="#4b7fa6" colspan="6">
                                    <span class="text2">Información del Producto</span></td>
                            </tr>
                            <tr>
                                <td bgcolor="#e3e3e3" colspan="1">
                                    <span class="text4">Cód:</span></td>
                                <td bgcolor="#e3e3e3" colspan="1">
                                   <span class="text4">Artículo:</span>&nbsp;<span class="text3"> </span>
                                </td>
                                <td bgcolor="#e3e3e3" colspan="1">
                                   <span class="text4">Proveedor:&nbsp;</span><span class="text3"></span>          
                                </td>
                                <td colspan="3" bgcolor="e3e3e3">
                                <span class="text4">Stock Actual<br />
                                    St/Ov/Oc:</span>&nbsp;<span class="text3"></span> 
                                </td>
                              </tr>
                              <tr>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3"></asp:Label></td>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3"></asp:Label></td>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3"></asp:Label></td>
                                  <td bgcolor="#e3e3e3" colspan="3">
                                      <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3"></asp:Label></td>
                              </tr>
                              <tr>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <span class="text4">Nivel Rep:&nbsp;</span><span class="text3"></span>
                                  </td>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                     <span class="text4">Mod.Compra:</span>&nbsp;<span class="text3"></span>          
                                  </td>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                     <span class="text4">Lead Time:&nbsp;</span><span class="text3"></span>          <span class="text4">
                              </span>
                                  </td>
                                  <td bgcolor="#e3e3e3" colspan="3">
                                      <span class="text4">Safety: </span><span class="text3"></span></td>
                              </tr>
                              <tr>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3"></asp:Label></td>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3"></asp:Label></td>
                                  <td bgcolor="#e3e3e3" colspan="1">
                                      <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3" ></asp:Label></td>
                                  <td bgcolor="#e3e3e3" colspan="3">
                                      <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="text3" ></asp:Label></td>
                              </tr>
                            
                          </table></td>
                        </tr>
                        <tr>
                          <td><table width="100%" border="0" cellpadding="8" cellspacing="1">
                            <tr>
                              <td colspan="6" align="center" bgcolor="#4b7fa6"><span class="text2">Promedios de Ventas<br>
                              </span></td>
                            </tr>
                            <tr>
                              <td align="center" bgcolor="0e4b88" class="text2">Enero<br />
                                  <asp:Label ID="lblY01" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Febrero<br />
                                  <asp:Label ID="lblY02" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Marzo<br />
                                  <asp:Label ID="lblY03" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Abril<br />
                                  <asp:Label ID="lblY04" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Mayo<br />
                                  <asp:Label ID="lblY05" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Junio<br />
                                  <asp:Label ID="lblY06" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                              <td align="center" bgcolor="e3e3e3" class="paginado"><asp:Label ID="Label01" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label02" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label03" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label04" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label05" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label06" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                            </tr>
                            <tr>
                              <td align="center" bgcolor="0e4b88" class="text2">Julio<br />
                                  <asp:Label ID="lblY07" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Agosto<br />
                                  <asp:Label ID="lblY08" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Septiembre<br />
                                  <asp:Label ID="lblY09" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Octubre<br />
                                  <asp:Label ID="lblY10" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Noviembre<br />
                                  <asp:Label ID="lblY11" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                              <td align="center" bgcolor="0e4b88" class="text2">Diciembre<br />
                                  <asp:Label ID="lblY12" runat="server" CssClass="text2" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                              <td align="center" bgcolor="e3e3e3" class="paginado"><asp:Label ID="Label07" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label08" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label09" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label010" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label011" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                              <td align="center" bgcolor="e3e3e3"><span class="paginado"><asp:Label ID="Label012" runat="server" Text="Label" CssClass="text3" Font-Size="Small"></asp:Label></span></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td style="height: 171px"><chart:webchartviewer id="WebChartViewer1" runat="server" Visible="False" />
                              </td>
                          
                        </tr>
                      </table></td>
                    </tr>
                  </table>
                  </td>
              </tr>

            </table></td>
          </tr>
          
        </table>
</asp:Content>