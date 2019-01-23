<%@ Page Title="Электронный табель" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Timetable.aspx.cs" Inherits="Timetable_WebApp.Timetable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link runat="server" rel="shortcut icon" href="~/timetable_logo.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="~/timetable_logo.ico" type="image/ico" />
     <link href="styles/grid.css" rel="stylesheet" />
    <link href="styles/style.css" rel="stylesheet" />
    <style type="text/css"> 
        .rgExpXLS 
        {
            color: white
        } 
    </style>
    <style type="text/css"> 
        .rgEditForm 
        { 
            padding: 5px !important; 
        } 
 </style>  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hfUserID" />
   <%-- <asp:HiddenField runat="server" ID="hfService" />--%>
    <telerik:RadWindowManager runat="server" ID="radWM" EnableShadow="true" AutoSize="true"></telerik:RadWindowManager>
    
    <asp:UpdatePanel runat="server" ID="up1">
         
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>

    
    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="10"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True" AllowSorting="true" Font-Names="Arial Unicode MS" 
        EnableLinqExpressions="false"
        Culture="ru-RU" AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand"  
        onupdatecommand="radgridDevice_UpdateCommand" OnItemCommand="radgrid_ItemCommand"
        OnInfrastructureExporting="radgrid_InfrastructureExporting" OnExportCellFormatting="radgrid_ExportCellFormating"
        OnExcelMLExportStylesCreated="radgrid_ExportCellStyles" OnExcelMLExportRowCreated="radgrid_ExportRow"
        OnItemDataBound="radgrid_OnDataBound" AllowMultiRowSelection="true">
      <GroupingSettings ShowUnGroupButton="true"  />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
        
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
              </ExportSettings>
      
      <ClientSettings>
            <Selecting AllowRowSelect="true"></Selecting>
         
        </ClientSettings>
        <MasterTableView EditMode="PopUp" IsFilterItemExpanded="True" EditFormSettings-PopUpSettings-Width="900px" DataKeyNames="ID" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Timetable"  AllowFilteringByColumn="true" >
            <PagerStyle Mode="NextPrevAndNumeric" />
            <CommandItemSettings ShowAddNewRecordButton="false" ExportToExcelImageUrl="images/excel.png" ExportToExcelText="ОТЧЕТ" ShowExportToExcelButton="false" ShowRefreshButton="false" />
            
            <ColumnGroups>
                
                <telerik:GridColumnGroup HeaderText="Отметки о явках и неявках по числам месяца(часов)" Name="DaysHeader" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Отработано за месяц" runat="server" Name="MonthHours" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Часов" runat="server" Name="HoursHeader" ParentGroupName="MonthHours" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Из них" runat="server" Name="FromHeader" ParentGroupName="HoursHeader" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Из них по причинам за месяц" runat="server" Name="ReasonsHeader" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Часы/Дни" runat="server" Name="DaysHoursCheck" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
            </ColumnGroups>
            
            
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn> 
                
                
                <telerik:GridDropDownColumn UniqueName="sv" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" DropDownControlType="RadComboBox" DataField="service_id" HeaderText="Служба" DataSourceID="dsServices" 
                    ListTextField="service_name" ListValueField="id" ItemStyle-Wrap="true">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server" Width="250px" ID="service_id" DataSourceID="dsServices" DataTextField="service_name"
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
                <telerik:GridBoundColumn HeaderText="Значение для заполнения" DataField="value_d" UniqueName="value_d" AllowFiltering="false" Display="false" runat="server" />
                <telerik:GridBoundColumn HeaderText="Диапазон дней" DataField="range_1" UniqueName="range_d" AllowFiltering="false" Display="False" runat="server" />
                <telerik:GridBoundColumn HeaderText="От" DataField="range_1" UniqueName="range_1" AllowFiltering="false" Display="False" runat="server" />
                <telerik:GridBoundColumn HeaderText="До" DataField="range_2" UniqueName="range_2" AllowFiltering="false" Display="False" runat="server" />
                <telerik:GridCheckBoxColumn HeaderText="Выходные" UniqueName="check_weekends" AllowFiltering="false" Display="false" />
                <telerik:GridBoundColumn DataField="number" HeaderText="Табельный номер" AllowSorting="true" UniqueName="number" SortExpression="number" ReadOnly="true" ShowFilterIcon="false" FilterDelay="2000" FilterControlToolTip="200" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="name" UniqueName="ep" HeaderText="ФИО" SortExpression="name" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="sex" HeaderText="Пол" SortExpression="sex" ReadOnly="true" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="position" UniqueName="pos" HeaderText="Должность" SortExpression="position" ReadOnly="true" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="selery" HeaderText="Оклад, тарифная ставка" ReadOnly="true" SortExpression="" AllowFiltering="false"></telerik:GridBoundColumn>                
                <telerik:GridCheckBoxColumn HeaderText="Дни" Display="false" UniqueName="days_check" ColumnGroupName="DaysHoursCheck" AllowFiltering="false" />
                <telerik:GridCheckBoxColumn HeaderText="Часы" Display="false" UniqueName="hours_check" ColumnGroupName="DaysHoursCheck" AllowFiltering="false" />
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d1" HeaderText="1" SortExpression="d1" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d2" HeaderText="2" SortExpression="d2" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d3" HeaderText="3" SortExpression="d3" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d4" HeaderText="4" SortExpression="d4" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d5" HeaderText="5" SortExpression="d5" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d6" HeaderText="6" SortExpression="d6" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d7" HeaderText="7" SortExpression="d7" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d8" HeaderText="8" SortExpression="d8" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d9" HeaderText="9" SortExpression="d9" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d10" HeaderText="10" SortExpression="d10" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d11" HeaderText="11" SortExpression="d11" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d12" HeaderText="12" SortExpression="d12" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d13" HeaderText="13" SortExpression="d13" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d14" HeaderText="14" SortExpression="d14" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d15" HeaderText="15" SortExpression="d15" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d16" HeaderText="16" SortExpression="d16" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d17" HeaderText="17" SortExpression="d17" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d18" HeaderText="18" SortExpression="d18" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d19" HeaderText="19" SortExpression="d19" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d20" HeaderText="20" SortExpression="d20" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d21" HeaderText="21" SortExpression="d21" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d22" HeaderText="22" SortExpression="d22" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d23" HeaderText="23" SortExpression="d23" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d24" HeaderText="24" SortExpression="d24" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d25" HeaderText="25" SortExpression="d25" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d26" HeaderText="26" SortExpression="d26" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d27" HeaderText="27" SortExpression="d27" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d28" HeaderText="28" SortExpression="d28" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d29" HeaderText="29" SortExpression="d29" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d30" HeaderText="30" SortExpression="d30" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="DaysHeader" DataField="d31" HeaderText="31" SortExpression="d31" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="graph_hours_count" HeaderText="Плановое количество часов по графику" SortExpression="graph_hours_count" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="MonthHours" DataField="work_days" HeaderText="Раб. дни" SortExpression="work_days" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="MonthHours" DataField="work_hours" HeaderText="Раб. часы" SortExpression="work_hours" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="MonthHours" DataField="companions" HeaderText="Совместители" SortExpression="companions" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="HoursHeader" DataField="rem_auto" HeaderText="Рем. авто" SortExpression="rem_auto" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="FromHeader" DataField="nights" HeaderText="Ночные" SortExpression="nights" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="FromHeader" DataField="evenings" HeaderText="Вечерние" SortExpression="evenings" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="FromHeader" DataField="graph_hours_party" HeaderText="Праздничные часы по графику" SortExpression="graph_hours_party" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code7" HeaderText="Командировки (код 7)" SortExpression="code7" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code8" HeaderText="Осн. ежегодный отпуск, доп. матери с детьми (код 8)" SortExpression="code8" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code26" HeaderText="По б/листам оплачиваем (код 26)" SortExpression="code26" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code27" HeaderText="По б/листам неоплачиваем (код 27)" SortExpression="code27" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code11" HeaderText="Б/Л РОДЫ (код 11)" SortExpression="code11" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code12" HeaderText="Учебный отпуск (код 12)" SortExpression="code12" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code13" HeaderText="Учебн. отп. без оплаты (код 13)" SortExpression="code13" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code18" HeaderText="Отпуск Б/ОПЛ (по семейным обостоят. дни из отп. отним.) (код 18)" SortExpression="code18" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code14" HeaderText="Отп. б/опл. (дни из отп. отним.) (код 14)" SortExpression="code14" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code16" HeaderText="Декретный отпуск(до 3 лет) (код 16)" SortExpression="code16" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code22" HeaderText="Гос. обязанности(донорские, вызов в суд, допризыв. подг., воен. уч. сборы) (код 22)" SortExpression="code22" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code24" HeaderText="Прогул(и др. неявки по неув. причинам) (код 24)" SortExpression="code24" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code28" HeaderText="По необъясним. прич. пропажей человека(прокуратура) (код 28)" SortExpression="code28" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code29" HeaderText="Др. неявки по кол. док.(1 сентября, дни из отп. неотним.) (код 29)" SortExpression="code29" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code30" HeaderText="Учеба(повышение квалификаиции) (код 30)" SortExpression="code30" AllowFiltering="false"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code45" HeaderText="Неявки в связи с поступлением и увольнением с работы (код 45)" SortExpression="code45" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ColumnGroupName="ReasonsHeader" DataField="code_none" HeaderText="По среднему" SortExpression="code_none" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="weekends" HeaderText="Выходные и праздники" SortExpression="weekends" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn UniqueName="is_hr" runat="server" ReadOnly="true" DataField="IsHR" HeaderText="Отдел кадров" AllowFiltering="false"></telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn UniqueName="is_plan" runat="server" ReadOnly="true" DataField="IsPlanning" HeaderText="Плановый отдел" AllowFiltering="false"></telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn UniqueName="is_bill" runat="server" ReadOnly="true" DataField="IsBilling" HeaderText="Расчетный отдел" AllowFiltering="false"></telerik:GridCheckBoxColumn>
                <telerik:GridDateTimeColumn UniqueName="date_s" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true" DataField="date_start" ReadOnly="true" HeaderText="Дата начала периода" DataFormatString="{0:dd/MM/yyyy}" />
                <telerik:GridDateTimeColumn UniqueName="date_e" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="170" AutoPostBackOnFilter="true" DataField="date_end" ReadOnly="true" HeaderText="Дата окончания периода" DataFormatString="{0:dd/MM/yyyy}" />
              
        </Columns>
            
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" UpdateImageUrl="images/save_5.png" CancelImageUrl="images/cancel_2.png" />
            
        </EditFormSettings>
        </MasterTableView>
</telerik:RadGrid>
    
    <div class="FormItem">
        <div class="label" style="padding: 5px">Дублирование сотрудников:</div>
        <div style="background: lavender; border: solid 1px black; height: 50px; width: 51%">
            <div style="padding: 5px">
                <asp:LinkButton runat="server" CssClass="FormButton" ID="lbDuplicate" OnClick="Duplicate_OnClick" OnClientClick="return confirm('Вы действительно хотите продублировать запись?');"><span>Дублирование</span></asp:LinkButton>
               
            </div>
        </div>

    </div>
    
    <div class="FormItem">
        <div style="padding: 5px"></div>
        <div class="label">Закрытие периода: </div>
        <div style="width: 100%; height: 50px; background: lavender; border: 1px solid black">
            <div style="padding: 5px">
                <asp:LinkButton runat="server" CssClass="FormButton" ID="lbHR" OnClick="HR_Click"><span>Отдел кадров - не верно</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
                <asp:LinkButton runat="server" CssClass="FormButton" ID="lbPlan" OnClick="Plan_Click"  ><span>Плановый отдел - не верно</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
            <asp:LinkButton runat="server" CssClass="FormButton" ID="lbBill" OnClick="Bill_Click"><span>Расчетный отдел - не верно</span></asp:LinkButton>&nbsp;&nbsp;&nbsp
            <asp:LinkButton runat="server" CssClass="FormButton" ID="Corrected" OnClick="Corrected_Click"><span>Исправлено</span></asp:LinkButton>
            </div>
        </div>
    </div>  
    
<asp:SqlDataSource ID="dsServices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveServices"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsEmployees" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveEmployees"></asp:SqlDataSource>
   
<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveTimetable" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateTimetable" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteInventory" DeleteCommandType="StoredProcedure" 
    >
    <SelectParameters>
        <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />
        <%--<asp:ControlParameter ControlID="hfService" PropertyName="Value" Name="serv_id" Type="Int32"/>--%>
    </SelectParameters>  
</asp:SqlDataSource>
</div>

</ContentTemplate>
        
</asp:UpdatePanel>
</asp:Content>
