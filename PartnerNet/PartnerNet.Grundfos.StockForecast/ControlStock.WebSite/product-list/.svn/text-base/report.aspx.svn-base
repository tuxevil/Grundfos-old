<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="Grundfos.StockForecast.product_list.report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reporte de Ordenes de Compras</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" CssClass="text1"
            Font-Bold="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
            RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="0">Con detalle</asp:ListItem>
            <asp:ListItem Value="1">Sin detalle</asp:ListItem>
        </asp:RadioButtonList><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            Height="1039px" Width="901px" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasSearchButton="False" HasViewList="False" PrintMode="ActiveX" HasZoomFactorList="False" />
        <%--<link href="../styles_grundfos.css" rel="stylesheet" type="text/css" />--%>
    
    </div>
    </form>
</body>
</html>
