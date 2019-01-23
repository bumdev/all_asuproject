using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelLibrary;

namespace kipia_web_application
{
    public partial class Report : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butGenerateF_Click(object sender, EventArgs e)
        {
            if (dpFrom.SelectedDate.HasValue && dpTo.SelectedDate.HasValue)
                if (cbNotPay.Checked || cbPay.Checked)
                {
                    DataSet ds = new DataSet("New_DataSet");
                    DataTable dtp = new DataTable();
                    //dtp.TableName = "Оплата";
                    DataTable dtnp = new DataTable();
                    //dtnp.TableName = "Без оплаты";
                    if (cbNotPay.Checked)
                    {
                        dtnp = ((DataView)dsNotPay.Select(DataSourceSelectArguments.Empty)).ToTable();
                        dtnp.TableName = "Без оплаты";
                        ds.Tables.Add(dtnp);
                    }
                    if (cbPay.Checked)
                    {
                        dtp = ((DataView)dsPay.Select(DataSourceSelectArguments.Empty)).ToTable();
                        dtp.TableName = "Оплата";
                        ds.Tables.Add(dtp);
                    }
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment;filename=file.xls");
                    MemoryStream m = new MemoryStream();
                    DataSetHelper.CreateWorkbook(m, ds);
                    m.WriteTo(Response.OutputStream);
                    Response.End();
                }
                else
                {
                    radWM.RadAlert("Необходимо выбрать тип расчета", null, null, "Предупреждение", "");
                }
            else
            {
                radWM.RadAlert("Необходимо заполнить дату", null, null, "Предупреждение", "");
            }
        }
    }
}