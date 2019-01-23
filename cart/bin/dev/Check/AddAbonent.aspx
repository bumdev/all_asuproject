<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddAbonent.aspx.cs" Inherits="kipia_web_application.AddAbonent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
         <!--Тут мы показываем наши высплывающие окошки.-->
        <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true" AutoSize="True">
          </telerik:RadWindowManager>
    
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

    

    <!--Скрытые занчения-->
    <asp:HiddenField runat="server" ID="hfDiameter"/>
    <asp:HiddenField runat="server" ID="hfSeller"/>
    <asp:HiddenField runat="server" ID="hfModel"/>

<asp:HiddenField runat="server" ID="hfClientType" Value="Private" />
<asp:HiddenField runat="server" ID="hfOrder" Value="0" />
<asp:HiddenField runat="server" ID="hfVisibleVodomerSearch" />
    <div style="margin-left: 10px;">
 <asp:Panel runat="server" ID="Step1">
       
        <!--Загаловок формы Шаг 1 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 1</span>
        
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
                <div class="label">Телефон:</div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender7" TargetControlID="tbPhone" WatermarkText="+380 50 111 11 11" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbPhone" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <div class="label">Адрес:</div>

                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender4" TargetControlID="asd" WatermarkText="Артема 942" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="asd" Width="200px" Visible="False"></asp:TextBox>
                    
                      <telerik:RadAutoCompleteBox ID="tbAddress" runat="server" Width="200" DropDownWidth="200"
                DropDownHeight="200" DataSourceID="SqlDataSource1" DataTextField="Name" Filter="StartsWith" InputType="Text" AllowCustomEntry="True"  Delimiter=" "
                DataValueField="Name">
                          <TextSettings SelectionMode="Single"></TextSettings>
            </telerik:RadAutoCompleteBox>
                    
                     <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:ConnectionInfoDB4 %>"
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT name FROM Streets ORDER By name asc ">
    </asp:SqlDataSource>

                </div>
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
    </asp:Panel>
    <asp:Panel runat="server" ID="Step2" Visible="false">
    <!--Загаловок формы Шаг 2 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 2</span></div>
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
SelectCommand="RetrieveVodomerTypeByContext" SelectCommandType="StoredProcedure" OnSelecting="dsJournal_Selecting">
	<SelectParameters>
    <asp:ControlParameter ControlID="tbVodomerSearch" PropertyName="Text" Name="q" Type="String" />
	</SelectParameters>

</asp:SqlDataSource>
    </div>
    </asp:Panel>
        
         <div style="float:left; text-align:left; font-size:13px; position:relative; margin-bottom:20px;  width:700px; border:0 solid black; margin-top:5px;">
        <asp:Literal runat="server" ID="litAbonentInfo"></asp:Literal>
    </div>
        
    <div style="width: 100%;">
        
        <div style="float: left; width: 50%;">
 <!--Навигация-->
    <div>
        <asp:Literal runat="server" ID="litD"></asp:Literal>
        <asp:Literal runat="server" ID="litS"></asp:Literal>
        <asp:Literal runat="server" ID="litM"></asp:Literal>
    </div>
<telerik:RadGrid runat="server" ID="radgridD" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"  Font-Names="Arial Unicode MS" OnNeedDataSource="radgridD_NeedDataSource" OnItemCommand="radgridD_ItemCommand" >
             <MasterTableView DataKeyNames="Diameter" DataMember="Diameter" Width="100%" AllowAutomaticUpdates="true" Name="Diameter"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать" >
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="Diameter" UniqueName="Diameter"  HeaderText="Диаметр" SortExpression="Diameter" ></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
</telerik:RadGrid>
 <asp:SqlDataSource ID="dsD" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveVodomerDiameters" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
<!--------------------------------Производитель------------------------------------------>
<!--------------------------------Производитель------------------------------------------>
<!--------------------------------Производитель------------------------------------------>

<telerik:RadGrid runat="server" ID="radgridP" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"    Font-Names="Arial Unicode MS" OnNeedDataSource="radgridP_NeedDataSource" OnItemCommand="radgridP_ItemCommand" >
                <GroupingSettings CaseSensitive="false"></GroupingSettings>
                        <MasterTableView DataKeyNames="ID" DataMember="seller" Width="100%" AllowAutomaticUpdates="true" Name="seller" CommandItemDisplay="Top" AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать" >
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn  FilterControlWidth="150px" DataField="seller" ShowFilterIcon="False"  AutoPostBackOnFilter="true"  CurrentFilterFunction="Contains" HeaderText="Производитель" SortExpression="seller" ></telerik:GridBoundColumn>
            </Columns>
                  <CommandItemTemplate>
                            <telerik:RadButton runat="server" ID="butAddNewDevice" CommandName="BackToDiameter" Text="Вернуться к выбору диаметра"></telerik:RadButton>                      
                  </CommandItemTemplate>          
        </MasterTableView>
</telerik:RadGrid>


<asp:SqlDataSource ID="dsP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveSellersByDiameter" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="hfDiameter" Name="Diameter" DbType="Int32" PropertyName="Value"/>
    </SelectParameters>
</asp:SqlDataSource>
<!----------------***********************Модель****************************-->
<!----------------***********************Модель****************************-->
<!----------------***********************Модель****************************-->
<telerik:RadGrid runat="server" ID="radgridM" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"  Font-Names="Arial Unicode MS" OnNeedDataSource="radgridM_NeedDataSource" OnItemCommand="radgridM_ItemCommand" >
            <GroupingSettings CaseSensitive="false"></GroupingSettings>
                <MasterTableView DataKeyNames="ID" DataMember="Model" Width="100%" AllowAutomaticUpdates="true" Name="Model" CommandItemDisplay="Top"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать">
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn  FilterControlWidth="150px" DataField="conventional_signth"  ShowFilterIcon="False"  AutoPostBackOnFilter="true"  CurrentFilterFunction="Contains" HeaderText="Модель" SortExpression="conventional_signth" ></telerik:GridBoundColumn>
            </Columns>
                       <CommandItemTemplate>
                            <telerik:RadButton runat="server" ID="butAddNewDevice" CommandName="BackToSeller" Text="Вернуться к выбору производителя"></telerik:RadButton>                      
                  </CommandItemTemplate>    
        </MasterTableView>
</telerik:RadGrid>
            

<asp:SqlDataSource ID="dsM" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveVodomerTypeBySellerIdAndDiameter" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="hfSeller" Name="ID" DbType="Int32" PropertyName="Value"/>
        <asp:ControlParameter ControlID="hfDiameter" Name="Diameter" DbType="Int32" PropertyName="Value"/>
    </SelectParameters>
</asp:SqlDataSource>
                        
<!--***********************************Ввод показаний********************************-->
<!--***********************************Ввод показаний********************************-->
 <asp:Panel runat="server" ID="panValues" Visible="false">     
             <div class="FormItem">
          <div class="label">Заводской номер:&nbsp;&nbsp;&nbsp;&nbsp;Показания:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Год выпуска:</div>
            <div>
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmtbFactoryNumber" TargetControlID="tbFactoryNumber" WatermarkText="Заводской номер" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender6" TargetControlID="tbStartValue" WatermarkText="Показания" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>

            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe" TargetControlID="tbStartValue" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>

            <asp:TextBox runat="server" ID="tbFactoryNumber"  Width="120px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="tbStartValue" Width="100px"></asp:TextBox>
            <telerik:RadDropDownList runat="server" ID="radddlYear"></telerik:RadDropDownList>
            <asp:CheckBox runat="server" ID="cbNew" Text="Новый:" TextAlign="Left"/>
                <div style="float:right;"><asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" 
                onclick="lbSearch_Click"><span>Добавить</span></asp:LinkButton></div>
            </div>
            <div style="clear:both;"></div>
        </div>
         </asp:Panel>  
        </div>
        <div style="float: right;width: 50%;">
            <div style=" margin-left: 10px;">
            <div><span>Список добавленных водомеров</span></div>
            <div>
                   <asp:Repeater runat="server" ID="repVodomer" onitemcommand="repVodomer_ItemCommand">
                <ItemTemplate>
                    <div style=" background-color:#ffebbe; font-size:12px; width: 450px; height:20px; border:1px solid silver; padding:2px;  margin-top:10px; margin-right:15px;"><span><b>Номер:</b>&nbsp;<asp:Literal runat="server" ID="litFN" Text=<%# Eval("FactoryNumber")%>></asp:Literal>&nbsp;&nbsp;<b>Показания:</b>&nbsp;<asp:Literal runat="server" ID="litStartValue" Text=<%# Eval("VodomerPreview.StartValue")%>></asp:Literal> &nbsp;&nbsp;<b>D:</b>&nbsp;<asp:Literal runat="server" ID="lbDiameter" Text=<%# Eval("VodomerPreview.Diameter")%>></asp:Literal>&nbsp;&nbsp;<b>Год:</b>&nbsp;<asp:Literal runat="server" ID="Literal1" Text=<%# Eval("VodomerPreview.Year")%>></asp:Literal>&nbsp;&nbsp;&nbsp;<b><asp:Literal runat="server" ID="Literal2" Text=<%# Eval("VodomerPreview.New")%>></asp:Literal> </b></span> <asp:LinkButton runat="server" ID="lbDel" CommandName="Item"><div class="Close"></div></asp:LinkButton>                    
                    </div>
                </ItemTemplate>
            </asp:Repeater>
                <div style="margin-top: 10px; width: 450px;">
                <asp:LinkButton runat="server" ID="lbSaveAll" CssClass="FormButton" onclick="lbSaveAll_Click" Visible="False"><span>Сохранить всё</span></asp:LinkButton>
                    </div>
            </div>
                </div>
        </div>
    </div>    
    </asp:Panel>
</div>
        
        
        
        
        
        </telerik:RadAjaxPanel>
</asp:Content>
