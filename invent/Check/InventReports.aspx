<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="InventReports.aspx.cs" Inherits="invent_app.InventReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>

    <telerik:RadFilter runat="server" FilterContainerID="radgrid" ID="radfilter" ShowApplyButton="true" RenderMode="Lightweight"></telerik:RadFilter>
    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../Images/excel (2).png"
                     OnClick="ExportButton_OnClick" AlternateText="Biff" />

  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" 
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS" PageSize="220"
        OnItemCommand="radgridExport_ItemCommand" >
      <GroupingSettings CaseSensitive="false"></GroupingSettings>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>   
      <ExportSettings HideStructureColumns="true" ExportOnlyData="true" OpenInNewWindow="true"></ExportSettings>
        <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" DataMember="Forder" Width="100%" Name="Invent"  AllowFilteringByColumn="true" >
            
            <Columns>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="NameType" ShowFilterIcon="false" HeaderText="Тип инвентаря" SortExpression="NameType" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="ModelName" ShowFilterIcon="false" HeaderText="Модель" SortExpression="ModelName" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="GroupName" ShowFilterIcon="false" HeaderText="Группа" SortExpression="GroupName" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="ProdName" ShowFilterIcon="false" HeaderText="Производитель" SortExpression="DepartName" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="Address" HeaderText="Местонахождение" SortExpression="Address" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="InventoryNumber" HeaderText="Инвентарный номер" SortExpression="InventoryNumber" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="NomenclativeNumber" HeaderText="Номенклатурный номер" SortExpression="NomenclativeNumber" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateExploitation" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="DateExploitation" HeaderText="Дата ввода в эксплуатацию" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false" DataField="CountImplements" HeaderText="Количество" SortExpression="CountImplements" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="Comment" HeaderText="Примечание" SortExpression="Comment" FilterControlWidth="30px"></telerik:GridBoundColumn>
        </Columns>
        </MasterTableView>
</telerik:RadGrid>    
    

<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveInventory" SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>
</div>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>