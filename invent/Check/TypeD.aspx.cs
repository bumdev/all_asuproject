using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;

namespace invent_app
{
    public partial class TypeD : ULPage
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
            ok = u.ChekPermission(Permissions.RegisterEditor.ToString());
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
                    tbType.Visible = true;
                    lbAdd.Visible = true;
                }
            }
        }
        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbType.Text))
            {
                SetMessege("Предупреждение", "Производитель не заполнен.");
            }
            else
            {
                // The "Add Order" button is in the sames cell as the GridView and SqlDataSource Controls.
                //DataControlFieldCell controlParent = (DataControlFieldCell)((Button)sender).Parent;
                // Insert the new order
                //SqlDataSource datasource = (SqlDataSource)controlParent.FindControl("dsJournal");
                dsJournal.InsertParameters[0].DefaultValue = tbType.Text.Trim();
                dsJournal.Insert();
                // After the order has been inserted, put the order in edit mode.
                /* GridView gridview = (GridView)controlParent.FindControl("gvJournal");
                 gridview.EditIndex = gridview.Rows.Count;*/
                gvJournal.DataBind();
                tbType.Text = "";
            }
        }
    }
}