<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="requests_app.RJ"CodeFile="RJ.aspx.cs"  %>
 
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns='http://www.w3.org/1999/xhtml'>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <asp:HiddenField runat="server" ID="hfUserID" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <div>
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../Images/Excel_XLSX.png"
            OnClick="ImageButton_Click" AlternateText="Xlsx" />
    </div>
    <div class="demo-container no-bg">
        <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" DataSourceID="dsArchiveJournal" AllowPaging="true"
            PageSize="7" AutoGenerateColumns="false" OnExcelMLWorkBookCreated="RadGrid1_ExcelMLWorkBookCreated"
            OnItemCreated="RadGrid1_ItemCreated" OnHTMLExporting="RadGrid1_HtmlExporting" OnItemCommand="RadGrid1_ItemCommand"
            OnBiffExporting="RadGrid1_BiffExporting">
            <MasterTableView CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridBoundColumn DataField="EmployeeID" HeaderText="Employee ID" HeaderStyle-Width="100px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" HeaderStyle-Width="130px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" HeaderStyle-Width="130px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BirthDate" HeaderText="Birth Date" DataFormatString="{0:MM-dd-yy}"
                        HeaderStyle-Width="140px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="HireDate" HeaderText="Hire Date" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderStyle-Width="140px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Address" HeaderText="Address" HeaderStyle-Width="240px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="City" HeaderText="City" HeaderStyle-Width="100px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Country" HeaderText="Country">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
        <asp:SqlDataSource ID="dsArchiveJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionInfo %>" 
        SelectCommand="RetrieveArchiveJournal" SelectCommandType="StoredProcedure">
	<SelectParameters>  	
            <asp:ControlParameter ControlID="hfUserID" PropertyName="Value" Name="UserID" Type="Int32" />  	
	</SelectParameters>
</asp:SqlDataSource>
</form>    