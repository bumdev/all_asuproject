<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WindowBox.aspx.cs" Inherits="Timetable_WebApp.WindowBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link runat="server" rel="shortcut icon" href="~/timetable_logo.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="~/timetable_logo.ico" type="image/ico" />
    <style type="text/css">
        .RadImageGallery ul.rigThumbnailsList  {
    background-color: lavender;
}
    </style>
</head>
<body>
    <telerik:RadButton runat="server" ID="BenjaminButtonReturn" NavigateUrl="ServiceAccept.aspx" ButtonType="LinkButton" Text="КОНТРОЛЬ СЛУЖБ"></telerik:RadButton>
    <telerik:RadButton runat="server" ID="MainTableButtonReturn" NavigateUrl="Timetable.aspx" ButtonType="LinkButton" Text="ТАБЕЛЬ"></telerik:RadButton>
    <telerik:RadButton runat="server" ID="FiredButtonReturn" NavigateUrl="FiredJournal.aspx" ButtonType="LinkButton" Text="ТАБЕЛЬ (УВОЛЕННЫЕ)"></telerik:RadButton>
    <telerik:RadButton runat="server" ID="FiredControlButton" NavigateUrl="FiredControl.aspx" ButtonType="LinkButton" Text="КОНТРОЛЬ СЛУЖБ (УВОЛЕННЫЕ)"></telerik:RadButton>
    <form id="form1" runat="server">
      
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div>
        <telerik:RadImageGallery runat="server" ID="radGallery" OnNeedDataSource="radgallery_NeedDataSource" 
            DataImageField="imageURL" BackColor="lavender" CssClass="rigThumbnailsList" DataTitleField="imageTitle">
            
        </telerik:RadImageGallery>    
    </div>
        
   
    </form>
    
    
</body>
</html>
