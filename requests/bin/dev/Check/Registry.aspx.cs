using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Data;

namespace kipia_web_application
{
    public partial class Registry : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                BindSellers();
                BindDiameter();
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
                if(IsEdit())
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        //Привязка производителей
        void BindSellers()
        {
            Seller s = new Seller();
            SellerDO sdo = new SellerDO();
            UniversalEntity ue = new UniversalEntity();
            ue = sdo.RetrieveSellers();
            ddlSeller.Items.Add(new ListItem("Производитель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                s = (Seller)ue[i];
                ddlSeller.Items.Add(new ListItem(s.SellerName, s.ID.ToString()));
            }
        }
        void BindDiameter()
        {
            ddlDiametr.Items.Clear();
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = crdo.RetrieveDiameters();
            ddlDiametr.Items.Add(new ListItem("Диаметр", "-1"));
            ArrayList al;
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlDiametr.Items.Add(new ListItem(al[0].ToString(), al[0].ToString()));
            }
        }

        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (ddlDiametr.SelectedValue == "-1")
            {
                e.Command.Parameters["@Diametr"].Value = DBNull.Value;
            }
            if (ddlSeller.SelectedValue == "0")
            {
                e.Command.Parameters["@Seller"].Value = DBNull.Value;
            }
        }

              
        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["id_seller"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlSeller") as DropDownList).SelectedValue);
            e.NewValues["conventional_signth"] = (gvJournal.Rows[e.RowIndex].FindControl("tbModel") as TextBox).Text;
            e.NewValues["description"] = (gvJournal.Rows[e.RowIndex].FindControl("tbDescription") as TextBox).Text;
        }

        protected void lbTypeAdd_Click(object sender, EventArgs e)
        {
            TypeAdd1.Visible = true;
        }
    }
}