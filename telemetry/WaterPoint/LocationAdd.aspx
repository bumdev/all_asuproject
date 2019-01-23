<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="LocationAdd.aspx.cs" Inherits="leak_detectors.LocationAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlWPLocation" runat="server" />
    <div class="CommonFormElement">
	    <div class="CommonFormDescription">Название:</div>
	    <div class="CommonFormData"><asp:TextBox runat="server" ID="tbTitle" Width="300px"></asp:TextBox></div>
	    <div class="ClearBoth"></div>
    </div>
    <div class="CommonFormElement">
	    <div class="CommonFormDescription">Описание:</div>
	    <div class="CommonFormData"><asp:TextBox runat="server" ID="tbDescripton" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox></div>
	    <div class="ClearBoth"></div>
    </div>
     <div class="CommonFormElement">
	    <div class="CommonFormDescription"></div>
	    <div class="CommonFormData"><asp:LinkButton runat="server" ID="lbSave" onclick="lbSave_Click" CssClass="FormButton"><span>Сохранить</span></asp:LinkButton></div>
	    <div class="ClearBoth"></div>
    </div>
</asp:Content>
