<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detalle.ascx.cs" Inherits="Grundfos.StockForecast.Templates.Detalle" %>
<link href="../styles_grundfos.css" rel="stylesheet" type="text/css" />

<table style="width: 700px; height: 108px" cellspacing=0 >
    <tr>
        <td style="width: 23px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 148px; height: 5px;">
        </td>
        <td style="width: 42px; height: 5px;">
        </td>
        <td style="width: 42px; height: 5px;">
        </td>
    </tr>
    <tr>
        <td style="width: 23px">
        </td>
        <td colspan="2" bgcolor="#FFFFFF">

<asp:Label ID="Label1" runat="server" Text="Precio de Compra:" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label><br />
            &nbsp;<asp:Label ID="Label2" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td colspan="2" bgcolor="#FFFFFF">
            <asp:Label ID="Label3" runat="server" Font-Size="X-Small"
        Text="Sobre Costos:" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label><br />
            &nbsp;<asp:Label ID="Label4" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 42px">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/btn_ver_fore.gif" OnClick="Button1_Click" AlternateText="Ver Forecast" Width="93" Height="21" />
        </td>
        <td style="width: 42px">
        </td>
    </tr>
    <tr>
        <td style="width: 23px">
        </td>
        <td colspan="2" bgcolor="#FFFFFF">
<asp:Label ID="Label5" runat="server" Font-Size="X-Small" Text="Precio de Venta:" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label><br />
            &nbsp;<asp:Label ID="Label6" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td colspan="2" bgcolor="#FFFFFF">
            <asp:Label ID="Label26" runat="server" CssClass="text4" Font-Bold="False" Font-Names="Verdana"
                Font-Size="X-Small" Text="Costo Standard:"></asp:Label><br />
            <asp:Label ID="Label27" runat="server" CssClass="text4" Font-Bold="False" Font-Size="X-Small"></asp:Label></td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 42px">
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_ver_desp.gif"
                OnClick="ImageButton2_Click" AlternateText="Ver Despiece" Visible="False" /></td>
        <td style="width: 42px">
        </td>
    </tr>
    <tr>
        <td style="width: 23px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px">
        </td>
        <td style="width: 42px">
        </td>
        <td style="width: 42px">
        </td>
    </tr>
    <tr>
        <td style="width: 23px">
        </td>
        <td colspan="4" bgcolor="#4b7fa6" >
<asp:Label ID="Label7" runat="server" Font-Size="X-Small" Text="Cantidad de Ventas de los Últimos" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px">
        </td>
        <td colspan="4" bgcolor="#4b7fa6" >
<asp:Label ID="Label16" runat="server" Font-Size="X-Small" Text="Promedio semanal de Ventas en los Últimos" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 42px">
        </td>
        <td style="width: 42px">
        </td>
    </tr>
    <tr>
        <td style="width: 23px">
        </td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label8" runat="server" Font-Size="X-Small" Text="12 Meses" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
            <asp:Label ID="Label10"
        runat="server" Font-Size="X-Small" Text="6 Meses" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label12" runat="server" Font-Size="X-Small" Text="3 Meses" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label14" runat="server" Font-Size="X-Small" Text="Mes" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label17" runat="server" Font-Size="X-Small" Text="12 Meses" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label19" runat="server" Font-Size="X-Small" Text="6 Meses" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label21" runat="server" Font-Size="X-Small" Text="3 Meses" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label23" runat="server" Font-Size="X-Small" Text="Mes" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 42px">
        </td>
        <td style="width: 42px">
        </td>
    </tr>
    <tr>
        <td style="width: 23px">
        </td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label9" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label11" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label13" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label15" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px">
        </td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label18" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label20" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
<asp:Label ID="Label22" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 148px" bgcolor="#FFFFFF" >
            <asp:Label ID="Label24" runat="server" Font-Size="X-Small" Font-Names="Verdana" CssClass="text4" Font-Bold="False"></asp:Label></td>
        <td style="width: 42px">
        </td>
        <td style="width: 42px">
        </td>
    </tr>
    <tr>
        <td style="width: 23px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
<asp:Label ID="Label25" runat="server" Visible="False" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label></td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 148px; height: 5px">
        </td>
        <td style="width: 42px; height: 5px">
        </td>
        <td style="width: 42px; height: 5px">
        </td>
    </tr>
</table>
