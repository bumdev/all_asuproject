<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentViewer.ascx.cs" Inherits="kipia_web_application.PaymentViewer" %>

 
        <div style=" z-index:3000; width:500px;height:400px;border:1px solid black;position:fixed; left:50%;top:50%; margin-left:-250px;margin-top:-200px; background-color:White;">
    <div style="width:500px; height:30px; background-color:Black; padding-top:10px;"><span style=" font-size:14px; color:Silver; margin-left:10px; font-weight:bold;">Поиск оплаты</span>
        <asp:LinkButton runat="server" ID="lbClose" onclick="lbClose_Click"><div style="background:url(../images/icons/close_white.png); float:right; margin-right:10px; width:17px; height:17px;"></div></asp:LinkButton></div>
    <div style="width:480px; height:340px; padding:10px; overflow:auto;">
        <asp:DataGrid runat="server" ID="dgPayment"></asp:DataGrid>
        
    </div>
</div>
<asp:HiddenField runat="server" ID="hfOKPO" />
