<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DisposalGrid.aspx.cs.aspx.cs" Inherits="cartridges_app.DisposalGrid" %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:HiddenField runat="server" ID="hfUserID" />
    <asp:UpdatePanel runat="server" ID="update_withd">
        <ContentTemplate>
            <div>
                <telerik:RadGrid runat="server" ID="radgrid" AutoGenerateColumns="false" PageSize="25"
                    CssClass="rad" OnNeedDataSource="RadGridWithD_NeedDataSource" AllowPaging="true" Font-Names="Arial Unicode MS"
                    Culture="ru-RU" OnInsertCommand="RadGrid_ItemInserted" AllowAutomaticInserts="true"
                    OnDeleteCommand="RadGrid_ItemDeleted" OnUpdateCommand="RadGrid_ItemUpdated">
                    <ExportSettings>
                        <Pdf DefaultFontFamily="Arial Unicode MS" PageTopMargin="2" PageLeftMargin="2" PageRightMargin="2" PageBottomMargin="2" />
                    </ExportSettings>
                    <MasterTableView  DataKeyNames="ID" DataMember="Forder" Width="100%"  Name="Order"  AllowFilteringByColumn="true"  CommandItemDisplay="top">
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn FilterControlWidth="40px" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" DataField="ID" HeaderText="№ заявки"  UniqueName="OrderID" ></telerik:GridBoundColumn>  
                            <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Phone" HeaderText="Тел."></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="50px" DataField="AmountDevice" HeaderText="Количество водомеров"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Address" HeaderText="Адрес"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateRequests" EnableTimeIndependentFiltering="True"  EnableRangeFiltering="true" UniqueName="DateRequests" HeaderText="Дата заявки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="100px"></ItemStyle>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateWithdrawal" EnableTimeIndependentFiltering="True" EnableRangeFiltering="true" UniqueName="DateWithdrawal" HeaderText="Дата снятия" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="100px"></ItemStyle>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn FilterControlWidth="60px" DataField="DateInstallation" EnableTimeIndependentFiltering="True" EnableRangeFiltering="true" UniqueName="DateInstallation" HeaderText="Дата установки" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="100px"></ItemStyle>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Comment" HeaderText="Примечание"></telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn FilterControlWidth="50px" DataField="IsInstallation" HeaderText="Установлен" SortExpression="IsInstallation"></telerik:GridCheckBoxColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn ButtonType="ImageButton" />
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
                <cuc:GridView runat="server" DataSourceID="dsJournal" AllowPaging="True" PageSize="50" Visible="false"
                    DataKeyNames="ID" ShowFooterWhenEmpty="true" ShowFooter="true"
                    ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                    EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="none"
                    CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="false"
                    AllowSorting="true" OnRowUpdating="gvJournal_OnRowUpdateding">
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
                        <asp:TemplateField Visible="false" AccessibleHeaderText="colEdit" HeaderStyle-Width="175px">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnEdit" CommandName="Edit" CssClass="FormButton"><span>Редакт.</span></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="LB1" CommandName="Delete" CssClass="FormButton" OnClientClick=<%# "return confirm('Вы действительно хотите удалить заявку на снятие?');" %> >
                                    <span>Удал.</span>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton runat="server" ID="btnUpdate" CommandName="Update" CssClass="FormButton"><span>Сохранить</span></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" CssClass="FormButton"><span>Отменить</span></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="№" DataField="ID" SortExpression="ID" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Заявка на снятие" SortExpression="Phone">
                            <ItemTemplate>
                                <b>Телефон:&nbsp;<asp:Literal runat="server" ID="litPhone" Text=<%# Eval("Phone") %>></asp:Literal></b><br/>
                                Количество водомеров:&nbsp;<asp:Literal runat="server" ID="litAmountDevice" Text=<%# Eval("AmountDevice") %> ></asp:Literal><br/>
                                Дата снятия:&nbsp;<asp:Literal runat="server" ID="litDateWithdrawal" Text=<%# Eval("DateWithdrawal")%>></asp:Literal><br/>
                                Дата установки:&nbsp;<asp:Literal runat="server" ID="litDateInstallation" Text=<%# Eval("DateInstallation")%>></asp:Literal><br/>
                                Адрес:&nbsp;<asp:Literal runat="server" ID="litAddress" Text=<%# Eval("Address") %>></asp:Literal><br/>
                                Примечание:&nbsp;<asp:Literal runat="server" ID="litComment" Text=<%# Eval("Comment") %>></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <b>Телефон:&nbsp;<asp:TextBox runat="server" ID="tbPhone" Text=<%# Eval("Phone")%> Width="300px"></asp:TextBox></b><br/>
                                Количество водомеров:&nbsp;<asp:TextBox runat="server" ID="tbAmountDevice" Text=<%# Eval("AmountDevice")%> Width="300px"></asp:TextBox><br/>
                                Дата снятия:&nbsp;<asp:TextBox runat="server" ID="tbDateWithdrawal" Text=<%# Eval("DateWithdrawal")%> Width="300px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="tbDateWithdrawal"></ajaxToolkit:CalendarExtender><br/>
                                Дата установки:&nbsp;<asp:TextBox runat="server" ID="tbDateInstallation" Text=<%# Eval("DateInstallation") %> Width="300px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="tbDateInstallation"></ajaxToolkit:CalendarExtender><br/>
                                Примечание:&nbsp;<asp:TextBox runat="server" ID="tbComment" Text=<%# Eval("Comment")%> Width="300px"></asp:TextBox><br/>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IsInstallation" HeaderText="Установлен"></asp:CheckBoxField>
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