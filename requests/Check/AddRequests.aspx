<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddRequests.aspx.cs" Inherits="requests_app.AddRequests" %>
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

<asp:HiddenField runat="server" ID="hfRequestsType" Value="Withdrawal" />
<asp:HiddenField runat="server" ID="hfOrder" Value="0" />
<asp:HiddenField runat="server" ID="hfVisibleVodomerSearch" />
    <div style="margin-left: 10px;">
 <asp:Panel runat="server" ID="Step1">
       
        <!--Загаловок формы Шаг 1 начало-->
        <div style="width:100%; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Заполнение заявки</span></div>
        
        <div style="width: 290px;  margin-bottom: 10px; float: left;">
            <span>Заявка: </span>&nbsp;
            <asp:LinkButton runat="server" ID="lbRequestClick"></asp:LinkButton>
        </div>
        <div style="clear: both;"></div>
        
        <!--Загаловок формы Шаг 1 конец-->
        
       
        <!--Форма для физ. лиц-->
        <asp:Panel runat="server" ID="panWithGrid" Visible="false">
           
            <div style="width: 400px; height: 200px; border: 1px solid black; float: left;" id="box1">
                <div class="FormItem">
                <div class="label"><div style="margin-left: 15px;">Телефон: </div></div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermakExtender1" TargetControlID="tbPhone" WatermarkCssClass="WaterText" WatermarkText="050-111-11-11"></ajaxToolkit:TextBoxWatermarkExtender>
                <div style="margin-left: 15px;"><asp:TextBox runat="server" ID="tbPhone" Width="200px"></asp:TextBox></div>
                <div style="clear: both;"></div>
            </div>
            <div class="FormItem">
                <div class="label"><div style="margin-left: 15px;">Количество водомеров: </div></div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermakExtender2" TargetControlID="tbAmountDevice" WatermarkCssClass="WaterText" WatermarkText="20"></ajaxToolkit:TextBoxWatermarkExtender>
                <div style="margin-left: 15px;"><asp:TextBox runat="server" ID="tbAmountDevice" Width="200px"></asp:TextBox></div>
                <div style="clear: both;"></div>
            </div>
            <div class="FormItem">
                <div class="label"><div style="margin-left: 15px;">Примечание: </div></div>
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermakExtender3" TargetControlID="tbComment" WatermarkCssClass="WaterText" WatermarkText="Примечание"></ajaxToolkit:TextBoxWatermarkExtender>
                <div style="margin-left: 15px;"><asp:TextBox runat="server" ID="tbComment" Width="200px"></asp:TextBox></div>
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
            </div>

            
            
            
             
          <div style="clear:both;"></div>
            
            <div class="FormItem">
                <asp:LinkButton runat="server" ID="lbSaveRequests" CssClass="FormButton" OnClick="lbSaveRequests_OnClick">
                    <span>Сохранить</span>
                </asp:LinkButton>
                <div style="clear: both;"></div>
            </div>
        </asp:Panel>
        
        
     </asp:Panel>
     
     <!-- здесь она заканчивается -->
     

    
</div>
        
        
        
        
        
        </telerik:RadAjaxPanel>
</asp:Content>
