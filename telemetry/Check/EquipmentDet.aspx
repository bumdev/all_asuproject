<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentDet.aspx.cs" Inherits="leak_detectors.EquipmentDetP" %>
<%@ Register src="../Controls/EquipDet.ascx" tagname="EquipDet" tagprefix="uc1" %>

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
        <uc1:EquipDet ID="EquipDet1" runat="server" />
    </div>
        
    </form>
</body>
</html>
