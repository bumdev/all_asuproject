<%@ Control Language="C#" AutoEventWireup="true" Inherits="requests_app.Controls_NotificationLabel" Codebehind="NotificationLabel.ascx.cs" %>
<asp:Panel runat="server" ID="pnl" CssClass="NotificationLabel" style="display: none;">
	<div class="RoundingContainer Top">
		<div class="Left"></div>
		<div class="Right"></div>
	</div>
	<div class="Body">
		<asp:Label runat="server" ID="lbl_Notification"></asp:Label>
	</div>
	<div class="RoundingContainer Bottom">
		<div class="Left"></div>
		<div class="Right"></div>
	</div>
</asp:Panel>
