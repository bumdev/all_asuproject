using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;
using System.Text;

namespace outcomming_mail
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
            litUserName.Text = "Сеанс " + u.UserName;
        }
        protected void lbVodomerAdd_Click(object sender, EventArgs e)
        {

        }
        /*protected void lbAbonentAdd_Click(object sender, EventArgs e)
        {
            Wizard1.Visible = true;
        }*/

        protected void lbMailAdd_Click(object sender, EventArgs e)
        {
            Wizard1.Visible = true;
        }
        private void LoadMenu()
        {
            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            string page = fi.Name;
            List<string> menu = new List<string>();
            menu.Add("<li style=\"\"><a href=\"MailOutJournal.aspx\"><span style=\"\">Корреспонденция физ. лица</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"PhysicalMailOutJournal.aspx\"><span style=\"\">Корреспонденция юр. лица</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"OtherMailOutJournal.aspx\"><span style=\"\">Прочая корреспонденция</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"AddMail.aspx\"><span style=\"\">Добавление почты</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Report.aspx\"><span style=\"\">Отчеты</span></a></li>");
            /* menu.Add("<li style=\"\"><a href=\"FJournal.aspx\"><span style=\"\">Журнал физ. лиц</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"UJournal.aspx\"><span style=\"\">Журнал юр. лиц</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"SJournal.aspx\"><span style=\"\">Снятие/установка</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"AddAbonent.aspx\"><span style=\"\">Принять абонента</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Report.aspx\"><span style=\"\">Отчеты</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Registry.aspx\"><span style=\"\">Реестр водомеров</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Sellers.aspx\"><span style=\"\">Производители</span></a></li>");*/

            StringBuilder sb = new StringBuilder();
            foreach (string s in menu)
            {
                if (s.ToLower().Contains(page.ToLower()))
                {
                    string t = s;
                    t = t.Replace("li style=\"\"", "li style=\"background-color:LightSteelBlue;\"");
                    t = t.Replace("span style=\"\"", "span style=\"color:black;\"");
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