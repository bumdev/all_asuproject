<%@ Control Language="C#" AutoEventWireup="true" Inherits="leak_detectors.MessageBox" Codebehind="MessageBox.ascx.cs" %>


        <div style=" z-index:3000; width:400px;height:300px;border:1px solid black;position:absolute; left:50%;top:50%; margin-left:-200px;margin-top:-150px; background-color:White;" runat="server" id="ff">
    <div style="width:400px; height:30px; background-color:Black; padding-top:10px;"><span style=" font-size:14px; color:Silver; margin-left:10px; font-weight:bold;"><asp:Literal runat="server" ID="litHeader"></asp:Literal></span>
        <asp:LinkButton runat="server" ID="lbClose" onclick="lbClose_Click"><div style="background:url(../images/icons/close_white.png); float:right; margin-right:10px; width:17px; height:17px;"></div></asp:LinkButton></div>
    <div style="width:400px; height:270px; padding:10px;"><asp:Literal runat="server" ID="litMessage"></asp:Literal></div>
</div>
