<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="requests_app.admin_Users" Codebehind="UserListing.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" UseJavascriptUnhide="false" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlPermission" runat="server" />

<cuc:GridView Width="600px" DataSourceID="dsUsers" AllowPaging="true" PageSize="50" DataKeyNames="UserID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True" ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow" EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" CssClass="GridView MaxWidth" ID="gvUsers" AutoGenerateColumns="false" AllowSorting="true" >
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
    <asp:TemplateField AccessibleHeaderText="colEdit" HeaderStyle-CssClass="Width75px">
			<ItemTemplate>                
				<a href="<%# "UserDetails.aspx?UserID=" + Eval("UserID") %>" class="FormButton"><span>Подр.</span></a>
			</ItemTemplate>
		</asp:TemplateField>
    <asp:TemplateField AccessibleHeaderText="colDelete" HeaderStyle-CssClass="Width75px">
			<ItemTemplate>
				<asp:LinkButton ID="btnDelete" CommandName="Delete" runat="server" CssClass="FormButton" OnClientClick=<%# "return confirm('Вы действительно хотите идалить элемент?');" %>><span>Удал.</span></asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
        <asp:BoundField HeaderText="ИД" DataField="UserID" SortExpression="UserID" />
        <asp:BoundField HeaderText="Имя" DataField="UserName" SortExpression="UserName" />
        <asp:BoundField HeaderText="Логин" DataField="UserLogin" SortExpression="UserLogin" />
        <asp:BoundField HeaderText="Группа" DataField="UserLocation" SortExpression="UserLocation" />
		<asp:TemplateField HeaderText="Статус" SortExpression="IsActive">
			<ItemTemplate>	
                <asp:CheckBox runat="server" ID="cbIsActive" Checked='<%# Eval("IsActive")%>' Enabled="false" />                						
			</ItemTemplate>
		</asp:TemplateField>		
	</Columns>
</cuc:GridView>
<asp:SqlDataSource ID="dsUsers" runat="server" 
	ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
	SelectCommand="RetrieveUsers" SelectCommandType="StoredProcedure"
	DeleteCommand="DeleteUser" DeleteCommandType="StoredProcedure">
	<SelectParameters>
					
	</SelectParameters>
	<DeleteParameters>
		<asp:Parameter Name="UserID" Type="Int32" />
	</DeleteParameters>
</asp:SqlDataSource>
</asp:Content>

