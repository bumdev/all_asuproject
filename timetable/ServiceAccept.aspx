<%@ Page Title="Контроль служб" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ServiceAccept.aspx.cs" Inherits="Timetable_WebApp.ServiceAccept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link runat="server" rel="shortcut icon" href="~/timetable_logo.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="~/timetable_logo.ico" type="image/ico" />
    <link href="styles/style.css" rel="stylesheet" type="text/css" />
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <telerik:RadScriptManager runat="server" ID="RadScriptManager2" /> --%>
    <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true" AutoSize="True">
          </telerik:RadWindowManager>
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
    <script src="scripts/openimage.js" type="text/javascript" ></script>     
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>
   
    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="10"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridServices_UpdateCommand"
        onitemdatabound="radgrid_ItemDataBound" OnItemCommand="radgrid_itemcommand">
      
      <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
          <Resizing AllowColumnResize="true" />
      </ClientSettings>
      <GroupHeaderItemStyle HorizontalAlign="center" />
      

         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
      
        <MasterTableView EditMode="PopUp" EditFormSettings-PopUpSettings-Width="600px" TableLayout="fixed" DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="ServiceAccept"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="False"  />
            
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Отдел кадров" Name="HRHeader" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Плановый отдел" Name="PlanHeader" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Расчетный отдел" Name="BillHeader" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
            </ColumnGroups>
            
            <Columns>
                
                <telerik:GridDropDownColumn UniqueName="sv" HeaderStyle-HorizontalAlign="Center" DropDownControlType="RadComboBox" DataField="serv_id" HeaderText="Служба" DataSourceID="dsServices" 
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
                <telerik:GridCheckBoxColumn UniqueName="is_chief" DataField="is_chief" HeaderStyle-HorizontalAlign="Center" HeaderText="Начальник службы" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn ColumnGroupName="HRHeader" DataField="is_hr" HeaderStyle-HorizontalAlign="Center" UniqueName="is_hr" HeaderText="Принято" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn ColumnGroupName="HRHeader" DataField="is_hrwrong" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="10" UniqueName="is_hrwrong" HeaderText="Правки" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn ColumnGroupName="PlanHeader" DataField="is_plan" HeaderStyle-HorizontalAlign="Center" HeaderText="Принято" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn ColumnGroupName="PlanHeader" DataField="is_planwrong" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="10" HeaderText="Правки" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn ColumnGroupName="BillHeader" DataField="is_bill" HeaderStyle-HorizontalAlign="Center" HeaderText="Принято" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn ColumnGroupName="BillHeader" DataField="is_billwrong" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="10" HeaderText="Правки" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn UniqueName="is_rollback" DataField="is_rollback" HeaderStyle-HorizontalAlign="Center" HeaderText="Откат" AllowFiltering="false" />
                <telerik:GridDateTimeColumn UniqueName="date_s" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="date_start" HeaderText="Дата начала периода" />
                <telerik:GridDateTimeColumn UniqueName="date_e" CurrentFilterFunction="EqualTo" HeaderStyle-HorizontalAlign="Center" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="195" DataField="date_end" HeaderText="Дата окончания периода" />
                <telerik:GridHyperLinkColumn ItemStyle-Width="150" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center"  DataTextFormatString="Документ" HeaderText="Прикрепленные файлы"
                        UniqueName="documents" DataTextField="documents" ItemStyle-HorizontalAlign="Center" AllowFiltering="false" >
                </telerik:GridHyperLinkColumn>
                
               
                
				
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
        </EditFormSettings>

        </MasterTableView>
     
</telerik:RadGrid>
    
    <div class="FormItem">
        <div class="label" style="padding: 10px">Контроль табеля:</div>
        <div style="width: 100%; background: lavender; height: 120px; border: solid 1px black">            
        <div class="label" style="width: 50%; padding: 10px">Закрытие периода: </div>
        <div style="width: 51%; height: 69px; float: left; border: solid 1px black">
        <div style="padding: 5px">
            <asp:LinkButton runat="server" CssClass="FormButton" ID="lbChief" OnClick="Chief_Click"><span>Начальник службы</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
            <asp:LinkButton runat="server" CssClass="FormButton" ID="lbSaveAll" OnClick="HR_Click" ><span>Отдел кадров</span></asp:LinkButton>&nbsp;&nbsp;&nbsp;
             <asp:LinkButton runat="server" CssClass="FormButton" ID="lbSavePlanning" OnClick="Planning_Click"><span>Плановый отдел</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
            <asp:LinkButton runat="server" CssClass="FormButton" ID="lbSaveBilling"  OnClick="Billing_Click"><span>Расчетный отдел</span></asp:LinkButton>&nbsp;&nbsp;&nbsp          
        </div>
    </div>
        
        <div style="width: 47%; height: 69px; float: right; border: solid 1px black">
            <div style="padding: 5px">
                <asp:LinkButton runat="server" CssClass="FormButton" ID="lbHRWrong" OnClick="HRWrong_Click"><span>Отдел кадров - не верно</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
                <asp:LinkButton runat="server" CssClass="FormButton" ID="lbPlanWrong" OnClick="PlanWrong_Click"  ><span>Плановый отдел - не верно</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
                <asp:LinkButton runat="server" CssClass="FormButton" ID="lbBillWrong" OnClick="BillWrong_Click"><span>Расчетный отдел - не верно</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
            </div>            
        </div>
        </div>
    </div>
    <div class="FormItem">
        <div class="label" style="padding: 10px">Закрытие периода и откат:</div>       
            <div style="float: left; width: 51%; height: 40px; border: solid 1px black; background: lavender">
                <div style="padding: 5px; display: flex; align-items: center; position: relative; margin: 0px">
                    <asp:LinkButton runat="server" CssClass="FormButton" ID="lbRollback" OnClick="Rollback_Click"  ><span>Откат</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
                    <asp:LinkButton OnClientClick="return confirm('Вы действительно хотите закрыть период?');" runat="server" CssClass="FormButton" ID="New_period" OnClick="New_period_Click" ><span>Закрыть период</span></asp:LinkButton>
                </div>
            </div>        
    </div>
    <div id="FormItem">
        <div style="padding: 10px"></div>
        <div class="label" style="padding: 10px; width: 47%">Загрузить документы</div>
        <div style="width: 50%; background: lavender; height: 80px; border: solid 1px black">
            <div style="padding: 10px; float: left">
                <telerik:RadAsyncUpload RenderMode="Lightweight" id="RadAsyncUpload1" Height="30px" Width="70px" runat="server" MultipleFileSelection="Automatic" />
            </div>            
            <div style="padding: 10px; float: right">
                <asp:Button runat="server" CssClass="FormButton" ID="Button1" Text="Загрузить" OnClick="BenjaminButton_OnClick" />
                <asp:Button runat="server" CssClass="FormButton" ID="ShowButton" Text="Показать документы" OnClick="ShowDocuments_OnClick" /> 
            </div>           
        </div>
    </div>

<asp:SqlDataSource ID="dsServices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveServices"></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveServiceAccept" SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
