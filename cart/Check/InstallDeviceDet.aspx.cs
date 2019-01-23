using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cartridges_app
{
    public partial class InstallDeviceDet : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    InstallDet1.OrderID = Utilities.ConvertToInt(Request["id"]);
                    InstallDet1.Bind();
                }
            }
        }
    }
}