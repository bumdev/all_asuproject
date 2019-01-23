using System;
using System.Web.UI;
using Entities;
using System.Data;
using ExcelLibrary.BinaryFileFormat;
using Telerik.Web.UI;


namespace leak_detectors
{
    public partial class Equipment : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Response.Write(Request.ApplicationPath);
                CheckLogin();
                if (Request["id"] != null)
                {
                    RadWindow window1 = new RadWindow();
                    window1.NavigateUrl = "check/EquipmentDet.aspx?id=" + Request["id"];// "FabonentDet.aspx?id=" + Request["id"];
                    window1.VisibleOnPageLoad = true;
                    window1.Width = 1000;
                    window1.Height = 700;
                    Page.Controls.Add(window1);
                }
            }
        }
        private void CheckPermissions()
        {
            User u = GetCurrentUser();
            hfUserID.Value = u.ID.ToString();
        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                CheckPermissions();

            }
        }
        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsEquipment.Select(DataSourceSelectArguments.Empty);
            }
        }
        protected void radgridDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ShowEquipment")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    string itemValue = dataItem["OrderID"].Text;
                    EquipDet1.OrderID = Utilities.ConvertToInt(itemValue);  // Convert.ToInt32(e.Item.OwnerTableView.Items[e.]);
                    EquipDet1.Bind();
                }
            }
        }
    }
}