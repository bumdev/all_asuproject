using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Entities;
using DomainObjects;
using System.Data;
using Juice;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Excel;
using ExcelLibrary;
using Telerik.Windows.Documents.Core;

namespace cartridges_app
{
    public partial class ForReports_1 : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
        }

        protected void ExcelImageExport_OnClick(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.MasterTableView.ExportToExcel();
        }

        protected void radgridBiff_ExportingBiff(object sender, GridBiffExportingEventArgs e)
        {
            e.ExportStructure.Tables[0].Columns[1].Style.BackColor = Color.LightGray;
        }

        protected void FilterComboBox_SelectedChaged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string filterExpression;
            filterExpression = "([Cartname] = '" + e.Value + "')";
            radgrid.MasterTableView.FilterExpression = filterExpression;
            radgrid.MasterTableView.Rebind();
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


        protected void OnSelectedChangedHandler(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["TypeCrtridgesID"] = e.Value;
        }


        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void ImageButton_OnClick(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.MasterTableView.ExportToExcel();
        }

        protected void radgridExport_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                radgrid.ExportSettings.ExportOnlyData = true;
                radgrid.ExportSettings.IgnorePaging = true;
                radgrid.ExportSettings.OpenInNewWindow = true;
                //radgrid.ExportSettings.FileName = "RadGridExport.xls";
                radgrid.MasterTableView.ExportToExcel();
            }
        }

        protected void radgridDocX_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToWordCommandName)
            {
                radgrid.ExportSettings.FileName = "export";
                radgrid.MasterTableView.ExportToWord();
            }
        }

        protected void ImageDocX_OnClick(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.HideStructureColumns = true;
            radgrid.ExportSettings.IgnorePaging = true;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.MasterTableView.ExportToWord();
        }

        public void ConfigureExport()
        {
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.IgnorePaging = true;
        }

        protected void radgridAll_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName || e.CommandName == RadGrid.ExportToCsvCommandName ||
                e.CommandName == RadGrid.ExportToWordCommandName)
            {
                ConfigureExport();
            }
        }

        protected void radtoolbarbutton_ItemdCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ExportToExcel")
            {
                ExportGrid();
            }
        }

        protected void rtbCommand_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Value == "Export")
            {
                ExportGrid();
            }
        }

        protected void ExportGrid()
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.IgnorePaging = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.FileName = "radgridexport";
            radgrid.MasterTableView.ExportToExcel();
        }

        protected void radgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                RadComboBox comboBox = (RadComboBox)editItem["cd"].Controls[0];
                comboBox.Width = Unit.Pixel(450);
                RadComboBox comboBox1 = (RadComboBox)editItem["tp"].Controls[0];
                comboBox1.Width = Unit.Pixel(450);
                RadComboBox comboBox2 = (RadComboBox)editItem["sl"].Controls[0];
                comboBox2.Width = Unit.Pixel(450);
                comboBox.Filter = RadComboBoxFilter.Contains;
                comboBox1.Filter = RadComboBoxFilter.Contains;
                comboBox2.Filter = RadComboBoxFilter.Contains;
                TextBox textBox = (TextBox)editItem["Information"].Controls[0];
                textBox.Width = Unit.Pixel(450);
                textBox.Height = Unit.Pixel(100);
                textBox.TextMode = TextBoxMode.MultiLine;
            }
        }
    }
}