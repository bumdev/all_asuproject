<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="ReportRate.aspx.cs" Inherits="kipia_web_application.ReportRate" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
с<telerik:RadDatePicker runat="server" ID="dFrom"></telerik:RadDatePicker> 
по<telerik:RadDatePicker runat="server" ID="dTo"></telerik:RadDatePicker> <br/>
<telerik:RadDropDownList runat="server" ID="radddlUnits">
<Items>
<telerik:DropDownListItem Value="3" Text="День" runat="server" />
<telerik:DropDownListItem Value="0" Text="Декада" runat="server" />
<telerik:DropDownListItem Value="1" Text="Месяц" runat="server" />
<telerik:DropDownListItem Value="2" Text="Год" runat="server" />
</Items>
</telerik:RadDropDownList>
<telerik:RadDropDownList runat="server" ID="radWP" DataTextField="Title" DataValueField="ID" >
</telerik:RadDropDownList>
<br/>
<telerik:RadButton runat="server" ID="btSet" Text="Добавить" onclick="btSet_Click"></telerik:RadButton>
<telerik:RadButton runat="server" ID="btCleare" Text="Очистить" 
        onclick="btCleare_Click"></telerik:RadButton>
</div>
<div>
  <telerik:RadChart ID="RadChart1" runat="server" Width="800px" Height="600px" >

</telerik:RadChart>
</div>
<asp:SqlDataSource ID="dsWP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
SelectCommand="RetrieveWPs" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
</asp:Content>
