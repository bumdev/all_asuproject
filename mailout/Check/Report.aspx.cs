using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainObjects;
using Entities;
using ExcelLibrary;

namespace outcomming_mail
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

        void BindPhysAdresatType()
        {
            dpPhysAdresat.Items.Clear();
            CustomRetrieverDO crDO = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = crDO.RetrievePhysicalAdresatType();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                dpPhysAdresat.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        protected void butGeneratePhysDate_OnClick(object sender, EventArgs e)
        {
            if (dpFromPhys.SelectedDate.HasValue && dpToPhys.SelectedDate.HasValue)
            {
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();

                dt = ((DataView) dsPhysicalMail.Select(DataSourceSelectArguments.Empty)).ToTable();
                dt.TableName = "Корреспонденция физ. лица";
                ds.Tables.Add(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename= report.xls");
                MemoryStream m = new MemoryStream();
                DataSetHelper.CreateWorkbook(m, ds);
                m.WriteTo(Response.OutputStream);
                Response.End();
            }
            else
            {
                radWM.RadAlert("Необходимо заполнить дату", null, null, "Предпреждение", "");
            }
        }

      
        protected void butGeneratePhysAdresat_OnClick(object sender, EventArgs e)
        {
            dpPhysAdresat.Items.Clear();
            CustomRetrieverDO crDO = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = crDO.RetrievePhysicalAdresatType();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                dpPhysAdresat.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();


                dt = ((DataView)dsPhysAdresat.Select(DataSourceSelectArguments.Empty)).ToTable();
                dt.TableName = "Корреспонденция физ. лица";
                ds.Tables.Add(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename= report.xls");
                MemoryStream m = new MemoryStream();
                DataSetHelper.CreateWorkbook(m, ds);
                m.WriteTo(Response.OutputStream);
                Response.End();
            }
            /*if (dpPhysAdresat.Items.Count > 0)
            {
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();
                

                dt = ((DataView) dsPhysAdresat.Select(DataSourceSelectArguments.Empty)).ToTable();
                dt.TableName = "Корреспонденция физ. лица";
                ds.Tables.Add(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename= report.xls");
                MemoryStream m = new MemoryStream();
                DataSetHelper.CreateWorkbook(m, ds);
                m.WriteTo(Response.OutputStream);
                Response.End();
            }
            else
            {
                radWM.RadAlert("Необходимо выбрать адресата", null, null, "Предпреждение", "");
            }*/
        }

        protected void butGenerateJurDate_Click(object sender, EventArgs e)
        {
            if (dpFromJur.SelectedDate.HasValue && dpToJur.SelectedDate.HasValue)
            {
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();

                dt = ((DataView) dsJuridicalMail.Select(DataSourceSelectArguments.Empty)).ToTable();
                dt.TableName = "Корреспонденция юр. лица";
                ds.Tables.Add(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename= report.xls");
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

        protected void butGenerateOtherDate_OnClick(object sender, EventArgs e)
        {
            if (dpFromOther.SelectedDate.HasValue && dpToOther.SelectedDate.HasValue)
            {
                DataSet ds = new DataSet("New_DataSet");
                DataTable dt = new DataTable();

                dt = ((DataView) dsOtherMail.Select(DataSourceSelectArguments.Empty)).ToTable();
                dt.TableName = "Прочая корреспонденция";
                ds.Tables.Add(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename= report.xls");
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

        
    }
}