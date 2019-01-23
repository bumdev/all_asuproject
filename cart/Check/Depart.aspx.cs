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
    public partial class Depart : ULPage
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
                    tbDepartName.Visible = true;
                    lbAdd.Visible = true;
                }
            }
        }
        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbDepartName.Text))
            {
                SetMessege("Предупреждение", "Производитель не заполнен.");
            }
            else
            {
                dsJournal.InsertParameters[0].DefaultValue = tbDepartName.Text.Trim();
                dsJournal.Insert();
                gvJournal.DataBind();
                tbDepartName.Text = "";
            }
        }
    }
}