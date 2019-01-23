using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Telerik.Web.UI;

namespace Timetable_WebApp
{
    public partial class FiredJournal : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
        }

        protected void DropDownList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            /*using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand("RetrieveTimetableJournal", conn);
                sc.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@serv_id",
                    Value = id
                };
                SqlParameter nameParam_1 = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = GetCurrentUser().ID
                };
                sc.Parameters.Add(nameParam);
                sc.Parameters.Add(nameParam_1);
                //sc.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(sc);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                
            }*/
            /*dsJournal.SelectCommand = "RetrieveTimetable";
            dsJournal.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            hfUserID.Value = GetCurrentUser().ID.ToString();
            //serv_id.Value = id.ToString();
            //dsJournal.SelectParameters.Add("@userId", hfUserID.Value);
            dsJournal.SelectParameters.Add("@serv_id", id.ToString());
            radgrid.DataSource = (DataView) dsJournal.Select(DataSourceSelectArguments.Empty);*/
            /*int id = int.Parse(ddlService.SelectedValue);
            ddlService = sender as RadComboBox;
            string filterexpression;
            filterexpression =*/
            ddlService = sender as RadComboBox;
            string rcbFilterExpr = ddlService.DataValueField + ddlService.SelectedValue;
            ViewState[ddlService.ID] = ddlService.SelectedValue;
            foreach (GridColumn gc in this.radgrid.MasterTableView.Columns)
            {
                if (gc.UniqueName.Equals(ddlService.SelectedValue))
                {
                    if (string.IsNullOrEmpty(ddlService.SelectedValue))
                    {
                        gc.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        gc.CurrentFilterValue = string.Empty;
                    }
                    else
                    {
                        gc.CurrentFilterFunction = GridKnownFunction.EqualTo;
                        gc.CurrentFilterValue = ddlService.SelectedValue;
                    }
                    this.radgrid.Rebind();
                    break;
                }
            }

        }


        protected void HR_Click(object sender, EventArgs e)
        {
            DateTime date_s = new DateTime();
            DateTime date_e = new DateTime();
            string name = "";
            string service = "";
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                service = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredMain_table set IsHR=1 where employee_id=@id and date_start=@date_s and date_end=@date_e";
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
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                service = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredMain_table set IsBilling=1 where employee_id=@id and date_start=@date_s and date_end=@date_e";
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
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                service = item["sv"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredMain_table set IsPlanning=1 where employee_id=@id and date_start=@date_s and date_end=@date_e";
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
            foreach (GridDataItem item in radgrid.SelectedItems)
            {
                name = item["ep"].Text;
                date_s = DateTime.Parse(item["date_s"].Text);
                date_e = DateTime.Parse(item["date_e"].Text);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select id from employees where name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                int id = (int)cmd.ExecuteScalar();
                sql = "update FiredMain_table set IsHR=0, IsPlanning=0, IsBilling=0 where employee_id=@id and date_start=@date_s and date_end=@date_e";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date_s", date_s);
                cmd.Parameters.AddWithValue("@date_e", date_e);
                cmd.ExecuteNonQuery();
            }
            radgrid.Rebind();
        }


        private void CheckPermission()
        {
            User u = GetCurrentUser();
            hfUserID.Value = u.ID.ToString();
            //hfServicesID.Value = u.SerivceID.ToString();
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
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                RadComboBox rbsv = (RadComboBox)editItem["sv"].Controls[0];
                rbsv.Width = Unit.Pixel(450);
                rbsv.Filter = RadComboBoxFilter.Contains;
                RadComboBox rbep = (RadComboBox)editItem["ep"].Controls[0];
                rbep.Width = Unit.Pixel(450);
                rbep.Filter = RadComboBoxFilter.Contains;
                CheckBox days_ch = (CheckBox)editItem["days_check"].Controls[0];
                CheckBox hours_ch = (CheckBox)editItem["hours_check"].Controls[0];

            }
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                CheckBox is_hr = (CheckBox)item["is_hr"].Controls[0];
                if (is_hr.Checked)
                {
                    TableCell tc = (TableCell)item["is_hr"];
                    tc.BackColor = Color.Red;
                    item.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_hr"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_plan = (CheckBox)item["is_plan"].Controls[0];
                if (is_plan.Checked)
                {
                    TableCell tc = (TableCell)item["is_plan"];
                    tc.BackColor = Color.Red;
                    item.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_plan"];
                    tc.BackColor = Color.White;
                }
                CheckBox is_bill = (CheckBox)item["is_bill"].Controls[0];
                if (is_bill.Checked)
                {
                    TableCell tc = (TableCell)item["is_bill"];
                    tc.BackColor = Color.Red;
                    item.BackColor = Color.Red;
                }
                else
                {
                    TableCell tc = (TableCell)item["is_bill"];
                    tc.BackColor = Color.White;
                }
            }
            /* if (e.Item is GridFilteringItem)
             {
                 GridFilteringItem filterItem = e.Item as GridFilteringItem;
                 TextBox tb1 = filterItem["serv_id"].Controls[0] as TextBox;                
                 //tb1.Text = ddlService.SelectedValue;
                 //tb1.AutoPostBack = false;
                 //tb1.FindFieldTemplate(ddlService.SelectedValue);
                 tb1.Attributes.Add("onchange", "return txtboxclick(sender, args);");
                 //tb1.TextChanged += TextBoxTextChanged;
             }*/
        }

        /* protected void TextBoxTextChanged(object sender, EventArgs e)
         {
              //radgrid.MasterTableView.FilterExpression = "[service_id] LIKE '%" + ddlService.SelectedValue + "%' ";
                 GridFilteringItem filter = (GridFilteringItem)radgrid.MasterTableView.GetItems(GridItemType.FilteringItem)[0];
                 TextBox txtbx = (TextBox) filter["serv_id"].Controls[0];
                 radgrid.MasterTableView.FilterExpression = "[service_id] LIKE '%" + ddlService.SelectedValue + "%' ";
                 GridColumn column = radgrid.MasterTableView.GetColumnSafe("serv_id");
                 column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                 ddlService.SelectedValue = txtbx.Text;
                 column.CurrentFilterValue = txtbx.Text;
                 column.AndCurrentFilterFunction = GridKnownFunction.EqualTo;
                 txtbx.Enabled = true;
                 txtbx.AutoPostBack = true;
                
                 radgrid.MasterTableView.Rebind();
        }*/

        int id_serv = 0;
        //string filter;
        //bool customfilter = false;

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            radgrid.MasterTableView.GetColumnSafe("serv_id").CurrentFilterFunction = GridKnownFunction.NoFilter;
            radgrid.MasterTableView.GetColumnSafe("serv_id").CurrentFilterValue = string.Empty;
            radgrid.MasterTableView.FilterExpression = "[service_id] LIKE '%" + ddlService.SelectedValue + "%' ";
            GridColumn column = radgrid.MasterTableView.GetColumnSafe("serv_id");
            column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            column.CurrentFilterValue = ddlService.SelectedValue;
            column.AndCurrentFilterFunction = GridKnownFunction.EqualTo;
            radgrid.MasterTableView.Rebind();
        }

        protected void radgrid_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                radgrid.MasterTableView.FilterExpression = "[service_id] LIKE '%" + ddlService.SelectedValue + "%' ";
                GridColumn column = radgrid.MasterTableView.GetColumnSafe("serv_id");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                column.CurrentFilterValue = ddlService.SelectedValue;
                column.AndCurrentFilterFunction = GridKnownFunction.EqualTo;
                radgrid.MasterTableView.Rebind();
            }
            /*if (radgrid.MasterTableView.FilterExpression != string.Empty)
            {
                dsJournal.SelectCommand = dsServices.SelectCommand + " WHERE " + radgrid.MasterTableView.FilterExpression.ToString();
                radgrid.MasterTableView.Rebind();
            }*/
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
                dsJournal.UpdateParameters.Add(new Parameter("service_id", DbType.Int32, (updatedItem["sv"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("employee_id", DbType.Int32, (updatedItem["ep"].Controls[0] as RadComboBox).SelectedValue));
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
                            if (value != "" && double.TryParse(value, out res))
                                count += double.Parse(value);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    ((TextBox)updatedItem["work_days"].Controls[0]).Text = count.ToString();
                }
                int code7_count = 0, code8_count = 0, code26_count = 0, code27_count = 0, code11_count = 0, code12_count = 0, code13_count = 0, code18_count = 0, code14_count = 0, code16_count = 0, code22_count = 0, code24_count = 0, code28_count = 0, code29_count = 0, code30_count = 0, code10_count = 0, weekends_count = 0;
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
                            code10_count++;
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
                        else if (code == "у")
                        {
                            code12_count++;
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
                ((TextBox)updatedItem["code10"].Controls[0]).Text = code10_count.ToString();
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
                dsJournal.UpdateParameters.Add(new Parameter("work_days", DbType.Double, (updatedItem["work_days"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("companions", DbType.Double, (updatedItem["companions"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("all_hours", DbType.Double, (updatedItem["all_hours"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("rem_auto", DbType.Double, (updatedItem["rem_auto"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("nights", DbType.Double, (updatedItem["nights"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("evenings", DbType.Double, (updatedItem["evenings"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("graph_hours_party", DbType.Double, (updatedItem["graph_hours_party"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("weekends", DbType.Double, (updatedItem["weekends"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code7", DbType.Double, (updatedItem["code7"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code8", DbType.Double, (updatedItem["code8"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code26", DbType.Double, (updatedItem["code26"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code27", DbType.Double, (updatedItem["code27"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code11", DbType.Double, (updatedItem["code11"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code12", DbType.Double, (updatedItem["code12"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code13", DbType.Double, (updatedItem["code13"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code18", DbType.Double, (updatedItem["code18"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code14", DbType.Double, (updatedItem["code14"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code16", DbType.Double, (updatedItem["code16"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code22", DbType.Double, (updatedItem["code22"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code24", DbType.Double, (updatedItem["code24"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code28", DbType.Double, (updatedItem["code28"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code29", DbType.Double, (updatedItem["code29"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code30", DbType.Double, (updatedItem["code30"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code45", DbType.Double, (updatedItem["code45"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code10", DbType.Double, (updatedItem["code10"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("code_none", DbType.Double, (updatedItem["code_none"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("all_absence", DbType.Double, (updatedItem["all_absence"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("ServiceTypeID", DbType.Int32, (updatedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("dateIn", DbType.DateTime, (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");
            }
            e.Item.Edit = false;
            // e.Canceled = true;
            //radgrid.Rebind();
        }

    }
}