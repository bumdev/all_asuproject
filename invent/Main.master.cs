using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;
using System.Text;

namespace invent_app
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
        protected void lbVodomerAdd_Click(object sender, EventArgs e)
        {

        }
        protected void lbAbonentAdd_Click(object sender, EventArgs e)
        {
            Wizard1.Visible = true;
        }
        private void LoadMenu()
        {
            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            string page = fi.Name;
            List<string> menu = new List<string>();
            menu.Add("<li style=\"\"><a href=\"Invent.aspx\"><span style=\"\">Инвентаризация</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"GroupD.aspx\"><span style=\"\">Группа</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"ModelD.aspx\"><span style=\"\">Модель</span></a></li>");
            //menu.Add("<li style=\"\"><a href=\"DepartamentD.aspx\"><span style=\"\">Отдел</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"TypeD.aspx\"><span style=\"\">Тип инвентаря</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"ManufacturerD.aspx\"><span style=\"\">Производитель</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"InventReports.aspx\"><span style=\"\">ОТЧЕТЫ</span></a></li>");
            
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