<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailOutcommingDet.aspx.cs" Inherits="outcomming_mail.MailOutcommingDet" %>
<%@ Register src="../Controls/MailOutDet.ascx" tagName="MailOutDet" tagPrefix="uc1"%>

<!DOCTYPE html>
<link href="../Css/style.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        
        <telerik:RadAjaxManager ID="RadAjaxManager" runat="server"  DefaultLoadingPanelID="LoadingPanel1"></telerik:RadAjaxManager>
    </div>
        <uc1:MailOutDet ID="MailOutDet1" runat="server" />
    

    </form>
</body>
</html>
