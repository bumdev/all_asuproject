using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClericalWork_WebApp.Admin
{
    public partial class AdminLogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["Aadminname"].Expires = DateTime.Now;
            Response.Cookies["adminpass"].Expires = DateTime.Now;
            Response.Redirect("Default.aspx");
        }
    }
}