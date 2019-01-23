using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using WindowsInput;
using WindowsInput.Native;
using AjaxControlToolkit;
using Entities;
using DomainObjects;
using System.Data;
using Microsoft.Office.Interop.Excel;
using Telerik.Web.Spreadsheet;
using Telerik.Web.UI;
using Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using Telerik.Windows.Documents.Core.TextMeasurer;
using Telerik.Windows.Documents.Primitives;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using BorderStyle = System.Web.UI.WebControls.BorderStyle;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Convert = System.Convert;
using Parameter = System.Web.UI.WebControls.Parameter;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace Timetable_WebApp
{
    public partial class Timetable : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
        }

        private void CheckPermission()
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
                //GetCurrentUser().SerivceID;
            }
            else
            {
                CheckPermission();
            }
        }

        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radgrid_ExportCellStyles(object sender, GridExportExcelMLStyleCreatedArgs e)
        {
            //StyleElement style = new StyleElement("myStyle");
            //style.AlignmentElement.Attributes.Add("ss:WrapText", "1");
            //e.Styles.Add(style);
        }

        protected void radgrid_ExportRow(object sender, GridExportExcelMLRowCreatedArgs e)
        {
            //if (e.RowType == GridExportExcelMLRowType.DataRow)
            //{
            //    foreach (GridColumn col in radgrid.MasterTableView.Columns)
            //    {
            //        CellElement xcell = e.Row.Cells.GetCellByName(col.UniqueName);
            //        xcell.StyleValue = "myStyle";
            //        e.Row.Attributes.Add("ss:AutoFitHeight", 1);
            //    }
            //}
        }

        protected void radgrid_InfrastructureExporting(object sender, GridInfrastructureExportingEventArgs e)
        {
            //Telerik.Web.UI.ExportInfrastructure.Table tbl = e.ExportStructure.Tables[0];
            //tbl.Columns[2].Width = 225;
            //for (int i = 1; i <= tbl.Rows.Count; i++)
            //{
            //    var cell1 = tbl.Cells[2, i];
            //    cell1.TextWrap = true;
            //}
            var colCount = e.ExportStructure.Tables[0].Columns.Count;
            ExportStyle headerStyle = new ExportStyle();
            for (var i = 1; i <= colCount; i++)
            {
                //e.ExportStructure.Tables[0].Cells[i, 1].Style = headerStyle;
                e.ExportStructure.Tables[0].Columns[i].Width = 200;
                e.ExportStructure.Tables[0].Cells[i, 1].TextWrap = true;
            }

        }

        protected void radgrid_ExportCellFormating(object sender, ExportCellFormattingEventArgs e)
        {

        }

        protected void ReportExcel_OnClick(object sender, ImageClickEventArgs e)
        {
            this.radgrid.MasterTableView.GetColumn("sv").Display = false;
            this.radgrid.MasterTableView.GetColumn("days_check").Display = false;
            this.radgrid.MasterTableView.GetColumn("hours_check").Display = false;
            this.radgrid.MasterTableView.GetColumn("date_s").Display = false;
            this.radgrid.MasterTableView.GetColumn("date_e").Display = false;
            this.radgrid.MasterTableView.GetColumn("is_hr").Display = false;
            this.radgrid.MasterTableView.GetColumn("is_plan").Display = false;
            this.radgrid.MasterTableView.GetColumn("is_bill").Display = false;
            this.radgrid.MasterTableView.GetColumn("EditCommandColumn").Display = false;
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.Excel.DefaultCellAlignment = HorizontalAlign.Center;
            radgrid.ExportSettings.IgnorePaging = true;
            radgrid.ExportSettings.FileName = "Табель";
            //radgrid.ExportSettings.Excel
            radgrid.MasterTableView.ExportToExcel();
        }

        protected void Duplicate_OnClick(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string name = "";
            string number = "";
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                number = item["number"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (name == "")
                return;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name and number=@num";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@num", number);
                int emp_id = (int)cmd.ExecuteScalar();
                sql = "insert into Main_table(service_id, employee_id, " +
                      "d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16, d17, d18, d19, d20, d21, d22, d23, d24, d25, d26, d27, d28, d29, d30, d31, " +
                      "graph_hours_count, work_days, work_hours, companions, all_hours, rem_auto, nights, evenings, graph_hours_party, weekends, " +
                      "code7, code8, code26, code27, code11, code12, code13, code18, code14, code16, code22, code24, code28, code29, code30, code45, code_none, date_start, date_end, UserID, IsBilling, IsHR, IsPlanning, IsEdit) " +
                      "select service_id, employee_id, " +
                      "d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16, d17, d18, d19, d20, d21, d22, d23, d24, d25, d26, d27, d28, d29, d30, d31, " +
                      "graph_hours_count, work_days, work_hours, companions, all_hours, rem_auto, nights, evenings, graph_hours_party, weekends, " +
                      "code7, code8, code26, code27, code11, code12, code13, code18, code14, code16, code22, code24, code28, code29, code30, code45, code_none, date_start, date_end, UserID, IsBilling, IsHR, IsPlanning, @edit " +
                      "from Main_table where employee_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", emp_id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.Parameters.AddWithValue("@edit", 0);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void DeleteDuplicate_OnClick(object sender, EventArgs e)
        {

        }

        protected void HR_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string name = "";
            string service = "";
            string number = "";
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                service = item["sv"].Text;
                number = item["number"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (name == "")
                return;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name and number=@num";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@num", number);
                int id = (int)cmd.ExecuteScalar();
                sql = "update Main_table set IsHR=1 where employee_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "select id from services where service_name=@name";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service);
                id = (int)cmd.ExecuteScalar();
                sql = "update ServiceAccept set is_chief=0, is_hrwrong=1 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void Bill_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string name = "";
            string service = "";
            string number = "";
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                service = item["sv"].Text;
                number = item["number"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (name == "")
                return;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name and number=@num";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@num", number);
                int id = (int)cmd.ExecuteScalar();
                sql = "update Main_table set IsBilling=1 where employee_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "select id from services where service_name=@name";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service);
                id = (int)cmd.ExecuteScalar();
                sql = "update ServiceAccept set is_chief=0, is_billwrong=1 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void Plan_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string name = "";
            string service = "";
            string number = "";
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                service = item["sv"].Text;
                number = item["number"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (name == "")
                return;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name and number=@num";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@num", number);
                int id = (int)cmd.ExecuteScalar();
                sql = "update Main_table set IsPlanning=1 where employee_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "select id from services where service_name=@name";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service);
                id = (int)cmd.ExecuteScalar();
                sql = "update ServiceAccept set is_chief=0, is_planwrong=1 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void Corrected_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string name = "";
            string number = "";
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                number = item["number"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (name == "")
                return;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name and number=@num";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@num", number);
                int id = (int)cmd.ExecuteScalar();
                sql = "update Main_table set IsHR=0, IsPlanning=0, IsBilling=0 where employee_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }





        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Timetable")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
                //radWM.RadAlert("Показания были успешно удалены.", 300, 200, "", "123");
            }

            e.Canceled = false;
        }
        
        protected void radgrid_OnDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                var dropDown = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                var totalcount = ((GridPagerItem)e.Item).Paging.DataSourceCount;
                var size = new Dictionary<string, string>()
                {
                    {"10", "10"},
                    {"20", "20"},
                    {"50", "50"}
                };
                size.Add("Все", totalcount.ToString());
                dropDown.Items.Clear();
                foreach (var siz in size)
                {
                    var cboItem = new RadComboBoxItem() { Text = siz.Key, Value = siz.Value };
                    cboItem.Attributes.Add("ownerTableViewId", e.Item.OwnerTableView.ClientID);
                    dropDown.Items.Add(cboItem);
                }
                dropDown.Width = Unit.Pixel(100);
                dropDown.FindItemByValue(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                TextBox ep = (TextBox)editItem["ep"].Controls[0];
                ep.Enabled = false;
                ep.Font.Bold = true;
                RadComboBox rbsv = (RadComboBox)editItem["sv"].Controls[0];
                rbsv.Width = Unit.Pixel(450);
                rbsv.Filter = RadComboBoxFilter.Contains;
                rbsv.Visible = false;
                RadDatePicker rdpBegin = (RadDatePicker)editItem["date_s"].Controls[0];
                rdpBegin.Visible = false;
                RadDatePicker rdpEnd = (RadDatePicker)editItem["date_e"].Controls[0];
                rdpEnd.Visible = false;
                CheckBox days_ch = (CheckBox)editItem["days_check"].Controls[0];
                CheckBox hours_ch = (CheckBox)editItem["hours_check"].Controls[0];
                ImageButton imageUpdate = (ImageButton)editItem.FindControl("UpdateButton");
                TextBox value_d = (TextBox)editItem["value_d"].Controls[0];
                value_d.Width = Unit.Pixel(50);
                TextBox range_1 = (TextBox)editItem["range_1"].Controls[0];
                range_1.Width = Unit.Pixel(50);
                TextBox range_2 = (TextBox)editItem["range_2"].Controls[0];
                range_2.Width = Unit.Pixel(50);
                TextBox range_d = (TextBox) editItem["range_d"].Controls[0];
                range_d.Visible = false;
                range_d.ReadOnly = true;
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem) e.Item;
                CheckBox is_hr = (CheckBox) item["is_hr"].Controls[0];
                if (is_hr.Checked)
                {
                    TableCell tc = (TableCell) item["is_hr"];
                    tc.BackColor = Color.Red;
                    item.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell) item["is_hr"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_plan = (CheckBox) item["is_plan"].Controls[0];
                if (is_plan.Checked)
                {
                    TableCell tc = (TableCell) item["is_plan"];
                    tc.BackColor = Color.Red;
                    item.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell) item["is_plan"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_bill = (CheckBox) item["is_bill"].Controls[0];
                if (is_bill.Checked)
                {
                    TableCell tc = (TableCell) item["is_bill"];
                    tc.BackColor = Color.Red;
                    item.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell) item["is_bill"];
                    tc.BackColor = Color.White;
                }
                //зачистка нулей в гриде
                string strVal7 = item["code7"].Text.ToString();
                if ((strVal7 == "0") || (strVal7 == null))
                {
                    item["code7"].Text = " ";
                }
                string strVal8 = item["code8"].Text.ToString();
                if ((strVal8 == "0") || (strVal8 == null))
                {
                    item["code8"].Text = " ";
                }
                string strVal26 = item["code26"].Text.ToString();
                if ((strVal26 == "0") || (strVal26 == null))
                {
                    item["code26"].Text = " ";
                }
                string strVal27 = item["code27"].Text.ToString();
                if ((strVal27 == "0") || (strVal27 == null))
                {
                    item["code27"].Text = " ";
                }
                string strVal11 = item["code11"].Text.ToString();
                if ((strVal11 == "0") || (strVal11 == null))
                {
                    item["code11"].Text = " ";
                }
                string strVal12 = item["code12"].Text.ToString();
                if ((strVal12 == "0") || (strVal12 == null))
                {
                    item["code12"].Text = " ";
                }
                string strVal13 = item["code13"].Text.ToString();
                if ((strVal13 == "0") || (strVal13 == null))
                {
                    item["code13"].Text = " ";
                }
                string strVal18 = item["code18"].Text.ToString();
                if ((strVal18 == "0") || (strVal18 == null))
                {
                    item["code18"].Text = " ";
                }
                string strVal14 = item["code14"].Text.ToString();
                if ((strVal14 == "0") || (strVal14 == null))
                {
                    item["code14"].Text = " ";
                }
                string strVal16 = item["code16"].Text.ToString();
                if ((strVal16 == "0") || (strVal16 == null))
                {
                    item["code16"].Text = " ";
                }
                string strVal22 = item["code22"].Text.ToString();
                if ((strVal22 == "0") || (strVal22 == null))
                {
                    item["code22"].Text = " ";
                }
                string strVal24 = item["code24"].Text.ToString();
                if ((strVal24 == "0") || (strVal24 == null))
                {
                    item["code24"].Text = " ";
                }
                string strVal28 = item["code28"].Text.ToString();
                if ((strVal28 == "0") || (strVal28 == null))
                {
                    item["code28"].Text = " ";
                }
                string strVal29 = item["code29"].Text.ToString();
                if ((strVal29 == "0") || strVal29 == null)
                {
                    item["code29"].Text = " ";
                }
                string strVal30 = item["code30"].Text.ToString();
                if ((strVal30 == "0") || (strVal30 == null))
                {
                    item["code30"].Text = " ";
                }
                string strVal45 = item["code45"].Text.ToString();
                if ((strVal45 == "0") || (strVal45 == null))
                {
                    item["code45"].Text = " ";
                }
                string strVal_none = item["code_none"].Text.ToString();
                if ((strVal_none == "0") || (strVal_none == null))
                {
                    item["code_none"].Text = " ";
                }
                string work_days = item["work_days"].Text.ToString();
                if ((work_days == "0") || (work_days == null))
                {
                    item["work_days"].Text = " ";
                }
                string work_hours = item["work_hours"].Text.ToString();
                if ((work_hours == "0") || (work_hours == null))
                {
                    item["work_hours"].Text = " ";
                }
            }

        }



        int id_serv = 0;


        protected void radgrid_PreRender(object sender, EventArgs e)
        {

        }

        protected void radgrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                radgrid.MasterTableView.GetColumn("sv").Visible = false;
                radgrid.MasterTableView.GetColumn("days_check").Visible = false;
                radgrid.MasterTableView.GetColumn("hours_check").Visible = false;
            }
        }

        protected void radgridDevice_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Timetable")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                //dsJournal.UpdateParameters.Add(new Parameter("date_start", DbType.DateTime, (updatedItem["date_start"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //dsJournal.UpdateParameters.Add(new Parameter("date_end", DbType.DateTime, (updatedItem["date_end"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //dsJournal.UpdateParameters.Add(new Parameter("service_id", DbType.Int32, (updatedItem["sv"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("employee_id", DbType.Int32, (updatedItem["ep"].Controls[0] as RadComboBox).SelectedValue));
                TextBox value_d = (TextBox)updatedItem["value_d"].Controls[0];
                TextBox range_s = (TextBox)updatedItem["range_1"].Controls[0];
                TextBox range_e = (TextBox)updatedItem["range_2"].Controls[0];
                if (value_d.Text != "")
                {
                    if (range_s.Text != "" && range_e.Text != "")
                    {
                        for (int i = int.Parse(range_s.Text); i <= int.Parse(range_e.Text); i++)
                        {
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = value_d.Text;
                        }
                    }
                }
                CheckBox check_wekends = (CheckBox)updatedItem["check_weekends"].Controls[0];
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                DateTime date = new DateTime();
                if (check_wekends.Checked)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        try
                        {
                            date = new DateTime(year, month, i);
                            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                                ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = "В";
                        }
                        catch (Exception)
                        {

                            break;
                        }

                    }
                }
                CheckBox days_ch = (CheckBox)updatedItem["days_check"].Controls[0];
                CheckBox hours_ch = (CheckBox)updatedItem["hours_check"].Controls[0];
                if (days_ch.Checked)
                {
                    int count = 0;
                    double res = 0;
                    for (int i = 1; i <= 31; i++)
                    {
                        try
                        {
                            string value = ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text;
                            if (value.Contains("."))
                                value = value.Replace(".", ",");
                            else if (value.Contains("."))
                                value = value.Replace(".", ",");
                            if (value != "" && double.TryParse(value, out res))
                                count++;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    ((TextBox)updatedItem["work_days"].Controls[0]).Text = count.ToString();
                }
                else if (hours_ch.Checked)
                {
                    double count = 0;
                    double res = 0;
                    for (int i = 1; i <= 31; i++)
                    {
                        try
                        {
                            string value = ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text;
                            if (value.Contains("."))
                                value = value.Replace(".", ",");
                            else if (value.Contains("."))
                                value = value.Replace(".", ",");
                            else if (value.Contains("."))
                                value = value.Replace(".", ",");
                            else if (value.Contains(","))
                                value = value.Replace(",", ",");
                            else if (value.Contains(","))
                                value = value.Replace(",", ",");
                            if (value != "" && double.TryParse(value, out res))
                                count += double.Parse(value);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    ((TextBox)updatedItem["work_hours"].Controls[0]).Text = count.ToString();
                }
                int code7_count = 0, code8_count = 0, code26_count = 0, code27_count = 0, code11_count = 0, code12_count = 0, code13_count = 0, code18_count = 0, code14_count = 0, code16_count = 0, code22_count = 0, code24_count = 0, code28_count = 0, code29_count = 0, code30_count = 0, weekends_count = 0;
                for (int i = 1; i <= 31; i++)
                {
                    try
                    {
                        string code = ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text;
                        code = code.ToUpper();
                        if (code.Contains(" "))
                            code = code.Trim();
                        if (code.Contains(@"\"))
                            code = code.Remove(code.IndexOf(@"\"), 1);
                        if (code.Contains("/"))
                            code = code.Remove(code.IndexOf("/"), 1);
                        if (code.Contains(","))
                            code = code.Remove(code.IndexOf(","), 1);
                        if (code.Contains("."))
                            code = code.Remove(code.IndexOf("."), 1);
                        if (code.Contains("."))
                            code = code.Remove(code.IndexOf("."), 1);
                        if (code.Contains(","))
                            code = code.Remove(code.IndexOf(","), 1);
                        if (code == "К" || code == "K")
                        {
                            code7_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "ОТ" || code == "OT")
                        {
                            code8_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "Б")
                        {
                            code26_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "С" || code == "C")
                        {
                            code27_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "Р" || code == "P")
                        {
                            code11_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "уо")
                        {
                            code12_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "ун")
                        {
                            code13_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "ОЗ")
                        {
                            code18_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "А" || code == "A")
                        {
                            code14_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "ОР" || code == "OP")
                        {
                            code16_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "ИН")
                        {
                            code22_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "ПР")
                        {
                            code24_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "НН")
                        {
                            code28_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "НК")
                        {
                            code29_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "У")
                        {
                            code30_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                        else if (code == "В" || code == "B")
                        {
                            weekends_count++;
                            ((TextBox)updatedItem["d" + Convert.ToString(i)].Controls[0]).Text = code;
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                ((TextBox)updatedItem["code7"].Controls[0]).Text = code7_count.ToString();
                ((TextBox)updatedItem["code8"].Controls[0]).Text = code8_count.ToString();
                ((TextBox)updatedItem["code26"].Controls[0]).Text = code26_count.ToString();
                ((TextBox)updatedItem["code27"].Controls[0]).Text = code27_count.ToString();
                ((TextBox)updatedItem["code11"].Controls[0]).Text = code11_count.ToString();
                ((TextBox)updatedItem["code12"].Controls[0]).Text = code12_count.ToString();
                ((TextBox)updatedItem["code13"].Controls[0]).Text = code13_count.ToString();
                ((TextBox)updatedItem["code18"].Controls[0]).Text = code18_count.ToString();
                ((TextBox)updatedItem["code14"].Controls[0]).Text = code14_count.ToString();
                ((TextBox)updatedItem["code16"].Controls[0]).Text = code16_count.ToString();
                ((TextBox)updatedItem["code22"].Controls[0]).Text = code22_count.ToString();
                ((TextBox)updatedItem["code24"].Controls[0]).Text = code24_count.ToString();
                ((TextBox)updatedItem["code28"].Controls[0]).Text = code28_count.ToString();
                ((TextBox)updatedItem["code29"].Controls[0]).Text = code29_count.ToString();
                ((TextBox)updatedItem["code30"].Controls[0]).Text = code30_count.ToString();
                ((TextBox)updatedItem["weekends"].Controls[0]).Text = weekends_count.ToString();
                dsJournal.UpdateParameters.Add(new Parameter("d1", DbType.String, (updatedItem["d1"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d2", DbType.String, (updatedItem["d2"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d3", DbType.String, (updatedItem["d3"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d4", DbType.String, (updatedItem["d4"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d5", DbType.String, (updatedItem["d5"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d6", DbType.String, (updatedItem["d6"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d7", DbType.String, (updatedItem["d7"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d8", DbType.String, (updatedItem["d8"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d9", DbType.String, (updatedItem["d9"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d10", DbType.String, (updatedItem["d10"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d11", DbType.String, (updatedItem["d11"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d12", DbType.String, (updatedItem["d12"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d13", DbType.String, (updatedItem["d13"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d14", DbType.String, (updatedItem["d14"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d15", DbType.String, (updatedItem["d15"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d16", DbType.String, (updatedItem["d16"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d17", DbType.String, (updatedItem["d17"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d18", DbType.String, (updatedItem["d18"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d19", DbType.String, (updatedItem["d19"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d20", DbType.String, (updatedItem["d20"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d21", DbType.String, (updatedItem["d21"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d22", DbType.String, (updatedItem["d22"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d23", DbType.String, (updatedItem["d23"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d24", DbType.String, (updatedItem["d24"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d25", DbType.String, (updatedItem["d25"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d26", DbType.String, (updatedItem["d26"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d27", DbType.String, (updatedItem["d27"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d28", DbType.String, (updatedItem["d28"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d29", DbType.String, (updatedItem["d29"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d30", DbType.String, (updatedItem["d30"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("d31", DbType.String, (updatedItem["d31"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("graph_hours_count", DbType.Double, (updatedItem["graph_hours_count"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("work_days", DbType.Int32, (updatedItem["work_days"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("work_hours", DbType.Double, (updatedItem["work_hours"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("companions", DbType.Double, (updatedItem["companions"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("rem_auto", DbType.Double, (updatedItem["rem_auto"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("nights", DbType.Double, (updatedItem["nights"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("evenings", DbType.Double, (updatedItem["evenings"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("graph_hours_party", DbType.Double, (updatedItem["graph_hours_party"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code7", DbType.Int32, (updatedItem["code7"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code8", DbType.Int32, (updatedItem["code8"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code26", DbType.Int32, (updatedItem["code26"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code27", DbType.Int32, (updatedItem["code27"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code11", DbType.Int32, (updatedItem["code11"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code12", DbType.Int32, (updatedItem["code12"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code13", DbType.Int32, (updatedItem["code13"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code18", DbType.Int32, (updatedItem["code18"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code14", DbType.Int32, (updatedItem["code14"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code16", DbType.Int32, (updatedItem["code16"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code22", DbType.Int32, (updatedItem["code22"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code24", DbType.Int32, (updatedItem["code24"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code28", DbType.Int32, (updatedItem["code28"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code29", DbType.Int32, (updatedItem["code29"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code30", DbType.Int32, (updatedItem["code30"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code45", DbType.Int32, (updatedItem["code45"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code_none", DbType.Int32, (updatedItem["code_none"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("weekends", DbType.Double, (updatedItem["weekends"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("ServiceTypeID", DbType.Int32, (updatedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("dateIn", DbType.DateTime, (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");
            }
            e.Item.Edit = false;
            // e.Canceled = true;
            radgrid.Rebind();
        }


    }
}