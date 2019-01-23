<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="Complaints.aspx.cs" Inherits="ClericalWork_WebApp.Complaints" %>
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
      
        <MasterTableView EditMode="PopUp" IsFilterItemExpanded="True" EditFormSettings-PopUpSettings-Width="900px" DataKeyNames="ID" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Complaints"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="False" />
            
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn> 
                
                <telerik:GridCheckBoxColumn Display="true" ReadOnly="false" DataField="IsAddContr"  HeaderText="Дополнительный контроль" UniqueName="IsAddContr" AllowFiltering="false"></telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn Display="true" ReadOnly="false" DataField="IsExecContr"  HeaderText="Выполнить дополнительно" UniqueName="IsExecContr" AllowFiltering="false"></telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn runat="server" DataField="RegNumber"  HeaderText="Регистрационный номер" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_comp" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateComplaints" HeaderText="Дата жалобы" />
                <telerik:GridBoundColumn runat="server" DataField="NameClient" HeaderText="ФИО" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn runat="server" DataField="AddressClient" HeaderText="Адрес" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDropDownColumn   UniqueName="dis" DropDownControlType="RadComboBox" DataField="DistrictID" HeaderText="Район" DataSourceID="dsDistrict" ListTextField="DistrictName" ListValueField="ID" ItemStyle-Wrap="true">
                     <FilterTemplate>                                       
                       <telerik:RadComboBox Width="180px" ID="DistrictID" DataSourceID="dsDistrict" DataTextField="DistrictName" 
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("dis").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="DistrictChanged" Filter="Contains">
                            <Items>
                                <telerik:RadComboBoxItem  />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock5" runat="server">
                            <script type="text/javascript">
                                function DistrictChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("dis", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridDateTimeColumn UniqueName="date_plane" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="PlaneDate" HeaderText="Плановая дата" />
                <telerik:GridBoundColumn runat="server" DataField="OutcommingNumber"  HeaderText="Исходящий номер" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_exec" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateExec" HeaderText="Дата выполнения" />
                <telerik:GridBoundColumn runat="server" DataField="FromSentComplaints"  HeaderText="Откуда передана жалоба" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn runat="server" DataField="CodeComplaintsJuridical"  HeaderText="Код жалобы предприятия" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_from" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateFrom" HeaderText="Дата откуда" />
                <telerik:GridDateTimeColumn UniqueName="appoint_date" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="AppointedTime" HeaderText="Назначенный срок" />
                <telerik:GridBoundColumn runat="server" DataField="Category"  HeaderText="Категория" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="rs" HeaderStyle-HorizontalAlign="Center" DropDownControlType="RadComboBox" DataField="ResponsibleServicesID" HeaderText="Ответственная служба" DataSourceID="dsServices" 
                    ListTextField="ServiceName" ListValueField="id" ItemStyle-Wrap="true" ItemStyle-Width="300" HeaderStyle-Width="300">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server"  Width="250px" ID="ResponsibleServicesID" DataSourceID="dsServices" DataTextField="ServiceName"
                            DataValueField="id" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("rs").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="DirectServices" Filter="Contains"
                            EmptyMessage="Поиск...">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                            <script type="text/javascript">
                                function DirectServices(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("rs", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridBoundColumn runat="server" DataField="NumberOrderMail"  HeaderText="Номер заказного письма" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_mail" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateOrderMail" HeaderText="Дата заказного письма" />
                <telerik:GridBoundColumn runat="server" DataField="ArchiveNumberOrder"  HeaderText="Архивный номер" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn UniqueName="date_contradd" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateAdditionalControl" HeaderText="Дата дополнительного контроля" />
                <telerik:GridDateTimeColumn UniqueName="date_execadd" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="DateExecAddControl" HeaderText="Дата выполнения дополнительного контроля" />

                <telerik:GridButtonColumn runat="server" CommandName="Delete" Text="Delete" ButtonType="ImageButton" ConfirmText="Вы точно хотите удалить?" />
                
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />            
        </EditFormSettings>
        

        </MasterTableView>
     
</telerik:RadGrid>
    
        

<asp:SqlDataSource ID="dsServices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveServices"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsDistrict" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveDistricts"></asp:SqlDataSource>



<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
    SelectCommand="RetrieveComplaints" SelectCommandType="StoredProcedure" 
    InsertCommand="CreateComplaints" InsertCommandType="StoredProcedure"
    UpdateCommand="UpdateComplaints" UpdateCommandType="StoredProcedure"
    DeleteCommand="DeleteIncommingMail" DeleteCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>



