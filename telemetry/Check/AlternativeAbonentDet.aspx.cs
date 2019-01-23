using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace leak_detectors
{
    public partial class AlternativeAbonentDet : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    AlternAbonDet1.OrderID = Utilities.ConvertToInt(Request["id"]);
                    AlternAbonDet1.Bind();
                }
            }
        }
    }
}