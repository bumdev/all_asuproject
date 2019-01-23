<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ClericalWork_WebApp.Home" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadPageLayout runat="server" ID="RadPageLayout1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn CssClass="jumbotron">
                        

                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn HiddenMd="true" HiddenSm="true" HiddenXs="true">
                        
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <telerik:RadPageLayout runat="server" ID="Content1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <%--<telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="CatComp" Text="Обновить категории жалоб" ButtonType="SkinnedButton" OnClick="CatComp_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                       
                        <telerik:RadButton runat="server" ID="OrgButt" Text="Обновить организацию" ButtonType="SkinnedButton" OnClick="OrgButt_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="MailButt" Text="Обновить тип почты" ButtonType="SkinnedButton" OnClick="Mail_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>--%>
                    
                    
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>    
</asp:Content>

<%--<asp:Content runat="server" ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3">
    <telerik:RadPageLayout runat="server" ID="Content5">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="ServiceButt" Text="Обновить службы" ButtonType="SkinnedButton" OnClick="ServiceButt_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>
                    
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="DistrButt" Text="Обновить районы" ButtonType="SkinnedButton" OnClick="DistrictButt_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>
                    
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="EnterButt" Text="Обновить предприятия" ButtonType="SkinnedButton" OnClick="Enterprises_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>
                    
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="QuestButt" Text="Обновить типы вопросов жалоб" ButtonType="SkinnedButton" OnClick="Question_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        
                        <telerik:RadButton runat="server" ID="ResponButt" Text="Обновить ответственные лица" ButtonType="SkinnedButton" OnClick="Respon_OnClick"></telerik:RadButton>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>--%>
