using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.CSharp;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;

namespace Timetable_WebApp
{
    public partial class ServiceReport : ULPage
    {
        static HSSFWorkbook hssfworkbook;

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
                Response.Redirect("Default.aspx");
            }
        }


        static void InitializeWorkbook()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\templates/report.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void WriteToFile()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\templates/service_report.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        protected void lbReportService_OnClick(object sender, EventArgs e)
        {
            if (radgrid.SelectedItems.Count == 0)
            {
                radWM.RadAlert("Вы не выбрали службу!", 300, 200, "Ошибка", "123");
                return;
            }
            else if (!dpFrom.SelectedDate.HasValue || !dpTo.SelectedDate.HasValue)
            {
                radWM.RadAlert("Вы не выбрали даты!", 300, 200, "Ошибка", "123");
                return;
            }
            DateTime date_s = dpFrom.SelectedDate.Value;
            DateTime date_e = dpTo.SelectedDate.Value;
            DataSet table = new DataSet();
            DataSet super_users = new DataSet();
            bool allow = false;
            string service_id = "";
            string service_name = "";
            InitializeWorkbook();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_id = item["ID"].Text;
                service_name = item["sv"].Text;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select UserID from UserPermission";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(super_users);
                for (int i = 0; i < super_users.Tables[0].Rows.Count; i++)
                {
                    if (super_users.Tables[0].Rows[i][0].ToString() == GetCurrentUser().ID.ToString())
                        allow = true;
                }
                if (!allow)
                {
                    if (GetCurrentUser().SerivceID.ToString() != service_id)
                    {
                        radWM.RadAlert("У Вас нет доступа!", 300, 200, "Ошибка", "123");
                        conn.Close();
                        return;
                    }
                }
                sql = "select e.number, e.name, e.sex, e.position, e.selery, " +
                             "m.d1, m.d2, m.d3, m.d4, m.d5, m.d6, m.d7, m.d8, m.d9, m.d10, m.d11, m.d12, m.d13, m.d14, m.d15, m.d16, m.d17, m.d18, m.d19, m.d20, m.d21, m.d22, m.d23, m.d24, m.d25, m.d26, m.d27, m.d28, m.d29, m.d30, m.d31, " +
                             "m.graph_hours_count, m.work_days, m.work_hours, m.companions, m.all_hours, m.rem_auto, m.nights, m.evenings, m.graph_hours_party, m.weekends, " +
                             "m.code7, m.code8, m.code26, m.code27, m.code11, m.code12, m.code13, m.code18, m.code14, m.code16, m.code22, m.code24, m.code28, m.code29, m.code30, m.code45, m.code_none " +
                             "from timetable.dbo.Main_table m left join timetable.dbo.Employees e on m.employee_id = e.id " +
                             "where service_id = @serv_id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@serv_id", service_id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            
            ISheet sheet1 = hssfworkbook.GetSheet("Табель");
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            borderedCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            borderedCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            borderedCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            borderedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            borderedCellStyle.Alignment = HorizontalAlignment.CENTER;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.TOP;
            for (int i = 0; i < table.Tables[0].Rows.Count; i++)
            {
                var row = sheet1.CreateRow(i + 4);
                for (int j = 0; j < table.Tables[0].Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    if (table.Tables[0].Rows[i][j].ToString() != "0")
                        cell.SetCellValue(table.Tables[0].Rows[i][j].ToString());
                    else
                        cell.SetCellValue("");
                    cell.CellStyle = borderedCellStyle;
                    cell.CellStyle.WrapText = true;
                }
            }
            WriteToFile();
            Response.Redirect("GetDocuments.ashx?ReportServ=1");
        }


        protected void radgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                RadComboBox rbsv = (RadComboBox)editItem["sv"].Controls[0];
                rbsv.Width = Unit.Pixel(450);
                rbsv.Filter = RadComboBoxFilter.Contains;
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
            //insertedItem.Width = 600;
            if (e.Item.OwnerTableView.Name == "ServiceAccept")
            {
                Entities.User u = GetCurrentUser();
                //dsJournal.InsertParameters.Add(new Parameter("NameCartridges", DbType.String, (insertedItem["NameCartridges"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Number", DbType.Int32, (insertedItem["Number"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Information", DbType.String, (insertedItem["Information"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("RefuelingCondition", DbType.Boolean, (insertedItem["RefuelingCondition"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("TypeCrtridgesID", DbType.Int32, (insertedItem["sl"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DepartamentID", DbType.Int32, (insertedItem["tp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("CartDirectID", DbType.Int32, (insertedItem["cd"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DateFueling", DbType.DateTime, (insertedItem["DateFueling"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("InTheWork", DbType.Boolean, (insertedItem["InTheWork"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateinWork", DbType.DateTime, (insertedItem["DateInWork"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.Insert();
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        
    }
}