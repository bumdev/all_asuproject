<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Invent.aspx.cs" Inherits="invent_app.Invent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div style="width:100%; margin-top:7px; border-bottom:3px solid black; height:30px;display: none;">
<asp:DropDownList runat="server" ID="ddlSeller"></asp:DropDownList>
<asp:DropDownList runat="server" ID="ddlDiametr"></asp:DropDownList>
<div style="float:right; width:100px;">
<asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" OnClick="lbSearch_Click"><span>Искать</span></asp:LinkButton></div>
</div>
<div>
<div style="width:200px; float:inherit; display: none;">
<asp:LinkButton runat="server" ID="lbInventAdd" CssClass="FormButton" 
        onclick="lbInventAdd_Click"><span>Добавить тип водомера</span></asp:LinkButton>
</div>

    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="50"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridDevice_UpdateCommand"
        OnItemDataBound="radgrid_OnDatabBound" >
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Invent"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>   
                
                <telerik:GridDropDownColumn UniqueName="ti" DropDownControlType="RadComboBox" DataField="TypeFurnID" HeaderText="Тип инвентаря" DataSourceID="dsTypeImplem" ListTextField="NameType" ListValueField="ID" ItemStyle-Wrap="true">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server" Width="180px" ID="TypeFurnID" DataSourceID="dsTypeImplem" DataTextField="NameType"
                            DataValueField="ID" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("ti").CurrentFilterValue %>'
                            OnClientSelectedIndexChanged="TypeImplemChanged" Filter="Contains">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
                            <script type="text/javascript">
                                function TypeImplemChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("ti", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadCodeBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="md" DropDownControlType="RadComboBox" DataField="ModelID" DataSourceID="dsModel" HeaderText="Модель" ListTextField="ModelName" ListValueField="ID" ItemStyle-Wrap="true">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server" Width="180px" ID="ModelID" DataSourceID="dsModel" DataTextField="ModelName"
                            DataValueField="ID" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("md").CurrentFilterValue %>'
                            OnClientSelectedIndexChanged="ModelChanged" Filter="Contains">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock3">
                            <script type="text/javascript">
                                function ModelChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("md", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadCodeBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="gp" DropDownControlType="RadComboBox" DataField="GroupID" DataSourceID="dsGroup" HeaderText="Группа" ListTextField="GroupName" ListValueField="ID" ItemStyle-Wrap="true">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server" Width="180px" ID="GroupID" DataSourceID="dsGroup" DataTextField="GroupName"
                            DataValueField="ID" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("gp").CurrentFilterValue %>'
                            OnClientSelectedIndexChanged="GroupChanged" Filter="Contains">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock4">
                            <script type="text/javascript">
                                function GroupChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("gp", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadCodeBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="mf" DropDownControlType="RadComboBox" DataField="ManufacturerID" DataSourceID="dsManufacturer" HeaderText="Производитель" ListTextField="ProdName" ListValueField="ID" ItemStyle-Wrap="true">
                    <FilterTemplate>
                        <telerik:RadComboBox runat="server" Width="180px" ID="ManufacturerID" DataSourceID="dsManufacturer" DataTextField="ProdName"
                            DataValueField="ID" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("mf").CurrentFilterValue %>'
                            OnClientSelectedIndexChanged="ManufacturerChanged" Filter="Contains">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock6">
                            <script type="text/javascript">
                                function ManufacturerChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("mf", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadCodeBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="Address" HeaderText="Местонахождение" SortExpression="Address" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false" DataField="InventoryNumber" HeaderText="Инвентарный номер" SortExpression="InventoryNumber" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false" DataField="NomenclativeNumber" HeaderText="Номенклатурный номер" SortExpression="NomenclativeNumber" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateExploitation" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="DateExploitation" HeaderText="Дата ввода в эксплуатацию" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false" DataField="CountImplements" HeaderText="Количество" SortExpression="CountImplements" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="Comment" HeaderText="Примечание" SortExpression="Comment" FilterControlWidth="30px"></telerik:GridBoundColumn>
                
                 
                
                
               <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" ConfirmText="Вы точно хотите удалить этот пункт?" />
        </Columns>
        <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
        </EditFormSettings>
        </MasterTableView>
</telerik:RadGrid>    

<cuc:GridView  DataSourceID="dsJournal" AllowPaging="True" PageSize="50"  Visible="false"
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="True" ShowFooter="True" 
        ShowHeaderWhenEmpty="True" HighlightedRowCssClass="HighlightedRow" 
        EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" 
        CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="False" 
        AllowSorting="True" onrowupdating="gvJournal_RowUpdating" >
	<AlternatingRowStyle CssClass="GridViewAltRow" />
	<RowStyle CssClass="GridViewRow" />
	<SortAscendingStyle CssClass="Sorted SortAscending" />
	<SortDescendingStyle CssClass="Sorted SortDescending" />
	<HeaderStyle CssClass="GridViewHeader" />
	<PagerStyle CssClass="GridViewHeader" />
	<EmptyDataTemplate>
			Нет данных
	</EmptyDataTemplate>
	<Columns>
        <asp:TemplateField Visible="false" AccessibleHeaderText="colEdit"  HeaderStyle-Width="175px">
            <ItemTemplate>
			    <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server"><span>Редакт</span></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить тип водомера?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        <asp:BoundField HeaderText="Диаметр" DataField="diameter" SortExpression="diameter"/>
        <asp:CheckBoxField HeaderText="НП" DataField="Active" SortExpression="Active"/>
        <asp:BoundField HeaderText="Гос регистрация" DataField="GovRegister" SortExpression="GovRegister"/>
        <asp:BoundField HeaderText="ПИ" DataField="CheckInterval" SortExpression="CheckInterval"/>
        <asp:CheckBoxField HeaderText="ВДК" DataField="Approve" SortExpression="Approve"/>
        <asp:BoundField HeaderText="Произведен" DataField="DateProduced" SortExpression="DateProduced"  DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
        
        <asp:TemplateField HeaderText="Водомер" SortExpression="conventional_signth">
            <ItemTemplate>
                <b>Модель:&nbsp;<asp:Literal runat="server" ID="litModel" Text=<%# Eval("conventional_signth") %>></asp:Literal></b><br/>
                Производитель:&nbsp<asp:Literal runat="server" ID="litSeller"  Text=<%# Eval("seller") %>></asp:Literal><br/>
                Описание:&nbsp;<asp:Literal runat="server" ID="litDescription"  Text=<%# Eval("description") %>></asp:Literal>
            </ItemTemplate>
            <EditItemTemplate>
                <b>Модель:&nbsp;<asp:TextBox runat="server" ID="tbModel" Text=<%# Eval("conventional_signth") %> Width="300px"></asp:TextBox></b><br/>
                Производитель:&nbsp<asp:DropDownList runat="server" ID="ddlSeller"  Width="300px" DataSourceID="dsSeller" DataTextField="seller" DataValueField="id" SelectedValue=<%# Eval("id_seller") %>></asp:DropDownList><br/>
                Описание:&nbsp;<asp:TextBox runat="server" ID="tbDescription"  Text=<%# Eval("description") %> Width="300px"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>	
	</Columns>
</cuc:GridView>

<asp:SqlDataSource ID="dsTypeImplem" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveTypeImplem"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsDepart" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveDepart"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsModel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveModel"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsGroup" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveGroup"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsDistrict" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveDistricts"></asp:SqlDataSource>
<asp:SqlDataSource ID="dsManufacturer" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveManufacturer"></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveInventory" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateInventory" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteInventory" DeleteCommandType="StoredProcedure" 
    InsertCommand="CreateInventory" InsertCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
