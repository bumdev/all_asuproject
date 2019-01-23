<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="DeviceAdd.aspx.cs" Inherits="cartridges_app.DeviceAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlDevice" runat="server" />
<div class="CommonFormElement">
	<div class="CommonFormDescription">Тип прибора:</div>
	<div class="CommonFormData"><telerik:RadDropDownList runat="server" ID="ddlTypeDevice"></telerik:RadDropDownList></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Название:</div>
	<div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbTitle"></telerik:RadTextBox></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Заводской №:</div>
	<div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbFN"></telerik:RadTextBox></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription">Описание:</div>
	<div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbDescription" TextMode="MultiLine" Width="350px" Height="90px"></telerik:RadTextBox></div>
	<div class="ClearBoth"></div>
</div>
<div class="CommonFormElement">
	<div class="CommonFormDescription"></div>
	<div class="CommonFormData"><telerik:RadButton runat="server" ID="lbSave" Text="Сохранить" onclick="lbSave_Click"></telerik:RadButton></div>
	<div class="ClearBoth"></div>
</div>

</asp:Content>


