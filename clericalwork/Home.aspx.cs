using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using NPOI.SS.Formula.Functions;

namespace ClericalWork_WebApp
{
    public partial class Home : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            User u = GetCurrentUser();
            
        }

        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("LoginProject.aspx");
            }
        }

        protected void Respon_OnClick(object sender, EventArgs e)
        {
            DataSet resp_sql = new DataSet();
            DataSet sprotv_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select FAM, NAME, OTCH, DOLGN from SPR_OTV";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(sprotv_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select NameResponsibleContractor, Position from ResponsibleContractor";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(resp_sql);
                for (int j = 0; j < sprotv_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into ResponsibleContractor (NameResponsibleContractor, Position) values (@res_name, @pos)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@res_name", sprotv_sql.Tables[0].Rows[j][0].ToString() + sprotv_sql.Tables[0].Rows[j][1].ToString() + sprotv_sql.Tables[0].Rows[j][2].ToString());
                    cmd.Parameters.AddWithValue("@pos", sprotv_sql.Tables[0].Rows[j][3].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Question_OnClick(object sender, EventArgs e)
        {
            DataSet quest_sql = new DataSet();
            DataSet vopr_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDBDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select VOPR from ZAL_VOPR_FOR_REPORT";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(vopr_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select NameQuestion from QuestionType";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(quest_sql);
                for (int j = 0; j < vopr_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into QuestionType (NameQuestion) values (@quest_name)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@quest_name", vopr_sql.Tables[0].Rows[j][0].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Enterprises_OnClick(object sender, EventArgs e)
        {
            DataSet enter_sql = new DataSet();
            DataSet from_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select ZAL_FROM, ADRESS, OPISANIE, ID_TYPE_ORGANIZASII from ZALOBI_FROM";
                SqlDataAdapter adapter= new SqlDataAdapter(sql, conn);
                adapter.Fill(from_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select NameEnter, AddressEnter, Description, OrganiztionsID from EnterprisesType";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(enter_sql);
                for (int j = 0; j < from_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into EnterprisesType (NameEnter, AddressEnter, Description, OrganiztionsID) values (@ent_name, @ent_add, @desc_ent, @orgid_ent)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ent_name", from_sql.Tables[0].Rows[j][0].ToString());
                    cmd.Parameters.AddWithValue("@ent_add", from_sql.Tables[0].Rows[j][1].ToString());
                    cmd.Parameters.AddWithValue("@desc_ent", from_sql.Tables[0].Rows[j][2].ToString());
                    cmd.Parameters.AddWithValue("@orgid_ent", from_sql.Tables[0].Rows[j][3].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void DistrictButt_OnClick(object sender, EventArgs e)
        {
            DataSet dis_sql = new DataSet();
            DataSet reg_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select REGION from REGION";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(reg_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select DistrictName from District";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dis_sql);
                for (int j = 0; j < reg_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into District (DistrictName) values (@dis_name)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dis_name", reg_sql.Tables[0].Rows[j][0].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void ServiceButt_OnClick(object sender, EventArgs e)
        {
            DataSet service_sql = new DataSet();
            DataSet slug_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select NAME from SLUG";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(slug_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select ServiceName from ServiceClericalWork";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(service_sql);
                for (int j = 0; j < slug_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into ServiceClericalWork (ServiceName) values (@serv_name)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@serv_name", slug_sql.Tables[0].Rows[j][0].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Mail_OnClick(object sender, EventArgs e)
        {
            DataSet mail_sql = new DataSet();
            DataSet sprmail_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select NAME from SPR_TYPE_POST";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(sprmail_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select TypeName from TypeMail";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(mail_sql);
                for (int j = 0; j < sprmail_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into TypeMail (TypeName) values (@mail_name)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mail_name", sprmail_sql.Tables[0].Rows[j][0].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void OrgButt_OnClick(object sender, EventArgs e)
        {
            DataSet org_sql = new DataSet();
            DataSet zalorg_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select NAME from ZALOBI_TYPE_ORGANIZASII";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(zalorg_sql);
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select ID, NameOrganization from Organizations";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(org_sql);
                for (int j = 0; j < zalorg_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into Organizations (NameOrganization) values (@name_org)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name_org", zalorg_sql.Tables[0].Rows[j][0].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void CatComp_OnClick(object sender, EventArgs e)
        {
            DataSet catcomp_sql = new DataSet();
            DataSet katzal_sql = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDbDelo"].ConnectionString))
            {
                conn.Open();
                string sql = "select KATEGOR from KATEGIRII_ZAL";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(katzal_sql);                
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString))
            {
                conn.Open();
                string sql = "select ID, NameCategory from CategoryComplaints";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(catcomp_sql);
                for (int j = 0; j < katzal_sql.Tables[0].Rows.Count; j++)
                {
                    sql = "insert into CategoryComplaints (NameCategory) values (@name_cat)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name_cat", katzal_sql.Tables[0].Rows[j][0].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}