<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UAbonentDet.aspx.cs" Inherits="invent_app.UAbonentDet" %>

<%@ Register src="../Controls/UAbonDet.ascx" tagname="UAbonDet" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/style.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <telerik:RadScriptManager runat="server" ID="RadScriptManager1" /> 
        
          <telerik:RadAjaxManager ID="RadAjaxManager" runat="server" DefaultLoadingPanelID="LoadingPanel1"></telerik:RadAjaxManager>
        <uc1:UAbonDet ID="UAbonDet1" runat="server" />
    </div>
    </form>
</body>
</html>
