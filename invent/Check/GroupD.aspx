<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="GroupD.aspx.cs" Inherits="invent_app.GroupD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; float:inherit; text-align:center;"><span style="font-weight:bold; font-size:18px; ">Группы</span></div>
<asp:TextBox runat="server" ID="tbGroup" Width="400px"  Visible="true"></asp:TextBox>
<asp:LinkButton runat="server" ID="lbAdd" CssClass="FormButton" 
        onclick="lbAdd_Click" Visible="true"><span>Добавить</span></asp:LinkButton>
<cuc:GridView  DataSourceID="dsJournal" AllowPaging="True" PageSize="50" 
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="True" ShowFooter="True" 
        ShowHeaderWhenEmpty="True" HighlightedRowCssClass="HighlightedRow" 
        EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" 
        CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="False" 
        AllowSorting="True" >
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
        <asp:TemplateField Visible="true" AccessibleHeaderText="colEdit"  HeaderStyle-Width="175px">
            <ItemTemplate>
			    <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server"><span>Редакт</span></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить группу?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        <asp:BoundField HeaderText="Название" ControlStyle-Width="400px" DataField="GroupName" SortExpression="GroupName"/>
	</Columns>
</cuc:GridView>
<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveGroup" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateGroups" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteGroups" DeleteCommandType="StoredProcedure"
InsertCommand="CreateGroups" InsertCommandType="StoredProcedure">
    <UpdateParameters>
        <asp:Parameter Name="ID" Type="Int32" />
        <asp:Parameter Name="GroupName" Type="String" />
    </UpdateParameters>
    <DeleteParameters>
	    <asp:Parameter Name="ID" Type="Int32" />
	</DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="GroupName" Type="String" />
    </InsertParameters>
</asp:SqlDataSource>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
