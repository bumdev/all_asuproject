using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik;
using Telerik.Web.UI;
using System.Data;

namespace requests_app
{
    public partial class DeviceManager : ULControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsDevices.Select(DataSourceSelectArguments.Empty);
            }
            //radgrid.MasterTableView.DetailTables[0].DataSource = (DataView)dsWP.Select(DataSourceSelectArguments.Empty);
        }

        protected void radgrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Attach")
            {
                if (Session["WPID"] != null)
                {
                    Entities.User u = GetCurrentUser();
                    //Response.Write(Session["WPID"].ToString());               
                    dsDevices.InsertParameters.Add(new Parameter("WPID", DbType.Int32, Session["WPID"].ToString()));
                    dsDevices.InsertParameters.Add(new Parameter("DeviceID", DbType.Int32, e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    //dsDevices.InsertParameters.Add(new Parameter("DateIn", DbType.String, DateTime.Now.ToString()));
                    dsDevices.InsertParameters.Add(new Parameter("UserID", DbType.Int32, u.ID.ToString()));
                    dsDevices.Insert();
                    //radWM.RadAlert("Устройство было успешно добавлено.", 300, 200, "", "123");
                    radgrid.Rebind();
                }
            }
            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = false;
                    }
                }
            }
        }

    }
}