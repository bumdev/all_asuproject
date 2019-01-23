using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.IO;
using Entities;

namespace invent_app
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

            if (context.Request["LittleWDKActRubles"] != null)
            {
                if (context.Request["LittleWDKActRubles"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/LittleWDKActRubles.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "LittleWDKActRubles.xls");
                    context.Response.ContentType = "application/octet-sream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["ActCheckOld"] != null)
            {
                if (context.Request["ActCheckOld"] == Abonent.Private.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/FActCheckOld.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FActCheckOld.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }


            //рублевые акты
            if (context.Request["ActRubles"] != null)
            {
                if (context.Request["ActRubles"] == Abonent.Corporate.ToString())
                {
                    Byte[] byData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/UactRubles.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(byData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "UactRubles.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }
            if (context.Request["ActOld"] != null)
            {
                if (context.Request["ActOld"] == Abonent.Private.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/FActOld.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FActOld.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }
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
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/RubFact.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "RubFact.xls");
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

            if (context.Request["WithdrawalAct"] != null)
            {
                if (context.Request["WithdrawalAct"] == Abonent.Private.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/WithdrawalAct.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "WithdrawalAct.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

           /* if (context.Request["WithdrawalActSpecial"] != null)
            {
                if (context.Request["WithdrawalActSpecial"] == Abonent.Special.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/WithdrawalActSpecial.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "WithdrawalActSpecial.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }*/

           /* if (context.Request["AlternativeAct"] != null)
            {
                if (context.Request["AlternativeAct"] == Abonent.Special.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/AlternativeAct.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Context-Disposition", "attachment; filename=" + "AlternativeAct.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }*/


            
             if (context.Request["AlternativeAct"] != null)
            {
                if (context.Request["AlternativeAct"] == Abonent.Special.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/AlternativeAct.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "AlternativeAct.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["ReplacementAct"] != null)
            {
                if (context.Request["ReplacementAct"] == Abonent.Special.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/ReplacementAct.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "ReplacementAct.xls");
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

            if (context.Request["billrub"] != null)
            {
                if (context.Request["billrub"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outBillRub.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outBillRub.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["littlewdkbillrubles"] != null)
            {
                if (context.Request["littlewdkbillrubles"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/littlewdkbillrubles.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "littlewdkbillrubles.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["billrubles"] != null)
            {
                if (context.Request["billrubles"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outBillRubles.xls"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outBillRubles.xls");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
                
            }

            

           /* if (context.Request["billrub"] != null)
            {
                Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/outBillRub.xls"));
                System.IO.MemoryStream mstream = new MemoryStream(bytData);
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "outBillRub.xls");
                context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(byteArray);
            }

            if (context.Request["billrubles"] != null)
            {
                Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/BillRubles.xls"));
                System.IO.MemoryStream mstream = new MemoryStream(bytData);
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "BillRubles.xls");
                context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(byteArray);
            }*/

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

            if (context.Request["registry"] != null)
            {
                if (context.Request["registry"] == Abonent.Special.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/withdrawalregistry.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "withdrawalregistry.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["alternativepay"] != null)
            {
                if (context.Request["alternativepay"] == Abonent.Special.ToString())
                {
                    Byte[] byData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/alternativepay.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(byData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "alternativepay.docx");
                    context.Response.AddHeader("Conent-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["specialpay"] != null)
            {
                if (context.Request["specialpay"] == Abonent.Special.ToString())
                {
                    Byte[] byData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/specialpay.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(byData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "specialpay.docx");
                    context.Response.AddHeader("Conent-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["pay"] != null)
            {
                if (context.Request["pay"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/kvituab.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "kvituab.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
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

            if (context.Request["payold"] != null)
            {
                if (context.Request["payold"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/payold.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "payold.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["uapayrub"] != null)
            {
                if (context.Request["uapayrub"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/payrubles.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "payrubles.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteArray);
                }
            }

            if (context.Request["littlewdkpayrubles"] != null)
            {
                if (context.Request["littlewdkpayrubles"] == Abonent.Corporate.ToString())
                {
                    Byte[] bytData = File.ReadAllBytes(context.Request.MapPath("~\\Templates/littlewdkpayrubles.docx"));
                    System.IO.MemoryStream mstream = new MemoryStream(bytData);
                    byte[] byteArray = mstream.ToArray();
                    mstream.Flush();
                    mstream.Close();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "littlewdkpayrubles.docx");
                    context.Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    context.Response.ContentType = "appliaction/octet-stream";
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