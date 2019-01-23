<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Direction.aspx.cs" Inherits="kipia_web_application.Direction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link href="Css/test.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#box1").hover(function () {
                $(this).animate({ opacity: 0.75, },{duration: 100,});
            },
            function () {
                $(this).animate({ opacity: 1 },{duration: 100,});
            });

            $("#box2").hover(function () {
                $(this).animate({ opacity: 0.75 },{duration: 100,});
            },
            function () {
                $(this).animate({ opacity: 1 },{duration: 100,});
            });

            $("#box3").hover(function () {
                $(this).animate({ opacity: 0.75 },{duration: 100,});
            },
            function () {
                $(this).animate({ opacity: 1 },{duration: 100,});
            });

            $("#box4").hover(function () {
                $(this).animate({ opacity: 0.75 },{duration: 100,});
            },
            function () {
                $(this).animate({ opacity: 1 },{duration: 100,});
            });

            $("#box5").hover(function () {
                $(this).animate({ opacity: 0.75 },{duration: 100,});
            },
            function () {
                $(this).animate({ opacity: 1 },{duration: 100,});
            });

             $("#box6").hover(function () {
                $(this).animate({ opacity: 0.75 },{duration: 100,});
            },
            function () {
                $(this).animate({ opacity: 1 },{duration: 100,});
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="width:830px;height:600px; position:absolute; top:50%; left:50%; margin-left:-400px; margin-top:-300px; border:0px solid black;">
    <div style="width:720px; height:20px; border:0px solid red; margin-bottom:10px; color:White; text-align:right; font-size:16px; font-weight:bold;">Добро пожаловать&nbsp;<asp:Literal runat="server" ID="litUserName"></asp:Literal></div>
    <a href="Check/FJournal.aspx">
        <div style="width:230px; height:230px; border:1px solid white; float:left; background-color:White;" id="box1">
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:10px; margin-top:205px;">Поверка водомеров</span>
        
        <div style="width:100px; height:100px; background-image:url('Images/check.jpeg'); margin-left:65px;margin-top:55px; border:0px solid black;"></div>
        </div>
        </a>
         <a href="#">
        <div style="width:230px; height:230px; border:1px solid black; float:left; margin-left:15px; background-color:#3399ff;"id="box2" >
        <span  style="font-size:16px;position:absolute; font-family:Verdana; margin-left:10px; margin-top:205px;">Установка водомеров</span>     
       
        <div style="width:100px; height:100px; background-image:url('Images/instal.png');margin-left:65px;margin-top:55px;"></div>
        </div>
        </a>
        <a href="#">
        <div style="width:230px; height:230px; border:1px solid black; float:left; margin-left:15px; background-color:#0033cc;"id="box3">
        <span  style="font-size:16px;position:absolute; font-family:Verdana; margin-left:10px; margin-top:205px;">Манометры и СИЗ</span>       
        
        <div style="width:100px; height:100px; background-image:url('Images/siz.png');margin-left:65px;margin-top:55px;"></div>
        </div>
        </a>
        <a href="Admin/Default.aspx">
        <div style="width:230px; height:230px; border:1px solid black; float:left; margin-top:15px; background-color:White;"id="box4">
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:10px; margin-top:205px;">Админ панель</span>
        
        <div style="width:100px; height:100px; background-image:url('Images/admin.jpg');margin-left:65px;margin-top:55px;"></div>
        </div>
        </a>
        

        <a href="WaterPoint/WPList.aspx">
        <div style="width:230px; height:230px; border:1px solid black; float:left; margin-left:15px; margin-top:15px; background-color:#cc0099;"id="box6" >
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:10px; margin-top:205px;">Приборы</span>        
        
        <div style="width:100px; height:100px; background-image:url('');margin-left:65px;margin-top:55px;"></div>
        </div>
        </a>



        <a href="LogOut.aspx">
        <div style="width:230px; height:230px; border:1px solid black; float:left; margin-left:15px; margin-top:15px;; background-color:#ff9933;"id="box5" >
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:10px; margin-top:205px;">Выход</span>        
        
        <div style="width:100px; height:100px; background-image:url('Images/logout-icon.png');margin-left:65px;margin-top:55px;"></div>
        </div>
        </a>
        <!--
          

          

        -->
    </div>
    </form>
</body>
</html>
