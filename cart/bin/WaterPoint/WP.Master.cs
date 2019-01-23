using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;

namespace kipia_web_application
{
    public partial class WP : ULMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User u = GetCurrentUser();
                u.GetPermissions();
                if (!u.ChekPermission(Permissions.WaterPoint.ToString()))
                {
                    Response.Redirect("../Direction.aspx");
                }
            }
        }
    }
}