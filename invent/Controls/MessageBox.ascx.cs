using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace invent_app
{
    public partial class MessageBox : ULControl
    {
        public string Header
        {
            set { litHeader.Text = value; }
        }
        public string Message
        {
            set { litMessage.Text = value; }
        }
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

}