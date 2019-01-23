using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Entities;
using DomainObjects;

namespace outcomming_mail
{
    public class AccountParser
    {
        public int n {get; set; }
        public string d1 {get; set; }
        public string d2 { get; set; } 

    }

    public partial class AccountParsing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<AccountParser> lst=new List<AccountParser>();
            FileStream stream = File.Open(@"E:\import.xls", FileMode.Open, FileAccess.Read);

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //...
            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //...
            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
           // DataSet result = excelReader.AsDataSet();
            //...
            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = false;
            DataSet result = excelReader.AsDataSet();

            //5. Data Reader methods
            string tmp=string.Empty;
            string abonent = string.Empty;
            string district = string.Empty;
            string district1 = string.Empty;
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            int i = 0;
            while (excelReader.Read())
            {
                //Response.Write(string.Format("{0},  {1}",excelReader.GetString(1),excelReader.GetString(7))) ;
                /*try
                {
                    Response.Write("6 "+excelReader.GetValue(6).ToString()+"<br/>");
                }
                catch (Exception ex)
                {
                }*/
                 try
                 {
                     if (excelReader.GetValue(0).ToString().Contains("Счет"))
                     {
                         tmp = excelReader.GetValue(0).ToString();
                         tmp = tmp.Replace("Счет-фактура ", "");
                         tmp = tmp.Remove(tmp.Length - 11, 11);
                         tmp = tmp.Replace(",", "");
                         tmp = tmp.Replace(".", "");
                         tmp = Regex.Replace(tmp, "\\D", "");

                         ue = crdo.RetrieveUserLocationByFOrder(Utilities.ConvertToInt(tmp));
                         if (ue.Count > 0)
                         {
                             district = ue[0].ToString();
                         }
                         else
                         {
                             district = "Не найден";
                         }
                         ue = crdo.RetrieveUserLocationBySurname(abonent);
                         if (ue.Count > 0)
                         {
                             district1 = ue[0].ToString();
                         }
                         else
                         {
                             district1 = "Не найден";
                         }
                         AccountParser ap=new AccountParser();
                         ap.n = i;
                         ap.d1 = district;
                         ap.d2 = district1;
                         lst.Add(ap);

                         //Response.Write(i.ToString()+ "  " +tmp + "      " + abonent + "           " + district + "           " + district + "<br/>");
                     }
                     else
                     {
                         abonent = excelReader.GetValue(0).ToString();
                         abonent = abonent.Split(' ')[1];
                         abonent = abonent.Replace(",", "");
                         abonent = abonent.Replace(".", "");
                     }
                     i++;

                 }
                 catch (Exception ex)
                {
                }
                //excelReader.GetInt32(0);
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();
            //ExportToExcel.GenerateAccountParser(lst);
        }
    }
}