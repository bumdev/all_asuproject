using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Timetable_WebApp
{
    public partial class admin_Default : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbLogin_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == ConfigurationManager.AppSettings.Get("AdminLogin") && tbPassword.Text == ConfigurationManager.AppSettings.Get("AdminPassword"))
            {
                HttpCookie name = new HttpCookie("adminname");
                HttpCookie pass = new HttpCookie("adminpass");

                name.Value = tbLogin.Text;
                pass.Value = tbPassword.Text;

                Response.Cookies.Add(name);
                Response.Cookies.Add(pass);

                name.Expires = DateTime.Now.AddYears(1);
                pass.Expires = DateTime.Now.AddYears(1);


                Response.Redirect("Panel.aspx");
            }
            else
            {
                litInfo.Text = "Логин и/или пароль не верны.";
            }
        }
    }
}