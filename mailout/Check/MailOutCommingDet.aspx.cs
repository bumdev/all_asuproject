using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace outcomming_mail
{
    public partial class MailOutcommingDet : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    MailOutDet1.SendID = Utilities.ConvertToInt(Request["id"]);
                    MailOutDet1.Bind();
                }
            }
        }
    }
}