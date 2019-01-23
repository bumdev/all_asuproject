<%@ Page Title="Справочник сотрудников" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="Timetable_WebApp.Employees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link runat="server" rel="shortcut icon" href="~/timetable_logo.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="~/timetable_logo.ico" type="image/ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <telerik:RadScriptManager runat="server" ID="RadScriptManager5" />--%>
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
        <ProgressTemplate>
            Подождите... <img src="images/loading-400 (1).gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; float:inherit; text-align:center;"><span style="font-weight:bold; font-size:18px; ">Справочник сотрудников</span></div>
<asp:TextBox runat="server" ID="tbTableNumber" Width="400px"  Visible="false"></asp:TextBox>
<asp:TextBox runat="server" ID="tbName" Width="400px" Visible="false"></asp:TextBox>
<asp:TextBox runat="server" ID="tbSex" Width="400px" Visible="false"></asp:TextBox>
<asp:TextBox runat="server" ID="tbPosition" Width="400px" Visible="false"></asp:TextBox>
<asp:TextBox runat="server" ID="tbSelery" Width="400px" Visible="false"></asp:TextBox>

<asp:LinkButton runat="server" ID="lbAdd" CssClass="FormButton" 
        onclick="lbAdd_Click" Visible="false"><span>Добавить</span></asp:LinkButton>
<asp:LinkButton runat="server" ID="lbRefresh" CssClass="FormButton"
    OnClick="Refresh_OnClick" Visible="true"><span>Обновить</span></asp:LinkButton>
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
        <asp:TemplateField Visible="false" AccessibleHeaderText="colEdit"  HeaderStyle-Width="175px">
            <ItemTemplate>
			    <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server"><span>Редакт</span></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить сотрудника?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        <asp:BoundField HeaderText="Табельный номер" ControlStyle-Width="400px" DataField="number" SortExpression="number"/>
	    <asp:BoundField HeaderText="ФИО" ControlStyle-Width="400px" DataField="name" SortExpression="name"/>
	    <asp:BoundField HeaderText="Пол" ControlStyle-Width="400px" DataField="sex" SortExpression="sex"/>
	    <asp:BoundField HeaderText="Должность" ControlStyle-Width="400px" DataField="position" SortExpression="position"/>
	    <asp:BoundField HeaderText="Оклад" ControlStyle-Width="400px" DataField="selery" SortExpression="selery"/>
	    
	</Columns>
</cuc:GridView>
<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEmployees" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateEmployees" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteEmployees" DeleteCommandType="StoredProcedure"
InsertCommand="CreateEmployees" InsertCommandType="StoredProcedure">
    <UpdateParameters>
        <asp:Parameter Name="ID" Type="Int32" />
        <asp:Parameter Name="number" Type="String" />
        <asp:Parameter Name="name" Type="String"/>
        <asp:Parameter Name="sex" Type="String" />
        <asp:Parameter Name="position" Type="String" />
        <asp:Parameter Name="selery" Type="Double" />

    </UpdateParameters>
    <DeleteParameters>
	    <asp:Parameter Name="ID" Type="Int32" />
	</DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="number" Type="String" />
        <asp:Parameter Name="name" Type="String"/>
        <asp:Parameter Name="sex" Type="String" />
        <asp:Parameter Name="position" Type="String" />
        <asp:Parameter Name="selery" Type="Double" />

    </InsertParameters>
</asp:SqlDataSource>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
