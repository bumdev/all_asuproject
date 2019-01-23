using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace outcomming_mail
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                tbSearch.Text = "assdfsd";
            tbSearch.Attributes.Add("onKeyUp", "javascript:__doPostBack('" +tbSearch.ClientID + "','')");   
            //tbSearch.Attributes.Add("onfocus", "javascript:setSelectionRange()");
            //SetFocus(tbSearch);
            //Response.Write("asdasdasd");
            //tbSearch.Attributes.Add("onKeyUp", "CallScript(this)");
            
          ScriptManager.RegisterStartupScript(this, this.GetType(), "tmp2", "l()", true);
        }

        protected void tbSearch_TextChanged(object sender, EventArgs e)
        {
            lbSearch.Items.Add(new ListItem("asd", "asd"));           
        }
    }
}