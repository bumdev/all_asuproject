using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;

namespace cartridges_app
{
    public partial class CartNameDirect : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
        }
        public bool IsEdit()
        {
            bool ok = false;
            User u = GetCurrentUser();
            u.GetPermissions();
            //ok = u.ChekPermission(Permissions.RegisterEditor.ToString());
            return ok;
        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (IsEdit())
                {
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
                    tbCartName.Visible = true;
                    lbAdd.Visible = true;
                }
            }
        }
        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCartName.Text))
            {
                SetMessege("Предупреждение", "Производитель не заполнен.");
            }
            else
            {
                dsJournal.InsertParameters[0].DefaultValue = tbCartName.Text.Trim();
                dsJournal.Insert();
                gvJournal.DataBind();
                tbCartName.Text = "";
            }
        }
    }
}