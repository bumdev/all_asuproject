using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace requests_app
{
    public partial class RemoveDeviceDet : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    RemoveDet1.OrderID = Utilities.ConvertToInt(Request["id"]);
                    RemoveDet1.Bind();
                }
            }
        }
    }
}