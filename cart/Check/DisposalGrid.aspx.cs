using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;


namespace cartridges_app
{
    public partial class DisposalGrid : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
        }

        private void CheckPermissions()
        {
            User u = GetCurrentUser();
            hfUserID.Value = u.ID.ToString();
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
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
                //CheckPermissions();
            }
        }

        protected void RadGrid_ItemUpdated(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updateItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Order")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.UpdateParameters.Add(new Parameter("Phone", DbType.String, (updateItem["Phone"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("AmountDevice", DbType.String, (updateItem["AmountDevice"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Address", DbType.String, (updateItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateWithdrawal", DbType.DateTime, (updateItem["DateWithdrawal"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateInstallation", DbType.DateTime, (updateItem["DateInstallation"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Comment", DbType.String, (updateItem["Comment"].Controls[0] as TextBox).Text));

                dsJournal.Update();
            }
            e.Item.Edit = false;
        }

        protected void RadGrid_ItemInserted(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            if (e.Item.OwnerTableView.Name == "Order")
            {
                Entities.User u = GetCurrentUser();
                //dsJournal.InsertParameters.Add(new Parameter("ID", DbType.Int32,(insertedItem["ID"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Phone", DbType.String, (insertedItem["Phone"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("AmountDevice", DbType.Int32, (insertedItem["AmountDevice"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateRequests", DbType.DateTime, (insertedItem["DateRequests"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateWithdrawal", DbType.DateTime, (insertedItem["DateWithdrawal"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateInstallation", DbType.DateTime, (insertedItem["DateInstallation"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Address", DbType.String, (insertedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("IsInstallation", DbType.Boolean, (insertedItem["IsInstallation"].Controls[0] as CheckBox).Checked.ToString()));

                dsJournal.Insert();
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void RadGrid_ItemDeleted(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Order")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
            }
        }

        protected void WithdrawalAdd_Click(object sender, EventArgs e)
        {
            //WithdrawalRequests1.Visible = false;
        }

        protected void RadGridWithD_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void gvJournal_OnRowUpdateding(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["Phone"] = (gvJournal.Rows[e.RowIndex].FindControl("tbPhone") as TextBox).Text;
            e.NewValues["AmountDevice"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("tbAmountDevice") as TextBox).Text);
            e.NewValues["Address"] = (gvJournal.Rows[e.RowIndex].FindControl("tbAddress") as TextBox).Text;
            e.NewValues["Comment"] = (gvJournal.Rows[e.RowIndex].FindControl("tbComment") as TextBox);
        }
    }
}