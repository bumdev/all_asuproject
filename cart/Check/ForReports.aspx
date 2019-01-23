<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="cartridges_app.ForReports" Codebehind="ForReports.aspx.cs"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField runat="server" ID="hfUserID" />
    
    <telerik:RadAjaxManagerProxy runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radgrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radgrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="FAbonDet1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="FAbonDet1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <!--Тут мы показывает что аякс всё-таки выполняется-->
      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />

    

     <!--Тут мы показываем наши высплывающие окошки.-->
        <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true">
        </telerik:RadWindowManager>
    
   <script type="text/javascript">
       function RowSelected(sender, args) {
           //alert(args.getDataKeyValue("ID"));
           var oWnd = radopen("FabonentDet.aspx?id=" + args.getDataKeyValue("ID"), null, 800, 600, 20, 20);
           oWnd.set_visibleStatusbar = false;
           oWnd.center();

       }
    </script>
     
    
    <telerik:RadWindow runat="server" ID="RadWindow2" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False"></telerik:RadWindow>

    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="25" OnItemCommand="radgridDevice_ItemCommand">
         <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
        <ExportSettings ExportOnlyData="true" OpenInNewWindow="true" IgnorePaging="true" />
     <MasterTableView DataKeyNames="ID" DataMember="Forder" Width="100%"  Name="Order"  AllowFilteringByColumn="true"  ClientDataKeyNames="ID">
         <CommandItemSettings ShowExportToExcelButton="true" ExportToExcelText="Export To Excel" />
            <Columns>
                
                <telerik:GridDateTimeColumn FilterControlWidth="100px"  DataField="DateInsert" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="DateInsert" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Cartname" FilterControlWidth="50px" HeaderText="Картридж" SortExpression="Cartname"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NameCartridge" FilterControlWidth="50px" HeaderText="Модель" SortExpression="NameCartridge"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DepartName" FilterControlWidth="50px" HeaderText="Отдел" SortExpression="DepartName"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Number" FilterControlWidth="50px"  HeaderText="Номер" SortExpression="Number"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Information" FilterControlWidth="70px"  HeaderText="Информация (статус)" SortExpression="Information"></telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="Comment" FilterControlWidth="50px"  HeaderText="Замечание (доп. инфо)" SortExpression="Comment"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn  DataField="DateInWork" UniqueName="DateInWork" FilterControlWidth="100px" HeaderText="Дата в работе" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                   <ItemStyle Width="50px"></ItemStyle>
               </telerik:GridDateTimeColumn>
               <telerik:GridCheckBoxColumn DataField="InTheWork" HeaderText="В работе" />
                <telerik:GridDateTimeColumn  DataField="DateFueling" UniqueName="DateFueling" FilterControlWidth="100px" HeaderText="Дата заправки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                   <ItemStyle Width="50px"></ItemStyle>
               </telerik:GridDateTimeColumn>
                <telerik:GridCheckBoxColumn DataField="RefuelingCondition" HeaderText="Заправлен" />
               
        </Columns>
        </MasterTableView>
</telerik:RadGrid>
    

    <asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrieveCartridgesWithUser" SelectCommandType="StoredProcedure">
	<SelectParameters>  	
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />  	
	</SelectParameters>
</asp:SqlDataSource>
    
   </asp:Content>

