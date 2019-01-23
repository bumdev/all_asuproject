<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="kipia_web_application.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/style.css" rel="stylesheet" type="text/css" />
    <link href="Css/test.css" rel="stylesheet" type="text/css" />
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="panLogin" DefaultButton="lbLogin">
    <div style="width:400px; height:300px; position:absolute; left:50%; top:50%; margin-left:-200px; margin-top:-150px;">
        <fieldset style="padding:10px;">
            <legend  style="color:White;">Вход в систему:</legend>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription" style="color:White;">Логин:</div>
	            <div class="CommonFormData"><asp:TextBox runat="server" ID="tbLogin" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvLogin" ControlToValidate="tbLogin" Text="*" ErrorMessage="Поле Логин не заполнено."></asp:RequiredFieldValidator>                </div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription" style="color:White;">Пароль:</div>
	            <div class="CommonFormData"><asp:TextBox runat="server" ID="tbPass" TextMode="Password" Width="200px"></asp:TextBox>
                    
                <asp:RequiredFieldValidator runat="server" ID="rfvPass" ControlToValidate="tbPass" Text="*" ErrorMessage="Поле Пароль не заполнено."></asp:RequiredFieldValidator>
                </div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription"><asp:LinkButton runat="server" ID="lbLogin" CssClass="FormButton" onclick="lbLogin_Click"><span>Войти</span></asp:LinkButton></div>
	            <div class="CommonFormData"><asp:ValidationSummary runat="server" ID="vs" CssClass="Error" /></div>
	            <div class="ClearBoth"></div>
            </div>
        </fieldset>
    </div>
    </asp:Panel>
    </form>
</body>
</html>
