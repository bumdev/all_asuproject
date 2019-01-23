<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="InComMail.aspx.cs" Inherits="ClericalWork_WebApp.InComMail" %>
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
      
        <MasterTableView EditMode="PopUp" IsFilterItemExpanded="True" EditFormSettings-PopUpSettings-Width="900px" DataKeyNames="ID" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="IncommingMail"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="False" />
            
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn> 
                

                <telerik:GridBoundColumn runat="server" DataField="RegNumber"  HeaderText="Регистрационный номер" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_reg" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateRegistration" HeaderText="Дата регистрации" />
                <telerik:GridBoundColumn runat="server" DataField="Correspondent" HeaderText="Корреспондент" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" runat="server" HeaderText="Адрес" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RegNumberJuridical" HeaderText="Регистрационный номер предприятия" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_juridical" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateJuridical" HeaderText="Дата предприятия" />
                <telerik:GridBoundColumn DataField="CodeFrom" HeaderText="Код откуда" runat="server" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_plane" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="PlaneDate" HeaderText="Плановая дата" />
                <telerik:GridDateTimeColumn UniqueName="date_exec" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateExecution" HeaderText="Дата выполнения" />
                <telerik:GridBoundColumn DataField="TypeLetter" runat="server" HeaderText="Тип письма" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TextResolution" runat="server" HeaderText="Текст резолюции" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_view" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateView" HeaderText="Дата просмотра" />
                <telerik:GridBoundColumn DataField="Viewer" runat="server" HeaderText="Рассмотрел" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RegNumberOut" runat="server" HeaderText="Исходящий номер" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TypeMail" runat="server" HeaderText="Тип почты" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Comment" runat="server" HeaderText="Примечание" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="rc" HeaderStyle-HorizontalAlign="Center" DropDownControlType="RadComboBox" DataField="ResponsibleContractorID" HeaderText="Исполнитель" DataSourceID="dsContractor" 
                    ListTextField="NameResponsibleContractor" ListValueField="id" ItemStyle-Wrap="true" ItemStyle-Width="300" HeaderStyle-Width="300">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server"  Width="250px" ID="ResponsibleContractorID" DataSourceID="dsContractor" DataTextField="NameResponsibleContractor"
                            DataValueField="id" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("rc").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="DirectContract" Filter="Contains"
                            EmptyMessage="Поиск...">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function DirectContract(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("rc", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridButtonColumn runat="server" CommandName="Delete" Text="Delete" ButtonType="ImageButton" ConfirmText="Вы точно хотите удалить?" />
                
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />            
        </EditFormSettings>
        

        </MasterTableView>
     
</telerik:RadGrid>
    
        

<asp:SqlDataSource ID="dsContractor" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveResponContract"></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
    SelectCommand="RetrieveIncommingMail" SelectCommandType="StoredProcedure" 
    InsertCommand="CreateIncommingMail" InsertCommandType="StoredProcedure"
    UpdateCommand="UpdateIncommingMail" UpdateCommandType="StoredProcedure"
    DeleteCommand="DeleteIncommingMail" DeleteCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


