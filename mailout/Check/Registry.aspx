<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Registry.aspx.cs" Inherits="outcomming_mail.Registry" %>
<%@ Register src="../Controls/TypeAdd.ascx" tagname="TypeAdd" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div style="width:100%; margin-top:7px; border-bottom:3px solid black; height:30px;display: none;">
<asp:DropDownList runat="server" ID="ddlSeller"></asp:DropDownList>
<asp:DropDownList runat="server" ID="ddlDiametr"></asp:DropDownList>
<div style="float:right; width:100px;">
<asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" OnClick="lbSearch_Click"><span>Искать</span></asp:LinkButton></div>
</div>
<div>
<div style="width:200px; float:inherit; display: none;">
<asp:LinkButton runat="server" ID="lbTypeAdd" CssClass="FormButton" 
        onclick="lbTypeAdd_Click"><span>Добавить тип водомера</span></asp:LinkButton>
</div>

    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="30"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridDevice_UpdateCommand" >
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Vodomer"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>   
                <telerik:GridBoundColumn DataField="diameter"  HeaderText="Диаметр" SortExpression="diameter" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="conventional_signth"  HeaderText="Модель" SortExpression="conventional_signth"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="description"  HeaderText="Описание" SortExpression="description"></telerik:GridBoundColumn>
                 <telerik:GridDropDownColumn   UniqueName="sl" DropDownControlType="RadComboBox" DataField="id_seller" HeaderText="Объект" DataSourceID="dsSeller" ListTextField="seller" ListValueField="ID" ItemStyle-Wrap="true">
                     <FilterTemplate>                                       
                       <telerik:RadComboBox Width="180px" ID="id_seller" DataSourceID="dsSeller" DataTextField="seller" 
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("sl").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="TitleIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="Все" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function TitleIndexChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("sl", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>  
                <telerik:GridBoundColumn DataField="GovRegister"  HeaderText="Гос реестр" SortExpression="GovRegister"  HeaderTooltip="Номер по гос реестру"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CheckInterval"  HeaderText="МПИ" SortExpression="CheckInterval" HeaderTooltip="Межповерочный интервал"  FilterControlWidth="30px"></telerik:GridBoundColumn>
                 <telerik:GridCheckBoxColumn DataField="Active"  HeaderText="Не Выпуск" SortExpression="Active"  HeaderTooltip="Производится"></telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="Approve"  HeaderText="ВДК" SortExpression="Approve"  HeaderTooltip="Поверяется нами"></telerik:GridCheckBoxColumn>
                 <telerik:GridBoundColumn DataField="gear_ratio"  HeaderText="ПК" SortExpression="gear_ratio"  HeaderTooltip="Проливочный коэфициент"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DateProduced"  HeaderText="Произведен" SortExpression="DateProduced"    ReadOnly="true"  FilterControlWidth="30px"></telerik:GridBoundColumn>
                 
                
                
               <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
        </EditFormSettings>
        </MasterTableView>
</telerik:RadGrid>    

<cuc:GridView  DataSourceID="dsJournal" AllowPaging="True" PageSize="50"  Visible="false"
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
    InsertCommand="CreateRegistry" InsertCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
<uc1:TypeAdd ID="TypeAdd1" runat="server" Visible="false" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
