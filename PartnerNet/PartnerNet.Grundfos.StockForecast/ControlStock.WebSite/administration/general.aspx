<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="general.aspx.cs" Inherits="Grundfos.StockForecast.administration.general" Title="Administración de Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <table border="0">
                <tr>
                    <td>
                        <asp:GridView ID="GridViewMemberUser" runat="server" OnSelectedIndexChanged="GridViewMembershipUser_SelectedIndexChanged"
                            OnRowDeleted="GridViewMembership_RowDeleted" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="UserName" DataSourceID="ObjectDataSourceMembershipUser" AllowSorting="True" CellPadding="4" CssClass="text1" ForeColor="#333333" GridLines="None">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar" DeleteImageUrl="~/Images/ErrorCircle.png" ShowEditButton="True" EditText="Modificar" EditImageUrl="~/Images/Search.png" ShowSelectButton="True" SelectText="Seleccionar" SelectImageUrl="~/Images/OKShield.png" ButtonType="Image" CancelImageUrl="~/Images/Cancel2.png" CancelText="Cancelar" UpdateImageUrl="~/Images/OKShield.png" UpdateText="Actualizar" />
                                <asp:CheckBoxField DataField="IsOnline" HeaderText="En Linea" ReadOnly="True" SortExpression="IsOnline" />
                                <asp:BoundField DataField="UserName" HeaderText="Usuario" ReadOnly="True" SortExpression="UserName" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="CreationDate" HeaderText="Fecha Creaci&#243;n" ReadOnly="True"
                                    SortExpression="CreationDate" />
                                <asp:CheckBoxField DataField="IsApproved" HeaderText="Aprobado" SortExpression="IsApproved" />
                                <asp:BoundField DataField="Comment" HeaderText="Comentario" SortExpression="Comment" />
                                <asp:BoundField DataField="LastLoginDate" HeaderText="Ultima Visita" SortExpression="LastLoginDate" ReadOnly="True"  />
                                <asp:BoundField DataField="LastActivityDate" HeaderText="Ultima Actividad" SortExpression="LastActivityDate" Visible="False" />
                                <asp:BoundField DataField="ProviderName" HeaderText="ProviderName" ReadOnly="True" Visible="False"
                                    SortExpression="ProviderName" />
                                <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate" Visible="False"
                                    ReadOnly="True" SortExpression="LastPasswordChangedDate" />
                                <asp:BoundField DataField="PasswordQuestion" HeaderText="PasswordQuestion" ReadOnly="True" Visible="False"
                                    SortExpression="PasswordQuestion" />
                                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" ReadOnly="True" Visible="False"
                                    SortExpression="IsLockedOut" />
                                <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="True" Visible="False"
                                    SortExpression="LastLockoutDate" />
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSourceMembershipUser" runat="server" DeleteMethod="Delete"
                            InsertMethod="Insert"  SelectMethod="GetMembers"
                            TypeName="MembershipUtilities.MembershipUserODS" UpdateMethod="Update"
                            SortParameterName="SortData" >
                            <DeleteParameters>
                                <asp:Parameter Name="UserName" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="isApproved" Type="Boolean" />
                                <asp:Parameter Name="comment" Type="String" />
                                <asp:Parameter Name="lastActivityDate" Type="DateTime" />
                                <asp:Parameter Name="lastLoginDate" Type="DateTime" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:Parameter Name="sortData" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="userName" Type="String" />
                                <asp:Parameter Name="isApproved" Type="Boolean" />
                                <asp:Parameter Name="comment" Type="String" />
                                <asp:Parameter Name="lastLockoutDate" Type="DateTime" />
                                <asp:Parameter Name="creationDate" Type="DateTime" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="lastActivityDate" Type="DateTime" />
                                <asp:Parameter Name="providerName" Type="String" />
                                <asp:Parameter Name="isLockedOut" Type="Boolean" />
                                <asp:Parameter Name="lastLoginDate" Type="DateTime" />
                                <asp:Parameter Name="isOnline" Type="Boolean" />
                                <asp:Parameter Name="passwordQuestion" Type="String" />
                                <asp:Parameter Name="lastPasswordChangedDate" Type="DateTime" />
                                <asp:Parameter Name="password" Type="String" />
                                <asp:Parameter Name="passwordAnswer" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
            <br />
            <br />
    <br />
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridViewRole" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceRoleObject"
                            DataKeyNames="RoleName" CellPadding="4" AllowPaging="True" CssClass="text1" ForeColor="#333333" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CausesValidation="false" Width="400px" OnClick="ToggleInRole_Click"
                                            Text='<%# ShowInRoleStatus( (string) Eval("UserName"),(string) Eval("RoleName")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfUsersInRole" HeaderText="Numero de Usuarios en Grupo"
                                    SortExpression="NumberOfUsersInRole" />
                                <asp:BoundField DataField="RoleName" ReadOnly="True" Visible="False" HeaderText="Nombre de Grupo"
                                    SortExpression="RoleName" />
                                <asp:CheckBoxField DataField="UserInRole" HeaderText="Usuarios en Grupo" Visible="False"
                                    SortExpression="UserInRole" />
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                        <asp:CheckBox ID="CheckBoxShowRolesAssigned" runat="server" AutoPostBack="True" Text="Mostrar solo grupos asignados" CssClass="text4" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="ButtonCreateNewRole" runat="server" OnClick="ButtonCreateNewRole_Click"
                            Text="Crear nuevo grupo" CssClass="text1" Visible="False" />
                        <asp:TextBox ID="TextBoxCreateNewRole" runat="server" CssClass="text4" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            
            <asp:ObjectDataSource ID="ObjectDataSourceRoleObject" runat="server" SelectMethod="GetRoles"
                TypeName="MembershipUtilities.RoleDataObject" InsertMethod="Insert" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" >
                <SelectParameters>
                    <asp:ControlParameter ControlID="GridViewMemberUser" Name="UserName" PropertyName="SelectedValue"
                        Type="String" />
                    <asp:ControlParameter ControlID="CheckBoxShowRolesAssigned" Name="ShowOnlyAssignedRolls"
                        PropertyName="Checked" Type="Boolean" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="RoleName" Type="String" />
                </InsertParameters>
                <DeleteParameters>
                    <asp:Parameter Name="RoleName" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
</asp:Content>
