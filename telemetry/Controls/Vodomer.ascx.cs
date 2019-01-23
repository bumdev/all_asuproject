using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace leak_detectors
{
    public partial class Controls_Vodomer : ULControl
    {
        bool _IsActive = false;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        protected void lbCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}