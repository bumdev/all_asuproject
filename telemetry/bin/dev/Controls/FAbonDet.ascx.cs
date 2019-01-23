using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Entities;
using DomainObjects;
using System.IO;
using Novacode;

namespace kipia_web_application.Controls
{
    public partial class FAbonDet : ULControl
    {
        //Сумма в гриде
        public double GetSum()
        {
            double sum = 0;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                sum += Convert.ToDouble((r.FindControl("price") as Literal).Text);
            }
            return sum;
        }
        //НДС
        public double GetVAT()
        {
            return Utilities.GetVAT(GetSum());
        }
        //Итоговая сумма
        public double GetFinalSum()
        {
            return Utilities.GetVAT(GetSum()) + GetSum();
        }
        int _OrderID = 0;

        public int OrderID
        {
            //get { return _OrderID; }
            set { _OrderID = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            litScript.Text = "";
            //Bind();
        }
        public void Bind()
        {
            cbSeld.Checked = false;
            cbSeld.Enabled = true;
            cbPaid.Checked = false;
            cbPaid.Enabled = true;
            

            tbPaymentDay.Enabled = true;
            tbPaymentDay.Text = DateTime.Now.ToShortDateString();

            FOrder fo = new FOrder();
            FOrderDO fodo = new FOrderDO();
            UniversalEntity ue = new UniversalEntity();
            ue = fodo.RetrieveFOrderById(_OrderID);
            if (ue.Count > 0)
            {
                
                fo = (FOrder)ue[0];
                /*if (!fo.IsPaid)
                {
                    btAct.Visible = false;
                }
                else
                {
                    btAct.Visible = true;
                }*/
                if (fo.DateOut != null)
                {
                    cbSeld.Checked = true;
                    cbSeld.Enabled = false;
                }
                if (fo.IsPaid)
                {
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                    if (fo.PaymentDay.HasValue)
                        tbPaymentDay.Text = fo.PaymentDay.Value.ToShortDateString();
                    tbPaymentDay.Enabled = false;
                }
            }

            hfODID.Value = _OrderID.ToString();
            StringBuilder sb = new StringBuilder();
            FAbonent fa = new FAbonent();
            FAbonentDO fado = new FAbonentDO();
            ue = fado.RetrieveByOrderID(_OrderID);
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                sb.AppendLine("<span>ФИО: " + fa.Surname + " " + fa.FirstName + " " + fa.LastName + "</span><br/>");
                sb.AppendLine("<span>Тел.: " + fa.Phone + "</span><br/>");
                sb.AppendLine("<span>Адрес: " + fa.Address + "</span><br/>");
                if (fa.NotPay)
                {
                    sb.AppendLine("<span>Без оплаты</span><br/>");
                }

                litAbonentInfo.Text = sb.ToString();
            }
        }
        //Обновляем конечные показания
        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {            
            e.NewValues["ID"] = (gvJournal.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["EndValue"] = (gvJournal.Rows[e.RowIndex].FindControl("tbEndValue") as TextBox).Text;
        }

        //Сохраняем всё
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            bool ok = true;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                if (string.IsNullOrEmpty((r.FindControl("litEndValue") as Literal).Text) && cbSeld.Checked)
                {
                    errorMessage += "Необходимо заполнить конечные показания водомеров.<br/>";
                    ok = false;
                    break;
                }
            }
            if (cbPaid.Checked && string.IsNullOrEmpty(tbPaymentDay.Text))
            {
                ok = false;
                errorMessage += "Необходимо заполнить дату оплаты.<br/>";
            }
            if (!ok)
            {
                radWM.RadAlert(errorMessage, null, null, "Предупреждение", "");
                //SetMessege("Предупреждение", errorMessage);
            }
            else
            {
                FOrder fo = new FOrder();
                FOrderDO fodo = new FOrderDO();
                fo.ID = Convert.ToInt32(hfODID.Value);
                fo.IsPaid = cbPaid.Checked;
                fo.UserID = GetCurrentUser().ID;
                fo.PaymentDay = Convert.ToDateTime(tbPaymentDay.Text);
                if (cbSeld.Checked)
                {
                    fo.DateOut = DateTime.Now;
                }
                bool rez=fodo.UpdateFOrder(fo);
                
                //(this.Parent.FindControl("gvJournal") as GridView).DataBind();
                //this.Visible = false;
                _OrderID = fo.ID;
                Bind();
            }
        }
        //генерация акта выполненых работ
        protected void btAct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateFAct(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?Act=Private\"></iframe>";
        }

        protected void btPay_Click(object sender, EventArgs e)
        {
            FAbonentDO faDO = new FAbonentDO();
            FOrderDO foDO = new FOrderDO();
            FOrder fo;
            FAbonent fa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = faDO.RetrieveByOrderID(id);

            FOrderDetailsDO fodDO = new FOrderDetailsDO();
            //FOrderDetails fod;
            double sum = 0;

            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                ue = foDO.RetrieveFOrderById(id);
                if (ue.Count > 0)
                {
                    fo = (FOrder)ue[0];

                    ue = fodDO.RetrieveFOrderDetailsByOrderID(id);
                    foreach(FOrderDetails fod in ue)
                    {
                        sum += fod.Price;
                    }
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/check.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", fa.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PNONE", fa.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NUMBER", fo.Prefix + fo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("TYPE", fo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ALL", (sum+Utilities.GetVAT(sum)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/outcheck.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?pay=1\"></iframe>";
                    }
                }
            }
        }

        protected void btOrder_Click(object sender, EventArgs e)
        {
            FAbonentDO faDO = new FAbonentDO();
            FAbonent fa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue=faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                using (DocX document = DocX.Load(Request.MapPath("~\\Templates/order.docx")))
                {
                    //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                    document.ReplaceText("FIO", fa.Surname+" "+fa.FirstName+" "+fa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("ADDRESS", fa.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("PNONE", fa.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    //VODOMER
                    document.SaveAs(Request.MapPath("~\\Templates/outorder.docx"));
                    litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?order=1\"></iframe>";
                }
            }


            
        }

        protected void btOrder_Check_Click(object sender, EventArgs e)
        {
            FAbonentDO faDO = new FAbonentDO();
            FOrderDO foDO = new FOrderDO();
            FOrder fo;
            FAbonent fa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = faDO.RetrieveByOrderID(id);

            FOrderDetailsDO fodDO = new FOrderDetailsDO();
            //FOrderDetails fod;
            double sum = 0;

            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                ue = foDO.RetrieveFOrderById(id);
                if (ue.Count > 0)
                {
                    fo = (FOrder)ue[0];

                    ue = fodDO.RetrieveFOrderDetailsByOrderID(id);
                    foreach (FOrderDetails fod in ue)
                    {
                        sum += fod.Price;
                    }
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/order_check.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", fa.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PNONE", fa.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NUMBER", fo.Prefix + fo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("TYPE", fo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ALL", (sum + Utilities.GetVAT(sum)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/outorder_check.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ordercheck=1\"></iframe>";
                    }
                }
            }
        }

        protected void btActCheck_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            User u = GetCurrentUser();
            ExportToExcel.GenerateFActSpecial(id,u.Location);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?Act=PrivateSpecial\"></iframe>";
        }
    }
}