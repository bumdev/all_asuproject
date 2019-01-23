using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Text;

namespace cartridges_app
{
    public partial class DisposalAdd : ULControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindSellers();
            }
        }
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        //Привязка производителей

    }
}