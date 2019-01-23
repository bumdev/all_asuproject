using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;

namespace invent_app
{
    public partial class Direction : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            User u = GetCurrentUser();
            litUserName.Text = u.UserName;
        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}