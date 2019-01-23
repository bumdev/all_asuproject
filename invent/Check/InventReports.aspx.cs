using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainObjects;
using Entities;
using Telerik.Web.UI;

namespace invent_app
{
    public partial class InventReports : ULPage
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
                /*if (IsEdit())
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;*/
            }
        }

        protected void radgridExport_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                radgrid.ExportSettings.ExportOnlyData = true;
                radgrid.ExportSettings.OpenInNewWindow = true;
                radgrid.ExportSettings.IgnorePaging = true;
                radgrid.MasterTableView.ExportToExcel();
            }
        }

        protected void ExportButton_OnClick(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.MasterTableView.ExportToExcel();
        }
        
        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radgrid_OnDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                RadComboBox rcb1 = (RadComboBox)editItem["ti"].Controls[0];
                RadComboBox rcb2 = (RadComboBox)editItem["dp"].Controls[0];
                RadComboBox rcb3 = (RadComboBox)editItem["md"].Controls[0];
                RadComboBox rcb4 = (RadComboBox)editItem["gp"].Controls[0];
                RadComboBox rcb5 = (RadComboBox)editItem["dis"].Controls[0];
                rcb1.Width = Unit.Pixel(450);
                rcb1.Filter = RadComboBoxFilter.Contains;
                rcb2.Width = Unit.Pixel(450);
                rcb2.Filter = RadComboBoxFilter.Contains;
                rcb3.Width = Unit.Pixel(450);
                rcb3.Filter = RadComboBoxFilter.Contains;
                rcb4.Width = Unit.Pixel(450);
                rcb4.Filter = RadComboBoxFilter.Contains;
                rcb5.Width = Unit.Pixel(450);
                rcb5.Filter = RadComboBoxFilter.Contains;
            }
        }
    }
}