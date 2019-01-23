<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FabonentDet.aspx.cs" Inherits="requests_app.FabonentDetP" %>
<%@ Register src="../Controls/FAbonDet.ascx" tagname="FAbonDet" tagprefix="uc1" %>

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
        
          <telerik:RadAjaxManager ID="RadAjaxManager" runat="server" DefaultLoadingPanelID="LoadingPanel1"></telerik:RadAjaxManager>
    </div>
        <uc1:FAbonDet ID="FAbonDet1" runat="server" />
    </form>
</body>
</html>
