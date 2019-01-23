<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherMailOutcommingDet.aspx.cs" Inherits="outcomming_mail.OtherMailOutcommingDet" %>
<%@ Register src="../Controls/OtherMailOutDet.ascx" tagName="OtherMailOutDet" tagPrefix="uc1"%>

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
        <uc1:OtherMailOutDet ID="OtherMailOutDet1" runat="server" />
    

    </form>
</body>
</html>
