<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Direction.aspx.cs" Inherits="invent_app.Direction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link href="Css/test.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#box1").hover(function () {
                $(this).animate({ opacity: 0.75, }, { duration: 100, });
            },
            function () {
                $(this).animate({ opacity: 1 }, { duration: 100, });
            });

            $("#box2").hover(function () {
                $(this).animate({ opacity: 0.75 }, { duration: 100, });
            },
            function () {
                $(this).animate({ opacity: 1 }, { duration: 100, });
            });

            $("#box3").hover(function () {
                $(this).animate({ opacity: 0.75 }, { duration: 100, });
            },
            function () {
                $(this).animate({ opacity: 1 }, { duration: 100, });
            });

            $("#box4").hover(function () {
                $(this).animate({ opacity: 0.75 }, { duration: 100, });
            },
            function () {
                $(this).animate({ opacity: 1 }, { duration: 100, });
            });

            $("#box5").hover(function () {
                $(this).animate({ opacity: 0.75 }, { duration: 100, });
            },
            function () {
                $(this).animate({ opacity: 1 }, { duration: 100, });
            });

            $("#box6").hover(function () {
                $(this).animate({ opacity: 0.75 }, { duration: 100, });
            },
           function () {
               $(this).animate({ opacity: 1 }, { duration: 100, });
           });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="width:830px;height:600px; position:absolute; top:50%; left:50%; margin-left:-400px; margin-top:-300px; border:0px solid black;">
    <div style="width:720px; height:20px; border:0px solid red; margin-bottom:10px; color:white; text-align:right; font-size:16px; font-weight:bold;">Приветствую,&nbsp;<asp:Literal runat="server" ID="litUserName"></asp:Literal></div>
    <a href="Check/Invent.aspx">
        <div style="width:230px; height:230px; border:1px solid white; float:left; margin-top: 15px; margin-right: 15px; background-color:White; border-radius: 250px;" id="box1">
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:50px; margin-top:250px; color: white;">Инвентаризация</span>
        
        <div style="width:128px; height:128px; background-image:url('Images/desktop.png'); margin-left:50px;margin-top:50px; border:0px solid black;"></div>
        </div>
        </a>
       
         
        
        <a href="Admin/Default.aspx">
        <div style="width:230px; height:230px; border:0px solid black; float:left; margin-top:15px; background-color:White; border-radius: 250px;"id="box4">
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:55px; margin-top:250px; color: white;">Администратор</span>
        
        <div style="width:128px; height:128px; background-image:url('Images/employee.png');margin-left:50px;margin-top:50px;"></div>
        </div>
        </a>
        

        


        <a href="LogOut.aspx">
        <div style="width:230px; height:230px; border:0px solid black; float:left; margin-left:15px; margin-top:15px;; background-color:white; border-radius: 250px;"id="box5" >
        <span style="font-size:16px;position:absolute; font-family:Verdana; margin-left:90px; margin-top:250px; color: white;">Выход</span>        
        
        <div style="width:128px; height:128px; background-image:url('Images/exit (5).png');margin-left:55px;margin-top:55px;"></div>
        </div>
        </a>
        <!--
          

          

        -->
    </div>
    </form>
</body>
</html>
