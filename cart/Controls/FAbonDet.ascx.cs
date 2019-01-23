using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Entities;
using DomainObjects;
using System.IO;
using Novacode;


namespace cartridges_app.Controls
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
            double sumrub = 0;
            double getvat = 0;
            sumrub = GetSum();
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            return getvat;
        }
        //Итоговая сумма
        public double GetFinalSum()
        {
            double getvat = 0;
            double finish = 0;
            double sumrub = 0;
            sumrub = GetSum();
            getvat = GetVAT();
            finish = (sumrub + getvat);
            return finish;
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
        public double GetVATRub()
        {
            return Utilities.GetVATRubU(GetSumRub());
        }

        //Итоговая сумма (рубли)
        public double GetFinalSumRub()
        {
            double getvat = 0;
            double finish = 0;
            double sumrub = 0;
            sumrub = Money();
            getvat = NDS();
            finish = (sumrub + getvat) + (sumrub + getvat);
            return finish;
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

        //цена для снятия/установки водомера
        public double WithdrawalMoney()
        {
            double sumwithdrawal = 0;
            foreach (GridViewRow r in gvJournal.Rows)
            {
                sumwithdrawal += Convert.ToDouble((r.FindControl("specialprice") as Literal).Text);
            }
            return sumwithdrawal;
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
        void BindDistricts()
        {
            ddlDistrict.Items.Clear();
            //ddlDistrict.Items.Add(new ListItem("Выбор района", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrieveDistricts();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                 ddlDistrict.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }
        public void EditorMode()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            //проверяем наличие прав для редактировнаия
            if (u.ChekPermission("Editor"))
            {
                punAdonentEditor.Visible = true;
                gvJournal.Visible = false;
                gvJournal2.Visible = true;
                BindDistricts();
                UniversalEntity ue = new UniversalEntity();
                FAbonent fa = new FAbonent();
                FAbonentDO fado = new FAbonentDO();
                ue = fado.RetrieveByOrderID(_OrderID);
                if (ue.Count > 0)
                {
                    fa = (FAbonent)ue[0];
                    tbNumberJournalPhysical.Text = fa.PhysicalNumberJournal;
                    tbAddress.Text = fa.Address;
                    tbClientSurname.Text = fa.Surname;
                    ddlDistrict.SelectedValue = fa.DistrictID.ToString();
                    tbClientName.Text = fa.FirstName;
                    tbClientLastName.Text = fa.LastName;
                    tbPhone.Text = fa.Phone;
                    cbNotPay.Checked = fa.NotPay;
                    //cbRejectVodomer.Checked = fa.RejectVodomer;    
                }
                FOrder fo = new FOrder();
                FOrderDO fodDO = new FOrderDO();
                ue = fodDO.RetrieveFOrderById(_OrderID);
                if (ue.Count > 0)
                {
                    fo = (FOrder)ue[0];
                    cbDefectVodomer.Checked = fo.DefectVodomer;
                }
            }
            else
            {
                punAdonentEditor.Visible = false;
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
            cbRejectVodomer.Checked = false;
            cbRejectVodomer.Enabled = true;
            cbDefectVodomer.Checked = false;
            cbDefectVodomer.Enabled = true;


            tbPaymentDay.Enabled = true;
            tbPaymentDay.Text = DateTime.Now.ToShortDateString();


            FOrder fo = new FOrder();
            FOrderDO fodo = new FOrderDO();
            //StringBuilder sbl = new StringBuilder();
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
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                }
                if (fo.IsPaid)
                {
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                    cbSeld.Checked = true;
                    cbSeld.Enabled = false;
                    if (fo.PaymentDay.HasValue)
                        tbPaymentDay.Text = fo.PaymentDay.Value.ToShortDateString();
                    tbPaymentDay.Enabled = false;
                }
                if (fo.DefectVodomer)
                {
                    cbDefectVodomer.Checked = true;
                    cbDefectVodomer.Enabled = false;
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
                if (fa.NotPay)
                {
                    cbPaid.Enabled = false;
                    btActRub.Enabled = false;
                    //btPay.Enabled = false;
                    btOrder.Enabled = false;
                   //btOrderCheck.Enabled = false;
                    btActCheckRub.Enabled = false;
                    btWithdrawalAct.Enabled = false;
                }
                sb.AppendLine("<span>ФИО: " + fa.Surname + " " + fa.FirstName + " " + fa.LastName + "</span><br/>");
                sb.AppendLine("<span>номер по журналу: " + fa.PhysicalNumberJournal + "</span><br/>");
                sb.AppendLine("<span>Тел.: " + fa.Phone + "</span><br/>");
                sb.AppendLine("<span>Адрес: " + fa.Address + "</span><br/>");
                if (fa.NotPay)
                {
                    sb.AppendLine("<span style=\"color:red;font-size:18px;\">Без оплаты</span><br/>");
                }



                litAbonentInfo.Text = sb.ToString();
            }
        }
        //Обновляем конечные показания
        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["ID"] = (gvJournal.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["EndValue"] = (gvJournal.Rows[e.RowIndex].FindControl("tbEndValue") as TextBox).Text;
            e.NewValues["DefectVodomer"] = (gvJournal.Rows[e.RowIndex].FindControl("tbDefectVodomer") as TextBox).Text;
        }
        protected void gvJournal2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["ID"] = (gvJournal2.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["EndValue"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbEndValue") as TextBox).Text;
            e.NewValues["StartValue"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbStartValue") as TextBox).Text;
            e.NewValues["FactoryNumber"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbFactoryNumber") as TextBox).Text;
            e.NewValues["DefectVodomer"] = (gvJournal2.Rows[e.RowIndex].FindControl("tbDefectVodomer") as TextBox).Text;
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
                    cbPaid.Checked = true;
                    break;
                }
                /*if (string.IsNullOrEmpty((r.FindControl("litDefectVodomer") as Literal).Text) && cbSeld.Checked)
                {
                    errorMessage += "Необходимо указать бракован ли водомер.<br/>";
                    ok = false;
                    cbPaid.Checked = true;
                    break;
                }*/
            }
            if (cbPaid.Checked && string.IsNullOrEmpty(tbPaymentDay.Text))
            {
                ok = false;
                cbSeld.Checked = true;
                errorMessage += "Необходимо заполнить дату оплаты.<br/>";
            }
            /*if (cbDefectVodomer.Checked)
            {
                ok = false;
                cbDefectVodomer.Checked = true;
            }*/
            /*if (!ok)
            {
                radWM.RadAlert(errorMessage, null, null, "Поставьте галочку - Выдан", "");
                //SetMessege("Предупреждение", errorMessage);
            }*/
            else
            {
                FOrder fo = new FOrder();
                FOrderDO fodo = new FOrderDO();
                fo.ID = Convert.ToInt32(hfODID.Value);
                fo.IsPaid = cbPaid.Checked;
                fo.DefectVodomer = cbDefectVodomer.Checked;
                fo.UserID = GetCurrentUser().ID;
                fo.PaymentDay = Convert.ToDateTime(tbPaymentDay.Text);
                if (cbSeld.Checked)
                {
                    fo.DateOut = DateTime.Now;
                }
                bool rez = fodo.UpdateFOrder(fo);

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


        protected void btActRub_CLick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateFActRub(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ActRub=Private\"></iframe>";
        }

        protected void btActOld_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateFActOld(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ActOld=Private\"></iframe>";
        }

        protected void btWithdrawalkAct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            ExportToExcel.GenerateWithdrawalact(id);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?WithdrawalAct=Private\"></iframe>";
        }

        //генерация квитнации
        protected void btPay_Click(object sender, EventArgs e)
        {
            FAbonentDO faDO = new FAbonentDO();
            FOrderDO foDO = new FOrderDO();
            VodomerType vt = new VodomerType();
            FOrder fo;
            FAbonent fa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = faDO.RetrieveByOrderID(id);

            Vodomer vod = new Vodomer();
            VodomerDO vodDO = new VodomerDO();

            FOrderDetailsDO fodDO = new FOrderDetailsDO();
            FOrderDetails fods = new FOrderDetails();
            //FOrderDetails fod;
            double sum = 0; //гривневая цена
            double getvat = 0; //гривневый ндс
            double uafin = 0; //гривневая итоговая сумма
            double sumrub = 0; //рублевая цена
            double getvatrub = 0; //рублевый ндс
            double finish = 0; //рублевая итоговая сумма

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
                        //гривневый счет
                        sum += fod.Price;
                        getvat = sum * 0.2;
                        getvat = Math.Round(getvat, 2);
                        uafin = (sum + getvat);
                        //рублевый счет
                        sumrub += fod.Price;
                        getvatrub = sumrub * 0.2;
                        getvatrub = Math.Round(getvatrub, 2);
                        finish = (sumrub + getvatrub) + (sumrub + getvatrub);
                    }
                    using (DocX document = DocX.Load(Request.MapPath("~\\Templates/kvit.docx")))
                    {
                        //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                        document.ReplaceText("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ADDRESS", fa.Address, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("PNONE", fa.Phone, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DATE", DateTime.Now.ToString("dd MMMM yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NUMBER", fo.Prefix + fo.ID.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VIEW", fo.ActionType, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("DIAMETR", vt.Diameter.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("NOMZAVOD", vod.FactoryNumber.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VAT", getvat.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ALL", uafin.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //document.ReplaceText("NDS", getvatrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", finish.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                        //VODOMER
                        document.SaveAs(Request.MapPath("~\\Templates/kvitfa.docx"));
                        litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?pay=Private\"></iframe>";
                    }
                }

                /* Vodomer vod = new Vodomer();
                 VodomerDO vodDO = new VodomerDO();
                 ue = vodDO.RetrieveVodomerById(id);
                 if (ue.Count > 0)
                 {
                     vod = (Vodomer)ue[0];
                     ue = vodDO.RetrieveVodomerById(id);
                 }
                 using (DocX document = DocX.Load(Request.MapPath("~\\Templates/kvit.docx")))
                 {
                     document.ReplaceText("NOMZAVOD", vod.FactoryNumber);
                     document.SaveAs(Request.MapPath("~\\Templates/kvitfa.docx"));
                     litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?pay=Private\"></iframe>";
                 }*/
            }
        }



        protected void btOrder_Click(object sender, EventArgs e)
        {
            FAbonentDO faDO = new FAbonentDO();
            FAbonent fa;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                using (DocX document = DocX.Load(Request.MapPath("~\\Templates/order.docx")))
                {
                    //DocXExtender.ReplaceFormatedText(document, "DDD", "дата");
                    document.ReplaceText("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
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
            double sum = 0; //гривневая цена
            double getvat = 0; //гривневый ндс
            double uafin = 0; //гривневая итоговая сумма
            double sumrub = 0; //рублевая цена
            double getvatrub = 0; //рублвый ндс
            double finish = 0; //рублевая итоговая сумма

            //RetrieveFOrderByID (данные из FOrder + данные из FAbonent)
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                ue = foDO.RetrieveFOrderById(id);
                //RetrieveFOrderByID (данные из FOrder + данные из FAbonent)
                if (ue.Count > 0)
                {
                    fo = (FOrder)ue[0];

                    ue = fodDO.RetrieveFOrderDetailsByOrderID(id);
                    //цикл с RetrieveFOrderDetailsByOrderID (данные из FOrderDetails + данные из FOrder)
                    foreach (FOrderDetails fod in ue)
                    {
                        //гривневый счет
                        sum += fod.Price;
                        getvat = sum * 0.2;
                        uafin = (sum + getvat);
                        //рублевый счет
                        sumrub += fod.Price;
                        getvatrub = sumrub * 0.2;
                        finish = (sumrub + getvatrub) + (sumrub + getvatrub);
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
                        //  document.ReplaceText("SUM", sum.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //  document.ReplaceText("VAT", Utilities.GetVAT(sum).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("ALL", uafin.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //  document.ReplaceText("CENA", sumrub.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        //   document.ReplaceText("NDS", (Utilities.GetVATRub(sumrub)).ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        document.ReplaceText("VSEGO", finish.ToString("0.00"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);


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
            ExportToExcel.GenerateFActSpecial(id, u.Location);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?Act=PrivateSpecial\"></iframe>";
        }

        //рублевый акт + квитанция
        protected void btActCheckRub_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            User u = GetCurrentUser();
            ExportToExcel.GenerateFActSpecialRub(id, u.Location);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ActRub=PrivateSpecialRub\"></iframe>";
        }

        protected void btActCheckOld_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfODID.Value);
            User u = GetCurrentUser();
            ExportToExcel.GenerateFActSpecialOld(id, u.Location);
            litScript.Text = "<iframe style=\"display:none;\" src=\"../GetDocument.ashx?ActCheckOld=Private\"></iframe>";
        }



        protected void radbutSaveAbonentInfo_Click(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            FAbonent fa = new FAbonent();
            FAbonentDO fado = new FAbonentDO();
            ue = fado.RetrieveByOrderID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                fa.PhysicalNumberJournal = tbNumberJournalPhysical.Text;
                fa.Address = tbAddress.Text;
                fa.Surname = tbClientSurname.Text;
                fa.DistrictID = Utilities.ConvertToInt(ddlDistrict.SelectedValue);
                fa.FirstName = tbClientName.Text;
                fa.LastName = tbClientLastName.Text;
                fa.Phone = tbPhone.Text;
                fa.NotPay = cbNotPay.Checked;
                //fa.RejectVodomer = cbRejectVodomer.Checked;
                fado.Update(fa, GetCurrentUser().ID);
            }









            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }
        //radbutDeleteAbonentInfo_Click
        protected void radbutDeleteAbonentInfo_Click(object sender, EventArgs e)
        {
            FAbonentDO fado = new FAbonentDO();
            fado.Delete(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }

        public string _FactoryNumber { get; set; }
    }
}