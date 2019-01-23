<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ForReports_1.aspx.cs" Inherits="cartridges_app.ForReports_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>
   
    <telerik:RadFilter runat="server" RenderMode="Lightweight" ID="radfilter" FilterContainerID="radgrid" ShowApplyButton="true"></telerik:RadFilter>
    <asp:ImageButton ID="ExportImageExport" runat="server" ImageUrl="../Images/excel (2).png"
            OnClick="ExcelImageExport_OnClick" AlternateText="Biff" />

  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="225" 
        OnItemCommand="radgridExport_ItemCommand" OnBiffExporting="radgridBiff_ExportingBiff">
      <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
      <ExportSettings HideStructureColumns="true"></ExportSettings>
        <MasterTableView IsFilterItemExpanded="false" DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="NameCartridges"  AllowFilteringByColumn="true" >
            <CommandItemSettings  ShowExportToExcelButton="false" ShowExportToWordButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
            <Columns>
                <telerik:GridBoundColumn DataField="Cartname"  HeaderText="Картридж" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NameCartridge" ShowFilterIcon="false"  HeaderText="Модель" SortExpression="NameCartridge"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DepartName" ShowFilterIcon="false"  HeaderText="Отдел" SortExpression="DepartName"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Number" ShowFilterIcon="false" FilterControlWidth="50px"  HeaderText="Номер" SortExpression="Number"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Information" ShowFilterIcon="false" FilterControlWidth="70px"  HeaderText="Информация (статус)" SortExpression="Information"></telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="Comment" ShowFilterIcon="false" FilterControlWidth="50px"  HeaderText="Замечание (доп. инфо)" SortExpression="Comment"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn  DataField="DateInWork" ShowFilterIcon="false" UniqueName="DateInWork" FilterControlWidth="100px" HeaderText="Дата в работе" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                   <ItemStyle Width="50px"></ItemStyle>
               </telerik:GridDateTimeColumn>
               <telerik:GridCheckBoxColumn DataField="InTheWork" ShowFilterIcon="false" HeaderText="В работе" />
                <telerik:GridDateTimeColumn  DataField="DateFueling" ShowFilterIcon="false" UniqueName="DateFueling" FilterControlWidth="100px" HeaderText="Дата заправки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                   <ItemStyle Width="50px"></ItemStyle>
               </telerik:GridDateTimeColumn>
                <telerik:GridCheckBoxColumn DataField="RefuelingCondition" ShowFilterIcon="false" HeaderText="Заправлен" />
                <telerik:GridDateTimeColumn FilterControlWidth="50px" ShowFilterIcon="false"  DataField="DateInsert" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="DateInsert" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
				
        </Columns>
        
        </MasterTableView>
</telerik:RadGrid>    



<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveCartridges" SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
