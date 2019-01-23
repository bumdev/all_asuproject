<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlternativeAbonentDet.aspx.cs" Inherits="requests_app.AlternativeAbonentDet" %>
<%@ Register src="../Controls/AlternAbonDet.ascx" tagName="AlternAbonDet" tagPrefix="uc1"%>

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
        <uc1:AlternAbonDet ID="AlternAbonDet1" runat="server" />
    
    </div>
    </form>
</body>
</html>
