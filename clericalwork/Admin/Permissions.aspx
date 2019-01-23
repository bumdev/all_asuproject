<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="ClericalWork_WebApp.admin_Permissions" Codebehind="Permissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" UseJavascriptUnhide="false" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlPermission" runat="server" />
<cuc:GridView Width="400px" DataSourceID="dsPermissions" AllowPaging="true" PageSize="50" DataKeyNames="PermissionID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True" ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow" EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" CssClass="GridView MaxWidth" ID="gvPermissions" AutoGenerateColumns="false" AllowSorting="true" OnRowCommand="gvPermissions_RowCommand">
	<AlternatingRowStyle CssClass="GridViewAltRow" />
	<RowStyle CssClass="GridViewRow" />
	<SortAscendingStyle CssClass="Sorted SortAscending" />
	<SortDescendingStyle CssClass="Sorted SortDescending" />
	<HeaderStyle CssClass="GridViewHeader" />
	<PagerStyle CssClass="GridViewHeader" />
	<EmptyDataTemplate>
			Нет данных
	</EmptyDataTemplate>
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>	
                <%# Eval("PermissionID")%>						
			</ItemTemplate>
			<FooterTemplate>
				<asp:LinkButton ID="btnAdd" CommandName="Insert" CausesValidation="true" CssClass="FormButton" runat="server"><span>Добавить</span></asp:LinkButton>
			</FooterTemplate>
		</asp:TemplateField>
        <asp:TemplateField>
			<ItemTemplate>
                <%# Eval("PermissionName")%>	    							
			</ItemTemplate>
			<FooterTemplate>
				<asp:TextBox runat="server" ID="tbPermission"></asp:TextBox>
			</FooterTemplate>
		</asp:TemplateField>
		<asp:TemplateField AccessibleHeaderText="colDelete" HeaderStyle-CssClass="Width75px">
			<ItemTemplate>
				<asp:LinkButton ID="btnDelete" CommandName="Delete" runat="server" CssClass="FormButton" OnClientClick=<%# "return confirm('Вы действительно хотите идалить элемент?');" %>><span>Удал.</span></asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</cuc:GridView>
<asp:SqlDataSource ID="dsPermissions" runat="server" 
	ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
	SelectCommand="RetrievePermissions" SelectCommandType="StoredProcedure"
	DeleteCommand="DeletePermission" DeleteCommandType="StoredProcedure">
	<SelectParameters>
					
	</SelectParameters>
	<DeleteParameters>
		<asp:Parameter Name="PermissionID" Type="Int32" />
	</DeleteParameters>
</asp:SqlDataSource>
</asp:Content>

