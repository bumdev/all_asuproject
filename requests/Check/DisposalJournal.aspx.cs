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
using Telerik.Web.UI;

namespace requests_app
{
    public partial class DisposalJournal : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                /*BindSellers();
                BindDiameter();*/
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
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        //Привязка производителей


        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }

        /*protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (ddlDiametr.SelectedValue == "-1")
            {
                e.Command.Parameters["@Diametr"].Value = DBNull.Value;
            }
            if (ddlSeller.SelectedValue == "0")
            {
                e.Command.Parameters["@Seller"].Value = DBNull.Value;
            }
        }*/


        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //e.NewValues["id_seller"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlSeller") as DropDownList).SelectedValue);
            e.NewValues["Phone"] = (gvJournal.Rows[e.RowIndex].FindControl("tbPhone") as TextBox).Text;
            e.NewValues["AmountDevice"] = (gvJournal.Rows[e.RowIndex].FindControl("tbAmountDevice") as TextBox).Text;
            e.NewValues["Address"] = (gvJournal.Rows[e.RowIndex].FindControl("tbAddress") as TextBox).Text;
        }

        protected void lbDisposalAdd_Click(object sender, EventArgs e)
        {
            //DisposalAdd1.Visible = true;
        }



        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
            }
        }
        protected void radgrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            if (e.Item.OwnerTableView.Name == "Disposal")
            {
                Entities.User u = GetCurrentUser();

                dsJournal.InsertParameters.Add(new Parameter("Phone", DbType.String, (insertedItem["Phone"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("AmountDevice", DbType.Int32, (insertedItem["AmountDevice"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Address", DbType.String, (insertedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateWithdrawal", DbType.DateTime, (insertedItem["DateWithdrawal"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateInstallation", DbType.DateTime, (insertedItem["DateInstallation"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("IsInstallation", DbType.Boolean, (insertedItem["IsInstallation"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));


                /*dsJournal.InsertParameters.Add(new Parameter("WPID", DbType.Int32, (insertedItem["WP"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("Rate", DbType.Int32, (insertedItem["Rate"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("dateIn", DbType.DateTime, (insertedItem["DateIn"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("userID", DbType.Int32, u.ID.ToString()));*/

                dsJournal.Insert();
                //radWM.RadAlert("Показания успешно добавлены.", 300, 200, "", "123");
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Disposal")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
                //radWM.RadAlert("Показания были успешно удалены.", 300, 200, "", "123");
            }


            e.Canceled = false;
        }
        protected void radgridDevice_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Disposal")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.InsertParameters.Add(new Parameter("Phone", DbType.String, (updatedItem["Phone"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("AmountDevice", DbType.Int32, (updatedItem["AmountDevice"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Address", DbType.String, (updatedItem["Address"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("DateWithdrawal", DbType.DateTime, (updatedItem["DateWithdrawal"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("DateInstallation", DbType.DateTime, (updatedItem["DateInstallation"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("IsInstallation", DbType.Boolean, (insertedItem["IsInstallation"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));



                //dsJournal.UpdateParameters.Add(new Parameter("ServiceTypeID", DbType.Int32, (updatedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("dateIn", DbType.DateTime, (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");

            }
            e.Item.Edit = false;
            // e.Canceled = true;
            //radgrid.Rebind();
        }
    }
}