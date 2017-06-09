<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="usersmail.aspx.cs" Inherits="Grundfos.StockForecast.administration.usersmail" Title="Administracion de Mails de Alertas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script src="../js/jquery-1.2.6.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(window).bind('load',function(){
    var headerChk =   $(".chkHeader input");
    var itemChk = $(".chkItem input");
    headerChk.bind("click",function(){
                                       itemChk.each(function(){this.checked = headerChk[0].checked;})
                                       }
                   );
    itemChk.bind("click",function(){if($(this).checked==false)headerChk[0].checked =false;});  
    });
    </script>
   <div style="text-align: center">
    <table width="100%" border="0" cellspacing="7" cellpadding="0">
        <tr>
        <td>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr class="fdo_tit">
                <td style="height: 27px"><asp:Label ID="lblTitulo" runat="server" CssClass="text2" Font-Bold="true" Font-Size="Medium" Text="Administración de Mails para Alertas"></asp:Label></td>
                </tr>
                <tr bgcolor="#e3e3e3">
                <td align="center" valign="top">
                <table width="90%" border="0" cellspacing="3" cellpadding="0">
                <tr>
                <td class="text1" align="center">Campaña 
                        
                        <asp:DropDownList id="ddlCampaña" style="Z-INDEX: 104;"  runat="server" OnSelectedIndexChanged="ddlCampaña_SelectedIndexChanged" AutoPostBack="true" Font-Size="Small" Width="220px">
                            <asp:ListItem Value="N/A">--Campaña--</asp:ListItem>
                        </asp:DropDownList></td>
                        </tr>
                </table>
                </td>
                </tr>
           <tr>
           <td>
              <asp:UpdatePanel ID="updCampañas" runat="server">
              <ContentTemplate><br />
              <asp:GridView ID="grdMembers" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CssClass="text1" 
                        Enabled="False" ForeColor="#333333" GridLines="None" PageSize="25" OnPageIndexChanging="grdMembers_PageIndexChanging">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Nombre" />
                    <asp:BoundField DataField="Email" HeaderText="E-Mail"/>
                    <asp:templatefield HeaderText="Alertar" >
                      
                      <HeaderTemplate>
                         <asp:checkbox id="chkHead" runat="server" ToolTip="Marcar/Desmarcar Todos" Enabled="true" Visible="true" CssClass="chkHeader" />
                      </HeaderTemplate>
                      
                      <itemtemplate>
                        <asp:checkbox id="chkAlertar" Enabled="false" Checked="false" runat="server" CssClass="chkItem" />
                      </itemtemplate>
                      
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="btnAgregarAlertasMail" runat="server" OnClick="btnAgregarAlertasMail_Click" Text="Modificar Alertas" Enabled="False" />&nbsp;</td>
         </tr>
      </table>
     </div>
</asp:Content>
