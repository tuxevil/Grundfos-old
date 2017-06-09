<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="export.aspx.cs" Inherits="Grundfos.StockForecast.product_list.export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="Both" BorderColor="#CCCCCC">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BorderColor="#CCCCCC" BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="ProductCode"  ItemStyle-Width="100px" HeaderText="Código" />
                <asp:BoundField DataField="Description" ItemStyle-Width="300px" HeaderText="Descripción"/>
                <asp:BoundField DataField="TrimestralSale" ItemStyle-Width="200px" HeaderText="Venta Total Trimestral"/>
                <asp:BoundField DataField="AnualSale" ItemStyle-Width="200px" HeaderText="Venta Total Anual"/>
                <asp:BoundField DataField="RepPoint" ItemStyle-Width="200px" HeaderText="Nivel de Reposición"/>
                <asp:BoundField DataField="RepLevel" ItemStyle-Width="200px" HeaderText="Modulo de Compra"/>
            </Columns>
            <PagerStyle BorderColor="#CCCCCC" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BorderColor="#CCCCCC" BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BorderColor="#CCCCCC" BackColor="#e1e0e0" Font-Bold="True" ForeColor="#666666" />
            <EditRowStyle BorderColor="#CCCCCC" BackColor="#999999" />
            <AlternatingRowStyle BorderColor="#CCCCCC" BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
