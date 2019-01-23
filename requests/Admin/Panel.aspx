<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" Inherits="requests_app.Admin_Panel" Codebehind="Panel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Добро пожаловать в панель администратора</h1>
<div style="width:600px; height:150px; border:1px solid black;">
<div style="width:600px; height:30px; margin-bottom:10px; border-bottom:1px solid black;">
<span style="font-size:16px;">Пользователи</span>
</div>
<a href="UserListing.aspx"><div style="background-image:url('../images/business_users.png'); width:100px;height:100px; float:left; margin-left:25px;"></div></a>
<a href="UserDetails.aspx?New=True"><div style="background-image:url('../images/business_users_add.png'); width:100px;height:100px; float:left; margin-left:25px;"></div></a>
<a href="Permissions.aspx"><div style="background-image:url('../images/Key-Icon-250x250.jpg'); width:100px;height:100px; float:left; margin-left:25px;"></div></a>
</div>
<!--
<div style="width:600px; height:100px; border:1px solid black;">

</div>
<div style="width:600px; height:100px; border:1px solid black;">

</div>
<div style="width:600px; height:100px; border:1px solid black;">

</div>
-->
</asp:Content>

