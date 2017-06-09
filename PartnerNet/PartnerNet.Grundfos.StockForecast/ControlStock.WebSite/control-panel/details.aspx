<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="Grundfos.StockForecast.control_panel.details" Title="Detalle de Alertas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Label ID="lblAlertName" runat="server" CssClass="text4" Font-Size="Medium" Text="Label"></asp:Label><br />
    <a href="javascript:history.back()" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image9','','../images/btn_volver2.gif',1)"><img src="../images/btn_volver.gif" alt="Volver" name="Image9" width="60" height="21" hspace="5" border="0"></a>
    <asp:LinkButton runat="server" ID="btnExportToExcel" CssClass="text4" Text="Exportar" OnClick="btnExportToExcel_Click" Visible ="true" />
    <br />
    <asp:GridView ID="GVOcConfirmadasNoEntregadas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CellPadding="4" CssClass="text1" Enabled="False" ForeColor="#333333" GridLines="None"
        PageSize="25" OnPageIndexChanging="GVOcConfirmadasNoEntregadas_PageIndexChanging" AllowSorting="True" OnSorting="GVOcConfirmadasNoEntregadas_Sorting">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="PurchaseOrderCode" SortExpression="PurchaseOrderCode" HeaderText="OC" />
            <asp:HyperLinkField DataNavigateUrlFields="PurchaseOrderItemCode" DataNavigateUrlFormatString="~/product-list/default.aspx?productcode={0}"
                DataTextField="PurchaseOrderItemCode" SortExpression="PurchaseOrderItemCode" HeaderText="Producto" />
            <asp:BoundField DataField="PurchaseOrderProviderCode" HeaderText="Codigo de Proveedor" SortExpression="PurchaseOrderProviderCode"/>
            <asp:BoundField DataField="PurchaseOrderProviderName" HeaderText="Proveedor" SortExpression="PurchaseOrderProviderName"/>
            <asp:BoundField DataField="Quantity" SortExpression="Quantity" HeaderText="Cantidad" />
            <asp:BoundField DataField="GAP" SortExpression="GAP" HeaderText="GAP" />
            <asp:BoundField DataField="WayOfDelivery" SortExpression="WayOfDelivery" HeaderText="Modo Envio" />
            <asp:BoundField DataField="Destination" SortExpression="Destination" HeaderText="Tipo" />
            <asp:BoundField DataField="ArrivalDate" SortExpression="ArrivalDate" DataFormatString="{0:d}" HeaderText="Llegada" />
            <asp:BoundField DataField="CalculatedArrivalDate" SortExpression="CalculatedArrivalDate" DataFormatString="{0:d}" HeaderText="Llegada (Calculada)" />
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:GridView ID="GVOcNoConfirmadas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CellPadding="4" CssClass="text1" Enabled="False" ForeColor="#333333" GridLines="None"
        PageSize="25" OnPageIndexChanging="GVOcNoConfirmadas_PageIndexChanging" AllowSorting="True" OnSorting="GVOcNoConfirmadas_Sorting">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="PurchaseOrderCode" SortExpression="PurchaseOrderCode" HeaderText="OC" />
            <asp:HyperLinkField DataNavigateUrlFields="PurchaseOrderItemCode" DataNavigateUrlFormatString="~/product-list/default.aspx?productcode={0}"
                DataTextField="PurchaseOrderItemCode" SortExpression="PurchaseOrderItemCode" HeaderText="Producto" />
            <asp:BoundField DataField="PurchaseOrderProviderCode" HeaderText="Codigo de Proveedor" SortExpression="PurchaseOrderProviderCode"/>
            <asp:BoundField DataField="PurchaseOrderProviderName" HeaderText="Proveedor" SortExpression="PurchaseOrderProviderName"/><asp:BoundField DataField="Quantity" SortExpression="Quantity" HeaderText="Cantidad" />
            <asp:BoundField DataField="GAP" SortExpression="GAP" HeaderText="GAP" />
            <asp:BoundField DataField="WayOfDelivery" SortExpression="WayOfDelivery" HeaderText="Modo Envio" />
            <asp:BoundField DataField="Destination" SortExpression="Destination" HeaderText="Tipo" />
            <asp:BoundField DataField="ArrivalDate" SortExpression="ArrivalDate" DataFormatString="{0:d}" HeaderText="Llegada" />
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:GridView ID="GVStockNegativo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CellPadding="4" CssClass="text1" Enabled="False" ForeColor="#333333" GridLines="None"
        PageSize="25" OnPageIndexChanging="GVStockNegativo_PageIndexChanging" AllowSorting="True" OnSorting="GVStockNegativo_Sorting">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ProductCode" DataNavigateUrlFormatString="~/product-list/default.aspx?productcode={0}"
                DataTextField="ProductCode" SortExpression="ProductCode" HeaderText="Producto" />
            <asp:BoundField DataField="StandardCost" SortExpression="StandardCost" HeaderText="Costo Standard" DataFormatString="{0:$ #,##0.000}" />
            <asp:BoundField DataField="SubTotal" SortExpression="SubTotal" HeaderText="SubTotal" DataFormatString="{0:$ #,##0.000}" />
            <asp:BoundField DataField="Quantity" SortExpression="Quantity" HeaderText="Cantidad" />
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:GridView ID="GVNoCumplimentadas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CellPadding="4" CssClass="text1" Enabled="False" ForeColor="#333333" GridLines="None"
        PageSize="25" OnPageIndexChanging="GVNoCumplimentadas_PageIndexChanging" AllowSorting="True" OnSorting="GVNoCumplimentadas_Sorting">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="SaleOrderCode" SortExpression="SaleOrderCode" HeaderText="OV" />
            <asp:HyperLinkField DataNavigateUrlFields="PurchaseOrderItemCode" DataNavigateUrlFormatString="~/product-list/default.aspx?productcode={0}"
                DataTextField="PurchaseOrderItemCode" SortExpression="PurchaseOrderItemCode" HeaderText="Producto" />
            <asp:BoundField DataField="PurchaseOrderCode" SortExpression="PurchaseOrderCode" HeaderText="OC" />
            <asp:BoundField DataField="Quantity" SortExpression="Quantity" HeaderText="Cantidad" />
            <asp:BoundField DataField="CustomerCode" SortExpression="CustomerCode" HeaderText="Cliente" />
            <asp:BoundField DataField="GAP" SortExpression="GAP" HeaderText="GAP" />
            <asp:BoundField DataField="WayOfDelivery" SortExpression="WayOfDelivery" HeaderText="Modo Envio" />
            <asp:BoundField DataField="SaleOrderDeliveryDate" SortExpression="SaleOrderDeliveryDate" DataFormatString="{0:d}" HeaderText="Entrega OV" />
            <asp:BoundField DataField="PurchaseOrderArrivalDate" SortExpression="PurchaseOrderArrivalDate" DataFormatString="{0:d}" HeaderText="Llegada OC" />
        <asp:BoundField DataField="OrderDate" SortExpression="OrderDate" DataFormatString="{0:d}" HeaderText="Creación" />
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:GridView ID="GVStockMenorSafety" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CellPadding="4" CssClass="text1" Enabled="False" ForeColor="#333333" GridLines="None"
        PageSize="25" OnPageIndexChanging="GVStockMenorSafety_PageIndexChanging" AllowSorting="True" OnSorting="GVStockMenorSafety_Sorting">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ProductCode" DataNavigateUrlFormatString="~/product-list/default.aspx?productcode={0}"
                DataTextField="ProductCode" SortExpression="ProductCode" HeaderText="Producto" />
            <asp:BoundField DataField="NegativeDate" SortExpression="NegativeDate" HeaderText="Fecha de Quiebre" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Quantity" SortExpression="Quantity" HeaderText="Cantidad" />
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView><asp:GridView ID="GVReposicionDiferente" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CellPadding="4" CssClass="text1" Enabled="False" ForeColor="#333333" GridLines="None"
        PageSize="25" AllowSorting="True" OnPageIndexChanging="GVReposicionDiferente_PageIndexChanging" OnSorting="GVReposicionDiferente_Sorting" OnRowDataBound="GVReposicionDiferente_RowDataBound" >
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ProductCode" DataNavigateUrlFormatString="~/product-list/default.aspx?productcode={0}"
                DataTextField="ProductCode" SortExpression="Product.ProductCode" HeaderText="Producto" />
            <asp:BoundField DataField="ProductName" HeaderText="Descripción" SortExpression="Product.Description" />
            <asp:BoundField DataField="Group" HeaderText="Grupo" SortExpression="Product.Group" />
            <asp:BoundField DataField="Sales" HeaderText="VTA. 12M" SortExpression="Sales" />
            <asp:BoundField DataField="CuatrimestralSales" HeaderText="VTA. 12M/3" SortExpression="Sales" />
            <asp:BoundField  DataField="SaleMonths" HeaderText="Vida del Producto(Meses)"/>
            <asp:BoundField DataField="RepositionLevel" HeaderText="Nivel de Reposici&#243;n"
                SortExpression="Product.RepositionLevel" />
            <asp:BoundField DataField="Result" HeaderText="Diferencia %" SortExpression="Result" />
            <asp:BoundField DataField="ProductCountryCode" HeaderText="Código de País" />
            <asp:BoundField DataField="OrderInfo" HeaderText="Información de Orden de Venta (Cant. Ordenes/%/N. Orden)" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
