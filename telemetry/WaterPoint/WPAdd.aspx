<%@ Page Title="" Language="C#" MasterPageFile="~/WaterPoint/WP.Master" AutoEventWireup="true" CodeBehind="WPAdd.aspx.cs" Inherits="leak_detectors.WPAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />
    <style>
        .alterDevice{
            background:whitesmoke;
        }
        .PartHeader{
            background:black;
            color:Silver;    
           
        }
        .Part
        {
            /*border-right:1px solid black;*/
            margin-left:10px;
            
            float:left;
            
        }
        .PartContent
        {
            background:#ededed;
            padding-top:10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cuc:NotificationLabel CleanCSS="CommonErrorMessage CleanNotification" DirtyCSS="CommonErrorMessage DirtyNotification" ID="nlWP" runat="server" />
<div>
    <div class="Part">
        <div class="PartHeader">
            <span class="PartHeader">Водовод</span>
        </div>
        <div class="PartContent">
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Объект:</div>
	            <div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlWPLocation" Visible="false"></asp:DropDownList>
                <telerik:RadDropDownList runat="server" ID="radddlWPLocation"></telerik:RadDropDownList>
                </div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Тип:</div>
	            <div class="CommonFormData"><asp:DropDownList runat="server" ID="ddlWPType" Visible="false"></asp:DropDownList>
                <telerik:RadDropDownList runat="server" ID="radddlWPType"></telerik:RadDropDownList>
                </div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Название:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbTitle"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">D:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbD"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">D расчетный:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbDCalc"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Q min:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbQmin"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Q max:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbQmax"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>    
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Участок до:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbLF"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Участок после:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbLS"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>           
            <div class="CommonFormElement">
	            <div class="CommonFormDescription">Комментарий:</div>
	            <div class="CommonFormData"><telerik:RadTextBox runat="server" ID="tbComment" TextMode="MultiLine"  Height="90px"></telerik:RadTextBox></div>
	            <div class="ClearBoth"></div>
            </div>
            <div class="CommonFormElement">
	            <div class="CommonFormDescription"></div>
	            <div class="CommonFormData"><telerik:RadButton runat="server" ID="lbSave"  onclick="lbSave_Click" Text="Сохранить"></telerik:RadButton></div>
	            <div class="ClearBoth"></div>
            </div>
        </div>
    </div>
    <div style="float:left;" class="Part">
        <div class="PartHeader">
            <span class="PartHeader">Поиск</span>
        </div>
        <div class="PartContent">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td colspan="2"><asp:DropDownList runat="server" ID="ddlTypeDevice" Width="250px" Visible="false"></asp:DropDownList>
                    <telerik:RadDropDownList runat="server" ID="radddlTypeDevice" Width="250px"></telerik:RadDropDownList>
                    </td>
                </tr>
                <tr>
                    <td><telerik:RadTextBox runat="server" ID="tbDeviceTitle" Width="170px"></telerik:RadTextBox></td>
                    <td><telerik:RadButton runat="server" ID="lb"  onclick="lb_Click" Text="Искать"></telerik:RadButton></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Repeater runat="server" ID="repDevice" onitemcommand="repDevice_ItemCommand">
                        <HeaderTemplate>
                            <table width="250px" cellpadding="0" cellspacing="0">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:HiddenField runat="server" ID="hfDeviceID" Value='<%# Eval("ID")%>' /><span style="font-size:12px; font-weight:bold;"><%# Eval("Title")%></span></td>
                                <td align="right"><span style="font-size:12px; "><%# Eval("FN")%></span></td>
                                <td><telerik:RadButton runat="server" ID="lbAddDeviceToWP"  CommandName="Add" Text="+"></telerik:RadButton></td>
                            </tr>
                            <tr>
                                <td colspan="3"><span style="font-size:12px;color:Gray;"><%# Eval("Description")%></span></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="alterDevice">
                                <td><asp:HiddenField runat="server" ID="hfDeviceID" Value='<%# Eval("ID")%>' /><span style="font-size:12px; font-weight:bold;"><%# Eval("Title")%></span></td>
                                <td align="right"><span style="font-size:12px; "><%# Eval("FN")%></span></td>
                                <td><telerik:RadButton runat="server" ID="lbAddDeviceToWP"  CommandName="Add" Text="+"></telerik:RadButton></td>
                            </tr>
                            <tr class="alterDevice">
                                <td colspan="3"><span style="font-size:12px;color:Gray;"><%# Eval("Description")%></span></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                        </asp:Repeater>    
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div  class="Part">
        <div class="PartHeader">
            <span class="PartHeader">Приборы</span>
        </div>
        <div class="PartContent">
            <asp:Repeater runat="server" ID="repDevicesSaved" 
                onitemcommand="repDevicesSaved_ItemCommand">
                        <HeaderTemplate>
                            <table width="250px" cellpadding="0" cellspacing="0">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:HiddenField runat="server" ID="hfDeviceID" Value='<%# Eval("ID")%>' /><span style="font-size:12px; font-weight:bold;"><%# Eval("Title")%></span></td>
                                <td align="right"><span style="font-size:12px; "><%# Eval("FN")%></span></td>
                                <td><telerik:RadButton runat="server" ID="lbAddDeviceToWP"  CommandName="Delete" Text="-"></telerik:RadButton></td>
                            </tr>
                            <tr>
                                <td colspan="3"><span style="font-size:12px;color:Gray;"><%# Eval("Description")%></span></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="alterDevice">
                                <td><asp:HiddenField runat="server" ID="hfDeviceID" Value='<%# Eval("ID")%>' /><span style="font-size:12px; font-weight:bold;"><%# Eval("Title")%></span></td>
                                <td align="right"><span style="font-size:12px; "><%# Eval("FN")%></span></td>
                                <td><telerik:RadButton runat="server" ID="lbAddDeviceToWP"  CommandName="Delete" Text="-"></telerik:RadButton></td>
                            </tr>
                            <tr class="alterDevice">
                                <td colspan="3"><span style="font-size:12px;color:Gray;"><%# Eval("Description")%></span></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                        </asp:Repeater>    
        </div>
    </div>
</div>
</asp:Content>
