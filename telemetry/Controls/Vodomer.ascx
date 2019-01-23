<%@ Control Language="C#" AutoEventWireup="true" Inherits="leak_detectors.Controls_Vodomer" Codebehind="Vodomer.ascx.cs" %>
<div style="width:100%;height:100%; background: rgba(0, 0, 0, 0.7); position:absolute; z-index:1000; top:0;left:0;">
    <div style="width:400px; height:500px; border:1px solid black; position:relative; top:50%;left:50%; margin-left:-200px;margin-top:-250px; background-color:White; padding:5px;">
    <div style="width:400px; height:20px;"><span>Добавление нового водомера</span></div>
    <div class="FormItem">
        <asp:LinkButton runat="server" ID="lbSave" CssClass="FormButton" OnClick="lbSave_Click"><span>Сохранить</span></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbCancel" CssClass="FormButton"  OnClick="lbCancel_Click"><span>Отменить</span></asp:LinkButton>
    </div>
    </div>
</div>