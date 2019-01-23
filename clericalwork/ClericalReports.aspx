<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="ClericalReports.aspx.cs" Inherits="ClericalWork_WebApp.ClericalReports" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
    <script src="scripts/openimage.js" type="text/javascript" ></script>     
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>
    <div class="FormItem">
        <div style="padding: 5px"></div>
        <div class="label">Отчет: </div>
        <div style="background: gray; width: 100%; height: 50px; border: solid 1px black">
            <div style="padding: 5px">
                <telerik:RadButton runat="server" ID="Makeort" Text="Сделать отчет" ButtonType="SkinnedButton" ></telerik:RadButton>&nbsp;&nbsp;&nbsp С &nbsp;&nbsp;&nbsp
                <telerik:RadDatePicker runat="server" ID="dpFrom"></telerik:RadDatePicker>
                &nbsp;&nbsp;По&nbsp;<telerik:RadDatePicker runat="server" ID="dpTo"></telerik:RadDatePicker>
            </div>
        </div>
    </div>
   
    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="10"
        onneeddatasource="radgridDevice_NeedDataSource" CssClass="rad" AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" ClientEvents-OnRequestStart="onRequestStart"
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridServices_UpdateCommand"
        onitemdatabound="radgrid_ItemDataBound" OnItemCommand="radgrid_itemcommand"
        AllowSorting="true" RenderMode="Auto">
      
      <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
         
      </ClientSettings>

         <ExportSettings ExportOnlyData="true" IgnorePaging="true">
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
      
        <MasterTableView IsFilterItemExpanded="True" EditFormSettings-PopUpSettings-Width="900px" DataKeyNames="ID" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Enterprises"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="False" />
            
            <Columns>
                <telerik:GridEditCommandColumn ItemStyle-Width="20px" UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn> 
                

                <%--<telerik:GridBoundColumn FilterControlWidth="40px" ItemStyle-Width="50px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="ID"  UniqueName="distr_id" ></telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="NameEnter" HeaderText="Название" SortExpression="NameEnter" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AddressEnter" HeaderText="Адрес" SortExpression="AddressEnter" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Описание" SortExpression="Description" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="org" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" DropDownControlType="RadComboBox" DataField="OrganiztionsID" HeaderText="Организация" DataSourceID="dsOrgan" 
                    ListTextField="NameOrganization" ListValueField="id" ItemStyle-Wrap="true">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server" Width="250px" ID="OrganiztionsID" DataSourceID="dsOrgan" DataTextField="NameOrganization"
                            DataValueField="id" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("org").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="DirectOrgan" Filter="Contains"
                            EmptyMessage="Поиск...">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function DirectOrgan(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("org", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                
                <telerik:GridButtonColumn runat="server" ItemStyle-Width="20px" CommandName="Delete" Text="Delete" ButtonType="ImageButton" ConfirmText="Вы точно хотите удалить?" />
                
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />            
        </EditFormSettings>
        

        </MasterTableView>
     
</telerik:RadGrid>


<asp:SqlDataSource ID="dsOrgan" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveOrganizations"></asp:SqlDataSource>
    
        
<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
    SelectCommand="RetrieveEnterprises" SelectCommandType="StoredProcedure" 
    InsertCommand="Createenterprises" InsertCommandType="StoredProcedure"
    UpdateCommand="UpdateEnterprises" UpdateCommandType="StoredProcedure"
    DeleteCommand="DeleteEnterprises" DeleteCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


