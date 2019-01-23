<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemoveDet.ascx.cs" Inherits="requests_app.Controls.RemoveDet" %>

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


    <asp:Panel runat="server" ID="RemoveForm" Width="1500px" Height="500px">
        <asp:HiddenField runat="server" ID="hfODID" Value="0" />
        <!--Загаловок формы  начало-->

        <!--Форма для исходящей почты-->
        <div style="width: 700px; min-height: 70px;">
            <div style="font-size: 14px; margin-top: 5px; float: left;">
                <b>
                    <asp:Literal runat="server" ID="litRequestsInfo"></asp:Literal></b>

            </div>
            <div style="float: right; border: 1px solid  black; padding: 10px;">


                <!--------------------Editor mode--------------------------->

                <asp:Panel runat="server" ID="punRequestsEditor" Visible="false">

		           <div class="FormItem">
                        <div class="label">Телефон:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbPhone" Width="200px"></asp:TextBox></div>
                        <div style="clear:both;"></div>
                    </div>
                    
                    <div class="FormItem">
                        <div class="label">Количество водомеров:</div>
                        <div>
                            <asp:TextBox runat="server" ID="tbAmountDevice" Width="200px"></asp:TextBox>
                        </div>
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
                        <div class="label">Примечание:</div>
                        <div style="text-align: left;">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="tbComment" Width="200px" Height="100px"></asp:TextBox>
                        </div>
                        <div style=" clear: both;"></div>
                    </div> 

                    
                    
                    
                    
                   <div class="FormItem">
                        <div class="label"></div>

                        <div>
                            <telerik:RadButton runat="server" Text="Сохранить изменения" ID="radbutSaveRequestsInfo" OnClick="radbutSaveRequestsInfo_Click"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Установлен" ID="radbutDeleteRequestsInfo" OnClick="radbutDeleteRequestsInfo_Click"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="В общее" ID="radbutCommonRequests" OnClick="radbutCommonRequests_Click"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Отказано" ID="radbutAnnulmentRequests" OnClick="radbutAnnulmentRequests_OnClick"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Замена" ID="radbutSubstitutionRemove" OnClick="radbutSubstitutionRemove_OnClick"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="В архив" ID="radbutArchive" OnClick="radbutArchive_OnClick"></telerik:RadButton>
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
        
        <!--Footer-->
        <div style="width: 750px; height: 25px; border: 0 solid black;">
            <div style="float: left">
                <div class="label"><b>Дата снятия: </b></div>
                <asp:TextBox runat="server"  ID="tbRegDate"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="tbRegDate"></ajaxToolkit:CalendarExtender>
            </div>
            <div style="float: left; margin-left: 25px;">
                <div class="label"><b>Дата установки: </b></div>
                <asp:TextBox runat="server" ID="tbDateInstall"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="tbDateInstall"></ajaxToolkit:CalendarExtender>
            </div>
            <br>
            <div style="float: right;">
                <telerik:RadButton runat="server" ID="lbSaveAll" Text="Сохранить" OnClick="lbSaveAll_Click"></telerik:RadButton>
                
            </div>
        </div>
        
    </asp:Panel>

</telerik:RadAjaxPanel>
