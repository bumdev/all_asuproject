<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ServiceReport.aspx.cs" Inherits="Timetable_WebApp.ServiceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link runat="server" rel="shortcut icon" href="~/timetable_logo.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="~/timetable_logo.ico" type="image/ico" />
    <link href="styles/style.css" rel="stylesheet" type="text/css" />
     <asp:Literal runat="server" ID="litScript"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <telerik:RadScriptManager runat="server" ID="RadScriptManager2" /> --%>
    <telerik:RadWindowManager runat="server" ID="radWM" EnableShadow="true" AutoSize="true"></telerik:RadWindowManager>
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
    <script src="scripts/openimage.js" type="text/javascript" ></script>     
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>
   
    <div class="FormItem">
        <div style="padding: 5px"></div>
        <div class="label">Отчет: </div>
        <div style="background: lavender; width: 100%; height: 50px; border: solid 1px black">
            <div style="padding: 5px">
                <asp:LinkButton runat="server" ID="lbReportService" OnClick="lbReportService_OnClick" CssClass="FormButton"><span>Сделать отчет</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
    С&nbsp;<telerik:RadDatePicker runat="server" ID="dpFrom"></telerik:RadDatePicker>
        &nbsp;&nbsp;По&nbsp;<telerik:RadDatePicker runat="server" ID="dpTo"></telerik:RadDatePicker>
            </div>
        </div>
    </div>
    
    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="50"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" onitemdatabound="radgrid_ItemDataBound">
      
      <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
          <Resizing AllowColumnResize="true" />
      </ClientSettings>
      <GroupHeaderItemStyle HorizontalAlign="center" />
      

         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
      
        <MasterTableView EditMode="PopUp" EditFormSettings-PopUpSettings-Width="600px" TableLayout="fixed" DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="ServiceAccept"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="False"  />
            
            
            <Columns>
                
                <telerik:GridBoundColumn UniqueName="ID" HeaderStyle-Width="50px" HeaderText="ID" runat="server" AllowFiltering="false" DataField="ID"></telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="sv" HeaderStyle-HorizontalAlign="Center" DropDownControlType="RadComboBox" DataField="id" HeaderText="Служба" DataSourceID="dsServices" 
                    ListTextField="service_name" ListValueField="id" ItemStyle-Wrap="true" ItemStyle-Width="300" HeaderStyle-Width="300">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server"  Width="250px" ID="serv_id" DataSourceID="dsServices" DataTextField="service_name"
                            DataValueField="id" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("sv").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="DirectService" Filter="Contains"
                            EmptyMessage="Поиск...">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function DirectService(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("sv", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                
               
                
				
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
        </EditFormSettings>

        </MasterTableView>
     
</telerik:RadGrid>
    
<asp:SqlDataSource ID="dsServices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveServices"></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveServices" SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
