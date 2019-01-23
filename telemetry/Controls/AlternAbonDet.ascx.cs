using System;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services.Description;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using DAO;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using DomainObjects;
using Entities;
using Novacode;
using NPOI.HPSF;
using NPOI.SS.Formula.Functions;

namespace leak_detectors.Controls
{
    public partial class AlternAbonDet : ULControl
    {
        public double WithdrawalMoney()
        {
            double sum = 0;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                sum += Convert.ToDouble((r.FindControl("specialprice") as Literal).Text);
            }
            return sum;
        }

        int _OrderID = 0;

        public int OrderID
        {
           // get { return _OrderID; }
            set { _OrderID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            litScriptS.Text = "";
        }

        void BindDistricts()
        {
            ddsDistrict.Items.Clear();
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrieveDistricts();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList) ue[i];
                ddsDistrict.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        public void EditorMode()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            if (u.ChekPermission("Editor"))
            {
                punAlternAbonentEditor.Visible = true;
                gvJournal.Visible = false;
                gvJournal2.Visible = true;
                BindDistricts();
                UniversalEntity ue = new UniversalEntity();
                AlternativeAbonent aa = new AlternativeAbonent();
                AlternativeAbonentDO aado = new AlternativeAbonentDO();
                ue = aado.RetrieveBySOrderID(_OrderID);
                if (ue.Count > 0)
                {
                    aa = (AlternativeAbonent) ue[0];
                    stbNumberJournalPhysical.Text = aa.PhysicalNumberJournal;
                    tbAddress.Text = aa.Address;
                    stbClientSurname.Text = aa.Surname;
                    stbClientName.Text = aa.FirstName;
                    stbClientLastName.Text = aa.LastName;
                    stbPhone.Text = aa.Phone;
                    ddsDistrict.SelectedValue = aa.DistrictID.ToString();
                }
            }
            else
            {
                punAlternAbonentEditor.Visible = false;
                gvJournal.Visible = true;
                gvJournal2.Visible = false;
            }
        }


        public void Bind()
        {
            EditorMode();
            cbSeld.Checked = false;
            cbSeld.Enabled = true;
            cbPaid.Checked = false;
            cbPaid.Enabled = true;

            tbPaymentDay.Enabled = true;
            tbPaymentDay.Text = DateTime.Now.ToShortDateString();

            AlternativeOrder ao = new AlternativeOrder();
            AlternativeOrderDO aodo = new AlternativeOrderDO();
            UniversalEntity ue = new UniversalEntity();
            ue = aodo.RetrieveSOrderById(_OrderID);
            if (ue.Count > 0)
            {
                ao = (AlternativeOrder) ue[0];
                if (ao.DateOut != null)
                {
                    cbSeld.Checked = true;
                    cbSeld.Enabled = false;
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                }
                if (ao.IsPaid)
                {
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                    cbSeld.Checked = true;
                    cbSeld.Enabled = false;
                    if (ao.PaymentDay.HasValue)
                        tbPaymentDay.Text = ao.PaymentDay.Value.ToShortDateString();
                    tbPaymentDay.Enabled = false;
                }
            }

            hfODID.Value = _OrderID.ToString();
            StringBuilder sb = new StringBuilder();
            AlternativeAbonent aa = new AlternativeAbonent();
            AlternativeAbonentDO aado = new AlternativeAbonentDO();
            ue = aado.RetrieveBySOrderID(_OrderID);
            if (ue.Count > 0)
            {
                aa = (AlternativeAbonent) ue[0];
                sb.AppendLine("<span>ФИО: " + aa.Surname + " " + aa.FirstName + " " + aa.LastName + "</span><br/>");
                sb.AppendLine("<span>Номер по журналу: " + aa.PhysicalNumberJournal + "</span><br/>");
                sb.AppendLine("<span>Тел. " + aa.Phone + "</span><br/>");
                sb.AppendLine("<span>Адрес: " + aa.Address + "</span><br/>");

                AlternAbonentInfo.Text = sb.ToString();
            }
        }

        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["ID"] = (gvJournal.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["EndValue"] = (gvJournal.Rows[e.RowIndex].FindControl("tbEndValue") as TextBox).Text;
        }

        protected void gvJournal2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["ID"] = (gvJournal2.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["EndValue"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbEndValue") as TextBox).Text;
            e.NewValues["StartValue"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbStartValue") as TextBox).Text;
            e.NewValues["FactoryNumber"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbFactoryNumber") as TextBox).Text;
        }

        protected void btAlternativeSpecialPay_Click(object sender, EventArgs e)
        {
            AlternativeAbonentDO aaDO = new AlternativeAbonentDO();
            AlternativeOrderDO aoDO = new AlternativeOrderDO();
            VodomerType vt = new VodomerType();
            AlternativeOrder ao;
            AlternativeAbonent aa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = aaDO.RetrieveBySOrderID(id);

            Vodomer vod = new Vodomer();
            VodomerDO vodDO = new VodomerDO();

            AlternativeOrderDetailsDO aodDO = new AlternativeOrderDetailsDO();
            AlternativeOrderDetails aods = new AlternativeOrderDetails();
            //FOrderDetails fod;
            double sum = 0; //гривневая цена
            double getvat = 0; //гривневый ндс
            double uafin = 0; //гривневая итоговая сумма
            double sumrub = 0; //рублевая цена
            double getvatrub = 0; //рублевый ндс
            double finish = 0; //рублевая итоговая сумма

            if (ue.Count > 0)
            {
                aa = (AlternativeAbonent)ue[0];
                ue = aoDO.RetrieveSOrderById(id);
                if (ue.Count > 0)
                {
                    ao = (AlternativeOrder)ue[0];

                    ue = aodDO.RetrieveSOrderDetailsBySorderID(id);
                    foreach (AlternativeOrderDetails fod in ue)
                    {
                        //гривневый счет
                        /*sum += fod.Price;
                        getvat = sum * 0.2;
                        getvat = Math.Round(getvat, 2);
                        uafin = (sum + getvat);*/
                        //рублевый счет
                        sumrub += fod.SpecialPrice;
                    }
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/payspecial.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("FIO", aa.FirstName + " " + aa.Surname + " " + aa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", aa.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PNONE", aa.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NUMBER", ao.Prefix + ao.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VIEW", ao.WorkType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("DIAMETR", vt.Diameter.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NOMZAVOD", vod.FactoryNumber.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VAT", getvat.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("ALL", uafin.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NDS", getvatrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/specialpay.docx"));
                        litScriptS.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?specialpay=Special\"></iframe>";
                    }
                }
            }
        }

        
          //генерация квитнации
        protected void btAlternativePay_Click(object sender, EventArgs e)
        {
            AlternativeAbonentDO aaDO = new AlternativeAbonentDO();
            AlternativeOrderDO aoDO = new AlternativeOrderDO();
            VodomerType vt = new VodomerType();
            AlternativeOrder ao;
            AlternativeAbonent aa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = aaDO.RetrieveBySOrderID(id);

            Vodomer vod = new Vodomer();
            VodomerDO vodDO = new VodomerDO();

            AlternativeOrderDetailsDO aodDO = new AlternativeOrderDetailsDO();
            AlternativeOrderDetails aods = new AlternativeOrderDetails();
            //FOrderDetails fod;
            double sum = 0; //гривневая цена
            double getvat = 0; //гривневый ндс
            double uafin = 0; //гривневая итоговая сумма
            double sumrub = 0; //рублевая цена
            double getvatrub = 0; //рублевый ндс
            double finish = 0; //рублевая итоговая сумма

            if (ue.Count > 0)
            {
                aa = (AlternativeAbonent)ue[0];
                ue = aoDO.RetrieveSOrderById(id);
                if (ue.Count > 0)
                {
                    ao = (AlternativeOrder)ue[0];

                    ue = aodDO.RetrieveSOrderDetailsBySorderID(id);
                    foreach (AlternativeOrderDetails fod in ue)
                    {
                        //гривневый счет
                        /*sum += fod.Price;
                        getvat = sum * 0.2;
                        getvat = Math.Round(getvat, 2);
                        uafin = (sum + getvat);*/
                        //рублевый счет
                        sumrub += fod.ReplacementPrice;
                    }
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/payaltern.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("FIO", aa.FirstName + " " + aa.Surname + " " + aa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", aa.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PNONE", aa.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NUMBER", ao.Prefix + ao.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VIEW", ao.WorkType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("DIAMETR", vt.Diameter.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NOMZAVOD", vod.FactoryNumber.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("VAT", getvat.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("ALL", uafin.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NDS", getvatrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/alternativepay.docx"));
                        litScriptS.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?alternativepay=Special\"></iframe>";
                    }
                }
            }
        }
         

        protected void btWithdrawalActSpecial_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateWithdrawalActSpecial(id);
            litScriptS.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?AlternativeAct=Special\"></iframe>";
        }

        protected void btReplacementAct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateReplacementAct(id);
            litScriptS.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ReplacementAct=Special\"></iframe>";
        }

        protected void radbutSaveAlternativeAbonentInfo_OnClick(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            AlternativeAbonent aa = new AlternativeAbonent();
            AlternativeAbonentDO aado = new AlternativeAbonentDO();
            ue = aado.RetrieveBySOrderID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                aa = (AlternativeAbonent) ue[0];
                aa.PhysicalNumberJournal = stbNumberJournalPhysical.Text;
                aa.FirstName = stbClientName.Text;
                aa.Surname = stbClientSurname.Text;
                aa.LastName = stbClientLastName.Text;
                aa.Phone = stbPhone.Text;
                aa.Address = tbAddress.Text;
                aado.UpdateWithHistory(aa, GetCurrentUser().ID);
            }

            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();

        }

        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            bool ok = true;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                if (string.IsNullOrEmpty((r.FindControl("litEndValue") as Literal).Text) && cbSeld.Checked)
                {
                    errorMessage += "Необходимо заполнить конечные показания водомеров. <br/>";
                    ok = false;
                    cbPaid.Checked = true;
                    break;
                }
            }
            if (cbPaid.Checked && string.IsNullOrEmpty(tbPaymentDay.Text))
            {
                ok = false;
                cbSeld.Checked = true;
                errorMessage += "Необходимо заполнить дату оплаты. <br/>";
            }
            else
            {
                AlternativeOrder ao = new AlternativeOrder();
                AlternativeOrderDO aodo = new AlternativeOrderDO();
                ao.ID = Convert.ToInt32(hfODID.Value);
                ao.IsPaid = cbPaid.Checked;
                ao.UserID = GetCurrentUser().ID;
                ao.PaymentDay = Convert.ToDateTime(tbPaymentDay.Text);
                if (cbSeld.Checked)
                {
                    ao.DateOut = DateTime.Now;
                }
                bool rez = aodo.UpdateSOrder(ao);
                _OrderID = ao.ID;
                Bind();
            }

        }

        protected void radbutDeleteAlternativeAbonentInfo_OnClick(object sender, EventArgs e)
        {
            AlternativeAbonentDO aado = new AlternativeAbonentDO();
            aado.Delete(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }
    }
}