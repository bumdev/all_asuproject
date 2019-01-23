using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;
using System.Text;

namespace Timetable_WebApp
{
    public partial class Main : ULMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserName();
            LoadMenu();
        }
        private void GetUserName()
        {
            User u = GetCurrentUser();
            litUserName.Text = "Пользователь " + u.UserName;
        }
        private void LoadMenu()
        {
            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            string page = fi.Name;
            List<string> menu = new List<string>();
            menu.Add("<li style=\"\"><a href=\"Timetable.aspx\"><span style=\"\">Табель</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"ServiceAccept.aspx\"><span style=\"\">Подтверждение по службам</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"FiredJournal.aspx\"><span style=\"\">Уволенные</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"FiredControl.aspx\"><span style=\"\">Контроль по службам (уволенные)</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"ServiceReport.aspx\"><span style=\"\">ОТЧЕТЫ</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Admin/AdminLogOut.aspx\"><span style=\"\">Админ панель</span></a></li>");
           
            
            StringBuilder sb = new StringBuilder();
            foreach (string s in menu)
            {
                if (s.ToLower().Contains(page.ToLower()))
                {
                    string t = s;
                    t = t.Replace("li style=\"\"", "li style=\"background-color:LightSteelBlue   ;\"");
                    t = t.Replace("span style=\"\"", "span style=\"color:white;\"");
                    sb.Append(t);
                }
                else
                {
                    sb.Append(s);
                }
            }
            litMenu.Text = sb.ToString();
        }
    }
}