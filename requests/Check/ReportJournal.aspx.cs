using System;
using System.Web.UI;
using Entities;
using System.Data;
using ExcelLibrary.BinaryFileFormat;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using xi = Telerik.Web.UI.ExportInfrastructure;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System;
using System.Web.UI.WebControls;


namespace requests_app
{
    public partial class ReportJournal : ULPage
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
                    window1.NavigateUrl = "check/ArchiveDeviceDet.aspx?id=" + Request["id"];// "FabonentDet.aspx?id=" + Request["id"];
                    window1.VisibleOnPageLoad = true;
                    window1.Width = 800;
                    window1.Height = 600;
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
                radgrid.DataSource = (DataView)dsArchiveJournal.Select(DataSourceSelectArguments.Empty);
            }
        }
        protected void radgridDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Requests")
            {                
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    string itemValue = dataItem["OrderID"].Text;
                    ArchiveDet1.OrderID = Utilities.ConvertToInt(itemValue);  // Convert.ToInt32(e.Item.OwnerTableView.Items[e.]);
                    ArchiveDet1.Bind();
                }
            }          
        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.MasterTableView.ExportToExcel();
        }

    }
}