<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="password.aspx.cs" Inherits="Grundfos.StockForecast.users.password" Title="Cambio de Contraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <table style="width: 273px; height: 126px">
        <tr>
            
            <td>
                <asp:ChangePassword ID="ChangePassword1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8"
                    BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="Small" CancelButtonText="Cancelar" CancelDestinationPageUrl="~/default.aspx" ChangePasswordButtonText="Cambiar Contraseña" ChangePasswordFailureText="Contrasela incorrecta o nueva contraseña invalida. Tamaño minimo requerido: {0}. Caracteres no alfanumericos requeridos: {1}." ChangePasswordTitleText="Cambiar Contraseña" ConfirmNewPasswordLabelText="Confirmar nueva contraseña:" ConfirmPasswordCompareErrorMessage="La confirmación de la nueva contraseña debe coincidir con la nueva contraseña." ConfirmPasswordRequiredErrorMessage="Confirmación de nueva contraseña requerida." ContinueButtonText="Continuar" ContinueDestinationPageUrl="~/default.aspx" CreateUserText="Creación de cuentas de usuarios." CreateUserUrl="~/administration/newuser.aspx" CssClass="text1" DisplayUserName="True" NewPasswordLabelText="Nueva contraseña:" NewPasswordRegularExpressionErrorMessage="Por favor ingrese una contraseña distinta." NewPasswordRequiredErrorMessage="Nueva contraseña requerida." PasswordLabelText="Contraseña:" PasswordRecoveryText="Olvidaste tu contraseña?" PasswordRecoveryUrl="~/login.aspx" PasswordRequiredErrorMessage="Contraseña requerida." SuccessText="Su contraseña fue cambiada." SuccessTitleText="Cambio de contraseña completado." UserNameLabelText="Usuario:" UserNameRequiredErrorMessage="Usuario requerido.">
                    <CancelButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                    <PasswordHintStyle Font-Italic="True" ForeColor="#888888" />
                    <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                    <ChangePasswordButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                    <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                    <TextBoxStyle Font-Size="0.8em" />
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                </asp:ChangePassword>
            </td>
            
        </tr>
    </table>
</asp:Content>
