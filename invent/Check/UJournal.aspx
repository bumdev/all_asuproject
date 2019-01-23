<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UJournal.aspx.cs" Inherits="invent_app.UJournal" %>
<%@ Register src="../Controls/UAbonDet.ascx" tagname="UAbonDet" tagprefix="uc1" %>

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
           var oWnd = radopen("UabonentDet.aspx?id=" + args.getDataKeyValue("ID"), null, 800, 700, 20, 20);
           oWnd.set_visibleStatusbar = false;
           oWnd.center();
       }
    </script>
     <telerik:RadWindow runat="server" ID="radFAbonent" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False">
            <ContentTemplate>
                   <uc1:UAbonDet ID="FAbonDet1" runat="server"/>
            </ContentTemplate>
        </telerik:RadWindow>
    
    <telerik:RadWindow runat="server" ID="RadWindow2" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False"></telerik:RadWindow>

    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="25"  DataSourceID="dsJournal">
         <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
     <MasterTableView DataKeyNames="ID"  Width="100%"  Name="Order"  AllowFilteringByColumn="true"  ClientDataKeyNames="ID" DataSourceID="dsJournal">
            <Columns>
                <telerik:GridBoundColumn FilterControlWidth="40px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="ID"  UniqueName="OrderID" ></telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="NumberJournal" HeaderText="Номер по журналу"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="100px" DataField="DateIn" EnableTimeIndependentFiltering="True"  EnableRangeFiltering="true" UniqueName="DateIn" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridCheckBoxColumn DataField="IsPaid" HeaderText="Оплата"/>
                <telerik:GridCheckBoxColumn DataField="IsBudget" HeaderText="Бюджет"/>
                <telerik:GridBoundColumn DataField="OKPO" HeaderText="ОКПО"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Title" HeaderText="Название"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nom_zavod" HeaderText="Номер водомера"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Адрес"></telerik:GridBoundColumn>
        </Columns>
        </MasterTableView>
</telerik:RadGrid>
    

    <asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrieveUJournalNewNewWithVodomer" SelectCommandType="StoredProcedure">
	<SelectParameters>  	
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />  	
	</SelectParameters>
</asp:SqlDataSource>
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   

</asp:Content>
