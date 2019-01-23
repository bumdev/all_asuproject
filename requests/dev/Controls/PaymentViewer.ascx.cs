using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace kipia_web_application
{
    public partial class PaymentViewer : ULControl
    {
        string _OKPO;
        DateTime _DateIn;

        public DateTime DateIn
        {
            get { return _DateIn; }
            set { _DateIn = value; }
        }

        public string OKPO
        {
            get { return _OKPO; }
            set { _OKPO = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void Bind()
        {
            hfOKPO.Value = _OKPO;

            int year = DateTime.Now.Year;
            string y1 = year.ToString();
            string y2 = (year - 1).ToString();

            string DateStart = DateIn.ToString();
            string DateEnd = DateTime.Now.ToString();

            string queryGetTables = "select rdb$relation_name"
                        + " from rdb$relations"
                        + " where rdb$relation_name like 'PR" + y1 + "%' or rdb$relation_name like 'PR" + y2 + "%' order by   rdb$relation_name asc ";
            string queryGetPayment = string.Empty;

            try
            {
                using (FbConnection fbCn = new FbConnection("Database=bank;Server=db4;User=SYSDBA;Password=masterkey;Role=All_rights_SVM;"))
                {
                    FbCommand fbCmd = new FbCommand();

                    fbCn.Open();
                    fbCmd = new FbCommand(queryGetTables, fbCn);

                    FbDataReader dr = fbCmd.ExecuteReader();

                    int count = 0;
                    while (dr.Read())
                    {
                        if (count == 0)
                        {
                            queryGetPayment += string.Format(" select summa,date_plat from {0}  where ROFILTR(NAZN, 'повер')=1 and OKPOKOR='{1}'  and date_plat between '{2}' and '{3}'", dr[0].ToString().TrimEnd(), hfOKPO.Value, DateStart, DateEnd);
                        }
                        else
                        {
                            queryGetPayment += string.Format(" union select summa,date_plat from {0}  where ROFILTR(NAZN, 'повер')=1 and OKPOKOR='{1}'   and date_plat between '{2}' and '{3}'", dr[0].ToString().TrimEnd(), hfOKPO.Value, DateStart, DateEnd);
                        }
                        count++;
                    }
                    dr.Close();
                    fbCmd = new FbCommand(queryGetPayment, fbCn);
                    dr = fbCmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dgPayment.DataSource = dt;
                    dgPayment.DataBind();
                    dr.Close();
                    dr.Dispose();
                    fbCn.Close();
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


    }
}