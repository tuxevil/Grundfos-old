<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="reportesordcompras.aspx.cs" Inherits="Grundfos.StockForecast.reportesordcompras" Title="Reportes de Ordenes de Compras" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" CssClass="text1"
            Height="1039px" Width="901px" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False" DisplayToolbar="False" />
</asp:Content>
