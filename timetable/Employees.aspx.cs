using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using FirebirdSql.Data.FirebirdClient;

namespace Timetable_WebApp
{
    public partial class Employees : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
            //lbRefresh.Visible = false;
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
            else
            {
                if (IsEdit())
                {
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
                    tbTableNumber.Visible = true;
                    tbName.Visible = true;
                    tbSex.Visible = true;
                    tbPosition.Visible = true;
                    tbSelery.Visible = true;
                    //ddlServices.Visible = true;
                    lbAdd.Visible = true;
                }
            }
        }

        protected void Refresh_OnClick(object sender, EventArgs e)
        {
            DataSet services_fb = new DataSet();
            DataSet services_sql = new DataSet();
            DataSet employees_fb = new DataSet();
            DataSet employees_sql = new DataSet();
            DataSet fired_employees_fb = new DataSet();
            DataSet main_table = new DataSet();
            using (FbConnection conn = new FbConnection(ConfigurationManager.ConnectionStrings["KadriConnection"].ConnectionString))
            {
                conn.Open();
                string sql = "select name from slug where name != ''";
                FbTransaction tr = conn.BeginTransaction();
                FbCommand cmd = new FbCommand(sql, conn, tr);
                FbDataReader reader = cmd.ExecuteReader();
                services_fb.Load(reader, LoadOption.OverwriteChanges, "Services");
                reader.Close();
                DataRow row = services_fb.Tables[0].NewRow();
                row["name"] = "Не указано";
                services_fb.Tables[0].Rows.Add(row);
                services_fb.Tables[0].AcceptChanges();
                sql = "SELECT PIP.TABNO, (Pip.Fam || ' ' || Substr(Pip.Name, 1, 1) || '.' || Substr(Pip.Otch, 1, 1) || '.') FamIO, PIP.pol, Slug.Name, NAZNACH.dolgname "
                      + "FROM Piple Pip LEFT OUTER JOIN NAZNACH Naznach ON(Pip.Naznach = Naznach.Kluch) LEFT OUTER JOIN SHTATI Shtati ON(Naznach.SHTATI = Shtati.KLUCH) "
                      + "LEFT OUTER JOIN SLUG SlWork ON(Naznach.SLUGKLUCH = SlWork.KLUCH) LEFT OUTER JOIN SLUG Slug ON(Shtati.SLUGKLUCH = Slug.KLUCH)"
                      + "WHERE(Pip.UVOLDAT is NULL)";
                cmd = new FbCommand(sql, conn, tr);
                reader = cmd.ExecuteReader();
                employees_fb.Load(reader, LoadOption.OverwriteChanges, "Employees");
                reader.Close();
                sql = "SELECT PIP.TABNO, (Pip.Fam || ' ' || Substr(Pip.Name, 1, 1) || '.' || Substr(Pip.Otch, 1, 1) || '.') FamIO, PIP.pol, Slug.Name, NAZNACH.dolgname "
                      + "FROM Piple Pip LEFT OUTER JOIN NAZNACH Naznach ON(Pip.Naznach = Naznach.Kluch) LEFT OUTER JOIN SHTATI Shtati ON(Naznach.SHTATI = Shtati.KLUCH) "
                      + "LEFT OUTER JOIN SLUG SlWork ON(Naznach.SLUGKLUCH = SlWork.KLUCH) LEFT OUTER JOIN SLUG Slug ON(Shtati.SLUGKLUCH = Slug.KLUCH)"
                      + "WHERE(Pip.UVOLDAT is not NULL)";
                cmd = new FbCommand(sql, conn, tr);
                reader = cmd.ExecuteReader();
                fired_employees_fb.Load(reader, LoadOption.OverwriteChanges, "FiredEmployees");
                reader.Close();
                tr.Rollback();
            }
            for (int i = 0; i < employees_fb.Tables[0].Rows.Count; i++)
            {
                for (int j = fired_employees_fb.Tables[0].Rows.Count - 1; j >= 0; j--)
                {
                    if (employees_fb.Tables[0].Rows[i][0].ToString() == fired_employees_fb.Tables[0].Rows[j][0].ToString() && employees_fb.Tables[0].Rows[i][1].ToString() == fired_employees_fb.Tables[0].Rows[j][1].ToString())
                    {
                        fired_employees_fb.Tables[0].Rows[j].Delete();
                        fired_employees_fb.Tables[0].AcceptChanges();
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id, service_name from services";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(services_sql);
                for (int i = 0; i < services_fb.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        if (services_sql.Tables[0].Rows[i][1].ToString() != services_fb.Tables[0].Rows[i][0].ToString())
                        {
                            sql = "update services set service_name=@s_name where id=@id";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@s_name", services_fb.Tables[0].Rows[i][0].ToString());
                            cmd.Parameters.AddWithValue("@id", services_sql.Tables[0].Rows[i][0].ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {
                        sql = "insert into services (service_name) values (@s_name)";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@s_name", services_fb.Tables[0].Rows[i][0].ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                if (services_sql.Tables[0].Rows.Count == 0)
                {
                    sql = "select id, service_name from services";
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(services_sql);
                }
                sql = "select id, number, name, sex, position, services_id from employees";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(employees_sql);
                bool fired = false;
                for (int i = 0; i < employees_sql.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < fired_employees_fb.Tables[0].Rows.Count; j++)
                    {
                        if (employees_sql.Tables[0].Rows[i][1].ToString() == fired_employees_fb.Tables[0].Rows[j][0].ToString() && employees_sql.Tables[0].Rows[i][2].ToString() == fired_employees_fb.Tables[0].Rows[j][1].ToString())
                        {
                            fired = true;
                            break;
                        }
                    }
                    if (fired)
                    {
                        sql = "update Employees set IsFired=1 where name=@name";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@name", employees_sql.Tables[0].Rows[i][2].ToString());
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql = "update Employees set IsFired=0 where name=@name";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@name", employees_sql.Tables[0].Rows[i][2].ToString());
                        cmd.ExecuteNonQuery();
                    }
                    fired = false;
                }
                employees_sql = new DataSet();
                sql = "select id, number, name, sex, position, services_id from employees where IsFired=0";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(employees_sql);
                for (int j = 0; j < employees_fb.Tables[0].Rows.Count; j++)
                {
                    for (int i = 0; i < employees_sql.Tables[0].Rows.Count; i++)
                    {
                        if (employees_sql.Tables[0].Rows[i][1].ToString() == employees_fb.Tables[0].Rows[j][0].ToString() && employees_sql.Tables[0].Rows[i][2].ToString() == employees_fb.Tables[0].Rows[j][1].ToString())
                        {
                            if (employees_sql.Tables[0].Rows[i][1].ToString() != employees_fb.Tables[0].Rows[j][0].ToString() || employees_sql.Tables[0].Rows[i][3].ToString() != employees_fb.Tables[0].Rows[j][2].ToString() || employees_sql.Tables[0].Rows[i][4].ToString() != employees_fb.Tables[0].Rows[j][3].ToString())
                            {
                                sql = "update employees set number=@num, name=@name, sex=@sex, position=@pos, services_id=@serv where id=@id";
                                SqlCommand cmd = new SqlCommand(sql, conn);
                                int service_id = 0;
                                cmd.Parameters.AddWithValue("@num", employees_fb.Tables[0].Rows[j][0].ToString());
                                cmd.Parameters.AddWithValue("@name", employees_fb.Tables[0].Rows[j][1].ToString());
                                cmd.Parameters.AddWithValue("@sex", employees_fb.Tables[0].Rows[j][2].ToString());
                                cmd.Parameters.AddWithValue("@pos", employees_fb.Tables[0].Rows[j][4].ToString());
                                for (int k = 0; k < services_sql.Tables[0].Rows.Count; k++)
                                {
                                    if (services_sql.Tables[0].Rows[k][1].ToString() == employees_fb.Tables[0].Rows[j][3].ToString())
                                        service_id = int.Parse(services_sql.Tables[0].Rows[k][0].ToString());
                                }
                                if (service_id == 0)
                                {
                                    for (int k = 0; k < services_sql.Tables[0].Rows.Count; k++)
                                    {
                                        if (services_sql.Tables[0].Rows[k][1].ToString() == "Не указано")
                                            service_id = int.Parse(services_sql.Tables[0].Rows[k][0].ToString());
                                    }
                                }
                                cmd.Parameters.AddWithValue("@serv", service_id);
                                cmd.Parameters.AddWithValue("@id", employees_sql.Tables[0].Rows[i][0].ToString());
                                cmd.ExecuteNonQuery();
                                break;
                            }
                            else
                                break;
                        }
                        else if (i == employees_sql.Tables[0].Rows.Count - 1)
                        {
                            sql = "insert into employees (number, name, sex, position, services_id, IsFired) values (@num, @name, @sex, @pos, @serv, @fired)";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            int service_id = 0;
                            cmd.Parameters.AddWithValue("@num", employees_fb.Tables[0].Rows[j][0].ToString());
                            cmd.Parameters.AddWithValue("@name", employees_fb.Tables[0].Rows[j][1].ToString());
                            cmd.Parameters.AddWithValue("@sex", employees_fb.Tables[0].Rows[j][2].ToString());
                            cmd.Parameters.AddWithValue("@pos", employees_fb.Tables[0].Rows[j][4].ToString());
                            for (int k = 0; k < services_sql.Tables[0].Rows.Count; k++)
                            {
                                if (services_sql.Tables[0].Rows[k][1].ToString() == employees_fb.Tables[0].Rows[j][3].ToString())
                                    service_id = int.Parse(services_sql.Tables[0].Rows[k][0].ToString());
                            }
                            if (service_id == 0)
                            {
                                for (int k = 0; k < services_sql.Tables[0].Rows.Count; k++)
                                {
                                    if (services_sql.Tables[0].Rows[k][1].ToString() == "Не указано")
                                        service_id = int.Parse(services_sql.Tables[0].Rows[k][0].ToString());
                                }
                            }
                            cmd.Parameters.AddWithValue("@serv", service_id);
                            cmd.Parameters.AddWithValue("@fired", 0);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                /*sql = "select Main_table.id, Employees.number, Employees.name, Main_table.service_id, Main_table.date_start, Main_table.date_end from Main_table left join Employees on Main_table.employee_id = Employees.id where Main_table.IsEdit=0";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(main_table);
                for (int i = 0; i < fired_employees_fb.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < main_table.Tables[0].Rows.Count; j++)
                    {
                        if (main_table.Tables[0].Rows[j][1].ToString() == fired_employees_fb.Tables[0].Rows[i][0].ToString() && main_table.Tables[0].Rows[j][2].ToString() == fired_employees_fb.Tables[0].Rows[i][1].ToString())
                        {
                            sql = "insert into FiredMain_table (service_id, employee_id, " +
                                  "d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16, d17, d18, d19, d20, d21, d22, d23, d24, d25, d26, d27, d28, d29, d30, d31, " +
                                  "graph_hours_count, work_days, companions, all_hours, rem_auto, nights, evenings, graph_hours_party, weekends, " +
                                  "code7, code8, code26, code27, code11, code12, code13, code18, code14,  code16, code22, code24, code28,code29, code30, code45, code10, code_none, all_absence, date_start, date_end, UserID, IsBilling, IsHR, IsPlanning, IsEdit) " +
                                  "select service_id, employee_id, " +
                                  "d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16, d17, d18, d19, d20, d21, d22, d23, d24, d25, d26, d27, d28, d29, d30, d31, " +
                                  "graph_hours_count, work_days, companions, all_hours, rem_auto, nights, evenings, graph_hours_party, weekends, " +
                                  "code7, code8, code26, code27, code11, code12, code13, code18, code14,  code16, code22, code24, code28,code29, code30, code45, code10, code_none, all_absence, date_start, date_end, UserID, IsBilling, IsHR, IsPlanning, IsEdit" +
                                  "from Main_table where id=@id";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", main_table.Tables[0].Rows[j][0].ToString());
                            cmd.ExecuteNonQuery();
                            sql = "delete from Main_table where id=@id";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", main_table.Tables[0].Rows[j][0].ToString());
                            cmd.ExecuteNonQuery();
                            sql = "select serv_id, date_start, date_end from FiredServiceAccept where serv_id=@id and date_start=@date_s and date_end=@date_e";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", main_table.Tables[0].Rows[j][2].ToString());
                            cmd.Parameters.AddWithValue("@date_s", main_table.Tables[0].Rows[j][3].ToString());
                            cmd.Parameters.AddWithValue("@date_e", main_table.Tables[0].Rows[j][4].ToString());
                            SqlDataReader rdr = cmd.ExecuteReader();
                            if (!rdr.HasRows)
                            {
                                rdr.Close();
                                sql = "insert into FiredServiceAccept (serv_id, is_chief, is_rollback, is_hr, is_bill, date_start, date_end, is_hrwrong, is_billwrong, is_added, is_plan, is_planwrong) " +
                                      "values (@service, @chief, @rollback, @hr, @bill, @date_s, @date_e, @hr_w, @bill_w, @added, @plan, @plan_w)";
                                cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@service", main_table.Tables[0].Rows[j][2].ToString());
                                cmd.Parameters.AddWithValue("@chief", 0);
                                cmd.Parameters.AddWithValue("@rollback", 0);
                                cmd.Parameters.AddWithValue("@hr", 0);
                                cmd.Parameters.AddWithValue("@bill", 0);
                                cmd.Parameters.AddWithValue("@date_s", main_table.Tables[0].Rows[j][3].ToString());
                                cmd.Parameters.AddWithValue("@date_e", main_table.Tables[0].Rows[j][4].ToString());
                                cmd.Parameters.AddWithValue("@hr_w", 0);
                                cmd.Parameters.AddWithValue("@bill_w", 0);
                                cmd.Parameters.AddWithValue("@added", 0);
                                cmd.Parameters.AddWithValue("@plan", 0);
                                cmd.Parameters.AddWithValue("@plan_w", 0);
                                cmd.ExecuteNonQuery();
                            }
                            rdr.Close();
                        }
                    }
                }*/
                //перенос записей в пустую таблицу Employees
                /*for (int j = 0; j < employees_fb.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into employees (number, name, sex, position, services_id, IsFired) values (@num, @name, @sex, @pos, @serv, @fired)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int service_id = 0;
                    cmd.Parameters.AddWithValue("@num", employees_fb.Tables[0].Rows[j][0].ToString());
                    cmd.Parameters.AddWithValue("@name", employees_fb.Tables[0].Rows[j][1].ToString());
                    cmd.Parameters.AddWithValue("@sex", employees_fb.Tables[0].Rows[j][2].ToString());
                    cmd.Parameters.AddWithValue("@pos", employees_fb.Tables[0].Rows[j][4].ToString());
                    for (int k = 0; k < services_sql.Tables[0].Rows.Count; k++)
                    {
                        if (services_sql.Tables[0].Rows[k][1].ToString() == employees_fb.Tables[0].Rows[j][3].ToString())
                            service_id = int.Parse(services_sql.Tables[0].Rows[k][0].ToString());
                    }
                    if (service_id == 0)
                    {
                        for (int k = 0; k < services_sql.Tables[0].Rows.Count; k++)
                        {
                            if (services_sql.Tables[0].Rows[k][1].ToString() == "Не указано")
                                service_id = int.Parse(services_sql.Tables[0].Rows[k][0].ToString());
                        }
                    }
                    cmd.Parameters.AddWithValue("@serv", service_id);
                    cmd.Parameters.AddWithValue("@fired", 0);
                    cmd.ExecuteNonQuery();
                }*/
                //перенос записей из таблицы Employee в таблицу Main_table
                /*DateTime date_st = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime date_e = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                DataSet temp = new DataSet();
                sql = "select id, services_id from employees where IsFired = 0";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(temp);
                for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                {
                    sql = "insert into main_table (service_id, employee_id, date_start, date_end, UserID, IsHR, IsBilling, IsPlanning, IsEdit) values (@serv, @emp, @date_st, @date_e, @user, @hr, @bill, @plan, @edit)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@serv", temp.Tables[0].Rows[i][1].ToString());
                    cmd.Parameters.AddWithValue("@emp", temp.Tables[0].Rows[i][0].ToString());
                    cmd.Parameters.AddWithValue("@date_st", date_st);
                    cmd.Parameters.AddWithValue("@date_e", date_e);
                    cmd.Parameters.AddWithValue("@user", 1);
                    cmd.Parameters.AddWithValue("@hr", 0);
                    cmd.Parameters.AddWithValue("@bill", 0);
                    cmd.Parameters.AddWithValue("@plan", 0);
                    cmd.Parameters.AddWithValue("@edit", 0);
                    cmd.ExecuteNonQuery();
                }
                sql = "select id from services";
                adapter = new SqlDataAdapter(sql, conn);
                temp = new DataSet();
                adapter.Fill(temp);
                for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                {
                    sql = "insert into ServiceAccept (serv_id, is_chief, is_rollback, is_hr, is_plan, is_bill, date_start, date_end, is_hrwrong, is_planwrong, is_billwrong) values (@id, @chief, @rb, @hr, @plan, @bill, @date_s, @date_e, @hrw, @planw, @billw)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", temp.Tables[0].Rows[i][0].ToString());
                    cmd.Parameters.AddWithValue("@chief", 0);
                    cmd.Parameters.AddWithValue("@rb", 0);
                    cmd.Parameters.AddWithValue("@hr", 0);
                    cmd.Parameters.AddWithValue("@plan", 0);
                    cmd.Parameters.AddWithValue("@bill", 0);
                    cmd.Parameters.AddWithValue("@date_s", date_st);
                    cmd.Parameters.AddWithValue("@date_e", date_e);
                    cmd.Parameters.AddWithValue("@hrw", 0);
                    cmd.Parameters.AddWithValue("@planw", 0);
                    cmd.Parameters.AddWithValue("@billw", 0);
                    cmd.ExecuteNonQuery();
                }*/
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbTableNumber.Text))
            {
                SetMessege("Предупреждение", "Табельный номер не заполнен.");
            }
            if (string.IsNullOrEmpty(tbName.Text))
            {
                SetMessege("Предупреждние", "ФИО не заполнено.");
            }
            if (string.IsNullOrEmpty(tbSex.Text))
            {
                SetMessege("Предупреждние", "Пол не заполнен");
            }
            if (string.IsNullOrEmpty(tbPosition.Text))
            {
                SetMessege("Предупреждние", "Должность не заполнена.");
            }
            if (string.IsNullOrEmpty(tbSelery.Text))
            {
                SetMessege("Предупреждние", "Оклад не заполнен.");
            }
            else
            {
                dsJournal.InsertParameters[0].DefaultValue = tbTableNumber.Text.Trim();
                dsJournal.InsertParameters[0].DefaultValue = tbName.Text.Trim();
                dsJournal.InsertParameters[0].DefaultValue = tbSex.Text.Trim();
                dsJournal.InsertParameters[0].DefaultValue = tbPosition.Text.Trim();
                dsJournal.InsertParameters[0].DefaultValue = tbSelery.Text.Trim();
                //dsJournal.InsertParameters[0].DefaultValue = ddlServices.SelectedValue.Trim();
                dsJournal.Insert();
                gvJournal.DataBind();
                tbTableNumber.Text = "";
                tbName.Text = "";
                tbSex.Text = "";
                tbPosition.Text = "";
                tbSelery.Text = "";
                //ddlServices.Sele = 0;
            }
        }
    }
}