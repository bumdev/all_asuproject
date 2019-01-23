<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EquipRegistration.aspx.cs" Inherits="leak_detectors.EquipRegistration" %>
<%@ Register src="../Controls/EquipAdd.ascx" tagname="EquipAdd" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">
<div style="width:100%; margin-top:7px; border-bottom:3px solid black; height:30px;display: none;">
<asp:DropDownList runat="server" ID="ddlTypeEquipment"></asp:DropDownList>
<div style="float:right; width:100px;">
<asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" OnClick="lbSearch_Click"><span>Искать</span></asp:LinkButton></div>
</div>
<div>
<div style="width:200px; float:inherit; display: none;">
<asp:LinkButton runat="server" ID="lbTypeAdd" CssClass="FormButton" 
        onclick="lbTypeAdd_Click"><span>Добавить обрудование</span></asp:LinkButton>
</div>

    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="30"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridDevice_UpdateCommand" >
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="EquipmentName"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>   
                
                <telerik:GridBoundColumn DataField="EquipmentName"  HeaderText="Модель" SortExpression="EquipmentName"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TechnicalCharachteristic"  HeaderText="Тех. характеристика" SortExpression="TechnicalCharachteristic"></telerik:GridBoundColumn>
                 <telerik:GridDropDownColumn   UniqueName="sl" DropDownControlType="RadComboBox" DataField="TypeEquipmentID" HeaderText="Тип оборудования" DataSourceID="dsTypeEquip" ListTextField="TypeName" ListValueField="ID" ItemStyle-Wrap="true">
                     <FilterTemplate>                                       
                       <telerik:RadComboBox Width="180px" ID="TypeEquipmentID" DataSourceID="dsTypeEquip" DataTextField="TypeName" 
                            DataValueField="ID"  AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("sl").CurrentFilterValue %>'
                            runat="server" OnClientSelectedIndexChanged="TitleIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="Все" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function TitleIndexChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("sl", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridDropDownColumn>  
                
                 
                
                
               <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
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
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить оборудование?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        
        
        <asp:TemplateField HeaderText="Оборудование" SortExpression="EquipmentName">
            <ItemTemplate>
                <b>Модель:&nbsp;<asp:Literal runat="server" ID="litModel" Text=<%# Eval("EquipmentName") %>></asp:Literal></b><br/>
                Тип оборудования:&nbsp<asp:Literal runat="server" ID="litTypeEquip"  Text=<%# Eval("TypeName") %>></asp:Literal><br/>
                Описание:&nbsp;<asp:Literal runat="server" ID="litTechnicalCharacteristic"  Text=<%# Eval("TechnicalCharachteristic") %>></asp:Literal>
            </ItemTemplate>
            <EditItemTemplate>
                <b>Модель:&nbsp;<asp:TextBox runat="server" ID="tbModel" Text=<%# Eval("EquipmentName") %> Width="300px"></asp:TextBox></b><br/>
                Тип оборудования:&nbsp<asp:DropDownList runat="server" ID="ddlTypeEquipment"  Width="300px" DataSourceID="dsTypeEquip" DataTextField="TypeName" DataValueField="ID" SelectedValue=<%# Eval("TypeEquipmentID") %>></asp:DropDownList><br/>
                Описание:&nbsp;<asp:TextBox runat="server" ID="tbTechnicalCharacteristic"  Text=<%# Eval("TechnicalCharachteristic") %> Width="300px"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>	
	</Columns>
</cuc:GridView>

<asp:SqlDataSource ID="dsTypeEquip" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" SelectCommand="RetrieveTypeEquipment" ></asp:SqlDataSource>


<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEquipRegistration" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateEquipRegistration" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteEquipRegistration" DeleteCommandType="StoredProcedure" 
    InsertCommand="CreateEquipRegistration" InsertCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>
<uc1:EquipAdd ID="EquipAdd1" runat="server" Visible="false" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
