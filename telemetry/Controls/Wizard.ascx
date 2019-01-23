<%@ Control Language="C#" AutoEventWireup="true" Inherits="leak_detectors.Controls.Controls_Wizard" Codebehind="Wizard.ascx.cs" %>




    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        
         <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="lbClient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Step1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbFindOKPO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Step1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="WithdrawalAbonentAdd">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="Step1" LoadingPanelID="RadAjaxLoadingPanel1" />
                 </UpdatedControls>
             </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="lbAbonentAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Step2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
<asp:HiddenField runat="server" ID="hfClientType" Value="Private" />
<asp:HiddenField runat="server" ID="hfOrder" Value="0" />
<asp:HiddenField runat="server" ID="hfVisibleVodomerSearch" />
 <asp:Panel runat="server" ID="Step1">
       
        <!--Загаловок формы Шаг 1 начало-->
        <div style="width:700px; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 1</span>
        
        </div>
        <!--Загаловок формы Шаг 1 конец-->
        <div style="width:290px; height:20px; margin-top:20px; margin-bottom:10px; float:left;"><span>Абонент:</span>&nbsp;<asp:LinkButton 
                           runat="server" ID="lbClient" onclick="lbClient_Click"></asp:LinkButton></div>
                           <div style="clear:both;"></div>
       
        <!--Форма для физ. лиц-->
        <asp:Panel runat="server" ID="panPerson" Visible="false">
             <div class="FormItem">
                <div class="label">Район:</div>
                <div><asp:DropDownList runat="server" ID="ddlDistrict" Width="200px" ></asp:DropDownList></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <div class="label">Фамилия:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender2" TargetControlID="tbClientSurname" WatermarkText="Иванов" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbClientSurname" Width="200px" ></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <div class="label">Имя:</div>
                <ajaxToolkit:TextBoxWatermarkExtender  runat="server" ID="TextBoxWatermarkExtender1" TargetControlID="tbClientName" WatermarkText="Иван" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbClientName" Width="200px"  ></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            
            <div class="FormItem">
                <div class="label">Отчество:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender3" TargetControlID="tbClientLastName" WatermarkText="Иванович" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbClientLastName" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <div class="label">Номер по журналу:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender13" TargetControlID="tbNumberJournal" WatermarkText="11245" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbNumberJournal" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <div class="label">Телефон:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender7" TargetControlID="tbPhone" WatermarkText="+380 50 111 11 11" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbPhone" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <div class="label">Адрес:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender4" TargetControlID="tbAddress" WatermarkText="Артема 942" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbAddress" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
             <div class="FormItem">
                <div class="label"><asp:CheckBox runat="server" ID="cbNotPay" Text="Без оплаты" TextAlign="Left"  /></div>                
                <div></div>
                <div style="clear:both;"></div>
            </div>
             <div class="FormItem">
        <asp:LinkButton runat="server" ID="lbAbonentAdd" CssClass="FormButton" 
                 onclick="lbAbonentAdd_Click"><span>Далее</span></asp:LinkButton>
        </div>
          <div style="clear:both;"></div>
        </asp:Panel>
        
        <!--Форма для юр.лиц-->
        <asp:Panel runat="server" ID="panCorporate" Visible="false" DefaultButton="lbFindOKPO">
            <div class="FormItem">
            <div style="float:left; padding-top:4px;"><asp:TextBox runat="server" ID="tbCorporateOKPO" Width="200px"></asp:TextBox></div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender5" TargetControlID="tbCorporateOKPO" WatermarkText="ОКПО организации" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div style="float:left; padding-left:10px;"><asp:LinkButton runat="server" ID="lbFindOKPO" CssClass="FormButton" onclick="lbFindOKPO_Click"><span>Искать</span></asp:LinkButton></div>       
            </div>
          <div style="clear:both;"></div>
           <div style="width:700px;height:300px; border:0px solid red; overflow:auto; margin-top:5px;">
           <asp:Literal runat="server" ID="litSearch"></asp:Literal>
           <asp:Repeater runat="server" ID="repCorporate" 
                   onitemcommand="repCorporate_ItemCommand">                   
                <ItemTemplate>               
                    <asp:LinkButton runat="server" ID="lbCorporate" CssClass="CorporateList" CommandName="GO">
                    <div style="width:675px; padding:5px; background-color:#dddddd; border:1px solid black; margin-top:5px; border-radius:6px; -webkit-border-radius:6px; -moz-border-radius:6px; -khtml-border-radius:6px;">
                        <span style="font-size:14px; font-weight:bold;"> <asp:Literal runat="server" ID="litID_DOC" Text='<%# Eval("ID_DOC")%>'></asp:Literal> &nbsp;&nbsp;&nbsp;<asp:Literal runat="server" ID="litFULL_NAME" Text=<%# Eval("FULL_NAME")%>></asp:Literal></span>&nbsp;<span style="font-size:12px;"> (<asp:Literal runat="server" ID="litBUDZHET" Text=<%# Eval("BUDZHET")%>></asp:Literal>)</span><br/>
                        <span style="font-size:12px;"><b>МФО:&nbsp;</b><asp:Literal runat="server" ID="litMFO" Text=<%# Eval("MFO")%>></asp:Literal>&nbsp;&nbsp;<b>Р/С:&nbsp;</b><asp:Literal runat="server" ID="litR_S" Text=<%# Eval("R_S")%>></asp:Literal> &nbsp;&nbsp;<b>Договор:</b>&nbsp;<asp:Literal runat="server" ID="litDOGOVOR" Text=<%# Eval("DOGOVOR")%>></asp:Literal></span><br/>
                        <span style="font-size:12px;"><b>Банк:</b>&nbsp;<asp:Literal runat="server" ID="litBANK" Text=<%# Eval("BANK")%>></asp:Literal></span><br/>
                         <span style="font-size:12px;"><b>ОКПО:</b>&nbsp;<asp:Literal runat="server" ID="litOKPO" Text=<%# Eval("OKPO")%>></asp:Literal></span><br/>
                        <span style="font-size:12px;"><b>Адрес:</b>&nbsp;<asp:Literal runat="server" ID="litUR_ADDRESS" Text=<%# Eval("UR_ADDRESS")%>></asp:Literal></span>    
                        <span style="font-size:12px;"><b>ИНН:</b>&nbsp;<asp:Literal runat="server" ID="litInn" Text=<%# Eval("inn")%>></asp:Literal></span>  
                        <span style="font-size:12px;"><b>Сд. нл пл:</b>&nbsp;<asp:Literal runat="server" ID="litSvidetelstvo" Text=<%# Eval("id_svid")%>></asp:Literal></span>  
                        <span style="font-size:12px;"><b>Контакт:</b>&nbsp;<asp:Literal runat="server" ID="litContact" Text=<%# Eval("kontact")%>></asp:Literal></span>  
                        <span style="font-size:12px;"><b>В лице:</b>&nbsp;<asp:Literal runat="server" ID="litFace" Text=<%# Eval("v_litse")%>></asp:Literal></span>  
                        <span style="font-size:12px;"><b>Основание:</b>&nbsp;<asp:Literal runat="server" ID="litCause" Text=<%# Eval("na_osnovanii")%>></asp:Literal></span>   
                        <span style="font-size:12px;"><b>Телефон:</b>&nbsp;<asp:Literal runat="server" ID="litPhone" Text=<%# Eval("contact_phone")%>></asp:Literal></span>    
                         <span style="font-size:12px;"><b>Тип налогообложения:</b>&nbsp;<asp:Literal runat="server" ID="litTaxType" Text=<%# Eval("name_no")%>></asp:Literal></span>               
                    </div>
                    </asp:LinkButton>
                </ItemTemplate>
           </asp:Repeater>
            <asp:DataGrid runat="server" ID="dgCorporate"></asp:DataGrid>
           </div>
        </asp:Panel>
     <!-- Форма для добавления абонента снятие/установки -->
     
     <asp:Panel runat="server" ID="panSpecial" Visible="false">
           <div class="FormItem">
               <div class="label">Район:</div>
               <div><asp:DropDownList runat="server" runat="server" ID="ddDistrictWithdrawal" Width="200px"></asp:DropDownList></div>
               <div style="clear:both;"></div>
           </div>
         <div class="FormItem">
             <div class="label">Номер по журналу:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender25" TargetControlID="tbWithdrawalNumberJournal" WatermarkCssClass="WaterText" WatermarkText="34562"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWithdrawalNumberJournal" Width="200px"></asp:TextBox></div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Фамилия:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender26" TargetControlID="tbWithdrawalFirstName" WatermarkCssClass="WaterText" WatermarkText="Иванов"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWithdrawalFirstName" Width="200px"></asp:TextBox></div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Имя:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender27" TargetControlID="tbWithdrawalSurname" WatermarkCssClass="WaterText" WatermarkText="Иван"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWithdrawalSurname" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Отчество:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender28" TargetControlID="tbWithdrawaLastName" WatermarkCssClass="WaterText" WatermarkText="Иванович"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWithdrawaLastName" Width="200px"></asp:TextBox></div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Телефон:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender29" TargetControlID="tbWithdrawalPhone" WatermarkCssClass="WaterText" WatermarkText="050-111-11-11"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWithdrawalPhone" Width="200px"></asp:TextBox></div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
                <div class="label">Адрес:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender31" TargetControlID="tbSpecialAddress" WatermarkText="Артема 942" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbSpecialAddress" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
         <div class="FormItem">
             <asp:LinkButton runat="server" ID="WithdrawalAbonentAdd" CssClass="FormButton" OnClick="WithdrawalAbonentAdd_Click">
                 <span>Далее</span>
             </asp:LinkButton>
         </div>
         <div style="clear:both;"></div>
     </asp:Panel>
       
    </asp:Panel>
    <asp:Panel runat="server" ID="Step2" Visible="false">
    <!--Загаловок формы Шаг 2 начало-->
        <div style="width:700px; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 2</span></div>
    <!--Загаловок формы Шаг 2 конец-->
    <!--Информация по абоненту-->
    <asp:Panel runat="server" ID="panVodomerSearch" Visible="false" CssClass="PanVodomerSearch" DefaultButton="lbVodomerSearch">

    <div style="height:30px;width:690px;">
    <div style="float:left; height:30px; " ><asp:TextBox runat="server" id="tbVodomerSearch" Width="500px"></asp:TextBox></div>
    <div style="float:right; ">
    <asp:LinkButton runat="server" ID="lbVodomerSearch" CssClass="FormButton" onclick="lbVodomerSearch_Click"><span>Найти</span></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbCancel" CssClass="FormButton" onclick="lbCancel_Click" ><span>Отменить</span></asp:LinkButton>
    </div>
    </div>
    <div style="height:325px;width:690px; overflow:auto;">
        <cuc:GridView  DataSourceID="dsJournal" AllowPaging="true" PageSize="50" 
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True" 
        ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow" 
        EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" 
        CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="false" 
        AllowSorting="true" onrowcommand="gvJournal_RowCommand" 
            onselectedindexchanged="gvJournal_SelectedIndexChanged" >
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
        <asp:TemplateField>
            <ItemTemplate>
            <asp:LinkButton runat="server" ID="lbSelect" CssClass="FormButton" CommandName="Select"><span>Выбрать</span></asp:LinkButton>
                <asp:HiddenField runat="server" ID="hfID" Value=<%# Eval("ID")%> />
                <asp:HiddenField runat="server" ID="hfSellerID" Value=<%# Eval("id_seller")%> />
            </ItemTemplate>
        </asp:TemplateField>
          
        <asp:TemplateField HeaderText="Водомер">
            <ItemTemplate>
                <b><span><%# Eval("seller")%> </span></b><br/>
                <span><%# Eval("conventional_signth")%></span><br/>
                <span><%# Eval("description")%></span><br/>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="D" DataField="diameter" SortExpression="diameter" /> 
	
	</Columns>
</cuc:GridView>
<asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
 SelectCommandType="StoredProcedure" OnSelecting="dsJournal_Selecting">
	<SelectParameters>
    <asp:ControlParameter ControlID="tbVodomerSearch" PropertyName="Text" Name="q" Type="String" />
	</SelectParameters>

</asp:SqlDataSource>
    </div>
    </asp:Panel>
    <div style="float:left; width:400px; border:0px solid black;">
    <div style="float:left; text-align:left; font-size:13px; position:relative; width:700px; border:0 solid black; margin-top:5px;">
        <asp:Literal runat="server" ID="litAbonentInfo"></asp:Literal>
    </div>
     
     <div style="/*float:right;*/ margin-top:15px; width:410px;">     
     <div style=""><span style="font-weight:bold; font-size:14px;">Водомер:</span></div>
        <div class="FormItem">
            <div class="label">Диаметр:</div>
            <div>
               <asp:DropDownList runat="server" ID="ddlDiameter1" AutoPostBack="True" 
                    onselectedindexchanged="ddlDiameter1_SelectedIndexChanged"></asp:DropDownList>
                    <div style="float:right"><asp:LinkButton runat="server" ID="lbVS"  
                            CssClass="FormButton" onclick="lbVS_Click"><span>Поиск</span></asp:LinkButton></div>
            </div>
            <div style="clear:both;"></div>
        </div> 
        <div class="FormItem">
            <div class="label">Производитель:</div>
            <div><asp:DropDownList runat="server" ID="ddlSeller" Width="400px" 
                    onselectedindexchanged="ddlSeller_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
            <div style="clear:both;"></div>
        </div>
        <div class="FormItem">
            <div class="label">Модель:</div>
            <div><asp:DropDownList runat="server" ID="ddlModel" Width="400px" ></asp:DropDownList></div>
            <div style="clear:both;"></div>
        </div>
          <div class="FormItem">
          <div class="label">Заводской номер:&nbsp;&nbsp;&nbsp;&nbsp;Показания:</div>
            <div>
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmtbFactoryNumber" TargetControlID="tbFactoryNumber" WatermarkText="Заводской номер" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender6" TargetControlID="tbStartValue" WatermarkText="Показания" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>

            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe" TargetControlID="tbStartValue" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>

            <asp:TextBox runat="server" ID="tbFactoryNumber"  Width="120px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="tbStartValue" Width="100px"></asp:TextBox>
            <div style="float:right;"><asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" 
                onclick="lbSearch_Click"><span>Добавить</span></asp:LinkButton></div>
            </div>
            <div style="clear:both;"></div>
        </div>
        <div class="FormItem">
        
        </div>
        <div style="clear:both;"></div>
       </div>     
       </div>  
        <div style="width:700px;height:200px; border-top:1px solid black; overflow:auto; position:relative; z-index:20;">
            <asp:Repeater runat="server" ID="repVodomer" onitemcommand="repVodomer_ItemCommand">
                <ItemTemplate>
                    <div style=" background-color:#ffebbe; font-size:12px; float:left; height:20px; border:1px solid silver; padding:2px;  margin-top:10px; margin-right:15px;"><span><b>Номер:</b>&nbsp;<asp:Literal runat="server" ID="litFN" Text=<%# Eval("FactoryNumber")%>></asp:Literal>&nbsp;&nbsp;<b>Показания:</b>&nbsp;<asp:Literal runat="server" ID="litStartValue" Text=<%# Eval("VodomerPreview.StartValue")%>></asp:Literal> &nbsp;&nbsp;<b>D:</b>&nbsp;<asp:Literal runat="server" ID="lbDiameter" Text=<%# Eval("VodomerPreview.Diameter")%>></asp:Literal></span> <asp:LinkButton runat="server" ID="lbDel" CommandName="Item"><div class="Close"></div></asp:LinkButton>                    
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:LinkButton runat="server" ID="lbSaveAll" CssClass="FormButton" onclick="lbSaveAll_Click"><span>Сохранить всё</span></asp:LinkButton>
    </asp:Panel>
     