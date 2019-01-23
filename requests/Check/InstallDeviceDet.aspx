<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstallDeviceDet.aspx.cs" Inherits="requests_app.InstallDeviceDet" %>
<%@ Register src="../Controls/InstallDet.ascx" tagname="InstallDet" tagprefix="uc1" %>

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
        <uc1:InstallDet ID="InstallDet1" runat="server" />
    </form>
</body>
</html>
