using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelLibrary;
using NPOI.POIFS.NIO;

namespace requests_app
{
    public partial class Report : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hfUserID.Value = GetCurrentUser().ID.ToString();
            }
        }

        protected void butGenerateVodomer_OnClick(object sender, EventArgs e)
        {
            if(vodFrom.SelectedDate.HasValue && vodTo.SelectedDate.HasValue)
                if (vdNotPay.Checked || vdPay.Checked)
                {
                    DataSet ds = new DataSet("New_DataSet");
                    DataTable dtp = new DataTable();
                    DataTable dtnp = new DataTable();
                    if (vdNotPay.Checked)
                    {
                        dtnp = ((DataView) vsNotPay.Select(DataSourceSelectArguments.Empty)).ToTable();
                        dtnp.TableName = "Без оплаты";
                        ds.Tables.Add(dtnp);
                    }
                    if (vdPay.Checked)
                    {
                        dtp = ((DataView) vsPay.Select(DataSourceSelectArguments.Empty)).ToTable();
                        dtp.TableName = "С оплатой";
                        ds.Tables.Add(dtp);
                    }
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment;filename=reportvodomer.xls");
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
        protected void butGenerateU_Click(object sender, EventArgs e)
        {
            if (dpFromU.SelectedDate.HasValue && dpToU.SelectedDate.HasValue)
            {
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();
                //dtp.TableName = "Оплата";

               dt = ((DataView)dsUAbonent.Select(DataSourceSelectArguments.Empty)).ToTable();
               dt.TableName = "Юрлица";
               ds.Tables.Add(dt);
             
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
                radWM.RadAlert("Необходимо заполнить дату", null, null, "Предупреждение", "");
            }
        }

        protected void butGenerateW_Click(object sender, EventArgs e)
        {
            if (dpFromW.SelectedDate.HasValue && dpToW.SelectedDate.HasValue)
            {
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();

                dt = ((DataView) dsSAbonent.Select(DataSourceSelectArguments.Empty)).ToTable();
                dt.TableName = "Установка";
                ds.Tables.Add(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=AlternativeReport.xls");
                MemoryStream ms = new MemoryStream();
                DataSetHelper.CreateWorkbook(ms, ds);
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
            else
            {
                radWM.RadAlert("Необходимо заполнить дату", null, null, "Предупреждение","");
            }
        }
    }
}