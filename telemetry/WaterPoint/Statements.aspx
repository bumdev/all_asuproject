<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="Statements.aspx.cs" Inherits="leak_detectors.Statements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
.rgCaption{color:black; font-size:14px; font-weight:bold; text-align:left; background:#ff9900; padding:5px;}
.rad{margin-left:81px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radgrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radgrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
     <!--Тут мы показываем наши высплывающие окошки.-->
        <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true">
        </telerik:RadWindowManager>
    <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"  
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand">
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Rate"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>   
                <telerik:GridDropDownColumn   UniqueName="WP" DropDownControlType="RadComboBox" DataField="WPID" HeaderText="Объект" DataSourceID="dsWP" ListTextField="Title" ListValueField="ID" ItemStyle-Wrap="true">
                    <FilterTemplate>                                       
                       <telerik:RadComboBox Width="180px" ID="RadComboBoxTitle" DataSourceID="dsWP" DataTextField="Title"
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("WP").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="TitleIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="Все" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function TitleIndexChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("WP", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>   
                <telerik:GridDateTimeColumn FilterControlWidth="150px" DataField="DateIn" UniqueName="DateIn" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="Rate" HeaderText="Показания" SortExpression="LineFirst" ></telerik:GridBoundColumn>
                <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
        </EditFormSettings>
        </MasterTableView>
</telerik:RadGrid>


<asp:SqlDataSource ID="dsRate" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveWPRate" 
SelectCommandType="StoredProcedure" 
InsertCommand="CreateWPRate" InsertCommandType="StoredProcedure" 
DeleteCommand="DeleteWPRate" DeleteCommandType="StoredProcedure" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsWP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveWPs" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
</asp:Content>
