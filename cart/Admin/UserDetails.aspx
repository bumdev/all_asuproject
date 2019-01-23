<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" Inherits="cartridges_app.Admin_UserAdd" Codebehind="UserDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlUser" runat="server" />
    <asp:HiddenField runat="server" ID="hfUserPassword" />
    <asp:HiddenField runat="server" ID="hfID" />
<div class="CommonFormElement">
	<div class="CommonFormDescription">Имя:</div>
	<div class="CommonFormData"><asp:TextBox runat="server" ID="tbName" Width="200px"></asp:TextBox></div>
	<div class="ClearBoth"></div>
</div>
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
<div class="CommonFormElement">
	<div class="CommonFormDescription">Подтверждение:</div>
	<div class="CommonFormData"><asp:TextBox runat="server" ID="tbPasswordConfirm" Width="200px" TextMode="Password"></asp:TextBox></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Группа:</div>
	<div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlGroup"></asp:DropDownList> </div>
	<div class="ClearBoth"></div>  
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Статус:</div>
	<div class="CommonFormData"><asp:CheckBox runat="server" ID="cbIsActive" /></div>
	<div class="ClearBoth"></div>  
</div>
<div class="CommonFormButtons">
	<asp:LinkButton runat="server" ID="lbSave"  CssClass="FormButton" 
        onclick="lbSave_Click"><span>Сохранить</span></asp:LinkButton>
</div>
<div class="ClearBoth"></div>
<asp:Panel runat="server" ID="panPermissions">
<div style="width:610px; border:0px solid black;">
    <div style="width:250px; float:left; border:0px solid black;"><asp:ListBox runat="server" ID="lbAllPermissions" Width="200px" Height="300px"></asp:ListBox></div>
    <div style="width:100px; float:left; border:0px solid black;">
        <asp:LinkButton runat="server" ID="lbAdd" CssClass="FormButton" 
            onclick="lbAdd_Click"><span>>>>>>>></span></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbDelete" CssClass="FormButton" 
            onclick="lbDelete_Click"><span><<<<<<<</span></asp:LinkButton>
    </div>
    <div style="width:250px; float:left; border:0px solid black;"><asp:ListBox runat="server" ID="lbUserPermissions" Width="200px" Height="300px"></asp:ListBox></div>
</div>
</asp:Panel>


</asp:Content>