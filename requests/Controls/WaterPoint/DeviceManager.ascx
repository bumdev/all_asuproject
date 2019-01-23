<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceManager.ascx.cs" Inherits="requests_app.DeviceManager" %>
<asp:HiddenField runat="server" ID="hfWPID"  EnableViewState="true"/>
<telerik:RadGrid  runat="server" ID="radgrid" AutoGenerateColumns="false"  
    onneeddatasource="radgridDevice_NeedDataSource" 
    onitemcommand="radgrid_ItemCommand" >
    <MasterTableView DataKeyNames="ID">
        <Columns>            
            <telerik:GridButtonColumn UniqueName="Attach" Text="Выбрать" CommandName="Attach"></telerik:GridButtonColumn>
            <telerik:GridBoundColumn DataField="qnt" HeaderText="Операций" AllowFiltering="false" SortExpression="qnt" ReadOnly="true" ></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Title" HeaderText="Название" SortExpression="Title" ></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="FN" HeaderText="Заводской №"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Description" HeaderText="Описание" EmptyDataText="Нет данных"></telerik:GridBoundColumn>
            <telerik:GridDropDownColumn   AllowFiltering="false" UniqueName="DeviceType" DropDownControlType="RadComboBox" DataField="WPTypeDeviceID" HeaderText="Тип" DataSourceID="dsDeviceType" ListTextField="Title" ListValueField="ID" ItemStyle-Wrap="true"></telerik:GridDropDownColumn>
            <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>


<asp:SqlDataSource ID="dsDevices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveLeftDevices" 
SelectCommandType="StoredProcedure"  InsertCommand="CreateWPDeviceAssign" InsertCommandType="StoredProcedure" DeleteCommand="DeleteDevice" DeleteCommandType="StoredProcedure" UpdateCommand="UpdateDevice" UpdateCommandType="StoredProcedure">
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsDeviceType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveDevicesType" 
SelectCommandType="StoredProcedure" >
</asp:SqlDataSource>