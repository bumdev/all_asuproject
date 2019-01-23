using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Entities;

namespace ClericalWork_WebApp
{
    public partial class Complaints : ULPage
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

            return ok;

        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("Home.aspx");
            }

        }

        protected void radgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                CheckBox is_add = (CheckBox) editItem["IsAddContr"].Controls[0];
                is_add.Visible = true;
                is_add.Enabled = true;
                is_add.Checked = false;
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                CheckBox is_add = (CheckBox) item["IsAddContr"].Controls[0];
                is_add.Visible = true;
                is_add.Enabled = true;
                is_add.Checked = false;
            }
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

            if (e.Item.OwnerTableView.Name == "Complaints")
            {
                Entities.User u = GetCurrentUser();

                dsJournal.InsertParameters.Add(new Parameter("IsAddContr", DbType.Boolean, (insertedItem["IsAddContr"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("IsExecContr", DbType.Boolean, (insertedItem["IsExecContr"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("RegNumber", DbType.String, (insertedItem["RegNumber"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("DateComplaints", DbType.DateTime, (insertedItem["date_comp"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("NameClient", DbType.String, (insertedItem["NameClient"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("AddressClient", DbType.String, (insertedItem["AddressClient"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DistrictID", DbType.Int32, (insertedItem["dis"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("PlaneDate", DbType.DateTime, (insertedItem["date_plane"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("OutcommingNumber", DbType.String, (insertedItem["OutcommingNumber"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateExec", DbType.DateTime, (insertedItem["date_exec"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("FromSentComplaints", DbType.String, (insertedItem["FromSentComplaints"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("CodeComplaintsJuridical", DbType.String, (insertedItem["CodeComplaintsJuridical"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateFrom", DbType.DateTime, (insertedItem["date_from"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("AppointedTime", DbType.DateTime, (insertedItem["appoint_date"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Category", DbType.String, (insertedItem["Category"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("ResponsibleServicesID", DbType.Int32, (insertedItem["rs"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("NumberOrderMail", DbType.String, (insertedItem["NumberOrderMail"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateOrderMail", DbType.DateTime, (insertedItem["date_mail"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("ArchiveNumberOrder", DbType.String, (insertedItem["ArchiveNumberOrder"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateAdditionalControl", DbType.DateTime, (insertedItem["date_contradd"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateExecAddControl", DbType.DateTime, (insertedItem["date_execadd"].Controls[0] as RadDatePicker).SelectedDate.ToString()));

                

                dsJournal.Insert();
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Complaints")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
            }


            e.Canceled = false;
        }


        protected void radgridServices_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Complaints")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));

                dsJournal.UpdateParameters.Add(new Parameter("IsAddContr", DbType.Boolean, (updatedItem["IsAddContr"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("IsExecContr", DbType.Boolean, (updatedItem["IsExecContr"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("RegNumber", DbType.String, (updatedItem["RegNumber"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateComplaints", DbType.DateTime, (updatedItem["date_comp"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("NameClient", DbType.String, (updatedItem["NameClient"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("AddressClient", DbType.String, (updatedItem["AddressClient"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DistrictID", DbType.Int32, (updatedItem["dis"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("PlaneDate", DbType.DateTime, (updatedItem["date_plane"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("OutcommingNumber", DbType.String, (updatedItem["OutcommingNumber"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateExec", DbType.DateTime, (updatedItem["date_exec"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("FromSentComplaints", DbType.String, (updatedItem["FromSentComplaints"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("CodeComplaintsJuridical", DbType.String, (updatedItem["CodeComplaintsJuridical"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateFrom", DbType.DateTime, (updatedItem["date_from"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("AppointedTime", DbType.DateTime, (updatedItem["appoint_date"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Category", DbType.String, (updatedItem["Category"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("ResponsibleServicesID", DbType.Int32, (updatedItem["rs"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("NumberOrderMail", DbType.String, (updatedItem["NumberOrderMail"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateOrderMail", DbType.DateTime, (updatedItem["date_mail"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("ArchiveNumberOrder", DbType.String, (updatedItem["ArchiveNumberOrder"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateAdditionalControl", DbType.DateTime, (updatedItem["date_contradd"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("DateExecAddControl", DbType.DateTime, (updatedItem["date_execadd"].Controls[0] as RadDatePicker).SelectedDate.ToString()));

                dsJournal.Update();

            }
            e.Item.Edit = false;
            radgrid.Rebind();
        }


        protected void radgrid_itemcommand(object sender, GridCommandEventArgs e)
        {

        }
    }
}
