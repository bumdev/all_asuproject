using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace outcomming_mail
{
    public partial class OtherMailOutcommingDet : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    OtherMailOutDet1.SendID = Utilities.ConvertToInt(Request["id"]);
                    OtherMailOutDet1.Bind();
                }
            }
        }
    }
}