<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Reports_1.aspx.cs" Inherits="cartridges_app.Reports_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div>
    <telerik:RadFilter runat="server" RenderMode="Lightweight" ID="radfilter" FilterContainerID="radgrid" ShowApplyButton="true"></telerik:RadFilter>
    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../Images/excel (1).png"
            OnClick="ImageButton_OnClick" AlternateText="Biff" />
    
  <telerik:RadGrid RenderMode="Lightweight" ID="radgrid" runat="server" DataSourceID="dsJournal" AllowPaging="true"
            PageSize="200" AutoGenerateColumns="false" 
             OnItemCommand="radgridExport_ItemCommand">
      
      <ExportSettings ExportOnlyData="true" OpenInNewWindow="true"></ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="NameCartridges"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
            <Columns>
                <telerik:GridBoundColumn DataField="Cartname"  HeaderText="Картридж" SortExpression="Cartname"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NameCartridge"  HeaderText="Модель" SortExpression="NameCartridge"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DepartName"  HeaderText="Отдел" SortExpression="DepartName"></telerik:GridBoundColumn>
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
                <telerik:GridDateTimeColumn FilterControlWidth="50px"  DataField="DateInsert" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="DateInsert" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
				
        </Columns>
        
        </MasterTableView>
</telerik:RadGrid>    

<asp:SqlDataSource ID="dsJournal" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
        SelectCommand="RetrieveCartridges" runat="server" SelectCommandType="StoredProcedure">  
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
