<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="ServiceAdd.aspx.cs" Inherits="leak_detectors.ServiceAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="../Css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlDevice" runat="server" />
<div class="CommonFormElement">
	<div class="CommonFormDescription">Тип выезда:</div>
	<div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlTypeService"></asp:DropDownList></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Заявка от:</div>
	<div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlSource"></asp:DropDownList></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Объект заявки:</div>
	<div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlWP"></asp:DropDownList></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Список работ:</div>
	<div class="CommonFormData">123123123</div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Описание:</div>
	<div class="CommonFormData"><asp:TextBox runat="server" ID="tbDescription" TextMode="MultiLine" Width="350px" Height="90px"></asp:TextBox></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription"></div>
	<div class="CommonFormData"><asp:LinkButton runat="server" ID="lbSave" CssClass="FormButton"><span>Сохранить</span></asp:LinkButton></div>
	<div class="ClearBoth"></div>
</div>
</asp:Content>
