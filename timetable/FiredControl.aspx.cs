using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;

namespace Timetable_WebApp
{
    public partial class FiredControl : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
            RadAsyncUpload1.Localization.Select = "Выбрать";
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
                Response.Redirect("Default.aspx");
            }
            /*else
            {
                if (IsEdit())
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
            }*/
        }





        protected void Chief_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            bool correct = true;
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "select IsHR, IsPlanning, IsBilling from FiredMain_table where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                DataSet temp = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(temp);
                for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                {
                    if (temp.Tables[0].Rows[i][0].ToString() == "True" || temp.Tables[0].Rows[i][1].ToString() == "True" || temp.Tables[0].Rows[i][2].ToString() == "True")
                        correct = false;
                }
                if (correct)
                {
                    sql = "update FiredServiceAccept set is_chief=1, is_rollback=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@date_s", date_s);
                    cmd.Parameters.AddWithValue("@date_e", date_e);
                    cmd.ExecuteNonQuery();
                    sql = "update FiredMain_table set IsEdit=1 where service_id=@id and date_start=@date_s and date_end=@date_e";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@date_s", date_s);
                    cmd.Parameters.AddWithValue("@date_e", date_e);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sb.Append("В вашем табеле присутствуют неисправленные записи!<br/>");
                    radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                }
            }
            radgrid.Rebind();
        }

        protected void HR_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_hr=1, is_hrwrong=0, is_rollback=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=1 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void Billing_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_bill=1, is_billwrong=0, is_rollback=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=1 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }


        protected void Planning_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_plan=1, is_planwrong=0, is_rollback=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=1 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void Rollback_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_rollback=1, is_chief=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=0 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
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
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                CheckBox is_chief = (CheckBox)item["is_chief"].Controls[0];
                if (is_chief.Checked)
                {
                    TableCell tc = (TableCell)item["is_chief"];
                    tc.BackColor = Color.Green;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_chief"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_hr = (CheckBox)item["is_hr"].Controls[0];
                CheckBox is_hrwrong = (CheckBox)item["is_hrwrong"].Controls[0];
                if (is_hrwrong.Checked)
                {
                    TableCell tc = (TableCell)item["is_hrwrong"];
                    tc.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_hrwrong"];
                    tc.BackColor = Color.White;
                }
                if (is_hr.Checked)
                {
                    TableCell tc = (TableCell)item["is_hr"];
                    tc.BackColor = Color.Green;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_hr"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_plan = (CheckBox)item["is_plan"].Controls[0];
                CheckBox is_planwrong = (CheckBox)item["is_planwrong"].Controls[0];
                if (is_planwrong.Checked)
                {
                    TableCell tc = (TableCell)item["is_planwrong"];
                    tc.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_planwrong"];
                    tc.BackColor = Color.White;
                }
                if (is_plan.Checked)
                {
                    TableCell tc = (TableCell)item["is_plan"];
                    tc.BackColor = Color.Green;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_plan"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_bill = (CheckBox)item["is_bill"].Controls[0];
                CheckBox is_billwrong = (CheckBox)item["is_billwrong"].Controls[0];
                if (is_billwrong.Checked)
                {
                    TableCell tc = (TableCell)item["is_billwrong"];
                    tc.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_billwrong"];
                    tc.BackColor = Color.White;
                }
                if (is_bill.Checked)
                {
                    TableCell tc = (TableCell)item["is_bill"];
                    tc.BackColor = Color.Green;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_bill"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_rollback = (CheckBox)item["is_rollback"].Controls[0];
                if (is_rollback.Checked)
                {
                    TableCell tc = (TableCell)item["is_rollback"];
                    tc.BackColor = Color.Orange;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_rollback"];
                    tc.BackColor = Color.White;
                }
            }
        }

        protected void HRWrong_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_chief=0, is_hrwrong=1, is_hr=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=0 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            //is_hrwrong = true;
            radgrid.Rebind();

        }

        protected void BillWrong_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_chief=0, is_billwrong=1, is_bill=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=0 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            //is_hrwrong = true;
            radgrid.Rebind();
        }

        protected void PlanWrong_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredServiceAccept set is_chief=0, is_planwrong=1, is_plan=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
                sql = "update FiredMain_table set IsEdit=0 where service_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            //is_hrwrong = true;
            radgrid.Rebind();
        }

        protected void New_period_Click(object sender, EventArgs e)
        {
            DateTime date_s_pr = new DateTime();
            DateTime date_e_pr = new DateTime();
            DateTime date_s_n = new DateTime();
            DateTime date_e_n = new DateTime();
            StringBuilder sb = new StringBuilder();
            string service_name = "";
            string services_not_accepted = "";
            bool new_period = false, all_accepted = false;
            if (radgrid.SelectedItems.Count == 0)
            {
                foreach (GridDataItem item in radgrid.Items)
                {
                    service_name = item["sv"].Text;
                    date_s_pr = DateTime.Parse(item["date_s"].Text);
                    date_e_pr = DateTime.Parse(item["date_e"].Text);
                    if (date_e_pr.Day >= 28 && date_e_pr.Day <= 31)
                        new_period = true;
                    if (new_period)
                    {
                        date_s_n = date_s_pr.AddMonths(1);
                        date_e_n = new DateTime(date_e_pr.Year, date_e_pr.Month + 1, 15);
                    }
                    else
                    {
                        date_s_n = date_s_pr;
                        date_e_n = new DateTime(date_e_pr.Year, date_e_pr.Month + 1, 1).AddDays(-1);
                    }
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
                    {
                        conn.Open();
                        string sql = "select id from services where service_name=@name";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@name", service_name);
                        int id = (int)cmd.ExecuteScalar();
                        sql = "select is_chief, is_rollback, is_hr, is_plan, is_bill from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@date_s", date_s_pr);
                        cmd.Parameters.AddWithValue("@date_e", date_e_pr);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                bool chief = rdr.GetBoolean(0);
                                bool rollback = rdr.GetBoolean(1);
                                bool hr = rdr.GetBoolean(2);
                                bool plan = rdr.GetBoolean(3);
                                bool bill = rdr.GetBoolean(4);
                                if (chief && !rollback && hr && plan && bill)
                                    all_accepted = true;
                            }
                        }
                        rdr.Close();
                        if (all_accepted)
                        {
                            if (new_period)
                                sql = "update FiredServiceAccept set is_chief=1, is_rollback=0, is_hr=1, is_plan=1, is_bill=1, is_hrwrong=0, is_planwrong=0, is_billwrong=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                            else
                                sql = "update FiredServiceAccept set is_chief=0, is_rollback=0, is_hr=0, is_plan=0, is_bill=0, is_hrwrong=0, is_planwrong=0, is_billwrong=0, date_end=@date_e_n where serv_id=@id and date_start=@date_s and date_end=@date_e";
                            cmd = new SqlCommand(sql, conn);
                            if (!new_period)
                                cmd.Parameters.AddWithValue("@date_e_n", date_e_n);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@date_s", date_s_pr);
                            cmd.Parameters.AddWithValue("@date_e", date_e_pr);
                            cmd.ExecuteNonQuery();
                            if (new_period)
                                sql = "update FiredMain_table set IsEdit=1 where service_id=@id and date_start=@date_s and date_end=@date_e";
                            else
                                sql = "update FiredMain_table set IsEdit=0, date_end=@date_e_n where service_id=@id and date_start=@date_s and date_end=@date_e";
                            cmd = new SqlCommand(sql, conn);
                            if (!new_period)
                                cmd.Parameters.AddWithValue("@date_e_n", date_e_n);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@date_s", date_s_pr);
                            cmd.Parameters.AddWithValue("@date_e", date_e_pr);
                            cmd.ExecuteNonQuery();
                            sql = "select serv_id, is_chief, is_rollback, is_hr, is_plan, is_bill, date_start, date_end, is_hrwrong, is_planwrong, is_billwrong from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@date_s", date_s_n);
                            cmd.Parameters.AddWithValue("@date_e", date_e_n);
                            rdr = cmd.ExecuteReader();
                            if (!rdr.HasRows && new_period)
                            {
                                DataSet temp = new DataSet();
                                sql = "select id from employees where services_id=@id and IsFired=0";
                                cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@id", id);
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(temp);
                                for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                                {
                                    sql = "insert into FiredMain_table (service_id, employee_id, date_start, date_end, IsEdit) values (@serv, @emp, @date_st, @date_e, @edit)";
                                    cmd = new SqlCommand(sql, conn);
                                    cmd.Parameters.AddWithValue("@serv", id);
                                    cmd.Parameters.AddWithValue("@emp", temp.Tables[0].Rows[i][0].ToString());
                                    cmd.Parameters.AddWithValue("@date_st", date_s_n);
                                    cmd.Parameters.AddWithValue("@date_e", date_e_n);
                                    cmd.Parameters.AddWithValue("@edit", 0);
                                    cmd.ExecuteNonQuery();
                                }
                                sql = "insert into FiredServiceAccept (serv_id, is_chief, is_rollback, is_hr, is_plan, is_bill, date_start, date_end, is_hrwrong, is_planwrong, is_billwrong) values (@id, @chief, @rb, @hr, @plan, @bill, @date_s, @date_e, @hrw, @planw, @billw)";
                                cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.Parameters.AddWithValue("@chief", 0);
                                cmd.Parameters.AddWithValue("@rb", 0);
                                cmd.Parameters.AddWithValue("@hr", 0);
                                cmd.Parameters.AddWithValue("@plan", 0);
                                cmd.Parameters.AddWithValue("@bill", 0);
                                cmd.Parameters.AddWithValue("@date_s", date_s_n);
                                cmd.Parameters.AddWithValue("@date_e", date_e_n);
                                cmd.Parameters.AddWithValue("@hrw", 0);
                                cmd.Parameters.AddWithValue("@planw", 0);
                                cmd.Parameters.AddWithValue("@billw", 0);
                                cmd.ExecuteNonQuery();
                            }
                            rdr.Close();
                        }
                        else
                        {
                            services_not_accepted += service_name + ", ";
                        }
                    }
                }
                if (services_not_accepted != "")
                {
                    sb.Append("Табель не закрыт для следующих служб (возможно, отсутствует подтверждение одной или нескольких инстанций): " + services_not_accepted + "!<br/>");
                    radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                    radWM.Width = Unit.Pixel(200);
                    radWM.Height = Unit.Pixel(300);
                }
            }
            else
            {
                foreach (GridDataItem item in radgrid.SelectedItems)
                {
                    service_name = item["sv"].Text;
                    date_s_pr = DateTime.Parse(item["date_s"].Text);
                    date_e_pr = DateTime.Parse(item["date_e"].Text);
                }
                if (date_e_pr.Day >= 28 && date_e_pr.Day <= 31)
                    new_period = true;
                if (new_period)
                {
                    date_s_n = date_s_pr.AddMonths(1);
                    date_e_n = new DateTime(date_e_pr.Year, date_e_pr.Month + 1, 15);
                }
                else
                {
                    date_s_n = date_s_pr;
                    date_e_n = new DateTime(date_e_pr.Year, date_e_pr.Month + 1, 1).AddDays(-1);
                }
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
                {
                    conn.Open();
                    string sql = "select id from services where service_name=@name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", service_name);
                    int id = (int)cmd.ExecuteScalar();
                    sql = "select is_chief, is_rollback, is_hr, is_plan, is_bill from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@date_s", date_s_pr);
                    cmd.Parameters.AddWithValue("@date_e", date_e_pr);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            bool chief = rdr.GetBoolean(0);
                            bool rollback = rdr.GetBoolean(1);
                            bool hr = rdr.GetBoolean(2);
                            bool plan = rdr.GetBoolean(3);
                            bool bill = rdr.GetBoolean(4);
                            if (chief && !rollback && hr && plan && bill)
                                all_accepted = true;
                        }
                    }
                    rdr.Close();
                    if (all_accepted)
                    {
                        if (new_period)
                            sql = "update FiredServiceAccept set is_chief=1, is_rollback=0, is_hr=1, is_plan=1, is_bill=1, is_hrwrong=0, is_planwrong=0, is_billwrong=0 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                        else
                            sql = "update FiredServiceAccept set is_chief=0, is_rollback=0, is_hr=0, is_plan=0, is_bill=0, is_hrwrong=0, is_planwrong=0, is_billwrong=0, date_end=@date_e_n where serv_id=@id and date_start=@date_s and date_end=@date_e";
                        cmd = new SqlCommand(sql, conn);
                        if (!new_period)
                            cmd.Parameters.AddWithValue("@date_e_n", date_e_n);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@date_s", date_s_pr);
                        cmd.Parameters.AddWithValue("@date_e", date_e_pr);
                        cmd.ExecuteNonQuery();
                        if (new_period)
                            sql = "update FiredMain_table set IsEdit=1 where service_id=@id and date_start=@date_s and date_end=@date_e";
                        else
                            sql = "update FiredMain_table set IsEdit=0, date_end=@date_e_n where service_id=@id and date_start=@date_s and date_end=@date_e";
                        cmd = new SqlCommand(sql, conn);
                        if (!new_period)
                            cmd.Parameters.AddWithValue("@date_e_n", date_e_n);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@date_s", date_s_pr);
                        cmd.Parameters.AddWithValue("@date_e", date_e_pr);
                        cmd.ExecuteNonQuery();
                        sql = "select serv_id, is_chief, is_rollback, is_hr, is_plan, is_bill, date_start, date_end, is_hrwrong, is_planwrong, is_billwrong from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@date_s", date_s_n);
                        cmd.Parameters.AddWithValue("@date_e", date_e_n);
                        rdr = cmd.ExecuteReader();
                        if (!rdr.HasRows && new_period)
                        {
                            rdr.Close();
                            DataSet temp = new DataSet();
                            sql = "select id from employees where services_id=@id and IsFired=0";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", id);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(temp);
                            for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                            {
                                sql = "insert into FiredMain_table (service_id, employee_id, date_start, date_end, IsEdit) values (@serv, @emp, @date_st, @date_e, @edit)";
                                cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@serv", id);
                                cmd.Parameters.AddWithValue("@emp", temp.Tables[0].Rows[i][0].ToString());
                                cmd.Parameters.AddWithValue("@date_st", date_s_n);
                                cmd.Parameters.AddWithValue("@date_e", date_e_n);
                                cmd.Parameters.AddWithValue("@edit", 0);
                                cmd.ExecuteNonQuery();
                            }
                            sql = "insert into FiredServiceAccept (serv_id, is_chief, is_rollback, is_hr, is_plan, is_bill, date_start, date_end, is_hrwrong, is_planwrong, is_billwrong) values (@id, @chief, @rb, @hr, @plan, @bill, @date_s, @date_e, @hrw, @planw, @billw)";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@chief", 0);
                            cmd.Parameters.AddWithValue("@rb", 0);
                            cmd.Parameters.AddWithValue("@hr", 0);
                            cmd.Parameters.AddWithValue("@plan", 0);
                            cmd.Parameters.AddWithValue("@bill", 0);
                            cmd.Parameters.AddWithValue("@date_s", date_s_n);
                            cmd.Parameters.AddWithValue("@date_e", date_e_n);
                            cmd.Parameters.AddWithValue("@hrw", 0);
                            cmd.Parameters.AddWithValue("@planw", 0);
                            cmd.Parameters.AddWithValue("@billw", 0);
                            cmd.ExecuteNonQuery();
                        }
                        rdr.Close();
                    }
                    else
                    {
                        sb.Append("Табель не подтвержден всеми инстанциями!<br/>");
                        radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                    }
                }
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

            if (e.Item.OwnerTableView.Name == "FiredServiceAccept")
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

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "FiredServiceAccept")
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
            //updatedItem.Width = 600;
            if (e.Item.OwnerTableView.Name == "NameCartridges")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));


                //dsJournal.UpdateParameters.Add(new Parameter("NameCartridges", DbType.String, (updatedItem["NameCartridges"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateInsert", DbType.DateTime, (updatedItem["DateInsert"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Number", DbType.Int32, (updatedItem["Number"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Information", DbType.String, (updatedItem["Information"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("RefuelingCondition", DbType.Boolean, (updatedItem["RefuelingCondition"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("TypeCrtridgesID", DbType.Int32, (updatedItem["sl"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DepartamentID", DbType.Int32, (updatedItem["tp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("CartDirectID", DbType.Int32, (updatedItem["cd"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DateFueling", DbType.DateTime, (updatedItem["DateFueling"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("InTheWork", DbType.Boolean, (updatedItem["InTheWork"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("DateInWork", DbType.DateTime, (updatedItem["DateInWork"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //updatedItem.Width = 600; 






                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");

            }
            e.Item.Edit = false;
            //e.Item.Width = 600;
            // e.Canceled = true;
            //radgrid.Rebind();
        }

        protected void BenjaminButton_OnClick(object sender, EventArgs e)
        {
            List<string> currentpath = new List<string>();
            List<string> existingpath = new List<string>();
            foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
            {
                string path = Server.MapPath("~/Misfits/");
                f.SaveAs(path + f.GetName());
                currentpath.Add(@"~/Misfits/" + f.GetName());
            }
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "select documents from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                string current = cmd.ExecuteScalar().ToString();
                if (current.Contains(';'))
                {
                    string[] temp = current.Split(';');
                    for (int i = 0; i < temp.Length; i++)
                        if (temp[i].Contains(" "))
                            temp[i] = temp[i].Trim();
                    for (int i = 0; i < currentpath.Count; i++)
                    {
                        bool exist = false;
                        for (int j = 0; j < temp.Length; j++)
                        {
                            if (currentpath[i] == temp[j])
                                exist = true;
                        }
                        if (!exist)
                            existingpath.Add(currentpath[i]);
                    }
                    for (int i = 0; i < temp.Length; i++)
                        existingpath.Add(temp[i]);
                    current = "";
                    for (int i = 0; i < existingpath.Count; i++)
                    {
                        if (i < existingpath.Count - 1)
                            current += existingpath[i] + ";";
                        else if (i == existingpath.Count - 1)
                            current += existingpath[i];
                    }
                }
                else if (current != "")
                {
                    bool exist = false;
                    if (current.Contains(" "))
                        current = current.Trim();
                    for (int i = 0; i < currentpath.Count; i++)
                    {
                        if (currentpath[i] == current)
                            exist = true;
                    }
                    if (!exist)
                        currentpath.Add(current);
                    current = "";
                    for (int i = 0; i < currentpath.Count; i++)
                    {
                        if (i < currentpath.Count - 1)
                            current += currentpath[i] + ";";
                        else if (i == currentpath.Count - 1)
                            current += currentpath[i];
                    }
                }
                else if (current == "")
                {
                    for (int i = 0; i < currentpath.Count; i++)
                    {
                        if (i < currentpath.Count - 1)
                            current += currentpath[i] + ";";
                        else if (i == currentpath.Count - 1)
                            current += currentpath[i];
                    }
                }
                sql = "update FiredServiceAccept set documents=@file, is_added=1 where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@file", current);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }

        protected void ShowDocuments_OnClick(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string service_name = "";
            string current = "";
            string[] path = { };
            StringBuilder sb = new StringBuilder();
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                service_name = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            if (service_name == "")
            {
                sb.Append("Выберите службу!<br/>");
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                return;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from services where service_name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", service_name);
                int id = (int)cmd.ExecuteScalar();
                sql = "select documents from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                current = cmd.ExecuteScalar().ToString();
                if (current == "")
                {
                    sb.Append("Отсутствуют документы для отображения!<br/>");
                    radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
                    return;
                }
                if (current.Contains(';'))
                {
                    path = current.Split(';');
                    Session["Count"] = path.Length.ToString();
                    for (int i = 0; i < path.Length; i++)
                    {
                        Session["image" + i] = path[i];
                    }
                }
                else
                {
                    Session["Count"] = "1";
                    Session["image0"] = current;
                }
            }
            Response.Redirect("WindowBox.aspx");
        }


        protected void radgrid_itemcommand(object sender, GridCommandEventArgs e)
        {
            /*if (e.CommandName == RadGrid.DownloadAttachmentCommandName)
            {
                e.Canceled = true;
                GridDownloadAttachmentCommandEventArgs args = e as GridDownloadAttachmentCommandEventArgs;
                string fileName = args.FileName;
                int attacmentID = (int)args.AttachmentKeyValues["ID"];
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                //Response.BinaryWrite(binaryData);
                Response.Flush();
                Response.Close();
                Response.End();
            }*/
            if (e.CommandName.Equals("DownloadImage"))
            {
                DownloadImage(e.CommandArgument.ToString());
            }
        }

        private void DownloadImage(string imagePath)
        {
            FileInfo fi = new FileInfo(imagePath);
            if (fi.Exists)
            {
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                Response.AddHeader("Content-Length", fi.Length.ToString());
                //Response.ContentType = Studio1BusinessLayer.Helpers.ReturnExtension(fi.Extension.ToLower());
                Response.TransmitFile(fi.FullName);
                Response.End();
            }
        }
    }
}