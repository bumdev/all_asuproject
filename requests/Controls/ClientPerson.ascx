<%@ Control Language="C#" AutoEventWireup="true" Inherits="requests_app.Controls_ClientPerson" Codebehind="ClientPerson.ascx.cs" %>
<%@ Register Assembly="ConvincingMail.AdvancedAutoSuggest" Namespace="ConvincingMail.AdvancedAutoSuggest" TagPrefix="cc1" %>

<div style="width:100%;height:100%; background: rgba(0, 0, 0, 0.7); position:absolute; z-index:1000; top:0;left:0;">
    <div style="width:400px; height:300px; border:1px solid black; position:relative; top:50%;left:50%; margin-left:-200px;margin-top:-150px; background-color:White; padding:5px;">
        <div style="width:400px; height:20px;"><span>Прием абонента (физ. лицо)</span></div>
        <asp:HiddenField runat="server" ID="AbonentID" OnValueChanged="AbonentID_ValueChanged" />
        <asp:Panel runat="server" ID="panPerson" >
        <asp:Literal runat="server" ID="litID"></asp:Literal>
        <asp:TextBox ID="CityTextBox" runat="server" AutoCompleteType="none"  
                AutoPostBack="true" ></asp:TextBox>					

			<asp:TextBox ID="CityIdTextBox" runat="server" Enabled="false" Width="30" ></asp:TextBox>
			<cc1:AdvancedAutoSuggestExtender TargetControlID="CityTextBox" ServiceUrl="~/Utility.asmx/CitySuggest" UpdateField="CityIdTextBox" ID="CityAASE" runat="server" />
            <asp:TextBox ID="txtMovie" runat="server" AutoCompleteType="MiddleName"></asp:TextBox>  
  
<ajaxToolkit:AutoCompleteExtender   
    ID="AutoCompleteExtender1"   
    TargetControlID="txtMovie"   
    runat="server"
    ServicePath="~/Utility.asmx"
    ServiceMethod="GetCompletionList"   
    MinimumPrefixLength="2" 
     />  



            <div class="FormItem">                
                <div><asp:DropDownList runat="server" ID="ddlDistrict" Width="200px"></asp:DropDownList></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender2" TargetControlID="tbClientSurname" WatermarkText="Фамилия" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                              <div><asp:TextBox runat="server" ID="tbClientSurname" Width="200px" AutoPostBack="true" OnTextChanged="CityTextBox_TextChanged" ></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender1" TargetControlID="tbClientName" WatermarkText="Имя" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbClientName" Width="200px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="tbClientName_AutoCompleteExtender" 
                        runat="server" DelimiterCharacters="" Enabled="True" 
                        ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="tbClientName" 
                        UseContextKey="True">
                    </ajaxToolkit:AutoCompleteExtender>
                </div>
                <div style="clear:both;"></div>
            </div>            
            <div class="FormItem">
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender3" TargetControlID="tbClientLastName" WatermarkText="Отчество" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbClientLastName" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender11" TargetControlID="tbNumberJournal" WatermarkText="32154" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbNumberJournal" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender5" TargetControlID="tbPhone" WatermarkText="Телефон" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbPhone" Width="200px"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
            <div class="FormItem">
                <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="TextBoxWatermarkExtender4" TargetControlID="tbAddress" WatermarkText="Адрес" WatermarkCssClass="WaterText"></ajaxToolkit:TextBoxWatermarkExtender>
                <div><asp:TextBox runat="server" ID="tbAddress" Width="200px" TextMode="MultiLine"></asp:TextBox></div>
                <div style="clear:both;"></div>
            </div>
        </asp:Panel>
        <div class="FormItem">
            <asp:LinkButton runat="server" ID="lbSave" CssClass="FormButton" OnClick="lbSave_Click"><span>Сохранить</span></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lbCancel" CssClass="FormButton"  OnClick="lbCancel_Click"><span>Отменить</span></asp:LinkButton>
        </div>
    </div>
</div>