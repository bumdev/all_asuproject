using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;
using System.Text;

namespace cartridges_app
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
            litUserName.Text = "Вы зашли под учетной записью " + u.UserName;
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
           // menu.Add("<li style=\"\"><a href=\"RemoveJournal.aspx\"><span style=\"\">Снятие</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Cartridges.aspx\"><span style=\"\">Картриджи</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"Depart.aspx\"><span style=\"\">Справочник отделов</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"TypeCart.aspx\"><span style=\"\">Справочник моделей картриджа</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"CartNameDirect.aspx\"><span style=\"\">Справочник картриджей</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"ForReports_1.aspx\"><span style=\"\">ОТЧЕТЫ</span></a></li>");
            //menu.Add("<li style=\"\"><a href=\"ForReports.aspx\"><span style=\"\">ОТЧЕТЫ</span></a></li>");
            /*menu.Add("<li style=\"\"><a href=\"InstallJournal.aspx\"><span style=\"\">Установка</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"CommonJournal.aspx\"><span style=\"\">Общий учет</span></a></li>");
            menu.Add("<li style=\"\"><a href=\"ArchiveJournal.aspx\"><span style=\"\">Архив</span></a></li>");*/
            //menu.Add("<li style=\"\"><a href=\"DisposalGrid.aspx\"><span style=\"\">Снятие#3</span></a></li>");
           // menu.Add("<li style=\"\"><a href=\"AddRequests.aspx\"><span style=\"\">Добавить заявку</span></a></li>");
           

            StringBuilder sb = new StringBuilder();
            foreach (string s in menu)
            {
                if (s.ToLower().Contains(page.ToLower()))
                {
                    string t = s;
                    t = t.Replace("li style=\"\"", "li style=\"background-color:LightSteelBlue   ;\"");
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