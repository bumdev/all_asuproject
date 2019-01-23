<%@ Page Language="C#" AutoEventWireup="true" Inherits="cartridges_app.admin_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="panLogin" DefaultButton="lbLogin">
     <div style="width:100%; height:100%; padding-top:250px;">
        <div style="width:250px; height:200px; border:1px solid black; position:absolute; left:50%;top:50%; margin-top:-100px;margin-left:-125px; padding:10px;">
            <div class="CommonFormElement">
		        <div class="CommonFormDescription">Логин:</div>
		        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbLogin" Width="200px"></asp:TextBox></div>
		        <div class="ClearBoth"></div>
	        </div>
            <div class="CommonFormElement">
		        <div class="CommonFormDescription">Пароль:</div>
		        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbPassword" Width="200px" TextMode="Password"></asp:TextBox></div>
		        <div class="ClearBoth"></div>
	        </div>
            <div class="CommonFormButtons">
				<asp:LinkButton runat="server" ID="lbLogin"  CssClass="FormButton" onclick="lbLogin_Click"><span>Вход</span></asp:LinkButton>
                <asp:Literal runat="server" ID="litInfo"></asp:Literal>
			</div>
            <a href="../Direction.aspx">Панель</a>
        </div>
    </div>
    </asp:Panel>
    </form>
</body>
</html>
