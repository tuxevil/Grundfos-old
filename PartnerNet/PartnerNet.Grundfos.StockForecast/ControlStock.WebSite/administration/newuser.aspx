<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="newuser.aspx.cs" Inherits="Grundfos.StockForecast.administration.newuser" Title="Creacion de Cuentas de Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8"
        BorderStyle="Solid" BorderWidth="1px" CssClass="text1" Font-Names="Verdana" Font-Size="Small" AnswerLabelText="Respuesta de Seguridad:" AnswerRequiredErrorMessage="Respuesta de seguridad requerida." CancelButtonText="Cancelar" CompleteSuccessText="Su cuenta ha sido creada con exito." ConfirmPasswordCompareErrorMessage="La contraseña y la confirmacion deben coincidir." ConfirmPasswordLabelText="Confirmar Contraseña:" ConfirmPasswordRequiredErrorMessage="Confirmacion de contraseña requerida." ContinueButtonText="Continuar" CreateUserButtonText="Crear Usuario" DuplicateEmailErrorMessage="La direccion de e-mail ya esta en uso. Seleccione otra diferente." DuplicateUserNameErrorMessage="Por favor ingrese un usuario diferente." EmailRegularExpressionErrorMessage="Por vafor ingrese un e-mail diferente." EmailRequiredErrorMessage="E-mail requerido." FinishCompleteButtonText="Finalizar" FinishPreviousButtonText="Anterior" InvalidAnswerErrorMessage="Por favor ingrese una respuesta de seguridad diferente." InvalidEmailErrorMessage="Por favor ingrese un e-mail valido." InvalidQuestionErrorMessage="Por favor ingrese una pregunta de seguridad diferente." PasswordLabelText="Contraseña:" PasswordRegularExpressionErrorMessage="Por favor ingrese una contraseña diferente." PasswordRequiredErrorMessage="Contraseña requerida." QuestionLabelText="Pregunta de Seguridad:" QuestionRequiredErrorMessage="Pregunta de seguridad requerida." StartNextButtonText="Siguiente" StepNextButtonText="Siguiente" StepPreviousButtonText="Anterior" UnknownErrorMessage="Su cuenta no fue creada, por favor intentelo de nuevo." UserNameLabelText="Usuario:" UserNameRequiredErrorMessage="Usuario requerido:" InvalidPasswordErrorMessage="Longitud mínima de contraseña: {0}. Caracteres no alfanuméricos requeridos: {1}." ContinueDestinationPageUrl="~/default.aspx">
        <SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" Font-Size="0.9em" VerticalAlign="Top" />
        <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
        <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" Font-Size="0.9em"
            ForeColor="White" HorizontalAlign="Center" />
        <CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <StepStyle BorderWidth="0px" />
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" Title="Creaci&#243;n de Cuentas">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server" Title="Completado">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
