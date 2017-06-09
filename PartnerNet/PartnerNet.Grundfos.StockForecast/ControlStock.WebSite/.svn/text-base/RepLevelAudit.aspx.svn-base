<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="RepLevelAudit.aspx.cs" Inherits="Grundfos.StockForecast.RepLevelAudit" Title="Auditoría de Cambios de Nivel de Reposición" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <center>
        <asp:GridView ID="gvRepositionLevel" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" PageSize="25" OnPageIndexChanging="gvRepositionLevel_PageIndexChanging" CssClass="text1">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Producto" />
                <asp:BoundField DataField="RepositionLevel" HeaderText="Nivel de Reposici&#243;n" />
                <asp:BoundField DataField="CreationDate" HeaderText="Fecha" />
            </Columns>
        </asp:GridView>
    </center>
</asp:Content>