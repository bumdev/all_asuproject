using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.IO;
using System.Web.UI;
using Entities;

namespace Timetable_WebApp
{
    /// <summary>
    /// Summary description for GetDocuments
    /// </summary>
    public class GetDocuments : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["ReportServ"] != null)
            {
                if (context.Request["ReportServ"] == "1")
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\templates/service_report.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "service_report.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }
            /*System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + "service_report.xls");
            response.TransmitFile();
            response.Flush();
            response.End();*/
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}