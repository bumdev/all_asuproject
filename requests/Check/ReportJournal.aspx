<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="requests_app.ReportJournal" Codebehind="ReportJournal.aspx.cs"  %>

<%@ Register src="../Controls/ArchiveDet.ascx" tagname="ArchiveDet" tagprefix="uc1" %>


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
             <telerik:AjaxSetting AjaxControlID="ArchiveDet1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ArchiveDet1" LoadingPanelID="RadAjaxLoadingPanel1" />
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
           var oWnd = radopen("ArchiveDeviceDet.aspx?id=" + args.getDataKeyValue("ID"), null, 800, 600, 20, 20);
           oWnd.set_visibleStatusbar = false;
           oWnd.center();

       }
    </script>
     <telerik:RadWindow runat="server" ID="radArchive" Title="Просмотр заявок"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False">
            <ContentTemplate>
                  <uc1:ArchiveDet ID="ArchiveDet1" runat="server"/>
            </ContentTemplate>
        </telerik:RadWindow>
    
    <telerik:RadWindow runat="server" ID="RadWindow2" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False"></telerik:RadWindow>
    <telerik:RadFilter runat="server" RenderMode="Lightweight" ID="radfilter" FilterContainerID="radgrid" ShowApplyButton="true"></telerik:RadFilter>
    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../Images/Excel_XLSX.png"
            OnClick="ImageButton_Click" AlternateText="Biff" />
    
    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="225" AllowSorting="true" 
        OnItemCommand="radgridDevice_ItemCommand">
         <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
        <ExportSettings ExportOnlyData="true" OpenInNewWindow="true"></ExportSettings>
     <MasterTableView DataKeyNames="ID" DataMember="Forder" Width="100%"  Name="Order"  AllowFilteringByColumn="true"  ClientDataKeyNames="ID">
         
            <Columns>
                <telerik:GridBoundColumn FilterControlWidth="40px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="№ заявки"  UniqueName="OrderID" ></telerik:GridBoundColumn>  
                <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Phone" HeaderText="Тел."></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlWidth="50px" DataField="AmountDevice" HeaderText="Количество"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Адрес"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateRequests" EnableTimeIndependentFiltering="True"  EnableRangeFiltering="true" UniqueName="DateRequests" HeaderText="Дата заявки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateWithdrawal" EnableTimeIndependentFiltering="True" EnableRangeFiltering="true" UniqueName="DateWithdrawal" HeaderText="Дата снятия" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateInstallation" EnableTimeIndependentFiltering="True" EnableRangeFiltering="true" UniqueName="DateInstallation" HeaderText="Дата установки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Comment" HeaderText="Примечание"></telerik:GridBoundColumn>
                
               
        </Columns>
        </MasterTableView>
</telerik:RadGrid>
    

    <asp:SqlDataSource ID="dsArchiveJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrieveArchiveJournal" SelectCommandType="StoredProcedure">
	<SelectParameters>  	
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />  	
	</SelectParameters>
</asp:SqlDataSource>
    
   </asp:Content>

