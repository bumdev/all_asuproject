using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirebirdSql.Data.FirebirdClient;
using Entities;
using DomainObjects;

namespace invent_app
{
    public partial class PaymentViewerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["okpo"] != null && Request["datein"]!=null)
                {
                    /*UniversalEntity ue=new UniversalEntity();
                    UAbonentDO uado=new UAbonentDO();
                    ue = uado.RetrieveByOrderID(Utilities.ConvertToInt(Request["id"]));
                    var abonent = ue[0] as UAbonent;
                    */

                    string okpo = string.Empty;
                    okpo = Request["okpo"];
                    int year = DateTime.Now.Year;
                    string y1 = year.ToString();
                    string y2 = (year - 1).ToString();

                   // string DateStart = Convert.ToDateTime(Request["datein"]).ToString("dd.MM.yyyy");
                    string DateStart = DateTime.Now.AddYears(-1).ToString("dd.MM.yyyy");
                    string DateEnd = DateTime.Now.ToString("dd.MM.yyyy");

                    string queryGetTables = "select rdb$relation_name"
                                + " from rdb$relations"
                                + " where rdb$relation_name like 'PR" + y1 + "%' or rdb$relation_name like 'PR" + y2 + "%' order by   rdb$relation_name asc ";
                    string queryGetPayment = string.Empty;

                    //try
                   // {
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
                                    queryGetPayment += string.Format(" select summa,date_plat from {0}  where  OKPOKOR='{1}'  and date_plat between '{2}' and '{3}'", dr[0].ToString().TrimEnd(), okpo, DateStart, DateEnd);

                                    //queryGetPayment += string.Format(" select summa,date_plat from {0}  where ROFILTR(NAZN, 'повер')=1 and OKPOKOR='{1}' ", dr[0].ToString().TrimEnd(), okpo);
                                }
                                else
                                {
                                    queryGetPayment += string.Format(" union select summa,date_plat from {0}  where  OKPOKOR='{1}'   and date_plat between '{2}' and '{3}'", dr[0].ToString().TrimEnd(), okpo, DateStart, DateEnd);
                                    //queryGetPayment += string.Format(" union select summa,date_plat from {0}  where ROFILTR(NAZN, 'повер')=1 and OKPOKOR='{1}'  ", dr[0].ToString().TrimEnd(), okpo);
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
                   /* }
                    catch (Exception ex)
                    {

                    }*/
                }
            }
        }
    }
}