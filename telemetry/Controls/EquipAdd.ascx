<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipAdd.ascx.cs" Inherits="leak_detectors.EquipAdd" %>

<div style="width:100%;height:100%; background: rgba(0, 0, 0, 0.7); position:fixed; z-index:10; top:0;left:0;" runat="server" id="ff">
  <div style="z-index:15; width:700px; min-height:400px; position:absolute; background-color:White; left:50%; top:50%; margin-left:-350px;margin-top:-200px; border:2px solid red; padding:10px;">
    <asp:Panel runat="server" ID="EquipForm">
       <asp:HiddenField runat="server" ID="hfODID" Value="0" />
        <!--Загаловок формы  начало-->
        <div style="width:700px; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Добавление нового типа в реестр</span>

        <asp:LinkButton 
                runat="server" ID="lbClose" onclick="lbClose_Click" ><div  style="background:url(../images/icons/close.png); float:right; width:17px; height:17px;"></div></asp:LinkButton>
        </div>
        <!--Загаловок формы конец-->
       
        <!--Форма -->    
        <div>
        <cuc:NotificationLabel ID="nlEquipAdd" runat="server" CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification"  />
        </div>
        <div class="FormItem">
            <div class="label">Модель*:</div>
            <div><asp:TextBox runat="server" ID="tbModel"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div> 
         <div class="FormItem">
            <div class="label">Тип оборудования*:</div>
            <div><asp:DropDownList runat="server" ID="ddlTypeEquipment" Width="400px"></asp:DropDownList></div>
            <div style="clear:both;"></div>
        </div>
                
         <div class="FormItem">
            <div class="label">Описание:</div>
            <div><asp:TextBox runat="server" ID="tbTechnicalCharacteristic" Width="400px"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>
         
         <div class="FormItem">
            <div class="label"></div>
            <div><asp:LinkButton runat="server" ID="lbSave" CssClass="FormButton" 
                    onclick="lbSave_Click"><span>Сохранить</span></asp:LinkButton></div>
            <div style="clear:both;"></div>
        </div>
    </asp:Panel>
        </div>
</div>




