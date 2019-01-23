<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAbonDet.ascx.cs" Inherits="kipia_web_application.Controls.FAbonDet" %>


<telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true"></telerik:RadWindowManager>
 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>

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
        <div style="width:700px; min-height:70px;">
       <div style="font-size:14px; margin-top:5px; float:left;">
            <b><asp:Literal runat="server" ID="litAbonentInfo"></asp:Literal></b>
       </div>
       </div>
       <div style="width:750px;height:25px;">
        <div style="float:right;">
        
    <div style=" margin-left:10px; float:left; text-align:center; font-weight:bold; font-size:14px;">
        <asp:Button runat="server" ID="btAct" Text="Акт" onclick="btAct_Click" />      
    </div>
    <div style=" margin-left:10px; float:left; text-align:center; font-weight:bold; font-size:14px;">
        <asp:Button runat="server" ID="btPay" Text="Квитанция" onclick="btPay_Click"  />
    </div>
    <div style=" margin-left:10px; float:left; text-align:center; font-weight:bold; font-size:14px;">
        <asp:Button runat="server" ID="btOrder" Text="Заявление" 
            onclick="btOrder_Click"  />     
    </div>
    <div style=" margin-left:10px; float:left; text-align:center; font-weight:bold; font-size:14px;">
        <asp:Button runat="server" ID="btOrderCheck" Text="Заявление + квитанция" 
            onclick="btOrder_Check_Click"  />       
    </div>
    <div style=" margin-left:10px; float:left; text-align:center; font-weight:bold; font-size:14px;">
        <asp:Button runat="server" ID="btActCheck" Text="Акт + квитанция" onclick="btActCheck_Click"    />       
    </div>
       </div>
       </div>
       <div style="border:1px solid black; width:750px;height:350px;  overflow:auto;">
            <cuc:GridView  DataSourceID="dsJournal" AllowPaging="true" PageSize="50" 
        DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True" 
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
           <asp:TemplateField AccessibleHeaderText="colEdit" >
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
                <asp:Literal runat="server" ID="price" Text=<%# Eval("price")%> ></asp:Literal>
            </ItemTemplate>
            <FooterTemplate>
                <table style="font-size: 12px;">
                    <tr>
                        <td><b>Итого:</b></td>
                        <td align="right"><%# GetSum().ToString("0.00")%> грн.</td>
                    </tr>
                    <tr>
                        <td><b>НДС 20%:</b></td>
                        <td align="right"><%# GetVAT().ToString("0.00")%> грн.</td>
                    </tr>
                    <tr>
                        <td><b> Всего:</b></td>
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
       </div>
        <!--Footer-->
       <div style="width:750px; height:25px; border: 0 solid black; position: absolute; top: 500px;">
       <div style="float:left">
       <asp:CheckBox runat="server" Text="Выдан" id="cbSeld" TextAlign="Left"  /> &nbsp;&nbsp;&nbsp;
       <asp:CheckBox ID="cbPaid" runat="server" Text="Оплачен" TextAlign="Left" />
       <asp:TextBox runat="server" ID="tbPaymentDay"></asp:TextBox>
       <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="tbPaymentDay"></ajaxToolkit:CalendarExtender> 
       </div>
       <div style="float:right;">
           <telerik:RadButton runat="server"  ID="lbSaveAll" onclick="lbSaveAll_Click" Text="Сохранить"></telerik:RadButton>
             </div>
       </div>
    </asp:Panel>
 
 </telerik:RadAjaxPanel>