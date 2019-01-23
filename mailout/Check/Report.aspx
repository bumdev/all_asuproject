<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="outcomming_mail.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hfUserID" Value="0"/>
      <!--Тут мы показываем наши высплывающие окошки.-->
        <telerik:RadWindowManager ID="radWM" runat="server" EnableShadow="true">
        </telerik:RadWindowManager>
    
    <div style="margin-top: 10px; width: 1000px; height: 30px; padding: 20px;">
        <span style="font-weight: bold;">Отчет по корреспонденции юр. лиц:</span>&nbsp;&nbsp;&nbsp;
        С&nbsp;<telerik:RadDatePicker runat="server" ID="dpFromJur"></telerik:RadDatePicker>
        &nbsp;&nbsp;По&nbsp;<telerik:RadDatePicker runat="server" ID="dpToJur"></telerik:RadDatePicker>
        <telerik:RadButton runat="server" ID="butGenerateJurDate" Text="Сформировать" OnClick="butGenerateJurDate_Click"></telerik:RadButton>
    </div>
    
    <div style="margin-top: 10px; width: 1000px; height: 30px; padding: 20px;">
        <span style="font-weight: bold;">Отчет по корреспонденции физ. лиц:</span>&nbsp;&nbsp;&nbsp;
        С&nbsp;<telerik:RadDatePicker runat="server" ID="dpFromPhys"></telerik:RadDatePicker>
        &nbsp;&nbsp;По&nbsp;<telerik:RadDatePicker runat="server" ID="dpToPhys"></telerik:RadDatePicker>
        <telerik:RadButton runat="server" ID="butGeneratePhysDate" Text="Сформировать" OnClick="butGeneratePhysDate_OnClick"></telerik:RadButton>
    </div>
    
    <div style="margin-top: 10px; width: 1000px; height: 30px; padding: 20px;">
        <span style="font-weight: bold;">Отчет по типу адресата физ. лица:</span>
        <asp:DropDownList runat="server" ID="dpPhysAdresat"></asp:DropDownList>
        <telerik:RadButton runat="server" ID="butGeneratePhysAdresat" Text="Сформировать" OnClick="butGeneratePhysAdresat_OnClick"></telerik:RadButton>
    </div>
    
    <div style="margin-top: 10px; width: 1000px; height: 30px; padding: 20px;">
        <span style="font-weight: bold;">отчет по прочей корреспонденции:</span>&nbsp;&nbsp;&nbsp;
        С&nbsp;<telerik:RadDatePicker runat="server" ID="dpFromOther"></telerik:RadDatePicker>
        &nbsp;&nbsp;По&nbsp;<telerik:RadDatePicker runat="server" ID="dpToOther"></telerik:RadDatePicker>
        <telerik:RadButton runat="server" ID="butGenerateOtherDate" Text="Сформировать" OnClick="butGenerateOtherDate_OnClick"></telerik:RadButton>
    </div>
    
    


    
    <asp:SqlDataSource runat="server" ID="dsJuridicalMail" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
        SelectCommand="RetrieveReportJuridicalMail" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpFromJur" PropertyName="SelectedDate" Name="dateFrom" Type="String" />
            <asp:ControlParameter ControlID="dpToJur" PropertyName="SelectedDate" Name="dateTo" Type="String" />
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="dsPhysicalMail" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
        SelectCommand="RetrieveReportPhysicalMail" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpFromPhys" PropertyName="SelectedDate" Name="dateFrom" Type="String" />
            <asp:ControlParameter ControlID="dpToPhys" PropertyName="SelectedDate" Name="dateTo" Type="String" />
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="dsPhysAdresat" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
        SelectCommand="RetrieveReportPhysAdresat" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpPhysAdresat" PropertyName="SelectedValue" Name="physAdresat" Type="String" />
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="dsOtherMail" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>"
        SelectCommand="RetrieveReportOtherMail" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpFromOther" PropertyName="SelectedDate" Name="dateFrom" Type="String" />
            <asp:ControlParameter ControlID="dpToOther" PropertyName="SelectedDate" Name="dateTo" Type="String" />
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    

</asp:Content>
