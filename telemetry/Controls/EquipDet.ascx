<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipDet.ascx.cs" Inherits="leak_detectors.Controls.EquipDet" %>

 <asp:HiddenField runat="server" ID="hfBudjet" Value="0" />
 <asp:Panel runat="server" ID="EquipForm">
       <asp:HiddenField runat="server" ID="hfODID" Value="0" />
    <asp:Literal runat="server" ID="litScript"></asp:Literal>
   <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true"></telerik:RadWindowManager>
        <!--Загаловок формы конец-->
       
        <!--Форма для юр. лиц-->      
        <div style="width:750px; min-height:200px;">
        <asp:Panel runat="server" ID="panView"  Visible="true">
           
            <div class="CommonFormElement">
            <div  style="font-size:14px; margin-top:5px; float:left;">  <asp:Literal runat="server" ID="litEquipInfo"></asp:Literal></div>          
            <div class="ClearBoth"></div>
            </div>
             <div class="CommonFormElement">
	        <div class="CommonFormData" style="width:750px;"><asp:LinkButton runat="server" ID="lbEdit" CssClass="FormButton" 
                    onclick="lbEdit_Click"><span>Редактировать</span></asp:LinkButton>
     
                         <div style=" float:right">
                             
                                 
          

    </div>
                    
                    </div>
	        <div class="ClearBoth"></div>
        </div>
            
  
        </asp:Panel>
            
          
            
    

        <asp:Panel runat="server" ID="panEdit" Visible="false">
        <asp:HiddenField runat="server" ID="hfID" />
        <asp:HiddenField runat="server" ID="hfDogID" />
        <div class="CommonFormElement">
            <div class="CommonFormDescription">Точка: </div>
            <div class="CommonFormData"><asp:TextBox runat="server" ID="tbNamePoints" Width="400px"></asp:TextBox></div>
            <div class="ClearBoth"></div>
        </div>
            <div class="CommonFormElement">
            <div class="CommonFormDescription">Тип точки: </div>
            <div class="CommonFormData"><asp:TextBox runat="server" ID="tbTypePoints" Width="400px"></asp:TextBox></div>
            <div class="ClearBoth"></div>
        </div>
		
        <div class="CommonFormElement">
	        <div class="CommonFormDescription">Адрес: </div>
	        <div class="CommonFormData"><asp:TextBox runat="server" ID="tbAddress" Width="400px"></asp:TextBox></div>
	        <div class="ClearBoth"></div>
        </div>
		<div class="CommonFormElement">
			<div class="CommonFormDescription">Район: </div>
			<div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlDistrict" Width="400px"></asp:DropDownList></div>
			<div class="ClearBoth"></div>
		</div>
        <div class="CommonFormElement">
	        <div class="CommonFormData"><asp:LinkButton runat="server" ID="lbSaveAbonent" CssClass="FormButton" 
                onclick="lbSavePoints_Click"><span>Сохранить</span></asp:LinkButton><asp:LinkButton runat="server" ID="LinkButton4" CssClass="FormButton" 
                onclick="lbSavePoints_Click"><span>Отменить</span></asp:LinkButton></div>
	        <div class="ClearBoth"></div>
        </div>
        </asp:Panel> 
       </div>
       <div style="border: 1px solid black; width: 750px; height: 270px; overflow: auto;">
            
            
            
            
            
            
            <!--Editor Mode MOFO-->
           <div style="width: 700px; height: 10px; border: 1px solid black; float: left; padding-bottom: 35px; margin-top: 5px; margin-left: 5px;">
               <div style="margin-left: 10px; float:left;">
                   <div class="label">Имя оборудования:</div>
                    <div>
                        <asp:DropDownList runat="server" ID="ddlEquipName" Width="150px" Visible="True"></asp:DropDownList>                        
                    </div>
                    <div style="clear: both;"></div>
               </div>
               <div style="margin-left: 10px; float:left;">
                   <div class="label">Ивентарный номер:</div>
                    <div>
                        <asp:TextBox runat="server" ID="tbInventoryNumber" Width="100px"></asp:TextBox>
                    </div>
                    <div style="clear: both;"></div>
               </div>
               <div style="margin-left: 10px; float:left;">
                   <div class="label">Состояние:</div>
                    <div>
                        <asp:TextBox runat="server" ID="tbState" Width="100px"></asp:TextBox>
                    </div>
                    <div style="clear: both;"></div>
               </div>
               <div style="float: right; margin-right: 5px; padding-right: 5px;">
                    <asp:LinkButton runat="server" OnClick="btAddEquip" Text="Добавить" />
               </div>
           </div>
                
                
                
               
           <div style="width: 750px; margin-top: 10px; padding-top: 10px; text-align: left; font-weight: bold; font-size: 14px; float: left;">
               <div class="label">Список оборудования на точке: </div>
           </div>
               <cuc:GridView DataSourceID="dsPoints2" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True"
                ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None"
                CssClass="GridView MaxWidth" ID="gvPoints2" AutoGenerateColumns="false"
                AllowSorting="true" OnRowUpdating="gvPoints2_RowUpdating" EmptyDataText="No records has been added.">
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
                    <asp:TemplateField HeaderText="Оборудование">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litEquipName" Text='<%# Eval("EquipmentName")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlEquipName"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Тип оборудования">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litTypeEquip" Text='<%# Eval("TypeName")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Инвентарный номер">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litInventoryNumber" Text='<%# Eval("InventoryNumber")%>'></asp:Literal>
                        </ItemTemplate>
                              <EditItemTemplate>
                            <asp:TextBox runat="server" ID="tbInventoryNumber" Text='<%# Eval("InventoryNumber")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Состояние">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litState" Text='<%# Eval("State")%>'></asp:Literal>
                        </ItemTemplate>
                           <EditItemTemplate>
                            
                            <asp:TextBox runat="server" ID="tbState" Text='<%# Eval("State")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                        
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Дата добавления">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litInsertDate" Text='<%# Eval("InsertDate").ToString()%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    
                </Columns>
                
            </cuc:GridView>
           
            <asp:SqlDataSource ID="dsPoints2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
                SelectCommand="RetrievePointsEquipByOrderID"
                SelectCommandType="StoredProcedure"
                UpdateCommand="UpdatePointsEquip"
                UpdateCommandType="StoredProcedure"
                DeleteCommand="DeletePointsEquip"
                DeleteCommandType="StoredProcedure"
                InsertCommand="InsertPointsEquipments"
                InsertCommandType="StoredProcedure"
                >
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="OrderID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="EquipmentID" Type="Int32" />
                    <asp:Parameter Name="State" Type="String" />
                    <asp:Parameter Name="InventoryNumber" Type="String" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:ControlParameter Name="ID" ControlID="hfODID" Direction="Output" DefaultValue="0" Type="Int32" />
                    <asp:ControlParameter Name="EquipmentID" ControlID="ddlEquipName" Type="Int32" />
                    <asp:ControlParameter Name="InventoryNumber" ControlID="tbInventoryNumber" Type="String" />
                    <asp:ControlParameter Name="State" ControlID="tbState" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            
                 <!--Editor Mode MOFO end-->
           
           
        </div>

     <div style="width: 750px; margin-top: 10px; padding-top: 10px; text-align: left; font-weight: bold; font-size: 14px; float: left;">
               <div class="label">История информации об оборудовании на точке: </div>
           </div>
     <div style="border: 1px solid black; width: 750px; height: 270px; overflow: auto; margin-top: 5px;">
         
         <cuc:GridView DataSourceID="dsPoints" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True"
                ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None"
                CssClass="GridView MaxWidth" ID="gvPoints" AutoGenerateColumns="false"
                AllowSorting="true" OnRowUpdating="gvPoints_RowUpdating">
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
                            <asp:LinkButton ID="btnEdit" CommandName="Edit" CssClass="FormButton" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("State").ToString()) %>'><span>Редакт.</span></asp:LinkButton>
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
                    
                    <asp:TemplateField HeaderText="Имя оборудования">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litEquipName" Text='<%# Eval("EquipmentName")%>' ></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Тип оборудования">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litTypeEquip" Text='<%# Eval("TypeName")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Инвентарный номер">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litInventoryNumber" Text='<%# Eval("InventoryNumber")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                    <asp:TemplateField HeaderText="Состояние">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litState" Text='<%# Eval("State")%>'></asp:Literal>
                        </ItemTemplate>
                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата добавления">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litInsertDate" Text='<%# Eval("InsertDate")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Дата удаления">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litDeletedDate" Text='<%# Eval("DeletedDate")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource ID="dsPoints" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
                SelectCommand="RetrievePointsEquipChangeByOrderID"
                SelectCommandType="StoredProcedure"
                UpdateCommand="UpdateOrderDetails"
                UpdateCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="OrderID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="State" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
     </div>
        <!--Footer-->
        
    </asp:Panel>

<asp:HiddenField runat="server" ID="hfOKPO" Value="0" />
<asp:HiddenField runat="server" ID="hfDateIn" />
