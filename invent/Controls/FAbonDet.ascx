<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAbonDet.ascx.cs" Inherits="invent_app.Controls.FAbonDet" %>


<telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>

<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="up">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="up" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>

<telerik:RadAjaxPanel runat="server" ID="up">

    <asp:Literal runat="server" ID="litScript"></asp:Literal>


    <asp:Panel runat="server" ID="FAbonForm" Width="750px" Height="500px">
        <asp:HiddenField runat="server" ID="hfODID" Value="0" />
        <!--Загаловок формы  начало-->

        <!--Форма для физ. лиц-->
        <div style="width: 700px; min-height: 70px;">
            <div style="font-size: 14px; margin-top: 5px; float: left;">
                <b>
                    <asp:Literal runat="server" ID="litAbonentInfo"></asp:Literal></b>

            </div>
            <div style="float: right; border: 1px solid  black; padding: 10px;">


                <!--------------------Editor mode--------------------------->

                <asp:Panel runat="server" ID="punAdonentEditor" Visible="false">
                    <div class="FormItem">
                        <div class="label">Район:</div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlDistrict" Width="200px"></asp:DropDownList></div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="FormItem">
                        <div class="label">Номер по журналу:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbNumberJournalPhysical" Width="200px"></asp:TextBox></div>
                        <div style="clear:both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">Фамилия:</div>

                        <div>
                            <asp:TextBox runat="server" ID="tbClientSurname" Width="200px"></asp:TextBox></div>
                        <div style="clear: both;"></div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Имя:</div>

                        <div>
                            <asp:TextBox runat="server" ID="tbClientName" Width="200px"></asp:TextBox></div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="FormItem">
                        <div class="label">Отчество:</div>

                        <div>
                            <asp:TextBox runat="server" ID="tbClientLastName" Width="200px"></asp:TextBox></div>
                        <div style="clear: both;"></div>
                    </div>
                    

                    <div class="FormItem">
                        <div class="label">Телефон:</div>

                        <div>
                            <asp:TextBox runat="server" ID="tbPhone" Width="200px"></asp:TextBox></div>
                        <div style="clear: both;"></div>
                    </div>
                    <div class="FormItem">
                        <div class="label">Адрес:</div>

                        <div>
                            <asp:TextBox runat="server" ID="tbAddress" Width="200px"></asp:TextBox>


                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <div class="FormItem">
                        <div class="label">
                            <asp:CheckBox runat="server" ID="cbNotPay" Text="Без оплаты" TextAlign="Left" /></div>
                        <div></div>
                        <div style="clear: both;"></div>
                    </div>
                    <div class="FormItem">
                        <div class="label">
                            <asp:CheckBox runat="server" ID="cbRejectVodomer" Text="Брак" TextAlign="Left" />
                        </div>
                        <div></div>
                        <div style="clear: both;"></div>
                    </div>
                    
                   <div class="FormItem">
                        <div class="label"></div>

                        <div>
                            <telerik:RadButton runat="server" Text="Обновить" ID="radbutSaveAbonentInfo" OnClick="radbutSaveAbonentInfo_Click"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Удалить" ID="radbutDeleteAbonentInfo" OnClick="radbutDeleteAbonentInfo_Click"></telerik:RadButton>

                        </div>
                        <div style="clear: both;"></div>
                    </div>



                </asp:Panel>

                <!---------------------------------------------------->


            </div>
        </div>
        <div style="width: 500px; height: 25px; border: 0px solid black; float: left; padding-bottom: 75px;">


            <div style="margin-left: 10px; float:left; text-align: center; font-weight: bold; font-size: 14px; padding-top: 5px;">
                <asp:Button runat="server" ID="btActRub" Text="Акт(ДНР)" OnClick="btActRub_CLick" />
            </div>
            
            <div style="margin-left: 10px; float: left; text-align: center; font-weight: bold; font-size: 14px; padding-top: 5px;">
                <asp:Button runat="server" ID="btActCheckRub" Text="Акт + квитанция(ДНР)" OnClick="btActCheckRub_Click" />
            </div>
            
           

        </div>
        <div style="border: 1px solid black; width: 750px; height: 270px; overflow: auto;">
            <cuc:GridView DataSourceID="dsJournal" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True"
                ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None"
                CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="false"
                AllowSorting="true" OnRowUpdating="gvJournal_RowUpdating">
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
                            <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("EndValue").ToString()) %>'><span>Редакт.</span></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
                            <asp:LinkButton ID="btnUpdate" CommandName="Update" CssClass="FormButton" runat="server"><span>Обновить</span></asp:LinkButton>
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
                    <asp:TemplateField HeaderText ="Брак">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litDefectVodomer" Text='<%# Eval("DefectVodomer")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe2" TargetControlID="tbDefectVodomer" ValidChars="+-#&@*()" FilterMode="ValidChars" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:TextBox runat="server" ID="tbDefectVodomer" Text='<%# Eval("DefectVodomer")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Цена">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="price" Text='<%# Eval("price")%>'></asp:Literal>
                            <asp:Literal runat="server" ID="pricerub" Text='<%# Eval("pricerub")%>'></asp:Literal>
                        </ItemTemplate>
                        <FooterTemplate>
                            <table style="font-size: 12px;">
                                <tr>
                                    <td><b>Итого:</b></td>
                                    <td align="right"><%# GetSum().ToString("0.00")%> грн.</td>
                                    <td align="right"><%# GetSumRub().ToString("0.00")%> руб.</td>
                                </tr>
                                <tr>
                                    <td><b>НДС 20%:</b></td>
                                    <td align="right"><%# GetVAT().ToString("0.00")%> грн.</td>                                  
                                </tr>
                                <tr>
                                    <td><b>Всего:</b></td>
                                    <td align="right"><%# GetFinalSum().ToString("0.00")%> грн.</td>
                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
                SelectCommand="RetrieveVodomersByOrderID"
                SelectCommandType="StoredProcedure"
                UpdateCommand="UpdateOrderDetails"
                UpdateCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="OrderID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="EndValue" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            
            
            
            
            
            <!--Editor Mode MOFO-->

               <cuc:GridView DataSourceID="dsJournal2" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True"
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
                            <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server" ><span>Редакт.</span></asp:LinkButton>
                             <asp:LinkButton ID="btnDelete" CommandName="Delete" CssClass="FormButton" runat="server" ><span>Удал.</span></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
                            <asp:LinkButton ID="btnUpdate" CommandName="Update" CssClass="FormButton" runat="server"><span>Обновить</span></asp:LinkButton>
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
                    <asp:TemplateField HeaderText="Брак">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litDefectVodomer" Text='<%# Eval("DefectVodomer")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe2" TargetControlID="tbDefectVodomer" ValidChars="+-#&@*()" FilterMode="ValidChars" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:TextBox runat="server" ID="tbDefectVodomer" Text='<%# Eval("DefectVodomer")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Цена">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="price" Text='<%# Eval("price")%>'></asp:Literal>
                            <asp:Literal runat="server" ID="pricerub" Text='<%# Eval("pricerub")%>'></asp:Literal>
                        </ItemTemplate>
                        <FooterTemplate>
                            <table style="font-size: 12px;">
                                <tr>
                                    <td><b>Итого:</b></td>
                                    <td align="right"><%# GetSum().ToString("0.00")%> грн.</td>
                                    <td align="right"><%# GetSumRub().ToString("0.00")%> руб. </td>
                                </tr>
                                <tr>
                                    <td><b>НДС 20%:</b></td>
                                    <td align="right"><%# GetVAT().ToString("0.00")%> грн.</td>
                                </tr>
                                <tr>
                                    <td><b>Всего:</b></td>
                                    <td align="right"><%# GetFinalSum().ToString("0.00")%> грн.</td>
                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource ID="dsJournal2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
                SelectCommand="RetrieveVodomersByOrderID"
                SelectCommandType="StoredProcedure"
                UpdateCommand="UpdateOrderDetails2"
                UpdateCommandType="StoredProcedure"
                DeleteCommand="DeleteOrderDetails"
                DeleteCommandType="StoredProcedure"
                >
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="OrderID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="EndValue" Type="String" />
                    <asp:Parameter Name="StartValue" Type="String" />
                    <asp:Parameter Name="FactoryNumber" Type="String" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>
            
                 <!--Editor Mode MOFO end-->
        </div>
        <!--Footer-->
        <div style="width: 750px; height: 25px; border: 0 solid black;">
            <div style="float: left">
                <asp:CheckBox runat="server" CssClass="myCheckBox" Text="Брак" ID="cbDefectVodomer" TextAlign="Left" />
                <asp:CheckBox runat="server" CssClass="myCheckBox" Text="Выдан" ID="cbSeld" TextAlign="Left" />
                &nbsp;&nbsp;&nbsp;
       <asp:CheckBox ID="cbPaid" runat="server" CssClass="myCheckBox" Text="Оплачен" TextAlign="Left" />
                <asp:TextBox runat="server" ID="tbPaymentDay"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="tbPaymentDay"></ajaxToolkit:CalendarExtender>
            </div>
            <div style="float: right;">
                <telerik:RadButton runat="server" ID="lbSaveAll" Text="Сохранить" OnClick="lbSaveAll_Click"></telerik:RadButton>
            </div>
        </div>
    </asp:Panel>

</telerik:RadAjaxPanel>
