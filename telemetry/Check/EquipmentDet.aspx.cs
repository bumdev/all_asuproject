using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace leak_detectors
{
    public partial class EquipmentDetP : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id"] != null)
                {
                    EquipDet1.OrderID = Utilities.ConvertToInt(Request["id"]);
                    EquipDet1.Bind();
                }
            }
        }
    }
}