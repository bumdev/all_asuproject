using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

namespace invent_app
{
    public partial class Statements : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsRate.Select(DataSourceSelectArguments.Empty);
            }

            /*radgrid.MasterTableView.DetailTables[0].DataSource = (DataView)dsDevices.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[1].DataSource = (DataView)dsEvents.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[0].DetailTables[0].DataSource = (DataView)dsService.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[1].DetailTables[0].DataSource = (DataView)dsEventWork.Select(DataSourceSelectArguments.Empty);*/

        }

        protected void radgrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            if (e.Item.OwnerTableView.Name == "Rate")
            {
                Entities.User u = GetCurrentUser();
                dsRate.InsertParameters.Add(new Parameter("WPID", DbType.Int32, (insertedItem["WP"].Controls[0] as RadComboBox).SelectedValue));
                dsRate.InsertParameters.Add(new Parameter("Rate", DbType.Int32, (insertedItem["Rate"].Controls[0] as TextBox).Text));
                dsRate.InsertParameters.Add(new Parameter("dateIn", DbType.DateTime, (insertedItem["DateIn"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsRate.InsertParameters.Add(new Parameter("userID", DbType.Int32, u.ID.ToString()));

                dsRate.Insert();
                radWM.RadAlert("Показания успешно добавлены.", 300, 200, "", "123");
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Rate")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsRate.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsRate.Delete();
                radWM.RadAlert("Показания были успешно удалены.", 300, 200, "", "123");
            }


            e.Canceled = false;
        }
    }
}