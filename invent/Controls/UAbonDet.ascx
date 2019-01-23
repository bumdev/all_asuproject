<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UAbonDet.ascx.cs" Inherits="invent_app.UAbonDet" %>

 <asp:HiddenField runat="server" ID="hfBudjet" Value="0" />
 <asp:Panel runat="server" ID="FAbonForm">
       <asp:HiddenField runat="server" ID="hfODID" Value="0" />
    <asp:Literal runat="server" ID="litScript"></asp:Literal>
   <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true"></telerik:RadWindowManager>
        <!--Загаловок формы конец-->
       
        <!--Форма для юр. лиц-->      
        <div style="width:750px; min-height:200px;">
        <asp:Panel runat="server" ID="panView"  Visible="true">
           
            <div class="CommonFormElement">
            <div  style="font-size:14px; margin-top:5px; float:left;">  <asp:Literal runat="server" ID="litAbonentInfo"></asp:Literal></div>          
            <div class="ClearBoth"></div>
            </div>
             <div class="CommonFormElement">
	        <div class="CommonFormData" style="width:750px;"><asp:LinkButton runat="server" ID="lbEdit" CssClass="FormButton" 
                    onclick="lbEdit_Click"><span>Редактировать</span></asp:LinkButton>
     
                         <div style=" float:right">
                             
    
                 
    <div style="float:right; margin-left: 10px; text-align: center; font-weight: bold; font-size: 14px; margin-bottom: 25px;">
        <asp:Button runat="server" ID="btBillRublesForLittleWDK" Text="Счет(малый водоканал)" OnClick="btBillRublesForLittleWDK_Click" />
    </div>
    
    <div style="float: right; margin-left: 10px; text-align: center; font-weight: bold; font-size: 14px; margin-bottom: 25px;">
        <asp:Button runat="server" ID="btActRublesForLittleWdk" Text="Акт(малый водоканал)" OnClick="btActRublesForLittleWDK_Click" />
    </div>

    <div style="float: right; margin-left: 10px; text-align: center; font-weight: bold; font-size: 14px; margin-bottom: 25px;">
        <asp:Button runat="server" ID="btUAPayRublesForLittleWDK" Text="Квитанция(малый водоканал)" OnClick="btUAPayRublesForLittleWdk_Click" />
    </div>
                             
          

    </div>
                    
                    </div>
	        <div class="ClearBoth"></div>
        </div>
            
  
        </asp:Panel>
            
          
            
    <div style=" margin-left: 10px; float:right; text-align:center; font-weight:bold; font-size:14px; padding-top: 10px;">
        <asp:Button runat="server" ID="btContract" Text="Договор" 
            onclick="btContract_Click" />
        
    </div>
    
    <div style="margin-left:10px; float:right; text-align:center; font-weight:bold; font-size:14px; padding-top: 10px;">
                                 <asp:Button runat="server" ID="btActRubles" Text="Акт(рубли)" OnClick="btActRubles_Click" />
    </div>
                             
    
  
    <div style="margin-left:10px; float:right; text-align:center; font-weight:bold; font-size:14px; padding-top: 10px;">
        <asp:Button runat="server" ID="btUAPayRubles" Text="Квитанция(рубли)" OnClick="btUAPayRubles_Click" />
     </div>
          

    

     <div style="margin-left:10px; float:right; text-align:center; font-weight:bold; font-size:14px; padding-top: 10px;">
        <asp:Button runat="server" ID="btBillRubles" Text="Счет(рубли)" OnClick="btBillRubles_Click" />
      </div>

        <asp:Panel runat="server" ID="panEdit" Visible="false">
        <asp:HiddenField runat="server" ID="hfID" />
        <asp:HiddenField runat="server" ID="hfDogID" />
        <div class="CommonFormElement">
            <div class="CommonFormDescription">Номер по журналу:</div>
            <div class="CommonFormData"><asp:TextBox runat="server" ID="tbNumberJournal" Width="400px"></asp:TextBox></div>
            <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Абонент:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbTitle" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">ОКПО:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbOKPO" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">МФО:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbMFO" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Р/С:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbRS" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">ИНН:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbINN" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Св. пл. НДС:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbVATPay" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Договор:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbContract" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Банк:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbBank" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Адрес:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbAddress" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Бюджет:</div>
	        <div class="CommonFormData"><asp:CheckBox runat="server" ID="cbBudjet" /></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Телефон:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbPhone" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">В лице:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbContactFace" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">На основании:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbCause" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Тип налогообложения:</div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbTaxType" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
        <div class="CommonFormElement">
	        <div class="CommonFormData"><asp:LinkButton runat="server" ID="lbSaveAbonent" CssClass="FormButton" 
                onclick="lbSaveAbonent_Click"><span>Сохранить</span></asp:LinkButton><asp:LinkButton runat="server" ID="LinkButton4" CssClass="FormButton" 
                onclick="lbSaveAbonent_Click"><span>Отменить</span></asp:LinkButton></div>
	        <div class="ClearBoth"></div>
        </div>
        </asp:Panel> 
       </div>
       <div style="border-top:1px solid black; width:750px; height:250px;">
       <div style="width:750px; max-height:200px;  overflow:auto;">
    <cuc:GridView  DataSourceID="dsJournal" AllowPaging="true" PageSize="50" 
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="false" 
        ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow" 
        EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None" 
        CssClass="GridView MaxWidth" ID="gvJournal" AutoGenerateColumns="false" 
        AllowSorting="true" onrowupdating="gvJournal_RowUpdating">
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
           <asp:TemplateField AccessibleHeaderText="colEdit"  ItemStyle-Width="200px">
						<ItemTemplate>
							<asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server" Visible=<%# string.IsNullOrEmpty(Eval("EndValue").ToString()) %>><span>Редакт.</span></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
                            <asp:LinkButton ID="btnCancel" CommandName="Cancel" CssClass="FormButton" runat="server"><span>Отменить</span></asp:LinkButton>
							<asp:LinkButton ID="btnUpdate" CommandName="Update" CssClass="FormButton" runat="server"><span>Обновить</span></asp:LinkButton>							
						</EditItemTemplate>
					</asp:TemplateField>
         <asp:TemplateField HeaderText="№">
            <ItemTemplate>
                <asp:Literal runat="server" ID="ODID" Text=<%# Eval("ID")%> ></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>        
         <asp:TemplateField HeaderText="D">
            <ItemTemplate>
                <asp:Literal runat="server" ID="litDiameter" Text=<%# Eval("diameter")%>></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Заводской номер">
            <ItemTemplate>
                <asp:Literal runat="server" ID="litFactoryNumber" Text=<%# Eval("nom_zavod")%>></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>       
        <asp:TemplateField HeaderText="Начальные показания">
            <ItemTemplate>
                <asp:Literal runat="server" ID="litStartValue" Text=<%# Eval("StartValue")%>></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Конечные показания">
            <ItemTemplate>
                <asp:Literal runat="server" ID="litEndValue" Text=<%# Eval("EndValue")%>></asp:Literal>
            </ItemTemplate>
            <EditItemTemplate>
            <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe" TargetControlID="tbEndValue" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                <asp:TextBox runat="server" ID="tbEndValue" Text=<%# Eval("EndValue")%> Width="100px"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>	
         <asp:TemplateField HeaderText="Цена">
            <ItemTemplate>
                <asp:Literal runat="server" ID="price" Text=<%# Eval("price")%> ></asp:Literal>&nbsp;грн.
                <asp:Literal runat="server" ID="pricerub" Text=<%# Eval("pricerub")%> ></asp:Literal>&nbsp;руб.
            </ItemTemplate>
        </asp:TemplateField>
	</Columns>
</cuc:GridView>
    <asp:SqlDataSource ID="dsJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveVodomersByUOrderID" 
SelectCommandType="StoredProcedure"
UpdateCommand="UpdateUOrderDetails" 
UpdateCommandType="StoredProcedure">
	<SelectParameters>
			<asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="OrderID" Type="Int32" />
	</SelectParameters>
    <UpdateParameters>
			<asp:Parameter Name="ID" Type="Int32" />
			<asp:Parameter Name="EndValue" Type="String" />
	</UpdateParameters>
</asp:SqlDataSource>
</div>
<div style="width:750px; height:50px; border:0px solid black;">
<div style="float:right; margin-right:3px;">
 <table cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right"><span style="font-weight:bold; font-size:13px;">Итого:&nbsp;</span></td>
                    <td align="right"><span style="font-size:13px;"><%= GetSum().ToString("0.00")%>&nbsp;грн.</span></td>
                    <td align="right"><span style="font-size:13px;"><%= GetSumRub().ToString("0.00")%>&nbsp;руб.</span></td>
                </tr>
                <tr>
                    <td align="right"> <span style="font-weight:bold; font-size:13px;"><nobr>НДС 20%:&nbsp;</nobr></span></td>
                    <td align="right"><span style="font-size:13px;"><%= GetVAT().ToString("0.00")%>&nbsp;грн.</span></td>                   
                </tr>
                <tr>
                    <td align="right"><span style="font-weight:bold; font-size:13px;">Всего:&nbsp;</span></td>
                    <td align="right"><span style="font-size:13px;"><%= GetFinalSum().ToString("0.00")%>&nbsp;грн.</span></td>                    
                </tr>
             </table>
             </div>
</div>
       </div>
       <div style="width:750px; height:25px;">
       <div style="float:left;">
                     <asp:CheckBox ID="cbPaid" runat="server" Text="Оплачен" TextAlign="Left" />
                     <asp:TextBox runat="server" ID="tbPaymentDay"></asp:TextBox>
            <telerik:RadButton runat="server" ID="lbGetPayment" onclick="lbGetPayment_Click" Text="Проверить оплату"></telerik:RadButton>&nbsp;&nbsp;&nbsp;
                      <asp:CheckBox runat="server" Text="Выдан" id="cbSeld" TextAlign="Left"  />
                     <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="tbPaymentDay"></ajaxToolkit:CalendarExtender>    
       </div>
       <div style="float:right;">
             <asp:LinkButton runat="server" ID="lbSaveAll" CssClass="FormButton" 
                 onclick="lbSaveAll_Click" ><span>Сохранить</span></asp:LinkButton>
             </div>
       </div>
    </asp:Panel>

<asp:HiddenField runat="server" ID="hfOKPO" Value="0" />
<asp:HiddenField runat="server" ID="hfDateIn" />
