<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PhysicalMailOutJournal.aspx.cs" Inherits="outcomming_mail.PhysicalMailOutJournal"%>
<%@ Register src="../Controls/PhysicalMailOutDet.ascx" tagName="PhysicalMailOutDet" tagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hfUserID" />
    
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radgrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radgrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="PhysicalMailOutDet1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PhysicalMailOutDet1" LoadingPanelID="RadAjaxLoadingPanel1" />
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
           var oWnd = radopen("PhysicalMailOutcommingDet.aspx?id=" + args.getDataKeyValue("ID"), null, 800, 700, 20, 20);
           oWnd.set_visibleStatusbar = false;
           oWnd.center();
       }
    </script>
     <telerik:RadWindow runat="server" ID="radPhysicalMailOut" Title="Просмотр исходящей почты"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False">
            <ContentTemplate>
                   <uc1:PhysicalMailOutDet ID="PhysicalMailOutDet1" runat="server"/>
            </ContentTemplate>
        </telerik:RadWindow>
    
    <telerik:RadWindow runat="server" ID="RadWindow2" Title="Просмотр исходящей почты"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False"></telerik:RadWindow>

    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="25"  DataSourceID="dsPhysicalMailOutJournal">
         <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
     <MasterTableView DataKeyNames="ID"  Width="100%"  Name="Order"  AllowFilteringByColumn="true"  ClientDataKeyNames="ID" DataSourceID="dsPhysicalMailOutJournal">
            <Columns>
                <telerik:GridBoundColumn FilterControlWidth="40px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="№ п/п"  UniqueName="MailSendID" ></telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="RegNumber" HeaderText="Рег. номер" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ContractNumber" HeaderText="Номер договора" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn  FilterControlWidth="45px" DataType="System.DateTime" DataField="date_registration" EnableTimeIndependentFiltering="True"  EnableRangeFiltering="true" UniqueName="date_registration" HeaderText="Дата регистрации" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Whom" HeaderText="Кому" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Notation" HeaderText="Примечание(кому ФИО)" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PhysicalAdresatType" HeaderText="В чьем лице" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SenderName" HeaderText="Отправитель" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="About" HeaderText="О чем" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DepartamentName" HeaderText="Отдел" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn  DataField="ResponName" HeaderText="Ответственный исполнитель" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnswerAbout" HeaderText="О чем (ответ)" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="45px"  DataField="AnswerDate" EnableTimeIndependentFiltering="true" EnableRangeFiltering="true" UniqueName="AnswerDate" HeaderText="Дата ответа" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
        </Columns>
        </MasterTableView>
</telerik:RadGrid>
    <asp:SqlDataSource ID="dsPhysicalMailOutJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrievePhysicalOutMailJournal" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>