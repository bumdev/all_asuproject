using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Entities;
using DomainObjects;
using FirebirdSql.Data.FirebirdClient;
using Novacode;
using System.Text.RegularExpressions;
using System.Data;


namespace invent_app.Controls
{
    public partial class Controls_Wizard : ULControl
    {

        //Закрываем мастер
        private void CloseWizard()
        {
            Step1.Visible = true;
            Step2.Visible = false;
            repVodomer.DataBind();
            litAbonentInfo.Text = "";
            //litAbonentInfoStep3.Text = "";
            Session["Abonent"] = null;
            this.Visible = false;
        }
        private void LoadStep3()
        {


            if (Session["Abonent"] != null)
            {
                Step1.Visible = false;
                Step2.Visible = false;


                StringBuilder sb = new StringBuilder();
                SessionAbonent sa = (SessionAbonent)Session["Abonent"];
                if (hfClientType.Value == Abonent.Private.ToString())
                {
                    MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                    (p.FindControl("FAbonDet2") as FAbonDet).OrderID = Utilities.ConvertToInt(hfOrder.Value);
                    (p.FindControl("FAbonDet2") as FAbonDet).Visible = true;
                    (p.FindControl("FAbonDet2") as FAbonDet).Bind();
                    CloseWizard();
                    /*FAbonent fa = sa.FAbon;
                    sb.AppendLine("<span>" + fa.Surname + " " + fa.FirstName + " " + fa.Surname + "</span><br/>");
                    sb.AppendLine("<span>" + fa.Phone + "</span><br/>");
                    sb.AppendLine("<span>" + fa.Address + "</span><br/>");
                    litAbonentInfoStep3.Text = sb.ToString();
                    btAct.Visible = true;
                    btOrder.Visible = true;
                    btCheck.Visible = true;*/
                }
                if (hfClientType.Value == Abonent.Corporate.ToString())
                {
                    MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                    (p.FindControl("UAbonDet2") as UAbonDet).OrderID = Utilities.ConvertToInt(hfOrder.Value);
                    (p.FindControl("UAbonDet2") as UAbonDet).Visible = true;
                    (p.FindControl("UAbonDet2") as UAbonDet).Bind();
                    CloseWizard();

                    /*UAbonForm.Visible = true;
                    hfODID.Value = sa.UAbon.ID.ToString();
                    BindUabonent();*/
                    /*UAbonent ua = sa.UAbon;
                    sb.AppendLine("<span><b>" + ua.Title + "   " + ua.DogID.ToString() + "</b></span><br/>");
                    sb.AppendLine("<span> <b>ОКПО:</b>&nbsp;" + ua.OKPO + "</span><br/>");
                    sb.AppendLine("<span><b>МФО</b>: &nbsp;" + ua.MFO + "</span><br/>");
                    sb.AppendLine("<span><b>Р/С:</b>&nbsp;" + ua.RS + "</span><br/>");
                    sb.AppendLine("<span><b>Договор:</b>&nbsp;" + ua.Contract + "</span><br/>");
                    if (ua.IsBudget)
                        sb.AppendLine("<span><b>Бюджет</b></span><br/>");
                    sb.AppendLine("<span><b>Банк:&nbsp;</b>" + ua.Bank + "</span><br/>");
                    sb.AppendLine("<span><b>Адрес:</b>&nbsp;" + ua.Address + "</span><br/>");
                    litAbonentInfoStep3.Text = sb.ToString();
                    btAct.Visible = true;
                    btContract.Visible = true;
                    btBill.Visible = true;*/
                }
                if (hfClientType.Value == Abonent.Special.ToString())
                {
                    MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                    (p.FindControl("AlternAbonDet2") as AlternAbonDet).OrderID = Utilities.ConvertToInt(hfOrder.Value);
                    (p.FindControl("AlternAbonDet2") as AlternAbonDet).Visible = true;
                    (p.FindControl("AlternAbonDet2") as AlternAbonDet).Bind();
                    CloseWizard();
                }
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /*MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                (p.FindControl("UAbonDet1") as UAbonDet).OrderID = 1;
                (p.FindControl("UAbonDet1") as UAbonDet).Visible = true;
                (p.FindControl("UAbonDet1") as UAbonDet).Bind();*/
                //btAct.Visible = true;
                SetClientType();

            }
            //SetClientType();
        }


        #region Bind
        //Выбор типа абонента
        private void SetClientType()
        {
            if (hfClientType.Value == Abonent.Private.ToString())
            {
                lbClient.Text = "Физическое лицо";
                panCorporate.Visible = false;
                panPerson.Visible = true;
                panSpecial.Visible = false;
                BindDistricts();
            }
            if (hfClientType.Value == Abonent.Corporate.ToString())
            {
                lbClient.Text = "Юридическое лицо";
                panCorporate.Visible = true;
                panPerson.Visible = false;
                panSpecial.Visible = false;
            }
            if (hfClientType.Value == Abonent.Special.ToString())
            {
                lbClient.Text = "Снятие/установка водомеров";
                panCorporate.Visible = false;
                panPerson.Visible = false;
                panSpecial.Visible = true;
                BindDistricts();
            }
        }
        //Привязка диаметров
        void BindDiameter()
        {
            ddlDiameter1.Items.Clear();
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = crdo.RetrieveDiameters();
            ddlDiameter1.Items.Add(new ListItem("Диаметр", "-1"));
            ArrayList al;
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlDiameter1.Items.Add(new ListItem(al[0].ToString(), al[0].ToString()));
            }
        }
        //Привязка производителей по диаметру
        void BindSellers(int d)
        {
            ddlSeller.Items.Clear();
            Seller s = new Seller();
            SellerDO sdo = new SellerDO();
            UniversalEntity ue = new UniversalEntity();
            ue = sdo.RetrieveSellersByDiameter(d);
            ddlSeller.Items.Add(new ListItem("Производитель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                s = (Seller)ue[i];
                ddlSeller.Items.Add(new ListItem(s.SellerName, s.ID.ToString()));
            }
        }
        //Привязка производителей
        void BindSellers()
        {
            Seller s = new Seller();
            SellerDO sdo = new SellerDO();
            UniversalEntity ue = new UniversalEntity();
            ue = sdo.RetrieveSellers();
            ddlSeller.Items.Add(new ListItem("Производитель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                s = (Seller)ue[i];
                ddlSeller.Items.Add(new ListItem(s.SellerName, s.ID.ToString()));
            }
        }
        //Привязка моделей водомеров

        void BindModels(int id)
        {
            if (id > 0)
            {
                ddlModel.Items.Clear();
                VodomerType v = new VodomerType();
                VodomerTypeDO vdo = new VodomerTypeDO();
                UniversalEntity ue = new UniversalEntity();
                int diameter = Convert.ToInt32(ddlDiameter1.SelectedValue);
                ue = vdo.RetrieveVodomerTypeBySellerIdAndDiameter(id, diameter);
                ddlModel.Items.Add(new ListItem("Модель", "0"));
                for (int i = 0; i < ue.Count; i++)
                {
                    v = (VodomerType)ue[i];
                    ddlModel.Items.Add(new ListItem(v.ConventionalSignth + " " + v.Description + " " + v.Diameter.ToString(), v.ID.ToString()));
                }
            }
            else
            {
                ddlModel.Items.Clear();
            }
        }
        //Привязка районов
        void BindDistricts()
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Add(new ListItem("Выбор района", "0"));
            ddDistrictWithdrawal.Items.Clear();
            ddDistrictWithdrawal.Items.Add(new ListItem("Выбор района", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrieveDistricts();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlDistrict.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
                ddDistrictWithdrawal.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }
        //Привязка водомеров
        private void BindVodomer()
        {
            if (Session["Abonent"] != null)
            {
                SessionAbonent sa = (SessionAbonent)Session["Abonent"];

                List<VodomerPreview> vpl = new List<VodomerPreview>();
                VodomerPreview vp = new VodomerPreview();


                repVodomer.DataSource = sa.Vodomer;
                repVodomer.DataBind();
            }
        }
        #endregion

        protected void ddlSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindModels(Convert.ToInt32(ddlSeller.SelectedValue));
        }
        protected void lbClient_Click(object sender, EventArgs e)
        {
            if (hfClientType.Value == Abonent.Private.ToString())
            {
                hfClientType.Value = Abonent.Corporate.ToString();
                hfClientType.Value = Abonent.Special.ToString();
                SetClientType();
                return;
            }
            if (hfClientType.Value == Abonent.Corporate.ToString())
            {
                hfClientType.Value = Abonent.Special.ToString();
                hfClientType.Value = Abonent.Private.ToString();
                SetClientType();
                return;
            }
            if (hfClientType.Value == Abonent.Special.ToString())
            {
                hfClientType.Value = Abonent.Private.ToString();
                hfClientType.Value = Abonent.Corporate.ToString();
                SetClientType();
                return;
            }
        }

        //добавление снятия/установки водомера
        protected void WithdrawalAbonentAdd_Click(object sender, EventArgs e)
        {
            Session["Abonent"] = null;
            SessionAbonent sa = new SessionAbonent();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbWithdrawalFirstName.Text))
            {
                sbError.Append("Необходимо заполнить фамилию.<br/>");
            }
            if (string.IsNullOrEmpty(tbWithdrawalSurname.Text))
            {
                sbError.Append("Необходимо заполнить имя.<br/>");
            }
            if (string.IsNullOrEmpty(tbWithdrawaLastName.Text))
            {
                sbError.Append("Необходимо заполнить отчество.<br/>");
            }
            if (string.IsNullOrEmpty(tbWithdrawalPhone.Text))
            {
                sbError.Append("Необходимо запонить телефон.<br/>");
            }
            if (string.IsNullOrEmpty(tbSpecialAddress.Text))
            {
                sbError.Append("Необходимо заполнить адрес.<br/>");
            }
            if (ddDistrictWithdrawal.SelectedValue == "0")
            {
                sbError.Append("Необходимо заполнить район.<br/>");
            }
            if (sbError.Length > 0)
            {
                SetMessege("Предупреждение", sbError.ToString());
            }
            else
            {
                if (hfClientType.Value == Abonent.Special.ToString())
                {
                    sa.Type = (short)Abonent.Special;
                    AlternativeAbonent sab = new AlternativeAbonent();
                    sab.Surname = tbWithdrawalSurname.Text.Trim();
                    sab.LastName = tbWithdrawaLastName.Text.Trim();
                    sab.FirstName = tbWithdrawalFirstName.Text.Trim();
                    sab.Address = tbSpecialAddress.Text.Trim();
                    sab.Phone = tbWithdrawalPhone.Text.Trim();
                    sab.PhysicalNumberJournal = tbWithdrawalNumberJournal.Text.Trim();
                    sab.DistrictID = Convert.ToInt32(ddDistrictWithdrawal.SelectedValue);
                    sa.AlternativeAbon = sab;
                    sb.AppendLine("<b><span>Номер по журналу:" + sab.PhysicalNumberJournal + "</span><br/>");
                    sb.AppendLine("<b><span>ФИО:" + sab.FirstName + " " + sab.Surname + " " + sab.LastName + "</span><br/>");
                    sb.AppendLine("<b><span>Телефон: " + sab.Phone + "</span><br/>");
                    sb.AppendLine("<b><span>Адрес: " + sab.Address + "</span><br/>");
                    litAbonentInfo.Text = sb.ToString();
                }

                Session["Abonent"] = sa;
                ClearSAbonentForm();
                Step1.Visible = false;
                Step2.Visible = true;
                BindSellers();
                BindDiameter();
            }

        }

        public void ClearSAbonentForm()
        {
            tbWithdrawalSurname.Text = "";
            tbWithdrawalFirstName.Text = "";
            tbWithdrawaLastName.Text = "";
            tbWithdrawalPhone.Text = "";
            tbWithdrawalNumberJournal.Text = "";
            tbSpecialAddress.Text = "";
            ddDistrictWithdrawal.SelectedValue = "0";

        }


        //Добавление абонента физ лицо
        protected void lbAbonentAdd_Click(object sender, EventArgs e)
        {
            Session["Abonent"] = null;
            SessionAbonent sa = new SessionAbonent();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbClientSurname.Text))
            {
                sbError.Append("Необходимо заполнить фамилию.<br/>");
            }
            if (string.IsNullOrEmpty(tbClientName.Text))
            {
                sbError.Append("Необходимо заполнить имя.<br/>");
            }
            if (string.IsNullOrEmpty(tbClientLastName.Text))
            {
                sbError.Append("Необходимо заполнить отчество.<br/>");
            }
            if (string.IsNullOrEmpty(tbNumberJournal.Text))
            {
                sbError.Append("Необходимо заполнить номер по журналу.<br/>");
            }
            if (string.IsNullOrEmpty(tbAddress.Text))
            {
                sbError.Append("Необходимо заполнить адрес.<br/>");
            }
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                sbError.Append("Необходимо заполнить телефон.<br/>");
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                sbError.Append("Необходимо выбрать район.<br/>");
            }

            if (sbError.Length > 0)
            {
                SetMessege("Предупреждение", sbError.ToString());
            }
            else
            {
                if (hfClientType.Value == Abonent.Private.ToString())
                {
                    sa.Type = (short)Abonent.Private;
                    FAbonent fa = new FAbonent();
                    fa.Surname = tbClientSurname.Text.Trim();
                    fa.FirstName = tbClientName.Text.Trim();
                    fa.LastName = tbClientLastName.Text.Trim();
                    fa.PhysicalNumberJournal = tbNumberJournal.Text.Trim();
                    fa.Phone = tbPhone.Text.Trim();
                    fa.Address = tbAddress.Text.Trim();
                    fa.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    fa.NotPay = cbNotPay.Checked;
                    sa.FAbon = fa;

                    sb.AppendLine("<span>Номер по журналу: " + fa.PhysicalNumberJournal + "</span><br/>");
                    sb.AppendLine("<span>ФИО: " + fa.Surname + " " + fa.FirstName + " " + fa.Surname + "</span><br/>");
                    sb.AppendLine("<span>Тел.: " + fa.Phone + "</span><br/>");
                    sb.AppendLine("<span>Адрес: " + fa.Address + "</span><br/>");
                    if (fa.NotPay)
                    {
                        sb.AppendLine("<span>Без оплаты</span><br/>");
                    }
                    litAbonentInfo.Text = sb.ToString();
                }
                Session["Abonent"] = sa;
                ClearFAbonentForm();
                Step1.Visible = false;
                Step2.Visible = true;
                BindSellers();
                BindDiameter();
            }
        }
        //Очистка формы
        private void ClearFAbonentForm()
        {
            tbClientSurname.Text = "";
            tbClientName.Text = "";
            tbClientLastName.Text = "";
            tbNumberJournal.Text = "";
            tbPhone.Text = "";
            tbAddress.Text = "";
            ddlDistrict.SelectedValue = "0";
            cbNotPay.Checked = false;
        }
        protected void lbBack_Click(object sender, EventArgs e)
        {
            CloseWizard();
        }
        protected void lbBack1_Click(object sender, EventArgs e)
        {
            CloseWizard();
        }
        protected void lbBack3_Click(object sender, EventArgs e)
        {
            CloseWizard();
        }
        //Поиск абонента по ОКПО
        protected void lbFindOKPO_Click(object sender, EventArgs e)
        {
            /*
             Поиск по базе водосбыта
             */
            if (tbCorporateOKPO.Text.Length >= 4)
            {
                try
                {
                    FbConnection fbCn = new FbConnection("Database=water_wdk;Server=db3;User=viewer_;Password=VIEWER_;Role=viewer;");
                    FbDataAdapter fbDa = new FbDataAdapter();
                    FbCommand fbCmd = new FbCommand();
                    //DataTable dt = new DataTable();

                    fbCn.Open();
                    fbCmd = new FbCommand(string.Format(@"select delzero(d.id_dog)||' / '||r.name_ray||' район' dogovor,d.id_doc, d.okpo,d.bank,d.r_s,d.mfo,
                    h.full_name,h.ur_address,
                    case k.gr when 3 then 1 when 4 then 1 when 5 then 1 when 6 then 1 else 0 end budzhet,h.inn, h.id_svid,
                    d.info_1 kontact,
                    d.v_litse,d.na_osnovanii,d.otv_litso,d.contact_phone,d.ot_imeni,h.sys_no,s.name_no
                    from dogovor d
                    
                    left join rayon r on r.id_ray=d.id_ray
                    left join history h on h.id_doc=d.id_doc
                    left join history n on n.id_doc=h.id_doc
                    and (n.datebeg>h.datebeg or n.datebeg=h.datebeg and n.kod>h.kod)
                    left join kods k on k.podgr=h.podgr
left join spr_sys_no s on s.id_no=h.sys_no
                    where d.okpo like '%'||{0}||'%'
                    and d.date_close_d is null
                    and n.kod is null", tbCorporateOKPO.Text), fbCn);

                    FbDataReader dr = fbCmd.ExecuteReader();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(dr);

                    if (dt.Rows.Count > 0)
                    {
                        repCorporate.DataSource = dt;
                        repCorporate.DataBind();
                        litSearch.Text = "";
                    }
                    else
                    {
                        repCorporate.DataBind();
                        litSearch.Text = "Нет данных";
                    }
                    dr.Close();
                    fbCn.Close();

                }
                catch (Exception ex)
                {
                    SetMessege("Ошибка", "Сбой подключения к базе данных   <br>" + ex.Message);
                }
            }
            else
            {
                SetMessege("Предупреждение", "Для поиска необходимо ввести минимум 4 символа.");
            }

        }
        //Добавление юр. лица
        protected void repCorporate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            Session["Abonent"] = null;
            SessionAbonent sa = new SessionAbonent();
            StringBuilder sb = new StringBuilder();

            if (hfClientType.Value == Abonent.Corporate.ToString())
            {
                sa.Type = (short)Abonent.Corporate;
                UAbonent ua = new UAbonent();

                ua.Title = (e.Item.FindControl("litFULL_NAME") as Literal).Text;
                ua.DogID = Convert.ToInt32((e.Item.FindControl("litID_DOC") as Literal).Text);

                ua.IsBudget = (e.Item.FindControl("litBUDZHET") as Literal).Text == "1" ? true : false; // Convert.ToBoolean((e.Item.FindControl("litBUDZHET") as Literal).Text);
                ua.MFO = (e.Item.FindControl("litMFO") as Literal).Text;
                ua.RS = (e.Item.FindControl("litR_S") as Literal).Text;
                ua.Contract = (e.Item.FindControl("litDOGOVOR") as Literal).Text;
                ua.Bank = (e.Item.FindControl("litBANK") as Literal).Text;
                ua.Address = (e.Item.FindControl("litUR_ADDRESS") as Literal).Text;


                ua.Phone = (e.Item.FindControl("litPhone") as Literal).Text;
                ua.ContactFace = (e.Item.FindControl("litFace") as Literal).Text;
                ua.Cause = (e.Item.FindControl("litCause") as Literal).Text;
                ua.INN = (e.Item.FindControl("litInn") as Literal).Text;
                ua.VATPay = (e.Item.FindControl("litSvidetelstvo") as Literal).Text;
                ua.TaxType = (e.Item.FindControl("litTaxType") as Literal).Text;


                ua.OKPO = (e.Item.FindControl("litOKPO") as Literal).Text; //tbCorporateOKPO.Text.Trim(); hfOKPO

                sa.UAbon = ua;

                //Вывод инфы по абоненту
                sb.AppendLine("<span><b>" + ua.Title + "   " + ua.DogID.ToString() + "</b></span><br/>");
                sb.AppendLine("<span> <b>ОКПО:</b>&nbsp;" + ua.OKPO + "</span>&nbsp;&nbsp;&nbsp;");
                sb.AppendLine("<span><b>МФО</b>: &nbsp;" + ua.MFO + "</span>&nbsp;&nbsp;&nbsp;");
                sb.AppendLine("<span><b>Р/С:</b>&nbsp;" + ua.RS + "</span>&nbsp;&nbsp;&nbsp;");
                sb.AppendLine("<span><b>Договор:</b>&nbsp;" + ua.Contract + "</span>&nbsp;&nbsp;&nbsp;");
                if (ua.IsBudget)
                    sb.AppendLine("<span><b>Бюджет</b></span><br/>");
                sb.AppendLine("<span><b>Банк:&nbsp;</b>" + ua.Bank + "</span><br/>");
                sb.AppendLine("<span><b>Адрес:</b>&nbsp;" + ua.Address + "</span><br/>");

                litAbonentInfo.Text = sb.ToString();
            }


            Session["Abonent"] = sa;
            Step1.Visible = false;
            Step2.Visible = true;
            BindSellers();
            BindDiameter();
        }
        //Добавление водомера
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            if (string.IsNullOrEmpty(tbStartValue.Text) || string.IsNullOrEmpty(tbFactoryNumber.Text) || ddlModel.SelectedValue == "0" || ddlModel.SelectedValue == "" || !regex.IsMatch(tbStartValue.Text) || ddlDiameter1.SelectedValue == "-1" || ddlSeller.SelectedValue == "0")
            {
                StringBuilder sb = new StringBuilder();

                if (ddlDiameter1.SelectedValue == "-1")
                {
                    sb.Append("Необходимо выбрать диаметр.<br/>");
                }
                else
                {
                    if (ddlSeller.SelectedValue == "0")
                    {
                        sb.Append("Необходимо выбрать производителя.<br/>");
                    }
                    else
                    {
                        if (ddlModel.SelectedValue == "0" || ddlModel.SelectedValue == "")
                        {
                            sb.Append("Необходимо выбрать модель.<br/>");
                        }
                    }
                }
                if (string.IsNullOrEmpty(tbStartValue.Text))
                {
                    sb.Append("Необходимо заполнить паказания.<br/>");
                }
                else
                {
                    if (!regex.IsMatch(tbStartValue.Text))
                    {
                        sb.Append("Показания не корректно заполнены.<br/>");
                    }
                }

                if (string.IsNullOrEmpty(tbFactoryNumber.Text))
                {
                    sb.Append("Необходимо заполнить заводской номер.<br/>");
                }

                SetMessege("Предупреждение", sb.ToString());
            }
            else
            {
                Vodomer v = new Vodomer();
                v.VodomerType = Convert.ToInt32(ddlModel.SelectedValue);
                v.FactoryNumber = tbFactoryNumber.Text.Trim();
                VodomerPreview vp = new VodomerPreview();
                vp.Diameter = Convert.ToInt32(ddlDiameter1.SelectedValue);
                vp.Model = ddlModel.SelectedItem.Text;
                vp.Seller = ddlSeller.SelectedItem.Text;
                vp.StartValue = tbStartValue.Text.Trim();
                v.VodomerPreview = vp;

                SessionAbonent sa = (SessionAbonent)Session["Abonent"];
                sa.AddVodomer(v);
                BindVodomer();
                tbFactoryNumber.Text = tbStartValue.Text = "";
            }
        }
        //Удаление водомера
        protected void repVodomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SessionAbonent sa = (SessionAbonent)Session["Abonent"];
            string fn = (e.Item.FindControl("litFN") as Literal).Text;
            foreach (Vodomer v in sa.Vodomer)
            {
                if (v.FactoryNumber == fn)
                {
                    sa.Vodomer.Remove(v);
                    break;
                }
            }
            Session["Abonent"] = sa;
            BindVodomer();
        }
        //Сохранение в базу
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            if (Session["Abonent"] != null)
            {
                SessionAbonent sa = (SessionAbonent)Session["Abonent"];
                if (sa.Vodomer.Count == 0)
                {
                    SetMessege("Предупреждение", "Необходимо добавить минимум 1 водомер.");
                }
                else
                {
                    if (sa.Type == (short)Abonent.Corporate)
                    {
                        UAbonent ua = sa.UAbon;
                        UAbonentDO uado = new UAbonentDO();
                        int uid = uado.Create(ua);
                        if (uid > 0)
                        {
                            sa.UAbon.ID = uid;
                            Session["Abonent"] = sa;
                            UOrder uo = new UOrder();
                            UOrderDO uodo = new UOrderDO();
                            uo.ActionType = "Определения метрологических характеристик водомера.";
                            uo.UAbonentID = uid;
                            uo.UserID = GetCurrentUser().ID;

                            int uoid = uodo.Create(uo);
                            if (uoid > 0)
                            {

                                hfOrder.Value = uoid.ToString();
                                UOrderDetails uod = new UOrderDetails();
                                UOrderDetailsDO uoddo = new UOrderDetailsDO();
                                VodomerDO vdo = new VodomerDO();
                                foreach (Vodomer v in sa.Vodomer)
                                {
                                    int vid = vdo.Create(v);
                                    uod.UOrderID = uoid;
                                    uod.VodomerID = vid;
                                    uod.StartValue = v.VodomerPreview.StartValue;
                                    int uodid = uoddo.Create(uod);
                                }
                            }
                        }
                    }
                    if (sa.Type == (short)Abonent.Private)
                    {
                        FAbonent fa = sa.FAbon;
                        FAbonentDO fado = new FAbonentDO();
                        int fid = fado.Create(fa);
                        if (fid > 0)
                        {
                            sa.FAbon.ID = fid;
                            Session["Abonent"] = sa;
                            FOrder fo = new FOrder();
                            FOrderDO fodo = new FOrderDO();
                            fo.ActionType = "Определения метрологических характеристик водомера.";
                            fo.FAbonentID = fid;
                            fo.UserID = GetCurrentUser().ID;

                            int foid = fodo.Create(fo);
                            if (foid > 0)
                            {
                                hfOrder.Value = foid.ToString();
                                FOrderDetails fod = new FOrderDetails();
                                FOrderDetailsDO foddo = new FOrderDetailsDO();
                                VodomerDO vdo = new VodomerDO();
                                foreach (Vodomer v in sa.Vodomer)
                                {
                                    int vid = vdo.Create(v);
                                    fod.FOrderID = foid;
                                    fod.VodomerID = vid;
                                    fod.StartValue = v.VodomerPreview.StartValue;
                                    int uodid = foddo.Create(fod);
                                }
                            }
                        }
                    }
                    if (sa.Type == (short)Abonent.Special)
                    {
                        AlternativeAbonent sab = sa.AlternativeAbon;
                        AlternativeAbonentDO sabo = new AlternativeAbonentDO();
                        int fid = sabo.Create(sab);
                        if (fid > 0)
                        {
                            sa.AlternativeAbon.ID = fid;
                            Session["Abonent"] = sa;
                            AlternativeOrder so = new AlternativeOrder();
                            AlternativeOrderDO sod = new AlternativeOrderDO();
                            so.WorkType = "Снятие/установка водомеров.";
                            so.SAbonentID = fid;
                            so.UserID = GetCurrentUser().ID;

                            int soid = sod.CreateSOrder(so);
                            if (soid > 0)
                            {
                                hfOrder.Value = soid.ToString();
                                AlternativeOrderDetails sd = new AlternativeOrderDetails();
                                AlternativeOrderDetailsDO sodDO = new AlternativeOrderDetailsDO();
                                VodomerDO vdo = new VodomerDO();
                                foreach (Vodomer v in sa.Vodomer)
                                {
                                    int vid = vdo.Create(v);
                                    sd.SOrderID = soid;
                                    sd.VodomerID = vid;
                                    sd.StartValue = v.VodomerPreview.StartValue;
                                    int uodid = sodDO.Create(sd);
                                }
                            }

                        }
                    }
                    //SetMessege("Статус", "Абонент и водомер успешно внесены в базу.");
                    LoadStep3();
                }
            }
        }

        protected void ddlDiameter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSellers(Utilities.ConvertToInt(ddlDiameter1.SelectedValue));
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }





        protected void lbVodomerSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
            tbVodomerSearch.Focus();
        }
        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }
        protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (string.IsNullOrEmpty(tbVodomerSearch.Text))
            {
                e.Command.Parameters["@q"].Value = "+++";
            }
        }
        protected void gvJournal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            /*string a= gvJournal.SelectedDataKey.Value.ToString();
            index = Convert.ToInt32(e.CommandArgument); //get row number selected
            GridViewRow row = gvJournal.Rows[index];*/


        }

        protected void gvJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            VodomerType vt = new VodomerType();
            VodomerTypeDO vtDO = new VodomerTypeDO();
            UniversalEntity ue = new UniversalEntity();
            ue = vtDO.RetrieveVodomerById(Utilities.ConvertToInt(gvJournal.SelectedDataKey.Value.ToString()));
            if (ue.Count > 0)
            {
                vt = (VodomerType)ue[0];
            }
            ddlDiameter1.SelectedValue = vt.Diameter.ToString();

            ddlSeller.SelectedValue = vt.SellerID.ToString();
            BindModels(vt.SellerID);
            ddlModel.SelectedValue = vt.ID.ToString();
            tbVodomerSearch.Text = "";
            gvJournal.DataBind();
            panVodomerSearch.Visible = false;
            Step2.CssClass = "";

        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            tbVodomerSearch.Text = "";
            gvJournal.DataBind();
            panVodomerSearch.Visible = false;
            Step2.CssClass = "";
        }

        protected void lbVS_Click(object sender, EventArgs e)
        {
            panVodomerSearch.Visible = true;
            tbVodomerSearch.Focus();
            Step2.CssClass = "Step2";
        }
    }
}