<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="WPList.aspx.cs" Inherits="cartridges_app.DeviceList" %>
<%@ Register src="../Controls/WaterPoint/DeviceManager.ascx" tagname="DeviceManager" tagprefix="uc1" %>
<%@ Register src="../Controls/WaterPoint/DeviceAddWithAssign.ascx" tagname="DeviceAddWithAssign" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
.rgCaption{color:black; font-size:14px; font-weight:bold; text-align:left; background:#ff9900; padding:5px;}
.rad{margin-left:81px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radgrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radgrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <!--Тут мы показывает что аякс всё-таки выполняется-->
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <telerik:RadButton runat="server" ID="brExport" Text="To Pdf" 
        onclick="brExport_Click" Visible="false"></telerik:RadButton>                

        
        <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="true" />
        
        
        <telerik:RadWindow runat="server" ID="radExistingDevices" Title="Список свободных устройств" Width="700px">
            <ContentTemplate>
                <uc1:DeviceManager ID="DeviceManager1" runat="server"  Visible="true"/>
            </ContentTemplate>
        </telerik:RadWindow>
        
        
         <telerik:RadWindow runat="server" ID="radAddNewDevice" Title="Добавить новое устройство" Width="700px" OnClientClose="ReloadDevice">
            <ContentTemplate>
                <uc2:DeviceAddWithAssign ID="DeviceAddWithAssign1" runat="server" />
            </ContentTemplate>
        </telerik:RadWindow>
         <telerik:RadScriptBlock ID="RadScriptBlock5" runat="server">
                        <script type="text/javascript">
                            function ReloadDevice() {
                                                             
                            }
                        </script>
                    </telerik:RadScriptBlock>



        
        
        <!--Тут мы показываем наши высплывающие окошки.-->
        <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>


<telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"  CssClass="rad"
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" 
        onitemcommand="radgridDevice_ItemCommand" 
        onneeddatasource="radgridDevice_NeedDataSource" 
        oninsertcommand="radgridDevice_InsertCommand" 
        oneditcommand="radgridDevice_EditCommand" 
        onupdatecommand="radgridDevice_UpdateCommand" 
        ondeletecommand="radgridDevice_DeleteCommand" ShowHeader="true" 
        ShowStatusBar="true" onitemcreated="radgrid_ItemCreated" 
    onitemdatabound="radgrid_ItemDataBound" 
        ondetailtabledatabind="radgrid_DetailTableDataBind" 
        onprerender="radgrid_PreRender">
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
</ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="WP"  Caption="Водоводы" AllowFilteringByColumn="true" >
            <DetailTables>
                        <telerik:GridTableView DataKeyNames="ID" DataMember="Device" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Device"  Caption="Приборы">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="ID" DataMember="Service" Width="300px" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Service" >
                                <ParentTableRelation>
                                    <telerik:GridRelationFields DetailKeyField="WPDeviceID" MasterKeyField="ID" />
                                </ParentTableRelation>
                                <Columns>
                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" ></telerik:GridEditCommandColumn>
                                    <telerik:GridDateTimeColumn DataField="DateIn" UniqueName="DateInService" HeaderText="Дата" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridDateTimeColumn>
                                    <telerik:GridDropDownColumn UniqueName="ServiceType" DropDownControlType="RadComboBox" DataField="WPTypeServiceID" HeaderText="Тип" DataSourceID="dsServiceType" ListTextField="Title" ListValueField="ID"></telerik:GridDropDownColumn>  
                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
                                </Columns>
                                 <EditFormSettings>
                                     <EditColumn ButtonType="ImageButton" />
                                  </EditFormSettings>
                            </telerik:GridTableView>
                            </DetailTables>
                        <ParentTableRelation>
                            <telerik:GridRelationFields DetailKeyField="WPID" MasterKeyField="ID" />
                        </ParentTableRelation>
                        <Columns>
                            <telerik:GridBoundColumn DataField="qnt" HeaderText="Операций" AllowFiltering="false" SortExpression="qnt" ReadOnly="true" ></telerik:GridBoundColumn>
                            <telerik:GridDropDownColumn   AllowFiltering="false" UniqueName="DeviceType" DropDownControlType="RadComboBox" DataField="WPTypeDeviceID" HeaderText="Тип" DataSourceID="dsDeviceType" ListTextField="Title" ListValueField="ID" ItemStyle-Wrap="true"></telerik:GridDropDownColumn>
                            <telerik:GridBoundColumn DataField="Title" HeaderText="Название" SortExpression="Title" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FN" HeaderText="Заводской №"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Описание" EmptyDataText="Нет данных"></telerik:GridBoundColumn>                            
                            <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
                         </Columns>
                         <CommandItemTemplate>
                          <telerik:RadScriptBlock ID="RadScriptBlock4" runat="server">
                        <script type="text/javascript">
                            function ShowExisting3() {
                                var oWnd = $find("<%# radExistingDevices.ClientID %>");
                                oWnd.show();
                            }
                            function ShowExisting4() {
                                var oWnd = $find("<%# radAddNewDevice.ClientID %>");
                                oWnd.show();
                            }
                        </script>
                    </telerik:RadScriptBlock>
                            <telerik:RadButton runat="server" ID="butAddDevice" CommandName="AddExistingDevice" Text="Привязать из списка" OnClientClicking="ShowExisting3"></telerik:RadButton>
                            <telerik:RadButton runat="server" ID="butAddNewDevice" CommandName="AddNewDevice" Text="Добавить и привязать" OnClientClicking="ShowExisting4"></telerik:RadButton>
                         </CommandItemTemplate>
                         <EditFormSettings>
                             <EditColumn ButtonType="ImageButton" />
                          </EditFormSettings>
                        </telerik:GridTableView>  
                        <telerik:GridTableView DataKeyNames="ID" DataMember="Event" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Event"  Caption="Выезды" >
                            <DetailTables>
                                 <telerik:GridTableView DataKeyNames="ID" DataMember="Works" Width="500px" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Works"  Caption="Работы на выезде">
                                <ParentTableRelation>
                                    <telerik:GridRelationFields DetailKeyField="WPEventID" MasterKeyField="ID" />
                                </ParentTableRelation>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true"></telerik:GridBoundColumn>                                    
                                    <telerik:GridDropDownColumn UniqueName="WorkType" DropDownControlType="RadComboBox" DataField="WPTypeWork" HeaderText="Тип" DataSourceID="dsEventWorkType" ListTextField="Title" ListValueField="ID" ></telerik:GridDropDownColumn>  
                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
                                </Columns>
                                 <EditFormSettings>
                                     <EditColumn ButtonType="ImageButton" />
                                  </EditFormSettings>
                            </telerik:GridTableView>
                            </DetailTables>
                        <ParentTableRelation>
                        <telerik:GridRelationFields DetailKeyField="WPID" MasterKeyField="ID" />
                        </ParentTableRelation>
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" ></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" ></telerik:GridBoundColumn>
                             <telerik:GridDateTimeColumn DataField="DateIn" UniqueName="DateInService" HeaderText="Дата" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridDateTimeColumn>
                            <telerik:GridDropDownColumn  AllowFiltering="false" UniqueName="EventType" DropDownControlType="RadComboBox" DataField="WPTypeEventID" HeaderText="Тип" DataSourceID="dsEventType" ListTextField="Title" ListValueField="ID"></telerik:GridDropDownColumn>
                            <telerik:GridDropDownColumn  AllowFiltering="false" UniqueName="EventSource" DropDownControlType="RadComboBox" DataField="WPEventSourceTypeID" HeaderText="Тип" DataSourceID="dsEventSourceType" ListTextField="Title" ListValueField="ID"></telerik:GridDropDownColumn>                            
                            <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
                         </Columns>
                         <EditFormSettings>
                             <EditColumn ButtonType="ImageButton" />
                          </EditFormSettings>
                        </telerik:GridTableView>                    
                    </DetailTables>
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>   
                <telerik:GridDropDownColumn   UniqueName="WPLocation" DropDownControlType="RadComboBox" DataField="WPLocationID" HeaderText="Объект" DataSourceID="DSLocations" ListTextField="Title" ListValueField="ID" ItemStyle-Wrap="true">
                    <FilterTemplate>                                       
                       <telerik:RadComboBox Width="180px" ID="RadComboBoxTitle" DataSourceID="DSLocations" DataTextField="Title"
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("WPLocation").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="TitleIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="Все" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function TitleIndexChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("WPLocation", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>                     
                <telerik:GridDropDownColumn  UniqueName="WPType" DropDownControlType="RadComboBox" DataField="WPTypeID" HeaderText="Тип" DataSourceID="dsWPType" ListTextField="Title" ListValueField="ID">
                        <FilterTemplate>                  
                    <telerik:RadComboBox Width="120px"  ID="RadComboBoxTitle2" DataSourceID="dsWPType" DataTextField="Title"
                        DataValueField="ID" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("WPType").CurrentFilterValue %>'
                        runat="server" OnClientSelectedIndexChanged="TitleIndexChanged1">
                        <Items>
                            <telerik:RadComboBoxItem Text="Все" />
                        </Items>
                    </telerik:RadComboBox>
                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                        <script type="text/javascript">
                            function TitleIndexChanged1(sender, args) {
                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                tableView.filter("WPType", args.get_item().get_value(), "EqualTo");
                            }
                        </script>
                    </telerik:RadScriptBlock>
                </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridBoundColumn FilterControlWidth="100px" DataField="Title" HeaderText="Название" SortExpression="Title" ></telerik:GridBoundColumn>
                 <telerik:GridDateTimeColumn FilterControlWidth="50px" DataField="StartDate" UniqueName="StartDate" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="LineFirst" HeaderText="L До" SortExpression="LineFirst" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="LineSecond" HeaderText="L После" SortExpression="LineSecond" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="D" HeaderText="D" SortExpression="D" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="DCalc" HeaderText="D Расч." SortExpression="DCalc" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="QMin" HeaderText="Q Min" SortExpression="QMin" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="QMax" HeaderText="Q Max" SortExpression="QMax" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="false" DataField="Comment" HeaderText="Комментарий" SortExpression="Comment" ></telerik:GridBoundColumn>                     
                <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
        </EditFormSettings>
        </MasterTableView>
</telerik:RadGrid>

<asp:SqlDataSource ID="dsDevices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveDevicesByWP" 
SelectCommandType="StoredProcedure"  InsertCommand="CreateWPDevice" InsertCommandType="StoredProcedure" DeleteCommand="DeleteDevice" DeleteCommandType="StoredProcedure" UpdateCommand="UpdateDevice" UpdateCommandType="StoredProcedure">
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsDeviceType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveDevicesType" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsEventSourceType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEventSourceTypes" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsWPType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveWPType" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="DSLocations" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveWPLocations" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsWP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveWPs" 
SelectCommandType="StoredProcedure" InsertCommand="CreateWaterPoint" InsertCommandType="StoredProcedure" UpdateCommand="UpdateWaterpoint" UpdateCommandType="StoredProcedure" DeleteCommand="DeleteWP" DeleteCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsEvents" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEventsByWP" InsertCommand="CreateWPEvent" InsertCommandType="StoredProcedure" UpdateCommand="UpdateWPEvent" UpdateCommandType="StoredProcedure" DeleteCommand="DeleteWPEvent" DeleteCommandType="StoredProcedure"
SelectCommandType="StoredProcedure" >
<DeleteParameters>
    <asp:Parameter Name="ID" Type="Int32" />
</DeleteParameters>
<UpdateParameters>
    <asp:Parameter Name="ID" Type="Int32" />
    <asp:Parameter Name="WPTypeEventID" Type="Int32" />
    <asp:Parameter Name="WPEventSourceTypeID" Type="Int32" />
    <asp:Parameter Name="DateIn" Type="DateTime" />
    <asp:Parameter Name="UserID" Type="Int32" />
</UpdateParameters>
<InsertParameters>
    <asp:Parameter Name="WPTypeEventID" Type="Int32" />
    <asp:Parameter Name="WPID" Type="Int32" />
    <asp:Parameter Name="WPEventSourceTypeID" Type="Int32" />
    <asp:Parameter Name="DateIn" Type="DateTime" />
    <asp:Parameter Name="UserID" Type="Int32" />
</InsertParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsEventWork" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEventWork"  InsertCommand="CreateEventWork" InsertCommandType="StoredProcedure" DeleteCommand="DeleteWork" DeleteCommandType="StoredProcedure"
SelectCommandType="StoredProcedure" >
<InsertParameters>
    <asp:Parameter Name="EventID" Type="Int32" />
    <asp:Parameter Name="TypeWorkID" Type="Int32" />
</InsertParameters>
<DeleteParameters>
    <asp:Parameter Name="ID" Type="Int32" />
</DeleteParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsEventWorkType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEventWorkType" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsEventType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEventTypes" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>


<asp:SqlDataSource ID="dsService" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveServicesByDeviceID" 
SelectCommandType="StoredProcedure"  InsertCommand="CreateWPDeviceService" InsertCommandType="StoredProcedure" DeleteCommand="DeleteService" DeleteCommandType="StoredProcedure" UpdateCommand="UpdateService" UpdateCommandType="StoredProcedure">
</asp:SqlDataSource>


<asp:SqlDataSource ID="dsServiceType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveServiceTypes" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>




 
</asp:Content>
