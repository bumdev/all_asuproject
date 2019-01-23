<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TypeAdd.ascx.cs" Inherits="cartridges_app.TypeAdd" %>

<div style="width:100%;height:100%; background: rgba(0, 0, 0, 0.7); position:fixed; z-index:10; top:0;left:0;" runat="server" id="ff">
  <div style="z-index:15; width:700px; min-height:400px; position:absolute; background-color:White; left:50%; top:50%; margin-left:-350px;margin-top:-200px; border:2px solid red; padding:10px;">
    <asp:Panel runat="server" ID="FAbonForm">
       <asp:HiddenField runat="server" ID="hfODID" Value="0" />
        <!--Загаловок формы  начало-->
        <div style="width:700px; border-bottom:1px solid black;"><span style="font-size:24px; font-weight:bold;">Добавление нового типа в реестр</span>

        <asp:LinkButton 
                runat="server" ID="lbClose" onclick="lbClose_Click" ><div  style="background:url(../images/icons/close.png); float:right; width:17px; height:17px;"></div></asp:LinkButton>
        </div>
        <!--Загаловок формы конец-->
       
        <!--Форма -->    
        <div>
        <cuc:NotificationLabel ID="nlTypeAdd" runat="server" CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification"  />
        </div>
        <div class="FormItem">
            <div class="label">Диаметр*:</div>
            <div><asp:TextBox runat="server" ID="tbDiametr"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>  
         <div class="FormItem">
            <div class="label">Производитель*:</div>
            <div><asp:DropDownList runat="server" ID="ddlSeller" Width="400px"></asp:DropDownList></div>
            <div style="clear:both;"></div>
        </div>
        <div class="FormItem">
            <div class="label">Модель*:</div>
            <div><asp:TextBox runat="server" ID="tbModel"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>         
         <div class="FormItem">
            <div class="label">Описание:</div>
            <div><asp:TextBox runat="server" ID="tbDescription" Width="400px"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>
         <div class="FormItem">
            <div class="label">Номер гос реестра*:</div>
            <div><asp:TextBox runat="server" ID="tbGovRegistry"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>
         <div class="FormItem">
            <div class="label">Дата производства:</div>
            <div><asp:TextBox runat="server" ID="tbDateProduce"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>
        <div class="FormItem">
            <div class="label">Межповерочный интервал*:</div>
            <div><asp:TextBox runat="server" ID="tbChreckInterval"></asp:TextBox></div>
            <div style="clear:both;"></div>
        </div>

         <div class="FormItem">
            <div class="label">Обслуживается:</div>
            <div><asp:CheckBox runat="server" ID="cbApprove" /></div>
            <div style="clear:both;"></div>
        </div>
         <div class="FormItem">
            <div class="label">Снят с производства:</div>
            <div><asp:CheckBox runat="server" ID="cbActive" /></div>
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




