<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Grundfos.StockForecast.login" Title="Inicio de Sesión" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4"
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="Small"
        ForeColor="#333333" CssClass="text1" DestinationPageUrl="~/default.aspx" FailureText="Su inicio de sesión no fue satisfactorio. Por favor intentelo de nuevo." LoginButtonText="Iniciar Sesión" PasswordLabelText="Contraseña:" PasswordRequiredErrorMessage="Contraseña requerida." RememberMeText="Recordarme la proxima vez." TitleText="Inicio de Sesión" UserNameLabelText="Usuario:" UserNameRequiredErrorMessage="Usuario requerido." DisplayRememberMe="False" CreateUserText="Creación de cuentas de usuarios." CreateUserUrl="~/administration/newuser.aspx">
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        <FailureTextStyle CssClass="text2" />
    </asp:Login>
    <br />
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" AnswerLabelText="Respuesta:"
        AnswerRequiredErrorMessage="Respuesta requerida." BackColor="#F7F6F3" BorderColor="#E6E2D8"
        BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" CssClass="text1" Font-Names="Verdana"
        Font-Size="Small" GeneralFailureText="Su intento de recuperación de contraseña no fue exitoso. Intente nuevamente."
        QuestionFailureText="Su respuesta no se pudo verificar. Intente nuevamente."
        QuestionInstructionText="Conteste la siguiente pregunta para recuperar su contraseña."
        QuestionLabelText="Pregunta:" QuestionTitleText="Confirmación de Identidad" SubmitButtonText="Continuar"
        SuccessText="Su contraseña le fue enviada por mail." UserNameFailureText="No pudimos acceder a su información. Intente de nuevo."
        UserNameInstructionText="Ingrese su usuario para recibir su contraseña." UserNameLabelText="Usuario:"
        UserNameRequiredErrorMessage="Usuario requerido." UserNameTitleText="Olvido su Contraseña?">
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <SuccessTextStyle Font-Bold="True" ForeColor="#5D7B9D" />
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        <SubmitButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
    </asp:PasswordRecovery>
</asp:Content>
