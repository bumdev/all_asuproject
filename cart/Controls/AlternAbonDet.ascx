<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlternAbonDet.ascx.cs" Inherits="cartridges_app.Controls.AlternAbonDet" %>


<telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>

<telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="up">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="up" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>

<telerik:RadAjaxPanel runat="server" ID="up">
    <asp:Literal runat="server" ID="litScriptS"></asp:Literal>
    
    <asp:Panel runat="server" ID="AlternAbonForm" Width="750px" Height="500px">
        <asp:HiddenField runat="server" ID="hfODID" Value="0"/>
        <!-- Заголовок формы начало -->
        
        <!-- Форма для снятия/установки водомера -->
        <div style="width:700px; min-height: 70px;">
            <div style="font-size: 14px; margin-top: 5px; float:left;">
                <b>
                    <asp:Literal runat="server" ID="AlternAbonentInfo"></asp:Literal></b>
            </div>
            <div style="float: right; border: 1px solid black; padding: 10px;">
                <!-- Editor mode -->
                <asp:Panel runat="server" ID="punAlternAbonentEditor" Visible="false">
                    <div class="FormItem">
                        <div class="label">Район: </div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddsDistrict" Width="200px"></asp:DropDownList>
                            <div style="clear: both;"></div>
                        </div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Номер по журналу: </div>
                        <div>
                            <asp:TextBox runat="server" ID="stbNumberJournalPhysical" Width="200px"></asp:TextBox>
                            <div style="clear: both;"></div>
                        </div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Фамилия: </div>
                        <div>
                            <asp:TextBox runat="server" ID="stbClientName" Width="200px"></asp:TextBox>
                            <div style="clear: both;"></div>
                        </div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Имя: </div>
                        <div>
                            <asp:TextBox runat="server" ID="stbClientSurname" Width="200px"></asp:TextBox>
                            <div style="clear:both;"></div>
                        </div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Отчество: </div>
                        <div>
                            <asp:TextBox runat="server" ID="stbClientLastName" Width="200px"></asp:TextBox>
                            <div style="clear:both;"></div>
                        </div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Телефон: </div>
                        <div>
                            <asp:TextBox runat="server" ID="stbPhone" Width="200px"></asp:TextBox>
                            <div style="clear: both;"></div>
                        </div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Адрес:</div>

                        <div>
                            <asp:TextBox runat="server" ID="tbAddress" Width="200px"></asp:TextBox>


                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <div class="FormItem">
                        <div class="label"></div>
                        <div>
                            <telerik:RadButton runat="server" Text="Обновить" ID="radbutSaveAlternativeAbonentInfo" OnClick="radbutSaveAlternativeAbonentInfo_OnClick"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Удалить" ID="radbutDeleteAlternativeAbonentInfo" OnClick="radbutDeleteAlternativeAbonentInfo_OnClick"></telerik:RadButton>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </asp:Panel>
                

            </div>
        </div>
        <div style="width: 500px; height: 25px; border: 0px solid black; float: left; padding-bottom:75px;">
            <div style="margin-left: 10px; float: left; text-align: center; font-weight: bold; font-size: 14px; padding-top: 5px;">
                <asp:Button runat="server" ID="btWithdrawalAct" Text="Акт(снятие/установка)" OnClick="btWithdrawalActSpecial_Click" />
            </div>
            <div style="margin-left: 10px; float: left; text-align: center;font-weight: bold; font-size: 14px; padding-top: 5px;">
                <asp:Button runat="server" ID="btReplacementAct" Text="Акт(замена водомера)" OnClick="btReplacementAct_Click" />
            </div>
            <div style="margin-left: 10px; float: left; text-align: center; font-weight: bold; font-size: 14px; padding-top: 5px;">
                <asp:Button runat="server" ID="btAlternativePay" Text="Квтанция(замена водомера)" OnClick="btAlternativePay_Click" />
            </div>
            <div style="margin-left: 10px; float:left; text-align: center; font-weight: bold; font-size: 14px; padding-top: 5px;">
                <asp:Button runat="server" ID="btAlternativeSpecialPay" Text="Квитанция(снятие)" OnClick="btAlternativeSpecialPay_Click"/>
            </div>
        </div>
        <div style="border: 1px solid black; width: 750px; height:270px; overflow: auto;">
            <cuc:GridView DataSourceID="dsJournal" AllowPaging="True" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="true"
                ShowHeaderWhenEmpty="True" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None"
                CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="false"
                AllowSorting="true" OnRowUpdating="gvJournal_RowUpdating">
                <AlternatingRowStyle CssClass="GridViewAltRow" />
                <RowStyle CssClass="GridViewRow" />
                <SortAscendingStyle CssClass="Sorted SortAscending" />
                <SortDescendingStyle CssClass="Sorted Sortdescending" />
                <HeaderStyle CssClass="GridViewHeader" />
                <PagerStyle CssClass="GridViewHeader" />
                <EmptyDataTemplate>
                    Нет данных
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="colEdit">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnEdit" CommandName="Edit" CssClass="FormButton" Visible='<%# string.IsNullOrEmpty(Eval("EndValue").ToString()) %>'><span>Редакт.</span></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" CssClass="FormButton"><span>Отменить</span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnUpdate" CommandName="Update" CssClass="FormButton"><span>Обновить</span></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="№">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="ODID" Text='<%# Eval("ID")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litDiameter" Text='<%# Eval("diameter")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Заводской номер">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litFactoryNumber" Text='<%# Eval("nom_zavod")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Начальные показания">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litStartValue" Text='<%# Eval("StartValue")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Конечные показания">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litEndValue" Text='<%# Eval("EndValue")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe" TargetControlID="tbEndValue" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:TextBox runat="server" ID="tbEndValue" Text='<%# Eval("EndValue")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Цена">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="specialprice" Text='<%# Eval("specialprice")%>'></asp:Literal>
                        </ItemTemplate>
                        <FooterTemplate>
                            <table style="font-size: 12px;">
                                <tr>
                                    <td><b>Итого: </b></td>
                                    <td align="right"><%# WithdrawalMoney().ToString("0.00")%> руб.</td>
                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource runat="server" ID="dsJournal" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
                SelectCommand="RetrieveVodomersBySOrdersID"
                SelectCommandType="StoredProcedure"
                UpdateCommand="UpdateAlternativeOrderDetails"
                UpdateCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="SOrderID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="EndValue" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            
            <!-- Editor Mode MOFO -->
            
            <cuc:GridView runat="server" DataSourceID="dsJournal2" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" ShowFooterWhenEmpty="true" ShowFooter="true"
                ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None"
                CssClass="GridView MaxWidth" ID="gvJournal2" AutoGenerateColumns="false"
                AllowSorting="true" OnRowUpdating="gvJournal2_RowUpdating">
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
                    <asp:TemplateField AccessibleHeaderText="colEdit">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnEdit" CommandName="Edit" CssClass="FormButton"><span>Редакт.</span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="Delete" CommandName="Delete" CssClass="FormButton"><span>Удал.</span></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" CssClass="FormButton"><span>Отмена</span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnUpdate" CommandName="Update" CssClass="FormButton"><span>Обновить</span></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="№">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="ODID" Text='<%# Eval("ID")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litDiameter" Text='<%# Eval("diameter")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Заводской номер">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litFactoryNumber" Text='<%# Eval("nom_zavod")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="tbFactoryNumber" Text='<%# Eval("nom_zavod")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Начальные показания">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litStartValue" Text='<%# Eval("StartValue")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe1" TargetControlID="tbStartValue" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:TextBox runat="server" ID="tbStartValue" Text='<%# Eval("StartValue")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Конечные показания">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litEndValue" Text='<%# Eval("EndValue")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe" TargetControlID="tbEndValue" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:TextBox runat="server" ID="tbEndValue" Text='<%# Eval("EndValue")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Цена">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="specialprice" Text='<%# Eval("specialprice")%>'></asp:Literal>
                        </ItemTemplate>
                        <FooterTemplate>
                            <table style="font-size: 12px;">
                                <tr>
                                    <td><b>Итого: </b></td>
                                    <td align="right"><%# WithdrawalMoney().ToString("0.00")%> руб.</td>
                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource runat="server" ID="dsJournal2" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
                SelectCommand="RetrieveVodomersBySOrdersID"
                SelectCommandType="StoredProcedure"
                UpdateCommand="UpdateAlternativeOrderDetails2"
                UpdateCommandType="StoredProcedure"
                DeleteCommand="DeleteAlternativeOrderDetails"
                DeleteCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="SOrderID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="EndValue" Type="String" />
                    <asp:Parameter Name="StartValue" Type="String" />
                    <asp:Parameter Name="FactoryNumber" Type="String" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name ="ID" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>
            <!-- Editro mode MOFO end -->
        </div>
        <!-- Footer -->
        <div style="width: 750px; height: 25px; border: 0 solid black;">
            <div style="float: left">
                <asp:CheckBox runat="server" Text="Выдан" ID="cbSeld" TextAlign="Left" />
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBox runat="server" Text="Оплачен" ID="cbPaid" TextAlign="Left" />
                <asp:TextBox runat="server" ID="tbPaymentDay"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="tbPaymentDay"></ajaxToolkit:CalendarExtender>
            </div>
            <div style="float: right">
                <telerik:RadButton runat="server" Text="Сохранить" ID="lbSaveAll" OnClick="lbSaveAll_Click"></telerik:RadButton>
            </div>
        </div>
    </asp:Panel>

</telerik:RadAjaxPanel>
