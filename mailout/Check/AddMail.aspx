<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddMail.aspx.cs" Inherits="outcomming_mail.AddMail" %>
<%@ Import Namespace="NPOI.SS.Formula.Functions" %>
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
    <asp:HiddenField runat="server" ID="hfDepartamentName"/>
    <asp:HiddenField runat="server" ID="hfResponContractor"/>
    
    

<asp:HiddenField runat="server" ID="hfClientType" Value="Out" />
<asp:HiddenField runat="server" ID="hfOrder" Value="0" />
<asp:HiddenField runat="server" ID="hfVisibleVodomerSearch" />
<asp:HiddenField runat="server" ID="hfResponContractSearch" />
    <div style="margin-left: 10px;">
 <asp:Panel runat="server" ID="Step1">
       
        <!--Загаловок формы Шаг 1 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Заполнение почты</span>
        
        </div>
        <!--Загаловок формы Шаг 1 конец-->
        <div style="width:290px; height:20px; margin-top:20px; margin-bottom:10px; float:left;"><span>Исходящая почта:</span>&nbsp;<asp:LinkButton 
                           runat="server" ID="lbClient" onclick="lbClient_Click"></asp:LinkButton></div>
                           <div style="clear:both;"></div>
       
        <!--Форма для исходящей почты -->
     <asp:Panel runat="server" ID="panMailOut" Visible="false">
         <div class="FormItem">
             <div class="label">
                <div style="margin-top: 50px">
                    <span>* - обязательные поля</span>
                </div>
             </div>
         </div>
         <div class="FormItem">
             <div class="label">Рег. номер*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender21" TargetControlID="tbRegNumber" WatermarkCssClass="WaterText" WatermarkText="01"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="float: left;">
                 <asp:TextBox runat="server" ID="tbRegNumber" Width="200px"></asp:TextBox>                                           
             </div>
             <div style="float:left; margin-left: 45px;">
                     <asp:LinkButton runat="server" ID="MailOutAdd" CssClass="FormButton" OnClick="MailOutAdd_Click"><span>Далеe </span></asp:LinkButton>  
                 </div>             
             <div style="clear: both;"></div>                         
         </div>
         <div class="FormItem">
             <div class="label">Лицевой счет: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender24" TargetControlID="tbPersonalAccount" WatermarkCssClass="WaterText" WatermarkText="12/56"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbPersonalAccount" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         
         <div class="FormItem">
             <div class="label">Кому(тип адресата)*: </div>
             <div><asp:DropDownList runat="server" ID="ddlAdresatType" Width="200px"></asp:DropDownList></div>
             <div style="clear: both; float: left;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Кому(ФИО)*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender22" TargetControlID="tbWhom" WatermarkCssClass="WaterText" WatermarkText="Иванов И.И."></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWhom" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender23" TargetControlID="tbAbout" WatermarkCssClass="WaterText" WatermarkText="О чем"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" TextMode="MultiLine" Wrap="true" ID="tbAbout" Width="200px" Height="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем ответ:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender56" TargetControlID="tbAnswerAbout" WatermarkCssClass="WaterText" WatermarkText="Очем (ответ)"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" TextMode="MultiLine" Wrap="true" ID="tbAnswerAbout" Width="200px" Height="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Дата ответа: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender70" TargetControlID="tbAnswerDate" WatermarkCssClass="WaterText" WatermarkText="01.01.2017"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbAnswerDate" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         
         
         
         
     </asp:Panel>
     
     <asp:Panel runat="server" ID="panPhysicalMail" Visible="false">
         <div class="FormItem">
             <div class="label">
                <div style="margin-top: 50px">
                    <span>* - обязательные поля</span>
                </div>
             </div>
         </div>
         <div class="FormItem">
             <div class="label">Рег. номер*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender59" TargetControlID="tbPhysicalRegNumber" WatermarkCssClass="WaterText" WatermarkText="01/23"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="float: left;">
                 <asp:TextBox runat="server" ID="tbPhysicalRegNumber" Width="200px"></asp:TextBox>
             </div>
             <div style="float: left; margin-left: 45px;">
                 <asp:LinkButton runat="server" ID="PhysicalMailAdd" CssClass="FormButton" OnClick="PhysicalMailAdd_OnClick"><span>Далее </span></asp:LinkButton>
             </div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Номер договора: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender57" TargetControlID="tbPhysicalContractNumber" WatermarkCssClass="WaterText" WatermarkText="123"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbPhysicalContractNumber" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Кому(тип адресата)*: </div>
             <div><asp:DropDownList runat="server" ID="ddlPhysicalAdresatType" Width="200px"></asp:DropDownList></div>
             <div style="clear: both; float: left;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Отправитель: </div>
             <div><asp:DropDownList runat="server" ID="ddlSender" Width="200px"></asp:DropDownList></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Кому(название организации)*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender60" TargetControlID="tbPhysicalWhom" WatermarkCssClass="WaterText" WatermarkText="ООО Название"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbPhysicalWhom" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Примечание(кому ФИО):</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender64" TargetControlID="tbPhysicalNotation" WatermarkCssClass="WaterText" WatermarkText="ФИО"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbPhysicalNotation" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender61" TargetControlID="tbPhysicalAbout" WatermarkCssClass="WaterText" WatermarkText="О чем"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" TextMode="MultiLine" Wrap="true" ID="tbPhysicalAbout" Width="200px" Height="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем ответ: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWaterMarkExtender62" TargetControlID="tbPhysicalAboutAnswer" WatermarkCssClass="WaterText" WatermarkText="О чем ответ"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" TextMode="MultiLine" Wrap="true" ID="tbPhysicalAboutAnswer" Width="200px" Height="200px"></asp:TextBox></div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Дата ответа: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender63" TargetControlID="tbPhysicalAnswerDate" WatermarkCssClass="WaterText" WatermarkText="01.01.2017"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbPhysicalAnswerDate" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
     </asp:Panel>
     
     <asp:Panel runat="server"  ID="panOtherMail" Visible="false">
         <div class="FormItem">
             <div class="label">
                <div style="margin-top: 50px">
                    <span>* - обязательные поля</span>
                </div>
             </div>
         </div>
         <div class="FormItem">
             <div class="label">Рег. номер*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender90" TargetControlID="tbOtherRegNumber" WatermarkCssClass="WaterText" WatermarkText="01/23"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="float: left;">
                 <asp:TextBox runat="server" ID="tbOtherRegNumber" Width="200px"></asp:TextBox>
             </div>
             <div style="float: left; margin-left: 45px;">
                 <asp:LinkButton runat="server" ID="OtherMailAdd" CssClass="FormButton" OnClick="OtherMailAdd_Click"><span>Далее </span></asp:LinkButton>
             </div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Кому(тип адресата)*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender91" TargetControlID="tbOtherAdresat" WatermarkCssClass="WaterText" WatermarkText="МО"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" TextMode="MultiLine" Wrap="true" ID="tbOtherAdresat" Width="200px" Height="100px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Кому(название организации/ФИО)*: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender92" TargetControlID="tbOtherWhom" WatermarkCssClass="WaterText" WatermarkText="Петров И.И."></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbOtherWhom" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Примечание(кому ФИО):</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender96" TargetControlID="tbOtherNotation" WatermarkCssClass="WaterText" WatermarkText="Примечание"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbOtherNotation" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем*:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender93" TargetControlID="tbOtherAbout" WatermarkCssClass="WaterText" WatermarkText="О чем"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" ID="tbOtherAbout" TextMode="MultiLine" Wrap="true" Width="200px" Height="200px"></asp:TextBox></div>
             <div style="clear:both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Дата ответа:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender94" TargetControlID="tbOtherAnswerDate" WatermarkCssClass="WaterText" WatermarkText="01.01.2015"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" ID="tbOtherAnswerDate" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем ответ:</div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender95" TargetControlID="tbOtherAnswerAbout" WatermarkCssClass="WaterText" WatermarkText="О чем ответ"></ajaxToolkit:TextBoxWatermarkExtender>
             <div style="text-align: left;"><asp:TextBox runat="server" TextMode="MultiLine" Wrap="true" ID="tbOtherAnswerAbout" Width="200px" Height="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
     </asp:Panel>

        
     

    </asp:Panel>
    <asp:Panel runat="server" ID="Step2" Visible="false">
    <!--Загаловок формы Шаг 2 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Выбор ответственного исполнителя</span></div>
    <!--Загаловок формы Шаг 2 конец-->
    <!--Информация по почте-->
    <asp:Panel runat="server" ID="panResponContractSearch" Visible="false" CssClass="PanResponContractSearch" DefaultButton="lbResponContractSearch">
        <div style="height:30px;width:690px;">
            <div style="float:left; height:30px;"><asp:TextBox runat="server" id="tbResponContractSearch" Width="500px"></asp:TextBox></div>
            <div style="float:right;">
                <asp:LinkButton runat="server" ID="lbResponContractSearch" CssClass="FormButton" OnClick="lbResponContractSearch_Click">
                    <span>Найти</span>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbCancel" CssClass="FormButton" OnClick="lbCancel_Click">
                    <span>Отменить</span>
                </asp:LinkButton>
            </div>
        </div>
        <div style="height:325px; width:690px; overflow: auto;">
            <cuc:GridView DataSourceID="dsMailJournal" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="true"
                ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="none"
                CssClass="GridView MaxWidth" ID="gvMailJournal" AutoGenerateColumns="false"
                AllowSorting="true" onrowcommand="gvMailJournal_RowCommand"
                onselectedindexchanged="gvMailJournal_SelectedChanged">
                <AlternatingRowStyle CssClass="GridViewRow" />
                <RowStyle CssClass="GridViewRow" />
                <SortAscendingStyle CssClass="Sorted Ascending" />
                <SortDescendingStyle CssClass="Sorted Descending" />
                <HeaderStyle CssClass="GridViewHeader" />
                <PagerStyle CssClass="GridViewHeader" />
                <EmptyDataTemplate>
                    Нет данных
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbSelect" CssClass="FormButton" CommandName="Select">
                                <span>Выбрать</span>
                            </asp:LinkButton>
                            <asp:HiddenField runat="server" ID="hfID" Value=<%# Eval("ID")%> />
                            <asp:HiddenField runat="server" ID="hfDepartamentName" Value=<%# Eval("DepartamentID")%> />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ответсвенный исполнитель">
                        <ItemTemplate>
                            <b><span><%# Eval("DepartamentName")%></span></b><br/>
                            <span><%# Eval("ResponName")%></span><br/>
                            <span><%# Eval("Position")%></span><br/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="№" DataField="CodeDepartament" SortExpression="CodeDepartament" />
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource ID="dsMailJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo%>"
                SelectCommand="RetrieveResponContractByContext" SelectCommandType="StoredProcedure" OnSelecting="dsMailJournal_Selecting">
                <SelectParameters>
                    <asp:ControlParameter ControlID="tbResponContractSearch" PropertyName="Text" Name="q" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </asp:Panel>
        
         <div style="float:left; text-align:left; font-size:13px; position:relative; margin-bottom:20px;  width:700px; border:0 solid black; margin-top:5px;">
        <asp:Literal runat="server" ID="litMailInfo"></asp:Literal>
    </div>
        
    <div style="width: 100%;">
        
        <div style="float: left; width: 50%;">
 <!--Навигация-->
    <div>
        <asp:Literal runat="server" ID="litD"></asp:Literal>
        <asp:Literal runat="server" ID="litRC"></asp:Literal>
       <!-- <asp:Literal runat="server" ID="litM"></asp:Literal> -->
    </div>
<telerik:RadGrid runat="server" ID="radgridD" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"  Font-Names="Arial Unicode MS" OnNeedDataSource="radgridD_NeedDataSource" OnItemCommand="radgridD_ItemCommand" >
             <MasterTableView DataKeyNames="ID" DataMember="DepartamentName" Width="100%" AllowAutomaticUpdates="true" Name="DepartamentName"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать" >
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="DepartamentName" UniqueName="DepartamentName"  HeaderText="Отдел" SortExpression="DepartamentName" ></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
</telerik:RadGrid>
 <asp:SqlDataSource ID="dsD" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveResponContractDepartament" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
<!--------------------------------Ответственный исполнитель------------------------------------------>
<!--------------------------------Ответственный исполнитель------------------------------------------>
<!--------------------------------Ответственный исполнитель------------------------------------------>

<telerik:RadGrid runat="server" ID="radgridP" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"  Font-Names="Arial Unicode MS" OnNeedDataSource="radgridP_NeedDataSource" OnItemCommand="radgridP_ItemCommand" >
            <GroupingSettings CaseSensitive="false"></GroupingSettings>
                <MasterTableView DataKeyNames="ID" DataMember="ResponName" Width="100%" AllowAutomaticUpdates="true" Name="ResponName" CommandItemDisplay="Top"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать">
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn  FilterControlWidth="150px" DataField="ResponName"  ShowFilterIcon="False"  AutoPostBackOnFilter="true"  CurrentFilterFunction="Contains" HeaderText="Ответственный исполнитель" SortExpression="ResponName" ></telerik:GridBoundColumn>
            </Columns>
                       <CommandItemTemplate>
                            <telerik:RadButton runat="server" ID="butAddNewDevice" CommandName="BackToDepartamentName" Text="Вернуться к выбору отдела"></telerik:RadButton>                      
                  </CommandItemTemplate>    
        </MasterTableView>
</telerik:RadGrid>
            

<asp:SqlDataSource ID="dsP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveResponNameByDeprtamentID" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="hfDepartamentName" Name="ID" DbType="Int32" PropertyName="Value"/>
      
    </SelectParameters>
</asp:SqlDataSource>
                        
<!--***********************************Подтвердить добавление********************************-->
<!--***********************************Подтвердить добавление********************************-->
        <asp:Panel runat="server" ID="panValues" Visible="false">
            <div class="FormItem">
                <div class="label">Дата регистрации:</div>
                <div>
                    <asp:TextBox runat="server" ID="tbDateRegistration"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender89" TargetControlID="tbDateRegistration" WatermarkText="Дата регистрации" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>

                    <div style="float: right;">
                        <asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" OnClick="lbSearch_Click">
                            <span>Добавить</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
            </div>        
       </asp:Panel>
             
        </div>
           <div style="float: right; width: 50%;">
                <div style="margin-left: 10px;">
                    <div>
                        <span>Выбранный ответственный исполнитель:</span>
                    </div>
                    <div>
                        <asp:Repeater runat="server" ID="repResponContract" OnItemCommand="repResponContract_ItemCommand">
                            <ItemTemplate>
                                <div style="font-size:12px; width: 450px; height:40px; border:1px solid silver; padding:2px;  margin-top:10px; margin-right:15px;">
                                    <span><b>Ответственный исполнитель:</b>&nbsp;
                                        <asp:Literal runat="server" ID="litResponName" Text=<%# Eval("ResponContractPreview.ResponName")%>></asp:Literal>
                                        <br><b>Отдел:</b>&nbsp;
                                        <asp:Literal runat="server" ID="litDepartamentName" Text=<%# Eval("ResponContractPreview.DepartamentName")%>></asp:Literal>
                                    </span>
                                    
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div style="margin-top: 10px; width: 450px;">
                            <asp:LinkButton runat="server" ID="lbSaveAll" CssClass="FormButton" OnClick="lbSaveAll_Click" Visible="false">
                            <span>Подтвердить/сохранить</span>
                        </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div> 
    </div>    
    </asp:Panel>
</div>
        
        
        
        
        
        </telerik:RadAjaxPanel>
</asp:Content>
