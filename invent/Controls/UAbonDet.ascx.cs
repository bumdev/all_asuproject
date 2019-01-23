using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using Entities;
using DomainObjects;
using FirebirdSql.Data.FirebirdClient;
using Novacode;
using Telerik.Web.UI;

namespace invent_app
{

    public partial class UAbonDet : ULControl
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
        //НДС в гриде
        public double GetVAT()
        {
            return Utilities.GetVAT(GetSum());
        }
        //Итоговая сумма в гриде
        public double GetFinalSum()
        {
            return Utilities.GetVAT(GetSum()) + GetSum();
        }


        //
        //
        //рублевый счет
        //
        //

        //сумма в гриде (рубли)
        public double GetSumRub()
        {
            double sumrub = 0;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                sumrub += Convert.ToDouble((r.FindControl("pricerub") as Literal).Text);
            }
            return sumrub;
        }
        //НДС (рубли)
        public double GetVatRub()
        {
            return Utilities.GetVATRubU(GetSumRub());
        }
        //Итоговая сумма (рубли)
        public double GetFinalSumRub()
        {
            return Utilities.GetVAT(GetSum()) + GetSum();
        }

        public double NDS()
        {
            double sumrub = 0;
            double getvat = 0;
            sumrub = Money();
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            return getvat;
        }

        public double Money()
        {
            double sumrub = 0;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                sumrub += Convert.ToDouble((r.FindControl("price") as Literal).Text);
            }
            return sumrub;
        }


        int _OrderID = 0;
        // ID по которму осуществляется привязка данных
        public int OrderID
        {
            //get { return _OrderID; }
            set { _OrderID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            litScript.Text = "";//Обнуляем iframe для выгрузки сгенерированного документа

        }
        //Закрывает контрол
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        /// <summary>
        /// Получение инфы об абоненте
        /// </summary>
        /// <param name="id">ID абонента</param>
        /// <returns>Разметка для вывода информации об абоненте.</returns>
        StringBuilder GetAbonentInfo(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            StringBuilder sb = new StringBuilder();
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            ue = uado.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                hfOKPO.Value = ua.OKPO;
                //Вывод инфы об абоненте
                hfBudjet.Value = ua.IsBudget ? "1" : "0";
                sb.AppendLine("<span><b>Номер по журналу: " + ua.NumberJournal + "</span><br/>");
                sb.AppendLine("<span><b>" + ua.Title + "   " + ua.DogID.ToString() + "</b></span><br/>");
                sb.AppendLine("<span> <b>ОКПО:</b>&nbsp;" + ua.OKPO + "</span>&nbsp;&nbsp;&nbsp;");
                sb.AppendLine("<span><b>МФО</b>: &nbsp;" + ua.MFO + "</span>&nbsp;&nbsp;&nbsp;");
                sb.AppendLine("<span><b>Р/С:</b>&nbsp;" + ua.RS + "</span><br/>");
                sb.AppendLine("<span><b>ИНН:</b>&nbsp;" + ua.INN + "</span>&nbsp;&nbsp;&nbsp;");
                sb.AppendLine("<span><b>Св. пл НДС:</b>&nbsp;" + ua.VATPay + "</span><br/>");
                sb.AppendLine("<span><b>Договор:</b>&nbsp;" + ua.Contract + "</span><br/>");
                if (ua.IsBudget)
                    sb.AppendLine("<span><b>Бюджет</b></span><br/>");
                sb.AppendLine("<span><b>Банк:&nbsp;</b>" + ua.Bank + "</span><br/>");
                sb.AppendLine("<span><b>Адрес:</b>&nbsp;" + ua.Address + "</span><br/>");
                sb.AppendLine("<span><b>Телефон:</b>&nbsp;" + ua.Phone + "</span><br/>");
                sb.AppendLine("<span><b>В лице:</b>&nbsp;" + ua.ContactFace + "</span><br/>");
                sb.AppendLine("<span><b>На основании:</b>&nbsp;" + ua.Cause + "</span><br/>");
                sb.AppendLine("<span><b>Тип налогообложения:</b>&nbsp;" + ua.TaxType + "</span><br/>");

            }
            return sb;
        }
        //Привязка данных в контрол 
        public void Bind()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            if (u.ChekPermission("littlewdk"))
            {
                btActRublesForLittleWdk.Visible = true;
                btBillRublesForLittleWDK.Visible = true;
                btUAPayRublesForLittleWDK.Visible = true;
            }
            else
            {
                btActRublesForLittleWdk.Visible = false;
                btBillRublesForLittleWDK.Visible = false;
                btUAPayRublesForLittleWDK.Visible = false;
            }
            
            
            panEdit.Visible = false;
            panView.Visible = true;

            cbSeld.Checked = false;
            cbSeld.Enabled = true;
            cbPaid.Checked = false;
            cbPaid.Enabled = true;

            tbPaymentDay.Enabled = true;
            tbPaymentDay.Text = DateTime.Now.ToShortDateString();

            UniversalEntity ue = new UniversalEntity();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            ue = uodo.RetrieveUOrderById(_OrderID);

            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                hfDateIn.Value = uo.DateIn.ToShortDateString();
                if (uo.DateOut != null)
                {
                    cbSeld.Checked = true;
                    cbSeld.Enabled = false;
                }
                if (uo.IsPaid)
                {
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                    if (uo.PaymentDay.HasValue)
                        tbPaymentDay.Text = uo.PaymentDay.Value.ToShortDateString();
                    tbPaymentDay.Enabled = false;
                }
            }

            hfODID.Value = _OrderID.ToString();
            litAbonentInfo.Text = GetAbonentInfo(_OrderID).ToString();

        }
        //Сохраняем конечные показания
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
            }
            else
            {
                UOrder fo = new UOrder();
                UOrderDO fodo = new UOrderDO();
                fo.ID = Convert.ToInt32(hfODID.Value);
                fo.IsPaid = cbPaid.Checked;
                fo.PaymentDay = Convert.ToDateTime(tbPaymentDay.Text);
                fo.UserID = GetCurrentUser().ID;

                if (cbSeld.Checked)
                {
                    fo.DateOut = DateTime.Now;
                }
                bool rez = fodo.UpdateUOrder(fo);


            }
        }
        //Генерация договора
        protected void btContract_Click(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            UniversalEntity uev = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();

            ue = uado.RetrieveByOrderID(id);
            UOrderDetailsDO uoddo = new UOrderDetailsDO();
            VodomerTypeDO vtdo = new VodomerTypeDO();
            VodomerType vt;
            UOrderDetails uod;
            if (ue.Count > 0)
            {
                double sum = 0;
                double sumvat = 0;
                string vodomer = string.Empty;
                ua = (UAbonent)ue[0];
                ue = uoddo.RetrieveUOrderDetailsByOrderID(id);
                if (ue.Count > 0)
                {

                    for (int i = 0; i < ue.Count; i++)
                    {
                        uod = (UOrderDetails)ue[i];
                        sum += uod.GetPriceWithVAT();
                        sumvat += uod.GetPriceVAT();
                        uev = vtdo.RetrieveVodomerTypeByVodomerId(uod.VodomerID);
                        if (uev.Count > 0)
                        {
                            for (int j = 0; j < uev.Count; j++)
                            {
                                vt = (VodomerType)uev[j];
                                vodomer += "Поверить " + vt.Description + " Ø " + vt.Diameter.ToString() + " мм  в количестве 1 шт.,  ";
                            }
                        }
                    }
                }
                UOrder uo = new UOrder();
                UOrderDO uoDO = new UOrderDO();
                ue = uoDO.RetrieveUOrderById(id);
                if (ue.Count > 0)
                {
                    uo = (UOrder)ue[0];
                }
                if (ua.IsBudget)
                {
                    //Для бюджетных организаций
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/noncommerce.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("NNN", uo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("TITLE", ua.Title, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VODOMER", vodomer, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SUM", RuDateAndMoneyConverter.CurrencyToTxt(sum, true), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NDS", RuDateAndMoneyConverter.CurrencyToTxt(sumvat, true), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SSS", (sum).ToString("0.00") + " грн", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("RS", ua.RS, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("BANK", ua.Bank, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("MFO", ua.MFO, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("OKPO", ua.OKPO, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", ua.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("FACE", ua.ContactFace, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("CAUSE", ua.Cause, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PHONE", ua.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //TAXTYPE
                        document.ReplaceText("TAXTYPE", ua.TaxType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/outnoncommerce.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?Commerce=1\"></iframe>";
                    }
                }
                else
                {
                    //для небюджетных организаций
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/commerce.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("NNN", uo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("TITLE", ua.Title, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VODOMER", vodomer, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SUM", RuDateAndMoneyConverter.CurrencyToTxt(sum, true), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NDS", RuDateAndMoneyConverter.CurrencyToTxt(sumvat, true), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("RS", ua.RS, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("BANK", ua.Bank, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("MFO", ua.MFO, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("OKPO", ua.OKPO, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", ua.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("FACE", ua.ContactFace, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("CAUSE", ua.Cause, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PHONE", ua.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("TAXTYPE", ua.TaxType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/outcommerce.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?Commerce=0\"></iframe>";
                    }
                }
            }
        }
        //Генерация акта выполненных работ
        protected void btAct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateUAct(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?Act=Corporate\"></iframe>";
        }

        protected void btUActRub_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateUactRub(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ActRub=Corporate\"></iframe>";
        }

        protected void btActRubles_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateUactRubles(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ActRubles=Corporate\"></iframe>";
        }

        protected void btActRublesForLittleWDK_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateActRublesLittleWDK(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?LittleWDKActRubles=Corporate\"></iframe>";
        }


        //генерация квитанции
        protected void btUAPay_Click(object sender, EventArgs e)
        {
            UAbonentDO uaDO = new UAbonentDO();
            UOrderDO uoDO = new UOrderDO();
            UOrder uo;
            UAbonent ua;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = uaDO.RetrieveByOrderID(id);

            UOrderDetailsDO uodDO = new UOrderDetailsDO();
            double sum = 0;
            double sumrub = 0;

            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                ue = uoDO.RetrieveUOrderById(id);
                if (ue.Count > 0)
                {
                    uo = (UOrder)ue[0];
                    ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                    foreach (UOrderDetails uod in ue)
                    {
                        sum += uod.Price;
                        //sumrub += (uod.Price)*2;
                    }

                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/kvitua.docx")))
                    {
                        //DocXEntender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("TITLE", ua.Title, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", ua.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PHONE", ua.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NNN", uo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VIEW", uo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ALL", (sum + Utilities.GetVAT(sum)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                       // document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                       // document.ReplaceText("NDS", Utilities.GetVATRubU(sumrub).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                      //  document.ReplaceText("VSEGO", (sumrub + Utilities.GetVAT(sumrub)).ToString("0.00"),false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/kvituab.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?pay=Corporate\"></iframe>";
                    }

                }
            }
        }

        protected void btUAPayRublesForLittleWdk_Click(object sender, EventArgs e)
        {
            UAbonentDO uaDO = new UAbonentDO();
            UOrderDO uoDO = new UOrderDO();
            UOrder uo;
            UAbonent ua;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = uaDO.RetrieveByOrderID(id);

            UOrderDetailsDO uodDO = new UOrderDetailsDO();
            double sum = 0;
            double sumrub = 0;
            double nds = 0;
            double finalsum = 0;

            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                ue = uoDO.RetrieveUOrderById(id);
                if (ue.Count > 0)
                {
                    uo = (UOrder)ue[0];
                    ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                    foreach (UOrderDetails uod in ue)
                    {
                        sum += uod.PriceRub;
                        // sumrub += (uod.Price) * 2;
                        /*sumrub = sum;
                        nds = sumrub*0.2;
                        nds = Math.Round(nds, 2);
                        finalsum = (nds + sumrub) + (nds + sumrub);*/
                    }

                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/littlewdkpayua.docx")))
                    {
                        //DocXEntender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("TITLE", ua.Title, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", ua.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PHONE", ua.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NNN", uo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VIEW", uo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("ALL", (sum + Utilities.GetVAT(sum)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NDS", Utilities.GetVATRubU(sumrub).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/littlewdkpayrubles.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?littlewdkpayrubles=Corporate\"></iframe>";
                    }

                }
            }
        }

        //генерация рублевой квитанции
        protected void btUAPayRubles_Click(object sender, EventArgs e)
        {
            UAbonentDO uaDO = new UAbonentDO();
            UOrderDO uoDO = new UOrderDO();
            UOrder uo;
            UAbonent ua;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = uaDO.RetrieveByOrderID(id);

            UOrderDetailsDO uodDO = new UOrderDetailsDO();
            double sum = 0;
            double sumrub = 0;
            double nds = 0;
            double finalsum = 0;

            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                ue = uoDO.RetrieveUOrderById(id);
                if (ue.Count > 0)
                {
                    uo = (UOrder)ue[0];
                    ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                    foreach (UOrderDetails uod in ue)
                    {
                        sum += uod.PriceRub;
                       // sumrub += (uod.Price) * 2;
                        /*sumrub = sum;
                        nds = sumrub*0.2;
                        nds = Math.Round(nds, 2);
                        finalsum = (nds + sumrub) + (nds + sumrub);*/
                    }

                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/payua.docx")))
                    {
                        //DocXEntender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("TITLE", ua.Title, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", ua.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PHONE", ua.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NNN", uo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VIEW", uo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("ALL", (sum + Utilities.GetVAT(sum)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NDS", Utilities.GetVATRubU(sumrub).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/payrubles.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?uapayrub=Corporate\"></iframe>";
                    }

                }
            }
        }

        protected void btPayOld_Click(object sender, EventArgs e)
        {
            UAbonentDO uaDO = new UAbonentDO();
            UOrderDO uoDO = new UOrderDO();
            UOrder uo;
            UAbonent ua;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = uaDO.RetrieveByOrderID(id);

            UOrderDetailsDO uodDO = new UOrderDetailsDO();
            double sum = 0;
            double sumrub = 0;
            double nds = 0;
            double finalsum = 0;

            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                ue = uoDO.RetrieveUOrderById(id);
                if (ue.Count > 0)
                {
                    uo = (UOrder)ue[0];
                    ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                    foreach (UOrderDetails uod in ue)
                    {
                        sum += uod.Price;
                        // sumrub += (uod.Price) * 2;
                        sumrub = sum;
                        nds = sumrub*0.2;
                        nds = Math.Round(nds, 2);
                        finalsum = (nds + sumrub) + (nds + sumrub);
                    }

                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/payoldua.docx")))
                    {
                        //DocXEntender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("TITLE", ua.Title, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", ua.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PHONE", ua.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NNN", uo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VIEW", uo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("ALL", (sum + Utilities.GetVAT(sum)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NDS", Utilities.GetVATRubU(sumrub).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", finalsum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/payold.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?payold=Corporate\"></iframe>";
                    }

                }
            }
        }

        //Режим редактиорвания абонента
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            if (u.ChekPermission("littlewdk"))
            {
                btActRublesForLittleWdk.Visible = true;
                btBillRublesForLittleWDK.Visible = true;
                btUAPayRublesForLittleWDK.Visible = true;
            }
            else
            {
                btActRublesForLittleWdk.Visible = false;
                btBillRublesForLittleWDK.Visible = false;
                btUAPayRublesForLittleWDK.Visible = false;
            }
            
            panEdit.Visible = true;
            panView.Visible = false;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            StringBuilder sb = new StringBuilder();
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            ue = uado.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];

                tbTitle.Text = ua.Title;
                tbNumberJournal.Text = ua.NumberJournal;
                tbOKPO.Text = ua.OKPO;
                tbMFO.Text = ua.MFO;
                tbContract.Text = ua.Contract;
                tbRS.Text = ua.RS;
                tbBank.Text = ua.Bank;
                tbAddress.Text = ua.Address;
                tbPhone.Text = ua.Phone;
                tbContactFace.Text = ua.ContactFace;
                tbCause.Text = ua.Cause;
                tbINN.Text = ua.INN;
                tbVATPay.Text = ua.VATPay;
                cbBudjet.Checked = ua.IsBudget;
                hfID.Value = ua.ID.ToString();
                hfDogID.Value = ua.DogID.ToString();
                tbTaxType.Text = ua.TaxType;
            }
        }
        //Сбор информации об абоненте из UI
        UAbonent CollectUAbonent()
        {
            UAbonent ua = new UAbonent();

            ua.Title = tbTitle.Text;
            ua.NumberJournal = tbNumberJournal.Text;
            ua.OKPO = tbOKPO.Text;
            ua.MFO = tbMFO.Text;
            ua.Contract = tbContract.Text;
            ua.RS = tbRS.Text;
            ua.Bank = tbBank.Text;
            ua.Address = tbAddress.Text;
            ua.Phone = tbPhone.Text;
            ua.ContactFace = tbContactFace.Text;
            ua.Cause = tbCause.Text;
            ua.INN = tbINN.Text;
            ua.VATPay = tbVATPay.Text;
            ua.IsBudget = cbBudjet.Checked;
            ua.ID = Utilities.ConvertToInt(hfID.Value);
            ua.DogID = Utilities.ConvertToInt(hfDogID.Value);
            ua.TaxType = tbTaxType.Text;

            return ua;
        }
        //Сохранение абонента
        protected void lbSaveAbonent_Click(object sender, EventArgs e)
        {
            UAbonentDO uado = new UAbonentDO();
            uado.Update(CollectUAbonent());

            panEdit.Visible = false;
            panView.Visible = true;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);

            litAbonentInfo.Text = GetAbonentInfo(id).ToString();
        }
        //Генерация счета
        protected void btBill_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateBill(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?bill=1\"></iframe>";
        }

        protected void btBillRub_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateBillRub(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?billrub=Corporate\"></iframe>";
        }

        protected void btBillRubles_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateBillRubles(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?billrubles=Corporate\"></iframe>";
        }

        protected void btBillRublesForLittleWDK_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateBillForLittleWDK(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?littlewdkbillrubles=Corporate\"></iframe>";
        }

        protected void lbGetPayment_Click(object sender, EventArgs e)
        {
            RadWindow window1 = new RadWindow();
            window1.NavigateUrl = "../check/PaymentViewer.aspx?okpo=" + hfOKPO.Value + "&datein=" + hfDateIn.Value;
            window1.VisibleOnPageLoad = true;
            window1.Width = 400;
            window1.Height = 200;
            window1.Title = "Просмотр абонента";
            this.Controls.Add(window1);

            /*
            PaymentViewer1.Visible = true;
            PaymentViewer1.OKPO = hfOKPO.Value;
            PaymentViewer1.DateIn = Convert.ToDateTime(hfDateIn.Value);
            PaymentViewer1.Bind();*/
        }
    }
}