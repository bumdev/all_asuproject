<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="QuestionType.aspx.cs" Inherits="ClericalWork_WebApp.QuestionType" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
    <script src="scripts/openimage.js" type="text/javascript" ></script>     
<div style="width:100%; border:0px solid black; min-height:300px;">
<div>
   
    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="10"
        onneeddatasource="radgridDevice_NeedDataSource" CssClass="rad" AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" ClientEvents-OnRequestStart="onRequestStart"
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridServices_UpdateCommand"
        onitemdatabound="radgrid_ItemDataBound" OnItemCommand="radgrid_itemcommand"
        AllowSorting="true" RenderMode="Auto">
      
      <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
         
      </ClientSettings>

         <ExportSettings ExportOnlyData="true" IgnorePaging="true">
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
      
        <MasterTableView IsFilterItemExpanded="True" EditFormSettings-PopUpSettings-Width="900px" DataKeyNames="ID" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="TypeQuestion"  AllowFilteringByColumn="true" >
            <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="False" />
            
            <Columns>
                <telerik:GridEditCommandColumn ItemStyle-Width="20px" UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn> 
                

                <%--<telerik:GridBoundColumn FilterControlWidth="40px" ItemStyle-Width="50px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="ID"  UniqueName="distr_id" ></telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="NameQuestion" HeaderText="Тип вопроса" SortExpression="NameQuestion" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"></telerik:GridBoundColumn>
                
                
                <telerik:GridButtonColumn runat="server" ItemStyle-Width="20px" CommandName="Delete" Text="Delete" ButtonType="ImageButton" ConfirmText="Вы точно хотите удалить?" />
                
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />            
        </EditFormSettings>
        

        </MasterTableView>
     
</telerik:RadGrid>
    
        
<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
    SelectCommand="RetrieveQuestion" SelectCommandType="StoredProcedure" 
    InsertCommand="CreateQuestion" InsertCommandType="StoredProcedure"
    UpdateCommand="UpdateQuestion" UpdateCommandType="StoredProcedure"
    DeleteCommand="DeleteQuestion" DeleteCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


