<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Cartridges.aspx.cs" Inherits="cartridges_app.Cartridges" %>
<%@ Register src="../Controls/CartAdd.ascx" tagname="CartAdd" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div style="width:100%; margin-top:7px; border-bottom:3px solid black; height:30px;display: none;">
<asp:DropDownList runat="server" Width="400px" ID="ddlTypeCart"></asp:DropDownList>
<asp:DropDownList runat="server" Width="400px" ID="ddlDepart"></asp:DropDownList>
<asp:DropDownList runat="server" Width="400px" ID="ddlCartName"></asp:DropDownList>
<div style="float:right; width:100px;">
<asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" OnClick="lbSearch_Click"><span>Искать</span></asp:LinkButton></div>
</div>
<div>
<div style="width:400px; float:inherit; display: none;">
<asp:LinkButton runat="server" ID="lbCartAdd" CssClass="FormButton" 
        onclick="lbCartAdd_Click"><span>Добавить обрудование</span></asp:LinkButton>
</div>

    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="30"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridCart_UpdateCommand"
        onitemdatabound="radgrid_ItemDataBound" >
     <%-- <ClientSettings>
          <Scrolling AllowScroll="true" EnableVirtualScrollPaging="true" SaveScrollPosition="true" ScrollHeight="700px" UseStaticHeaders="true" />
      </ClientSettings>--%>

         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
      
        <MasterTableView EditMode="PopUp" EditFormSettings-PopUpSettings-Width="600px" TableLayout="fixed" DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="NameCartridges"  AllowFilteringByColumn="true" >
            
            <Columns>
                
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>   
                
                <telerik:GridDateTimeColumn FilterControlWidth="100px"  DataField="DateInsert" EnableTimeIndependentFiltering="false"  EnableRangeFiltering="false" UniqueName="DateInsert" HeaderText="Дата ввода" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px"></ItemStyle>
                </telerik:GridDateTimeColumn>
               <telerik:GridDropDownColumn  UniqueName="cd" DropDownControlType="RadComboBox" DataField="CartDirectID" HeaderText="Картридж" DataSourceID="dsCartDirect"
                      ListTextField="Cartname" ListValueField="ID"  ItemStyle-Wrap="true">
                     <FilterTemplate>                                       
                       <telerik:RadComboBox Width="250px" ID="CartDirectID" DataSourceID="dsCartDirect" DataTextField="Cartname" 
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("cd").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="DirectCart" Filter="Contains"
                           EmptyMessage="Поиск...">
                            <Items>
                                <telerik:RadComboBoxItem  />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                            <script type="text/javascript">
                                function DirectCart(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("cd", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                            
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn   UniqueName="sl" DropDownControlType="RadComboBox" DataField="TypeCrtridgesID" HeaderText="Модель" DataSourceID="dsTypeCart"
                      ListTextField="NameCartridge" ListValueField="ID" ItemStyle-Wrap="true">
                     <FilterTemplate>                                       
                       <telerik:RadComboBox Width="250px" ID="TypeCrtridgesID" DataSourceID="dsTypeCart" DataTextField="NameCartridge" 
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("sl").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="CartChanged" Filter="Contains"
                           EmptyMessage="Поиск...">
                            <Items>
                                <telerik:RadComboBoxItem />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function CartChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("sl", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                            
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>              
                <telerik:GridDropDownColumn   UniqueName="tp" DropDownControlType="RadComboBox" DataField="DepartamentID" HeaderText="Отделы" DataSourceID="dsDepartName" 
                    ListTextField="DepartName" ListValueField="ID" ItemStyle-Wrap="true">
                     <FilterTemplate>                                       
                       <telerik:RadComboBox Width="250px" ID="DepartamentID" DataSourceID="dsDepartName" DataTextField="DepartName" 
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("tp").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="TitleIndexChanged" EmptyMessage="Поиск..."
                           Filter="Contains">
                            <Items>
                                <telerik:RadComboBoxItem  />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                            <script type="text/javascript">
                                function TitleIndexChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("tp", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>
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
				<telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" ConfirmText="Вы точно хотите удалить картридж?"  />
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
        <asp:TemplateField Visible="false" AccessibleHeaderText="colEdit"  HeaderStyle-Width="400px">
            <ItemTemplate>
			    <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server"><span>Редакт</span></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить картридж?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        
        
        <asp:TemplateField HeaderText="Картриджи" SortExpression="NameCartridges">
            <ItemTemplate>
                <b>Наименование картриджа:&nbsp;<asp:Literal runat="server" ID="litNameCart" Text=<%# Eval("NameCartridges") %>></asp:Literal></b><br/>
                Картридж:&nbsp;<asp:Literal runat="server" ID="litCartDirect" Text=<%# Eval("Cartname") %>></asp:Literal><br/>
                Модель картриджа:&nbsp<asp:Literal runat="server" ID="litTypeCart"  Text=<%# Eval("NameCartridge") %>></asp:Literal><br/>
                Отдел:&nbsp;<asp:Literal runat="server" ID="litDepartament"  Text=<%# Eval("DepartName") %>></asp:Literal><br/>
                Номер:&nbsp;<asp:Literal runat="server" ID="litNumber" Text=<%# Eval("Number") %>></asp:Literal><br/>
                Информация (статус):&nbsp;<asp:Literal runat="server" ID="litInformation" Text=<%# Eval("Information") %>></asp:Literal><br/>
                Дата заправки:&nbsp;<asp:Literal runat="server" ID="litDateFueling" Text=<%# Eval("DateFueling") %>></asp:Literal>
            </ItemTemplate>
            <EditItemTemplate>
                <b>Наименование картриджа:&nbsp;<asp:TextBox runat="server" ID="tbNameCart" Text=<%# Eval("NameCartridges") %> Width="300px"></asp:TextBox></b><br/>
                Картридж:&nbsp;<asp:DropDownList runat="server" ID="ddlCartName" Width="300px" DataSourceID="dsCartDirect" DataTextField="Cartname" DataValueField="ID" SelectedValue=<%# Eval("CartnameID") %>></asp:DropDownList><br/>
                Модель картриджа:&nbsp<asp:DropDownList runat="server" ID="ddlTypeCart"  Width="300px" DataSourceID="dsTypeCart" DataTextField="NameCartridge" DataValueField="ID" SelectedValue=<%# Eval("TypeCrtridgesID") %>></asp:DropDownList><br/>
                Отдел:&nbsp;<asp:DropDownList runat="server" ID="ddlDepartName" Width="300px" DataSourceID="dsDepartName" DataTextField="DepartName" DataValueField="ID" SelectedValue=<%# Eval("DepartamentID") %>></asp:DropDownList><br/>
                Номер:&nbsp;<asp:TextBox runat="server" ID="tbNumber" Text=<%# Eval("Number") %> Width="300px"></asp:TextBox><br/>
                Информация (статус):&nbsp;<asp:TextBox runat="server" ID="tbInfo" Text=<%# Eval("Information") %> Width="300px"></asp:TextBox><br/>
                Дата заправки:&nbsp;<asp:TextBox runat="server" ID="tbDateFueling" Text=<%# Eval("DateFueling") %>></asp:TextBox>
                
            </EditItemTemplate>
        </asp:TemplateField>	
	</Columns>
</cuc:GridView>

<asp:SqlDataSource ID="dsTypeCart" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveTypeCart" ></asp:SqlDataSource>
<asp:SqlDataSource ID="dsDepartName" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveDepart" ></asp:SqlDataSource>
<asp:SqlDataSource ID="dsCartDirect" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveCartDirect" ></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveCartridges" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateCartridge" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteCartridge" DeleteCommandType="StoredProcedure" 
    InsertCommand="CreateCartridges" InsertCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
<uc1:CartAdd ID="CartAdd1" runat="server" Visible="false" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
