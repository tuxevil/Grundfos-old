<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetalleOC.ascx.cs" Inherits="Grundfos.StockForecast.Templates.DetalleOC" %>
<script language="javascript" type="text/javascript">
<!--


// -->
</script>


<link href="../styles_grundfos.css" rel="stylesheet" type="text/css" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<table style="width: 800px; height: 150px">
    <tr>
        <td rowspan="3">
        <div style="OVERFLOW: auto; height: 170px; width:800px" >
               
            <asp:Repeater ID="repItems" runat="server" OnItemCommand="repItems_ItemCommand" OnItemDataBound="repItems_ItemDataBound" >
            <HeaderTemplate>
            <table border="0" cellspacing="1" cellpadding="2" >
                <tr bgcolor="#0e4b88" class="text2">
                <td width=80>Código</td>
                <td width=250>Descripción</td>
                <td width=70>Cantidad</td>
                <td width=90>Precio Unit.</td>
                <td width=90>Total</td>
                <td width=60>Stock</td>
                <td width=75></td>
                <td width=80>Cantidad Sugerida</td>
                </tr>
            </table>
            </HeaderTemplate>
            <ItemTemplate>
                    <table border="0" cellspacing="1" cellpadding="2" bgcolor="white">
                        <tr runat="server" id="rowLine" class="paginado">
                        <td width=80><asp:Label ID="lblCodigoOCItem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>' Visible="false"></asp:Label><%# DataBinder.Eval(Container, "DataItem.ProductCode") %></td>
                        <td width=250><%# DataBinder.Eval(Container, "DataItem.ProductName") %></td>
                        <td width=70><asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Width=50></asp:TextBox></td>
                        <td width=90><%# DataBinder.Eval(Container, "DataItem.Currency").ToString() + " " + Convert.ToDouble(DataBinder.Eval(Container, "DataItem.Price")).ToString("#,##0.00") %></td>
                        <td width=90><%# DataBinder.Eval(Container, "DataItem.Currency").ToString() + " " + Convert.ToDouble(DataBinder.Eval(Container, "DataItem.TotalPrice")).ToString("#,##0.00")%></td>
                        <td width=60><%# DataBinder.Eval(Container, "DataItem.Stock") %></td>
                        <td width=75><asp:ImageButton ID="btnGuardarInd" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id") %>' AlternateText="Save" CommandName="Save" Width="15px" Height="15px" runat="server" ImageUrl="~/Images/3floppy_unmount.ico" /><asp:CheckBox ID="CheckBox1" runat="server" ></asp:CheckBox><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/3floppy_mount.ico" Width="15px" Height="15px" Visible="false" /></td>
                        <td width=80><%# DataBinder.Eval(Container, "DataItem.QuantitySuggested") %></td>
                        </tr>
                    </table> 
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
            </asp:Repeater>
            
            </div>
        </td>
        <td rowspan="3" valign="top" style="width: 134px; text-align: left;">
            <a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image1','','../images/btn_imp_oc2.gif',1)"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="../images/btn_imp_oc.gif" AlternateText="Imprimir Orden de Compra" width="131" height="21" OnClick="ImageButton3_Click" /></a><br />
            <a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image2','','../images/btn_cancelar_oc2.gif',1)"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/btn_cancelar_oc.gif" AlternateText="Cancelar Orden de Compra" Width="131" Height="21" BorderWidth="0" OnClick="ImageButton1_Click" /></a><br />
            <a style="cursor:pointer" onmouseout="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image3','','../images/btn_exp_oc2.gif',1)">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/btn_exp_oc.gif" AlternateText="Exportar orden de compra" Width="131" Height="21" BorderWidth="0" OnClick="ImageButton2_Click" Visible="False" /></a><br />
            <asp:Label ID="Label2" runat="server" CssClass="text4" Font-Size="Smaller" ForeColor="Red"
                Text="Cambio exitoso!" Visible="False"></asp:Label><br />
            <asp:RadioButtonList ID="rblWOD" runat="server" AutoPostBack="True" CssClass="text4"
                OnSelectedIndexChanged="rblWOD_SelectedIndexChanged">
                <asp:ListItem Value="1">Maritimo</asp:ListItem>
                <asp:ListItem Value="2">Aereo</asp:ListItem>
                <asp:ListItem Value="3">Courrier</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" ></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label></td>
            
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
</table>
</ContentTemplate>
            </asp:UpdatePanel>