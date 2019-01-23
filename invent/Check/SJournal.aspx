<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SJournal.aspx.cs" Inherits="invent_app.SJournal"%>
<%@ Register src="../Controls/AlternAbonDet.ascx" tagName="AlternAbonDet" tagPrefix="uc1"%>
<%@ Register TagPrefix="uc1" Namespace="invent_app.Controls" Assembly="invent_app" %>
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
             <telerik:AjaxSetting AjaxControlID="AlternAbonDet1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AlternAbonDet1" LoadingPanelID="RadAjaxLoadingPanel1" />
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
           var oWnd = radopen("AlternativeAbonentDet.aspx?id=" + args.getDataKeyValue("ID"), null, 800, 700, 20, 20);
           oWnd.set_visibleStatusbar = false;
           oWnd.center();
       }
    </script>
     <telerik:RadWindow runat="server" ID="radSAbonent" Title="Просмотр - выдача"  Width="720px" MinHeight="430px" CenterIfModal="True" KeepInScreenBounds="True" VisibleStatusbar="False">
            <ContentTemplate>
                   <uc1:AlternAbonDet ID="AlternAbonDet1" runat="server"/>
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
                <telerik:GridBoundColumn DataField="StartValue" HeaderText="Показания"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="conventional_signth" HeaderText="Тип водомера"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nom_zavod" HeaderText="Номер водомера"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn  DataField="Surname" HeaderText="Абонент">
                   <ItemTemplate>
                        <b><span><%# Eval("FirstName") + " " + Eval("Surname") + " " + Eval("LastName")%></span></b>
                   </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Тел." ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Адрес"></telerik:GridBoundColumn>
        </Columns>
        </MasterTableView>
</telerik:RadGrid>
    <asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrieveSJournal" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>