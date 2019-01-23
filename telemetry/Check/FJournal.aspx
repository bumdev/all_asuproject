<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="leak_detectors.Check_VodomerTake" Codebehind="FJournal.aspx.cs"  %>

<%@ Register src="../Controls/FAbonDet.ascx" tagname="FAbonDet" tagprefix="uc1" %>

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
     <telerik:RadWindow runat="server" ID="radFAbonent" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False">
            <ContentTemplate>
                   <uc1:FAbonDet ID="FAbonDet1" runat="server"/>
            </ContentTemplate>
        </telerik:RadWindow>
    
    <telerik:RadWindow runat="server" ID="RadWindow2" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False"></telerik:RadWindow>

    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="25" OnItemCommand="radgridDevice_ItemCommand">
         <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
     <MasterTableView DataKeyNames="ID" DataMember="Forder" Width="100%"  Name="Order"  AllowFilteringByColumn="true"  ClientDataKeyNames="ID">
         
            <Columns>
                <telerik:GridBoundColumn FilterControlWidth="40px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="ID"  UniqueName="OrderID" ></telerik:GridBoundColumn>  
                <telerik:GridBoundColumn FilterControlWidth="40px" DataField="NumberJournal" HeaderText="№ журнала"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateIn" EnableTimeIndependentFiltering="True"  EnableRangeFiltering="true" UniqueName="DateIn" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridCheckBoxColumn DataField="IsPaid" HeaderText="Оплата"/>
                <telerik:GridCheckBoxColumn DataField="NotPay" HeaderText="БО"/>
                <telerik:GridBoundColumn DataField="DefectVodomer" HeaderText="Брак" FilterControlWidth="20px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlWidth="50px" DataField="nom_zavod" HeaderText="№ водомера"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlWidth="40px" DataField="conventional_signth" HeaderText="Тип водомера"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="date_make" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="date_make" HeaderText="Год водомера" DataFormatString="{0:yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn  DataField="Surname" HeaderText="Абонент">
                   <ItemTemplate>
                        <b><span><%# Eval("Surname") + " " + Eval("FirstName") + " " + Eval("LastName")%></span></b>
                   </ItemTemplate>
               </telerik:GridTemplateColumn>
                 <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Phone" HeaderText="Тел."></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Адрес"></telerik:GridBoundColumn>
               
        </Columns>
        </MasterTableView>
</telerik:RadGrid>
    

    <asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrieveFJournalNumberJournalVodomer" SelectCommandType="StoredProcedure">
	<SelectParameters>  	
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />  	
	</SelectParameters>
</asp:SqlDataSource>
    
   </asp:Content>

