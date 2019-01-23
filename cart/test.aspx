<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="cartridges_app.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
       /* function RefreshUpdatePanel() {
            __doPostBack('<%= tbSearch.ClientID %>', '');
        };*/



       /*$("#<%= tbSearch.ClientID %>").live("keyup",function () {
            $("#<%= tbSearch.ClientID %>").focus();
        })*/

       function l() {
           var val = $("#<%= tbSearch.ClientID %>").val();
           $("#<%= tbSearch.ClientID %>").val('');
           $("#<%= tbSearch.ClientID %>").focus().val(val.toString());
        };

</script>
<asp:Literal runat="server" ID="scriptF"></asp:Literal>
</head>
<body onload="l()">
    <form id="form1" runat="server">
    <div>
        <span onclick="l()">qwe</span>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        


        <asp:TextBox runat="server" ID="tbSearch" AutoPostBack="True" 
            ontextchanged="tbSearch_TextChanged"  Tex="123123"></asp:TextBox>
       
        <asp:ListBox runat="server" ID="lbSearch"></asp:ListBox>
      
        
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
