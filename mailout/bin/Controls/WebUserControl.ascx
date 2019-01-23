<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebUserControl" Codebehind="WebUserControl.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="ConvincingMail.AdvancedAutoSuggest" Namespace="ConvincingMail.AdvancedAutoSuggest" TagPrefix="cc1" %>

<div style="position:absolute;top:50%; left:50%; z-index:1000;">
						<asp:TextBox ID="CityTextBox" runat="server" AutoCompleteType="none"></asp:TextBox>					

						<asp:TextBox ID="CityIdTextBox" runat="server" Enabled="false" Width="30"></asp:TextBox>
			<cc1:AdvancedAutoSuggestExtender TargetControlID="CityTextBox" ServiceUrl="~/Utility.asmx/CitySuggest" UpdateField="CityIdTextBox" ID="CityAASE" runat="server" />
            </div>