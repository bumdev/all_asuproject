<%@ Control Language="C#" AutoEventWireup="true" Inherits="outcomming_mail.Wizard" Codebehind="Wizard.ascx.cs" %>


<telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true" AutoSize="True">
          </telerik:RadWindowManager>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        
         <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="lbClient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Step1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
             <telerik:AjaxSetting AjaxControlID="lbMailAdd">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="Step1" LoadingPanelID="RadAjaxLoadingPanel1" />
                 </UpdatedControls>
             </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="lbMailOutAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Step2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
<asp:HiddenField runat="server" ID="hfDepartamentName"/>
<asp:HiddenField runat="server" ID="hfResponContractor"/>
<asp:HiddenField runat="server" ID="hfClientType" Value="Private" />
<asp:HiddenField runat="server" ID="hfOrder" Value="0" />
<asp:HiddenField runat="server" ID="hfVisibleVodomerSearch" />
 <asp:Panel runat="server" ID="Step1">
       
        <!--Загаловок формы Шаг 1 начало-->
        <div style="width:700px; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 1</span>
        
        </div>
        <!--Загаловок формы Шаг 1 конец-->
        <div style="width:290px; height:20px; margin-top:20px; margin-bottom:10px; float:left;"><span>Почта:</span>&nbsp;<asp:LinkButton 
                           runat="server" ID="lbClient" onclick="lbClient_Click"></asp:LinkButton></div>
                           <div style="clear:both;"></div>
       
        <!--Форма для физ. лиц-->
        <asp:Panel runat="server" ID="panMailOut" Visible="false">
         <div class="FormItem">
             <div class="label">Рег. номер: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender21" TargetControlID="tbRegNumber" WatermarkCssClass="WaterText" WatermarkText="01"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbRegNumber" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">В чьем лице: </div>
             <div><asp:DropDownList runat="server" ID="ddlAdresatType" Width="200px"></asp:DropDownList></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">Кому: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender22" TargetControlID="tbWhom" WatermarkCssClass="WaterText" WatermarkText="ООО Имя"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbWhom" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <div class="label">О чем: </div>
             <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender23" TargetControlID="tbAbout" WatermarkCssClass="WaterText" WatermarkText="О чем"></ajaxToolkit:TextBoxWatermarkExtender>
             <div><asp:TextBox runat="server" ID="tbAbout" Width="200px"></asp:TextBox></div>
             <div style="clear: both;"></div>
         </div>
         <div class="FormItem">
             <asp:LinkButton runat="server" ID="lbAddMail" CssClass="FormButton" OnClick="lbAddMail_OnClick"><span>Далеe: </span></asp:LinkButton>
         </div>
         <div style="clear: both;"></div>
     </asp:Panel>
        
        <!--Форма для юр.лиц-->
        
     <!-- Форма для добавления абонента снятие/установки -->
     
     
       
    </asp:Panel>
    <asp:Panel runat="server" ID="Step2" Visible="false">
    <!--Загаловок формы Шаг 2 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 2</span></div>
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
                onselectedindexchanged="gvMailJournal_SelctedIndexChanged">
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
    
        
         <div style="float:left; width:400px; border:0px solid black;">
    <div style="float:left; text-align:left; font-size:13px; position:relative; width:700px; border:0 solid black; margin-top:5px;">
        <asp:Literal runat="server" ID="litMailInfo"></asp:Literal>
    </div>
     
     <div style="/*float:right;*/ margin-top:15px; width:410px;">     
     <div style=""><span style="font-weight:bold; font-size:14px;">Ответственный исполнитель:</span></div>
        <div class="FormItem">
            <div class="label">Отдел:</div>
            <div>
               <asp:DropDownList runat="server" ID="ddlDepartament1" AutoPostBack="True" 
                    onselectedindexchanged="ddlDepartament1_SelectedIndexChanged"></asp:DropDownList>
                    <div style="float:right"><asp:LinkButton runat="server" ID="lbRC"  
                            CssClass="FormButton" onclick="lbRC_Click"><span>Поиск</span></asp:LinkButton></div>
            </div>
            <div style="clear:both;"></div>
        </div> 
        <div class="FormItem">
            <div class="label">Ответственный исполнитель:</div>
            <div><asp:DropDownList runat="server" ID="ddlResponContract" Width="400px" AutoPostBack="true"></asp:DropDownList></div>
            <div style="clear:both;"></div>
        </div>
        
          <div class="FormItem">
          
            <asp:TextBox runat="server" ID="tbFactoryNumber"  Width="120px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="tbStartValue" Width="100px"></asp:TextBox>
            <div style="float:right;"><asp:LinkButton runat="server" ID="lbSearchSearch" CssClass="FormButton" 
                onclick="lbSearch_Click"><span>Добавить</span></asp:LinkButton></div>
            
            <div style="clear:both;"></div>
        </div>
        <div class="FormItem">
        
        </div>
        <div style="clear:both;"></div>
       </div>     
       </div>  
        <div style="width:700px;height:200px; border-top:1px solid black; overflow:auto; position:relative; z-index:20;">
            <asp:Repeater runat="server" ID="repResponContract" onitemcommand="repResponContract_ItemCommand">
                <ItemTemplate>
                    <div style=" background-color:#ffebbe; font-size:12px; float:left; height:20px; border:1px solid silver; padding:2px;  margin-top:10px; margin-right:15px;">
                        <span><b>Номер:</b>&nbsp;
                            <asp:Literal runat="server" ID="litFN" Text=<%# Eval("FactoryNumber")%>></asp:Literal>&nbsp;&nbsp;
                            <b>Показания:</b>&nbsp;
                            <asp:Literal runat="server" ID="litStartValue" Text=<%# Eval("VodomerPreview.StartValue")%>></asp:Literal> &nbsp;&nbsp;
                            <b>D:</b>&nbsp;<asp:Literal runat="server" ID="lbDiameter" Text=<%# Eval("VodomerPreview.Diameter")%>></asp:Literal></span>
                         <asp:LinkButton runat="server" ID="lbDel" CommandName="Item"><div class="Close"></div></asp:LinkButton>                    
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:LinkButton runat="server" ID="lbSaveAll" CssClass="FormButton" onclick="lbSaveAll_Click"><span>Сохранить всё</span></asp:LinkButton>    
    </asp:Panel>
     