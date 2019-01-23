﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhysicalMailOutDet.ascx.cs" Inherits="outcomming_mail.Controls.PhysicalMailOutDet" %>

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


    <asp:Panel runat="server" ID="PhysicalMailOutForm" Width="1500px" Height="500px">
        <asp:HiddenField runat="server" ID="hfODID" Value="0" />
        <!--Загаловок формы  начало-->

        <!--Форма для исходящей почты-->
        <div style="width: 700px; min-height: 70px;">
            <div style="font-size: 14px; margin-top: 5px; float: left;">
                <b>
                    <asp:Literal runat="server" ID="litPhysicalMailOutInfo"></asp:Literal></b>

            </div>
            <div style="float: right; border: 1px solid  black; padding: 10px;">


                <!--------------------Editor mode--------------------------->

                <asp:Panel runat="server" ID="punPhysicalMailOutEditor" Visible="false">

		           <div class="FormItem">
                        <div class="label">Рег. номер:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbPhysicalRegNumber" Width="200px"></asp:TextBox></div>
                        <div style="clear:both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">Номер договора:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbPhysicalContractNumber" Width="200px"></asp:TextBox>
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="FormItem">
                        <div class="label">В чьем лице:</div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlPhysicalAdresat" Width="200px"></asp:DropDownList></div>
                        <div style="clear: both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">Отправитель:</div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlSender" Width="200px"></asp:DropDownList>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">Кому:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbPhysicalWhom" Width="200px"></asp:TextBox>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
					
					<div class="FormItem">
                        <div class="label">Примечание:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbPhysicalNotation" Width="200px"></asp:TextBox>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">О чем:</div>
                        <div style="text-align: left;">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="tbPhysicalAbout" Width="200px" Height="100px"></asp:TextBox>
                        </div>
                        <div style=" clear: both;"></div>
                    </div> 

                    
                    <div class="FormItem">
                        <div class="label">Дата ответа:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbPhysicalDate" Width="200px"></asp:TextBox>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">О чем ответ:</div>
                        <div style="text-align: left;">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="tbPhysicalAnswer" Width="200px" Height="100px"></asp:TextBox>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    
                    
                   <div class="FormItem">
                        <div class="label"></div>

                        <div>
                            <telerik:RadButton runat="server" Text="Обновить" ID="lbSavePhysicalMail" OnClick="lbSavePhysicalMail_Click"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Удалить" ID="lbDeletePhysicalMail" OnClick="lbDelete_Click"></telerik:RadButton>
                        </div>
                        <div style="clear: both;"></div>
                    </div>



                </asp:Panel>

                <!---------------------------------------------------->


            </div>
        </div>
        <br>

        <div style="border: 0px solid black; width: 750px; height: 40px; overflow: auto;">
            
        </div>
        <div style="border: 1px solid black; width: 750px; height: 270px; overflow: auto;">
            
            
            
            
            
            
            <!--Editor Mode MOFO-->

               <cuc:GridView DataSourceID="dsPhysicalMailOutJournal2" AllowPaging="true" PageSize="50"
                DataKeyNames="ID" runat="server" ShowFooterWhenEmpty="true" ShowFooter="True"
                ShowHeaderWhenEmpty="true" HighlightedRowCssClass="HighlightedRow"
                EnableRowHighlighting="true" SortStyle="HeaderAndCells" GridLines="None"
                CssClass="GridView MaxWidth" ID="gvPhysicalMailOutJournal2" AutoGenerateColumns="false"
                AllowSorting="true" >
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
                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="FormButton">
                                <span>Удалить</span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="№">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="ODID" Text='<%# Eval("ID")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Отдел">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litDepartamentName" Text='<%# Eval("DepartamentName")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ответственный исполнитель">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litResponName" Text='<%# Eval("ResponName")%>'></asp:Literal>
                        </ItemTemplate>
                              
                    </asp:TemplateField>
                   
                    
                </Columns>
            </cuc:GridView>
            <asp:SqlDataSource ID="dsPhysicalMailOutJournal2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
			SelectCommand="RetrieveRCByPhysicalSendID" 
			SelectCommandType="StoredProcedure"
			DeleteCommand="DeletePhysicalMailDetails"
			DeleteCommandType="StoredProcedure">
				<SelectParameters>
					<asp:ControlParameter ControlID="hfODID" PropertyName="Value" Name="SendID" Type="Int32" />
				</SelectParameters>
				<DeleteParameters>
					<asp:Parameter Name="ID" Type="Int32" />
				</DeleteParameters>
</asp:SqlDataSource>
            
                 <!--Editor Mode MOFO end-->
        </div>
        <!--Footer-->
        <div style="width: 750px; height: 25px; border: 0 solid black;">
            <div style="float: left">
                <div class="label"><b>Дата регистрации: </b></div>
                <asp:TextBox runat="server"  ID="tbRegDate"></asp:TextBox>
                
            </div>
            <br>
            <div style="float: right;">
                <telerik:RadButton runat="server" ID="lbSaveAll" Text="Сохранить" OnClick="lbSaveAll_Click"></telerik:RadButton>
                
            </div>
        </div>
        
    </asp:Panel>

</telerik:RadAjaxPanel>