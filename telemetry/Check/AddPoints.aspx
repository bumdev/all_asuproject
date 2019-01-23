<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddPoints.aspx.cs" Inherits="leak_detectors.AddPoints" %>
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
    <asp:HiddenField runat="server" ID="hfTypeEquip"/>
    <asp:HiddenField runat="server" ID="hfSeller"/>
    <asp:HiddenField runat="server" ID="hfModel"/>

<asp:HiddenField runat="server" ID="hfPointsType" Value="Common" />
<asp:HiddenField runat="server" ID="hfOrder" Value="0" />
<asp:HiddenField runat="server" ID="hfVisibleVodomerSearch" />
    <div style="margin-left: 10px;">
 <asp:Panel runat="server" ID="Step1">
       
        <!--Загаловок формы Шаг 1 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Добавление точки:</span></div>
        
        <div style="width: 290px;  margin-bottom: 10px; float: left;">
            <span>Точка: </span>&nbsp;
            <asp:LinkButton runat="server" ID="lbPointsClick" OnClick="lbPointsClick_onClick"></asp:LinkButton>
        </div>
        <div style="clear: both;"></div>
        
        <!--Загаловок формы Шаг 1 конец-->
        
       
        <!--Форма для физ. лиц-->
        <asp:Panel runat="server" ID="panPoints" Visible="false">
           
            <div style="width: 400px; height: 200px; border: 1px solid black; float: left;" id="box1">
                <div class="FormItem">
                    <div class="label"><div style="margin-left: 15px;">Имя точки: </div></div>
                    <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender1" TargetControlID="tbNamePoints" WatermarkCssClass="WaterText" WatermarkText="Калининский 01"></ajaxToolkit:TextBoxWatermarkExtender>
                    <div style="margin-left: 15px;"><asp:TextBox runat="server" ID="tbNamePoints" Width="200px"></asp:TextBox></div>
                    <div style="clear: both;"></div>
                </div>
                
                <div class="FormItem">
                    <div class="label"><div style="margin-left: 15px;">Тип точки: </div></div>
                    <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender2" TargetControlID="tbTypePoints" WatermarkCssClass="WaterText" WatermarkText="ПНС"></ajaxToolkit:TextBoxWatermarkExtender>
                    <div style="margin-left: 15px;"><asp:TextBox runat="server" ID="tbTypePoints" Width="200px"></asp:TextBox></div>
                    <div style="clear: both;"></div>
                </div>
                <div class="FormItem">
                <div class="label"><div style="margin-left: 15px;">Адрес:</div></div>

                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender4" TargetControlID="asd" WatermarkText="Артема 942" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div style="margin-left: 15px;"><asp:TextBox runat="server" ID="asd" Width="200px" Visible="False"></asp:TextBox>
                    
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
                    <div class="label"><div style="margin-left: 15px;">Район: </div></div>
                    <div style="margin-left: 15px;"><asp:DropDownList runat="server" ID="ddlDistrict" Width="200px"></asp:DropDownList></div>
                    <div style="clear: both;"></div>
                </div>
                
                
            </div>

            <div style="clear:both;"></div>
            <div class="FormItem">
             <asp:LinkButton runat="server" ID="PointsAdd" CssClass="FormButton" OnClick="PointsAdd_Click">
                 <span>Далее</span>
             </asp:LinkButton>
         </div>
            
             
          
            
            
        </asp:Panel>
        
        
     </asp:Panel>
     
     <!-- здесь она заканчивается -->
     <asp:Panel runat="server" ID="Step2" Visible="false">
    <!--Загаловок формы Шаг 2 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Шаг 2</span></div>
    <!--Загаловок формы Шаг 2 конец-->
    <!--Информация по абоненту-->
    <asp:Panel runat="server" ID="panEquipmentSearch" Visible="false" CssClass="PanEquipmentSearch" DefaultButton="lbEquipmentSearch">

    <div style="height:30px;width:690px;">
    <div style="float:left; height:30px; " ><asp:TextBox runat="server" id="tbEquipmentSearch" Width="500px"></asp:TextBox></div>
    <div style="float:right; ">
    <asp:LinkButton runat="server" ID="lbEquipmentSearch" CssClass="FormButton" onclick="lbEquipmentSearch_Click"><span>Найти</span></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbCancel" CssClass="FormButton" onclick="lbCancel_Click" ><span>Отменить</span></asp:LinkButton>
    </div>
    </div>
    <div style="height:325px;width:690px; overflow:auto;">
        <cuc:GridView  DataSourceID="dsPoints" AllowPaging="true" PageSize="50" 
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True" 
        ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow" 
        EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" 
        CssClass="GridView MaxWidth" ID="gvPoints" AutoGenerateColumns="false" 
        AllowSorting="true" onrowcommand="gvPoints_RowCommand" 
            onselectedindexchanged="gvPoints_SelectedIndexChanged" >
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
                <asp:HiddenField runat="server" ID="hfSellerID" Value=<%# Eval("SellerID")%> />
            </ItemTemplate>
        </asp:TemplateField>
          
        <asp:TemplateField HeaderText="Оборудование">
            <ItemTemplate>
                <b><span><%# Eval("seller")%> </span></b><br/>
                <span><%# Eval("EquipmentName")%></span><br/>
                <span><%# Eval("State")%></span><br/>
            </ItemTemplate>
        </asp:TemplateField>
        
	
	</Columns>
</cuc:GridView>
<asp:SqlDataSource ID="dsPoints" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEquipmentTypeByContext" SelectCommandType="StoredProcedure" OnSelecting="dsPoints_Selecting">
	<SelectParameters>
    <asp:ControlParameter ControlID="tbEquipmentSearch" PropertyName="Text" Name="q" Type="String" />
	</SelectParameters>

</asp:SqlDataSource>
    </div>
    </asp:Panel>
        
         <div style="float:left; text-align:left; font-size:13px; position:relative; margin-bottom:20px;  width:700px; border:0 solid black; margin-top:5px;">
        <asp:Literal runat="server" ID="litPointsInfo"></asp:Literal>
    </div>
        
    <div style="width: 100%;">
        
        <div style="float: left; width: 50%;">
 <!--Навигация-->
    <div>
        <asp:Literal runat="server" ID="litTE"></asp:Literal>
        <asp:Literal runat="server" ID="litS"></asp:Literal>
        <asp:Literal runat="server" ID="litME"></asp:Literal>
    </div>
<telerik:RadGrid runat="server" ID="radgridD" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"  Font-Names="Arial Unicode MS" OnNeedDataSource="radgridTE_NeedDataSource" OnItemCommand="radgridTE_ItemCommand" >
             <MasterTableView DataKeyNames="ID" DataMember="TypeEquipments" Width="100%" AllowAutomaticUpdates="true" Name="TypeEquipments"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать" >
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="50px" DataField="TypeName" UniqueName="TypeName"  HeaderText="Тип оборудования" SortExpression="TypeName" ></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
</telerik:RadGrid>
 <asp:SqlDataSource ID="dsTE" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveTypeEquipment" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>

<!----------------***********************Модель****************************-->
<!----------------***********************Модель****************************-->
<!----------------***********************Модель****************************-->
<telerik:RadGrid runat="server" ID="radgridME" AutoGenerateColumns="false" CssClass="rad" AllowPaging="False"  Font-Names="Arial Unicode MS" OnNeedDataSource="radgridME_NeedDataSource" OnItemCommand="radgridME_ItemCommand" >
            <GroupingSettings CaseSensitive="false"></GroupingSettings>
                <MasterTableView DataKeyNames="ID" DataMember="Model" Width="100%" AllowAutomaticUpdates="true" Name="Model" CommandItemDisplay="Top"  AllowFilteringByColumn="true" >
            <Columns>
                <telerik:GridButtonColumn  CommandName="Select" Text="Выбрать">
                    <ItemStyle Width="80px"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn  FilterControlWidth="150px" DataField="EquipmentName"  ShowFilterIcon="False"  AutoPostBackOnFilter="true"  CurrentFilterFunction="Contains" HeaderText="Модель обрудования" SortExpression="EquipmentName" ></telerik:GridBoundColumn>
            </Columns>
                       <CommandItemTemplate>
                            <telerik:RadButton runat="server" ID="butAddNewDevice" CommandName="BackToTypeEquipment" Text="Вернуться к выбору типу оборудования"></telerik:RadButton>                      
                  </CommandItemTemplate>    
        </MasterTableView>
</telerik:RadGrid>
            

<asp:SqlDataSource ID="dsM" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveEquipmentByTypeEquipmentID" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="hfTypeEquip" Name="ID" DbType="Int32" PropertyName="Value"/>
    </SelectParameters>
</asp:SqlDataSource>
                        
<!--***********************************Ввод показаний********************************-->
<!--***********************************Ввод показаний********************************-->
 <asp:Panel runat="server" ID="panValues" Visible="false">     
             <div class="FormItem">
          <div class="label">Инвентарный номер:&nbsp;&nbsp;&nbsp;&nbsp;Состояние:</div>
            <div>
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmtInventoryNumber" TargetControlID="tbInventoryNumber" WatermarkText="Инвентарный номер" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender6" TargetControlID="tbState" WatermarkText="Состояние" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>

           

            <asp:TextBox runat="server" ID="tbInventoryNumber"  Width="120px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="tbState" Width="100px"></asp:TextBox>
            
            <asp:CheckBox runat="server" ID="cbRepairs" Text="В ремонте:" TextAlign="Left"/>
                <div style="float:right;"><asp:LinkButton runat="server" ID="lbSearch" CssClass="FormButton" 
                onclick="lbSearch_Click"><span>Добавить</span></asp:LinkButton></div>
            </div>
            <div style="clear:both;"></div>
        </div>
         </asp:Panel>  
        </div>
        <div style="float: right;width: 50%;">
            <div style=" margin-left: 10px;">
            <div><span>Список оборудования:</span></div>
            <div>
                   <asp:Repeater runat="server" ID="repEquipment" onitemcommand="repEquipment_ItemCommand">
                <ItemTemplate>
                    <div style=" background-color:#ffebbe; font-size:12px; width: 450px; height:20px; border:1px solid silver; padding:2px;  margin-top:10px; margin-right:15px;"><span><b>Номер:</b>&nbsp;<asp:Literal runat="server" ID="litFN" Text=<%# Eval("InventoryNumber")%>></asp:Literal>&nbsp;&nbsp;<b>Состояние:</b>&nbsp;<asp:Literal runat="server" ID="litState" Text=<%# Eval("State")%>></asp:Literal>  </span> <asp:LinkButton runat="server" ID="lbDel" CommandName="Item"><div class="Close"></div></asp:LinkButton>                    
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
