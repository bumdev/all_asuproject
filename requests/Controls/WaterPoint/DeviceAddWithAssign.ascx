<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceAddWithAssign.ascx.cs" Inherits="requests_app.DeviceAddWithAssign" %>
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