using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace requests_app
{
    public partial class ArchiveDeviceDet : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    ArchiveDet1.OrderID = Utilities.ConvertToInt(Request["id"]);
                    ArchiveDet1.Bind();
                }
            }
        }
    }
}