<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Grundfos.StockForecast.breakdown._default" Title="Despiece de Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <table style="width: 643px" cellspacing=0>
        <tr class="fdo_tit">
            <td colspan="1" style="width: 100px; height: 18px">
            </td>
            <td colspan="2" style="height: 18px">
                <asp:Label ID="Label5" runat="server" CssClass="text4" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" Text="Despiece de Productos"></asp:Label></td>
            <td colspan="1" style="width: 100px; height: 18px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 18px">
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="text4"></asp:Label></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label" CssClass="text3"></asp:Label></td>
            <td style="width: 100px; height: 18px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 18px">
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Descripción:" CssClass="text4"></asp:Label></td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Label" CssClass="text3"></asp:Label></td>
            <td style="width: 100px; height: 18px">
            </td>
        </tr>
        </table>
         <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr class="fdo_controles">
            <td height="45" align="center"></td>
          </tr>
        </table>     
        <table>
        <tr>
            <td colspan="1" style="width: 100px">
            </td>
            <td colspan="2">
            <div style="OVERFLOW: auto; height: 300px" >
            <asp:Repeater ID="repItems" runat="server" >
            <HeaderTemplate>
            <table border="0" cellspacing="1" cellpadding="2">
                <tr bgcolor="#0e4b88" class="text2">
                <td width=75>Código</td>
                <td width=250>Descripción</td>
                <td width=60>Cantidad</td>
                </tr>
            </table>
            </HeaderTemplate>
            <ItemTemplate>
                    <table border="0" cellspacing="1" cellpadding="2" bgcolor="white">
                        <tr runat="server" id="rowLine" class="paginado">
                        <td width=75><%# DataBinder.Eval(Container, "DataItem.Part.ProductCode") %></td>
                        <td width=250><%# DataBinder.Eval(Container, "DataItem.Part.Description") %></td>
                        <td width=60><%# DataBinder.Eval(Container, "DataItem.Quantity") %></td>
                        </tr>
                    </table> 
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
            </asp:Repeater>
            </div>

            </td>
            <td colspan="1" style="width: 100px">
            </td>
        </tr>
    </table>
     <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr class="fdo_controles">
        <td height="45" align="center"><a href="javascript:history.back()" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image9','','../images/btn_volver2.gif',1)"><img src="../images/btn_volver.gif" alt="Volver" name="Image9" width="60" height="21" hspace="5" border="0"></a></td>
      </tr>
    </table>     
</asp:Content>
