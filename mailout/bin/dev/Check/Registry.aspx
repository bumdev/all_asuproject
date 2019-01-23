<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Registry.aspx.cs" Inherits="kipia_web_application.Registry" %>
<%@ Register src="../Controls/TypeAdd.ascx" tagname="TypeAdd" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div style="width:100%; margin-top:7px; border-bottom:3px solid black; height:30px;">
<asp:DropDownList runat="server" ID="ddlSeller"></asp:DropDownList>
<asp:DropDownList runat="server" ID="ddlDiametr"></asp:DropDownList>
<div style="float:right; width:100px;">
<asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" OnClick="lbSearch_Click"><span>Искать</span></asp:LinkButton></div>
</div>
<div>
<div style="width:200px; float:inherit;">
<asp:LinkButton runat="server" ID="lbTypeAdd" CssClass="FormButton" 
        onclick="lbTypeAdd_Click"><span>Добавить тип водомера</span></asp:LinkButton>
</div>
<div style="width:100%; float:inherit; text-align:center;"><span style="font-weight:bold; font-size:18px; margin-left:-100px;">Реестр водомеров</span></div>
</div>
    
<cuc:GridView  DataSourceID="dsJournal" AllowPaging="True" PageSize="50" 
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="True" ShowFooter="True" 
        ShowHeaderWhenEmpty="True" HighlightedRowCssClass="HighlightedRow" 
        EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" 
        CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="False" 
        AllowSorting="True" onrowupdating="gvJournal_RowUpdating" >
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
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить тип водомера?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        <asp:BoundField HeaderText="Диаметр" DataField="diameter" SortExpression="diameter"/>
        <asp:CheckBoxField HeaderText="НП" DataField="Active" SortExpression="Active"/>
        <asp:BoundField HeaderText="Гос регистрация" DataField="GovRegister" SortExpression="GovRegister"/>
        <asp:BoundField HeaderText="ПИ" DataField="CheckInterval" SortExpression="CheckInterval"/>
        <asp:CheckBoxField HeaderText="ВДК" DataField="Approve" SortExpression="Approve"/>
        <asp:BoundField HeaderText="Произведен" DataField="DateProduced" SortExpression="DateProduced"  DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
        
        <asp:TemplateField HeaderText="Водомер" SortExpression="conventional_signth">
            <ItemTemplate>
                <b>Модель:&nbsp;<asp:Literal runat="server" ID="litModel" Text=<%# Eval("conventional_signth") %>></asp:Literal></b><br/>
                Производитель:&nbsp<asp:Literal runat="server" ID="litSeller"  Text=<%# Eval("seller") %>></asp:Literal><br/>
                Описание:&nbsp;<asp:Literal runat="server" ID="litDescription"  Text=<%# Eval("description") %>></asp:Literal>
            </ItemTemplate>
            <EditItemTemplate>
                <b>Модель:&nbsp;<asp:TextBox runat="server" ID="tbModel" Text=<%# Eval("conventional_signth") %> Width="300px"></asp:TextBox></b><br/>
                Производитель:&nbsp<asp:DropDownList runat="server" ID="ddlSeller"  Width="300px" DataSourceID="dsSeller" DataTextField="seller" DataValueField="id" SelectedValue=<%# Eval("id_seller") %>></asp:DropDownList><br/>
                Описание:&nbsp;<asp:TextBox runat="server" ID="tbDescription"  Text=<%# Eval("description") %> Width="300px"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>	
	</Columns>
</cuc:GridView>

<asp:SqlDataSource ID="dsSeller" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveSellers" ></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveRegistry" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateRegisty" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteRegistry" DeleteCommandType="StoredProcedure" 
OnSelecting="dsJournal_Selecting" >
	<SelectParameters>
         <asp:ControlParameter ControlID="ddlSeller" PropertyName="SelectedValue" Name="Seller" Type="Int32" />
         <asp:ControlParameter ControlID="ddlDiametr" PropertyName="SelectedValue" Name="Diametr" Type="Int32" />
	</SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="ID" Type="Int32" />
        <asp:Parameter Name="diameter" Type="Int32" />
        <asp:Parameter Name="Active" Type="Boolean" />
        <asp:Parameter Name="GovRegister" Type="String" />
        <asp:Parameter Name="CheckInterval" Type="Int32" />
        <asp:Parameter Name="Approve" Type="Boolean" />
        <asp:Parameter Name="id_seller" Type="Int32" />
        <asp:Parameter Name="conventional_signth" Type="String" />
        <asp:Parameter Name="description" Type="String" />
    </UpdateParameters>
    <DeleteParameters>
					<asp:Parameter Name="ID" Type="Int32" />
				</DeleteParameters>
</asp:SqlDataSource>
</div>
<uc1:TypeAdd ID="TypeAdd1" runat="server" Visible="false" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
