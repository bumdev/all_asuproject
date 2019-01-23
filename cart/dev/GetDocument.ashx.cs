using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Entities;

namespace kipia_web_application
{
    /// <summary>
    /// Summary description for GetDocument
    /// </summary>
    public class GetDocument : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["Commerce"] != null)
            {
                if (context.Request["Commerce"] == "0")
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outcommerce.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outcommerce.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                if (context.Request["Commerce"] == "1")
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outnoncommerce.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outnoncommerce.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }
            if (context.Request["Act"] != null)
            {
                if (context.Request["Act"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/Uact.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "Uact.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                if (context.Request["Act"] == Abonent.Private.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/Fact.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "Fact.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                if (context.Request["Act"] == "PrivateSpecial")
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/out act_check.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FactSpecial.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }

            }
            //рублевые акты
            if (context.Request["ActRub"] != null)
            {
                if (context.Request["ActRub"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/UactRub.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "UactRub.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                if (context.Request["ActRub"] == Abonent.Private.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/FactRub.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FactRub.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                if (context.Request["ActRub"] == "PrivateSpecialRub")
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/out act_check_rub.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FactSpecialRub.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }

            }
            if (context.Request["bill"] != null)
            {
                Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outBill.xls"));
                System.IO.MemoryStream mstream = new MemoryStream(bytData);
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outBill.xls");
                context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(byteArray);
            }
            if (context.Request["order"] != null)
            {
                Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outorder.docx"));
                System.IO.MemoryStream mstream = new MemoryStream(bytData);
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outorder.docx");
                context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(byteArray);
            }
            if (context.Request["pay"] != null)
            {
                if (context.Request["pay"] == Abonent.Corporate.ToString())
                {
                    Byte[] byData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/kvituab.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(byData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Context-Disposition", "attachment; filename =" + "kvituab.docx");
                    context.Response.AddHeader("Conten-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                if (context.Request["pay"] == Abonent.Private.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/kvitfa.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "kvitfa.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }
            if (context.Request["ordercheck"] != null)
            {
                Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outorder_check.docx"));
                System.IO.MemoryStream mstream = new MemoryStream(bytData);
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outorder_check.docx");
                context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(byteArray);
            }
            //Я вот как делал. Прекрасно работает.ordercheck




            /*File.Open(context.Request.MapPath("~\\Templates/outcommerce.rtf"));
            context.Response.ContentType = "application/rtf";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FileName");
            context.Response.BinaryWrite((byte[])rdr["FieldName"]);*/
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