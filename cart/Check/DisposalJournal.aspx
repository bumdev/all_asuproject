<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DisposalJournal.aspx.cs" Inherits="cartridges_app.DisposalJournal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div style="width:100%; border:0px solid black; min-height:300px;">

<div>

    
  <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false"   PageSize="30"
        CssClass="rad" onneeddatasource="radgridDevice_NeedDataSource" 
        AllowPaging="True"  Font-Names="Arial Unicode MS"
        Culture="ru-RU" oninsertcommand="radgrid_InsertCommand" 
        AllowAutomaticInserts="true" ondeletecommand="radgrid_DeleteCommand" onupdatecommand="radgridDevice_UpdateCommand" >
         <ExportSettings>
    <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2"/>
    </ExportSettings>
        <MasterTableView DataKeyNames="ID" DataMember="WP" Width="100%" CommandItemDisplay="Top" AllowAutomaticUpdates="true" Name="Disposal"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>
				<telerik:GridBoundColumn DataField="Phone"  HeaderText="Телефон" SortExpression="Phone"></telerik:GridBoundColumn>				
                <telerik:GridBoundColumn DataField="AmountDevice"  HeaderText="Количество водомеров" SortExpression="AmountDevice" FilterControlWidth="30px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address"  HeaderText="Адрес" SortExpression="Address"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateRequests" EnableTimeIndependentFiltering="True"  EnableRangeFiltering="true" UniqueName="DateRequests" HeaderText="Дата заявки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateWithdrawal" EnableTimeIndependentFiltering="True" EnableRangeFiltering="true" UniqueName="DateWithdrawal" HeaderText="Дата снятия" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateInstallation" EnableTimeIndependentFiltering="True" EnableRangeFiltering="true" UniqueName="DateInstallation" HeaderText="Дата установки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="100px"></ItemStyle>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Comment"  HeaderText="Примечание" SortExpression="Comment"></telerik:GridBoundColumn> 
                <telerik:GridCheckBoxColumn DataField="IsInstallation"  HeaderText="Установлен" SortExpression="IsInstallation"  HeaderTooltip="Установлен"></telerik:GridCheckBoxColumn>
                
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
                <asp:LinkButton ID="LinkButton1" CommandName="Delete" CssClass="FormButton" runat="server"  OnClientClick=<%# "return confirm('Вы действительно хотите удалить заявку?');" %> ><span>Удалить</span></asp:LinkButton>
		    </ItemTemplate>
		    <EditItemTemplate>
			    <asp:LinkButton ID="btnUpdate" CommandName="Update"  CssClass="FormButton" runat="server"><span>Сохранить</span></asp:LinkButton>
			    <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
		    </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
        <asp:BoundField HeaderText="Телефон" DataField="Phone" SortExpression="Phone"/>
        <asp:BoundField HeaderText="Количество водомеров" DataField="AmountDevice" SortExpression="AmoutnDevice"/>
        <asp:BoundField HeaderText="Адрес" DataField="Address" SortExpression="Address"/>
        <asp:BoundField HeaderText="Дата заявки" DataField="DateRequests" SortExpression="DateRequests"  DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
        <asp:BoundField HeaderText="Дата снятия" DataField="DateInstallation" SortExpression="DateInstallation"  DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
        <asp:BoundField HeaderText="Дата установки" DataField="DateWithdrawal" SortExpression="DateWithdrawal"  DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
        <asp:TemplateField HeaderText="Заявка" SortExpression="Phone">
            <ItemTemplate>
                <b>Телефон:&nbsp;<asp:Literal runat="server" ID="litPhone" Text=<%# Eval("Phone") %>></asp:Literal></b><br/>
                Количество:&nbsp;<asp:Literal runat="server" ID="litAmountDevice"  Text=<%# Eval("AmountDevice") %>></asp:Literal><br/>
                Примечание:&nbsp;<asp:Literal runat="server" ID="litComment"  Text=<%# Eval("Comment") %>></asp:Literal>
            </ItemTemplate>
            <EditItemTemplate>
                <b>Телефон:&nbsp;<asp:TextBox runat="server" ID="tbPhone" Text=<%# Eval("Phone") %> Width="300px"></asp:TextBox></b><br/>
                Количество водомеров:&nbsp;<asp:TextBox runat="server" ID="tbAmountDevice" Text=<%# Eval("AmountDevice") %> Width="300px"></asp:TextBox></b><br/>
                Примечание:&nbsp;<asp:TextBox runat="server" ID="tbComment"  Text=<%# Eval("Comment") %> Width="300px"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
	</Columns>
</cuc:GridView>



<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveDisposal" SelectCommandType="StoredProcedure" 
UpdateCommand="UpdateDisposal" UpdateCommandType="StoredProcedure"
DeleteCommand="DeleteDisposal" DeleteCommandType="StoredProcedure" 
    InsertCommand="CreateDisposal" InsertCommandType="StoredProcedure">
</asp:SqlDataSource>
</div>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
