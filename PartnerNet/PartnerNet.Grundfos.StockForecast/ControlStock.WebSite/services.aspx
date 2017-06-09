<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="services.aspx.cs" Inherits="Grundfos.StockForecast.services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp;
        <table width="80%">
            <tr>
                <td style="width: 210px">
                    Seleccion de fecha especifica</td>
                <td style="width: 210px">
        <asp:Calendar ID="Calendar1" runat="server" Font-Size="X-Small" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar><asp:Label ID="Label1" runat="server" Text="Label" /></td>
                <td>
                    <asp:Button ID="btnUpdateProductLifeOfAlertProduct" runat="server" OnClick="btnUpdateProductLifeOfAlertProduct_Click"
                        Text="Actualizar Vida util de productos en AlertProduct" Width="274px" /></td>
                <td colspan="1">
                </td>
                <td colspan="1">
                </td>
                <td colspan="1">
                </td>
                <td colspan="1">
                </td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    Proveedores</td>
                <td style="width: 210px">
                </td>
                <td>
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Actualizar Listado de Proveedores"
            Width="250px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    Productos</td>
                <td style="width: 210px">
                </td>
                <td>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Actualizar Listado de Productos"
            Width="250px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 210px">
                    semanales</td>
                <td style="width: 210px">
                    historico</td>
                <td>
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Generar Transacciones Semanales"
            Width="250px" /></td>
                <td>
                    <asp:Button ID="Button19" runat="server" OnClick="Button19_Click" Text="Limpiar Transacciones de Semana Seleccionada"
                        Width="298px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    estadisticas</td>
                <td>
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Generar Promedios Semanales"
            Width="250px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 210px">
                    mensuales</td>
                <td style="width: 210px">
                    historico</td>
                <td>
        <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="Generar Transacciones Mensuales"
            Width="239px" /></td>
                <td>
                    <asp:Button ID="Button20" runat="server" OnClick="Button20_Click" Text="Limpiar Transacciones de Mes Seleccionado"
                        Width="294px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    estadisticas</td>
                <td>
                    <asp:Button ID="Button21" runat="server" OnClick="Button21_Click" Text="Generar Promedios Mensuales"
                        Width="249px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 210px">
                    forecast</td>
                <td style="width: 210px">
                    manual</td>
                <td>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Generar Forecast" /></td>
                <td>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td>
        <asp:Button ID="Button16" runat="server" OnClick="Button16_Click" Text="Generar Forecast para Productos Faltantes"
            Width="275px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    automatico</td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    ordenes de compras</td>
                <td style="width: 210px">
                </td>
                <td>
    <asp:Button runat="server" ID="btnTest" OnClick="btnTest_Click" Text="Generar OC Automaticas" /></td>
                <td>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Limpiar OC Automaticas"
            Visible="False" /></td>
                <td>
                    <asp:Button ID="Button22" runat="server" OnClick="Button22_Click" Text="Limpiar OC generadas en el dia" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    Alertas</td>
                <td style="width: 210px">
        <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="Ejecutar todas" /></td>
                <td>
        <asp:Button ID="Button12" runat="server" OnClick="Button12_Click" Text="Alerta1" /></td>
                <td>
        <asp:Button ID="Button13" runat="server" OnClick="Button13_Click" Text="Alerta2" /></td>
                <td>
        <asp:Button ID="Button14" runat="server" OnClick="Button14_Click" Text="Alerta3" /></td>
                <td>
        <asp:Button ID="Button15" runat="server" OnClick="Button15_Click" Text="Alerta4" /></td>
                <td>
         <asp:Button ID="stockNegativo" runat="server" OnClick="stockNegativo_Click" Text="Alerta5" />
                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnRepositionLevel" runat="server" OnClick="btnRepositionLevel_Click"
                        Text="Reposicion" /></td>
            </tr>
            <tr>
                <td style="width: 210px">
                    VARIOS</td>
                <td style="width: 210px">
                </td>
                <td>
        <asp:Button runat="server" Text="Ejecutar Transacciones Pasadas" ID="Button8" OnClick="Button8_Click" /></td>
                <td>
        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Ejecutar Proceso Completo"
            Width="250px" /></td>
                <td>
        <asp:Button ID="Button17" runat="server" OnClick="Button17_Click" Text="Prueba de Envio de Mail"
            Width="158px" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                </td>
                <td style="width: 210px">
                </td>
                <td>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button18" runat="server" OnClick="Button18_Click" Text="Validar multiplo" />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
                <td>
        <asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="Crear Event" /></td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:Button ID="Button23" runat="server" OnClick="Button23_Click" Text="Productos Activos" /></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        &nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp;<br />
        <br />
        &nbsp;<br />
    &nbsp;<br />
        <br />
        &nbsp;<br />
        &nbsp;&nbsp;
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        </div>
        
    </form>
</body>
</html>
