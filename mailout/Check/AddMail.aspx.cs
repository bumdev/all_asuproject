using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using NPOI.SS.Formula.Functions;
using Telerik.Web.UI;


namespace outcomming_mail
{
    public partial class AddMail : ULPage
    {
        private void CloseWizard()
        {
            Step1.Visible = true;
            Step2.Visible = false;
            repResponContract.DataBind();
            litMailInfo.Text = "";
            Session["Mail"] = null;
            this.Visible = false;
        }
        
        private void SetMailOutType()
        {
            if (hfClientType.Value == Mail.Out.ToString())
            {
                lbClient.Text = "Корреспонденция физ. лица";
                panMailOut.Visible = true;
                panPhysicalMail.Visible = false;
                panOtherMail.Visible = false;
                BindAdresatType();
            }
            if (hfClientType.Value == Mail.Physical.ToString())
            {
                lbClient.Text = "Корреспонденция юр. лица";
                panMailOut.Visible = false;
                panPhysicalMail.Visible = true;
                panOtherMail.Visible = false;
                BindPhysicalAdresatType();
                BindSender();
            }
            if (hfClientType.Value == Mail.Other.ToString())
            {
                lbClient.Text = "Прочая корреспонденция";
                panMailOut.Visible = false;
                panPhysicalMail.Visible = false;
                panOtherMail.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetMailOutType();
            }
        }

        #region Bind
        //привязка типа адресата
        void BindAdresatType()
        {
            ddlAdresatType.Items.Clear();
            ddlAdresatType.Items.Add(new ListItem("Тип Адресата", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrieveAdresatType();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlAdresatType.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        void BindSender()
        {
            ddlSender.Items.Clear();
            ddlSender.Items.Add(new ListItem("Отправитель", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrieveSender();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList) ue[i];
                ddlSender.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        void BindPhysicalAdresatType()
        {
            ddlPhysicalAdresatType.Items.Clear();
            ddlPhysicalAdresatType.Items.Add(new ListItem("Тип Адресата", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrievePhysicalAdresatType();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList) ue[i];
                ddlPhysicalAdresatType.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        private void CheckCountResponContract()
        {
            if (Session["Mail"] == null) return;
            var sm = (SessionMail) Session["Mail"];
            lbSaveAll.Visible = sm.ResponsibleContractor.Count > 0;
        }

        //привязка ответственного исполнителя

        private void BindResponContract()
        {
            if (Session["Mail"] != null)
            {
                SessionMail sa = (SessionMail)Session["Mail"];

                List<ResponContractPreview> rcpl = new List<ResponContractPreview>();
                ResponContractPreview rcp = new ResponContractPreview();

                repResponContract.DataSource = sa.ResponsibleContractor;
                repResponContract.DataBind();
            }
        }
        #endregion

        //добавление корреспонденции юр лиц
        protected void PhysicalMailAdd_OnClick(object sender, EventArgs e)
        {
            Session["Mail"] = null;
            SessionMail sm = new SessionMail();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbPhysicalRegNumber.Text))
            {
                sbError.Append("Необходимо заполнить регистрационный номер. <br/>");
            }
            if (ddlPhysicalAdresatType.SelectedValue =="0")
            {
                sbError.Append("Необходимо выбрать тип адресата. <br/>");
            }
            if (string.IsNullOrEmpty(tbPhysicalWhom.Text))
            {
                sbError.Append("Необходимо заполнить Кому. <br/>");
            }
            if (string.IsNullOrEmpty(tbPhysicalAbout.Text))
            {
                sbError.Append("Необходимо заполнить О чем. <br/>");
            }
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предпреждение", "");
            }
            else
            {
                if (hfClientType.Value == Mail.Physical.ToString())
                {
                    sm.Type = (short) Mail.Physical;
                    PhysicalMailOut pmo = new PhysicalMailOut();
                    pmo.RegNumber = tbPhysicalRegNumber.Text.Trim();
                    pmo.ContractNumber = tbPhysicalContractNumber.Text.Trim();
                    pmo.Whom = tbPhysicalWhom.Text.Trim();
                    pmo.Notation = tbPhysicalNotation.Text.Trim();
                    pmo.About = tbPhysicalAbout.Text.Trim();
                    pmo.AnswerAbout = tbPhysicalAboutAnswer.Text.Trim();
                    pmo.AnswerDate = tbPhysicalAnswerDate.Text.Trim();
                    pmo.PhysicalAdresatID = Convert.ToInt32(ddlPhysicalAdresatType.SelectedValue);
                    pmo.SenderID = Convert.ToInt32(ddlSender.SelectedValue);
                    sm.PhysicalMailOut = pmo;
                    sb.AppendLine("<b><span>Рег. номер*: " + pmo.RegNumber + "</span><br/>");
                    sb.AppendLine("<b><span>Номер договора: " + pmo.ContractNumber + "</span><br/>");
                    sb.AppendLine("<b><span>Кому(название организации)*: " + pmo.Whom + "</span><br/>");
                    sb.AppendLine("<b><span>Примечание(кому ФИО): " + pmo.Notation + "</span><br/>");
                    sb.AppendLine("<b><span>О чем*: " + pmo.About + "</span><br/>");
                    sb.AppendLine("<b><span>О чем ответ: " + pmo.AnswerAbout + "</span><br/>");
                    sb.AppendLine("<b><span>Дата ответа: " + pmo.AnswerDate + "</span><br/>");
                    litMailInfo.Text = sb.ToString();
                }

                Session["Mail"] = sm;
                ClearPhysicalMailForm();
                Step1.Visible = false;
                Step2.Visible = true;
                radgridD.Rebind();
            }
        }

        public void ClearPhysicalMailForm()
        {
            tbPhysicalRegNumber.Text = "";
            tbPhysicalWhom.Text = "";
            tbPhysicalAbout.Text = "";
            tbPhysicalAboutAnswer.Text = "";
            ddlPhysicalAdresatType.SelectedValue = "0";
            ddlSender.SelectedValue = "0";
        }

        //добавление исходящей почты
         protected void MailOutAdd_Click(object sender, EventArgs e)
        {
            Session["Mail"] = null;
            SessionMail sm = new SessionMail();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbRegNumber.Text))
            {
                sbError.Append("Необходимо заполнить регистрационный номер. <br/>");
            }
            if (ddlAdresatType.SelectedValue == "0")
            {
                sbError.Append("Необходимо выбрать признак адресата. <br/>");
            }
            if (string.IsNullOrEmpty(tbWhom.Text))
            {
                sbError.Append("Необходимо заполнить Кому.<br/>");
            }
            if (string.IsNullOrEmpty(tbAbout.Text))
            {
                sbError.Append("Необходимо заполнить О чем. <br/>");
            }
             /*if (string.IsNullOrEmpty(tbAnswerAbout.Text))
             {
                 sbError.Append("Необходимо заполнить ответ. <br/>");
             }
             if (string.IsNullOrEmpty(tbAnswerDate.Text))
             {
                 sbError.Append("Необходимо указать дату ответа. <br/>");
             }*/
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                if (hfClientType.Value == Mail.Out.ToString())
                {
                    sm.Type = (short) Mail.Out;
                    MailOut mo = new MailOut();
                    mo.RegNumber = tbRegNumber.Text.Trim();
                    mo.Whom = tbWhom.Text.Trim();
                    mo.PersonalAccount = tbPersonalAccount.Text.Trim();
                    //mo.Notation = tbNotation.Text.Trim();
                    mo.About = tbAbout.Text.Trim();
                    mo.AnswerAbout = tbAnswerAbout.Text.Trim();
                    mo.AnswerDate = tbAnswerDate.Text.Trim();
                    //mo.Notation = tbNotation.Text.Trim();
                    /*mo.AnswerDate = Convert.ToDateTime(tbAnswerDate.Text.Trim());
                    if (mo.AnswerDate.HasValue)
                        tbAnswerDate.Text = mo.AnswerDate.Value.ToShortDateString();*/
                    mo.AdresatID = Convert.ToInt32(ddlAdresatType.SelectedValue);
                    sm.OutMail = mo;
                    sb.AppendLine("<b><span>Рег номер*: " + mo.RegNumber + "</span><br/>");
                    sb.AppendLine("<b><span>Лицевой счет: " + mo.PersonalAccount + "</span><br/>");
                    sb.AppendLine("<b><span>Кому(ФИО)*: " + mo.Whom + "</span><br/>");
                    //sb.AppendLine("<b><span>Примечание: " + mo.Notation + "</span><br/>");
                    sb.AppendLine("<b><span>О чем*: " + mo.About + "</span><br/>");
                    sb.AppendLine("<b><span>О чем ответ: " + mo.AnswerAbout + "</span><br/>");
                    sb.AppendLine("<b><span>Дата ответа: " + mo.AnswerDate + "</span><br/>");
                    litMailInfo.Text = sb.ToString();
                }

                Session["Mail"] = sm;
                ClearMailOutForm();
                Step1.Visible = false;
                Step2.Visible = true;
                radgridD.Rebind();
            }
        }

        

        public void ClearMailOutForm()
        {
            tbRegNumber.Text = "";
            tbWhom.Text = "";
            tbAbout.Text = "";
            tbAnswerAbout.Text = "";
            ddlAdresatType.SelectedValue = "0";
        }

        protected void OtherMailAdd_Click(object sender, EventArgs e)
        {
            Session["Mail"] = null;
            SessionMail sm = new SessionMail();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbOtherRegNumber.Text))
            {
                sbError.Append("Необходимо заполнить рег. номер. <br/>");
            }
            if (string.IsNullOrEmpty(tbOtherAdresat.Text))
            {
                sbError.Append("Необходимо заполнить тип адресата. <br/>");
            }
            if (string.IsNullOrEmpty(tbOtherWhom.Text))
            {
                sbError.Append("Необходимо заполнить кому. <br/>");
            }
            if (string.IsNullOrEmpty(tbOtherAbout.Text))
            {
                sbError.Append("Необходимо заполнить о чем. <br/>");
            }
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                if (hfClientType.Value == Mail.Other.ToString())
                {
                    sm.Type = (short)Mail.Other;
                    OtherMailOut omo = new OtherMailOut();
                    omo.RegNumber = tbOtherRegNumber.Text.Trim();
                    omo.AdreastType = tbOtherAdresat.Text.Trim();
                    omo.Whom = tbOtherWhom.Text.Trim();
                    omo.Notation = tbOtherNotation.Text.Trim();
                    omo.About = tbOtherAbout.Text.Trim();
                    omo.AnswerDate = tbOtherAnswerDate.Text.Trim();
                    omo.AnswerAbout = tbOtherAnswerAbout.Text.Trim();
                    sm.OtherMailOut = omo;
                    sb.AppendLine("<b><span>Рег номер*: " + omo.RegNumber + "</span><br/>");
                    sb.AppendLine("<b><span>Кому(тип адресата)*:" + omo.AdreastType + "</span><br/>");
                    sb.AppendLine("<b><span>Кому(ФИО)*: " + omo.Whom + "</span><br/>");
                    sb.AppendLine("<b><span>Примечание(кому ФИО/примечание): " + omo.Notation + "</span><br/>");
                    sb.AppendLine("<b><span>О чем*: " + omo.About + "</span><br/>");
                    sb.AppendLine("<b><span>О чем ответ: " + omo.AnswerAbout + "</span><br/>");
                    sb.AppendLine("<b><span>Дата ответа: " + omo.AnswerDate + "</span><br/>");
                    litMailInfo.Text = sb.ToString();
                }

                Session["Mail"] = sm;
                ClearOtherMailForm();
                Step1.Visible = false;
                Step2.Visible = true;
                radgridD.Rebind();
            }
        }

        public void ClearOtherMailForm()
        {
            tbOtherAbout.Text = "";
            tbOtherAdresat.Text = "";
            tbOtherWhom.Text = "";
            tbOtherAbout.Text = "";
            //tbOtherAnswerDate.Text = "";
            //tbOtherAnswerAbout.Text = "";
        }


        protected void lbClient_Click(object sender, EventArgs e)
        {
            if (hfClientType.Value == Mail.Out.ToString())
            {
                hfClientType.Value = Mail.Physical.ToString();
                hfClientType.Value = Mail.Other.ToString();
                SetMailOutType();
            }
            else if (hfClientType.Value == Mail.Physical.ToString())
            {
                hfClientType.Value = Mail.Out.ToString();
                hfClientType.Value = Mail.Other.ToString();
                SetMailOutType();
            }
            else if (hfClientType.Value == Mail.Other.ToString())
            {
                hfClientType.Value = Mail.Out.ToString();
                hfClientType.Value = Mail.Physical.ToString();
                SetMailOutType();
            }
        }

        protected void radgridD_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgridD.DataSource = (DataView)dsD.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radgridD_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    hfDepartamentName.Value = dataItem.GetDataKeyValue("ID").ToString();
                    radgridD.Visible = false;
                    radgridP.Visible = true;
                    radgridP.Rebind();
                    litD.Text = dataItem["DepartamentName"].Text;
                }
            }
        }

        protected void radgridP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgridP.DataSource = (DataView)dsP.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radgridP_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    hfResponContractor.Value = dataItem.GetDataKeyValue("ID").ToString();
                    litRC.Text = dataItem["ResponName"].Text;
                   // radgridP.Rebind();
                    panValues.Visible = true;
                    //lbSaveAll.Visible = true;
                }
            }
            if (e.CommandName == "BackToDepartamentName")
            {
                radgridP.Visible = false;
                radgridD.Visible = true;
                litRC.Text = "";
            }
        }

        //добавление ответственного исполнителя
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            if (string.IsNullOrEmpty(tbDateRegistration.Text))
            {
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrEmpty(tbDateRegistration.Text))
                {
                    sb.Append("Необходимо указать дату регистрации. <br/>");
                }
            }
            else
            {
                MailSend ms = new MailSend();
                /*ms.DateRegister = Convert.ToDateTime(tbDateRegistration.Text);
                if (ms.DateRegister.HasValue)
                    tbDateRegistration.Text = ms.DateRegister.Value.ToShortDateString();*/
                ms.DateRegister = tbDateRegistration.Text.Trim();
                ResponsibleContractor rc = new ResponsibleContractor();
                rc.ResponContractDirectory = Convert.ToInt32(hfResponContractor.Value);               
                ResponContractPreview rcp = new ResponContractPreview();
                rcp.DepartamentName = litD.Text;
                rcp.ResponName = litRC.Text;
                
                rc.ResponContractPreview = rcp;

                SessionMail sa = (SessionMail)Session["Mail"];
                sa.AddResponContract(rc);
                BindResponContract();
                //tbDateRegistration.Text = "";
            }
            CheckCountResponContract();
        }

        //удаление ответственного исполнителя
        protected void repResponContract_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SessionMail sm = (SessionMail) Session["Mail"];
            string fn = (e.Item.FindControl("ResponName") as Literal).Text;
            foreach (ResponsibleContractor rc in sm.ResponsibleContractor)
            {
               // rc.DateRegister = Convert.ToDateTime(tbDateRegistration.Text);
                if (rc.ResponContractDirectory.ToString() == fn)
                {
                    sm.ResponsibleContractor.Remove(rc);
                    break;
                }
            }
            Session["Mail"] = sm;
            BindResponContract();
            CheckCountResponContract();
        }

        protected void lbResponContractSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
            tbResponContractSearch.Focus();
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            tbResponContractSearch.Text = "";
            gvMailJournal.DataBind();
            panResponContractSearch.Visible = false;
            Step2.CssClass = "";
        }

        protected void lbRC_Click(object sender, EventArgs e)
        {
            panResponContractSearch.Visible = true;
            tbResponContractSearch.Focus();
            Step2.CssClass = "Step2";
        }

        private void ExecuteSearch()
        {
            DataView view = (DataView) dsMailJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsMailJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (string.IsNullOrEmpty(tbResponContractSearch.Text))
            {
                e.Command.Parameters["@q"].Value = "+++";
            }
        }

        protected void gvMailJournal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
        }

        protected void gvMailJournal_SelectedChanged(object sender, EventArgs e)
        {
            
        }

        //Сохранение в базу
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            if (Session["Mail"] != null)
            {
                SessionMail sm = (SessionMail) Session["Mail"];
                if (sm.ResponsibleContractor.Count == 0)
                {
                    radWM.RadAlert("Необходимо добавить ответственного исполнителя.", null, null, "Предупреждение", "");
                }
                else
                {
                    if (sm.Type == (short)Mail.Out)
                    {
                        MailOut mo = sm.OutMail;
                        MailOutDO moDO = new MailOutDO();
                        int mid = moDO.Create(mo);
                        if (mid > 0)
                        {
                            sm.OutMail.ID = mid;
                            Session["Mail"] = sm;
                            MailSend sms = new MailSend();
                            MailSendDO smsDO = new MailSendDO();
                            sms.DateRegister = tbDateRegistration.Text.Trim();
                            /*sms.DateRegister = Convert.ToDateTime(tbDateRegistration.Text);
                            if (sms.DateRegister.HasValue)
                                tbDateRegistration.Text = sms.DateRegister.Value.ToShortDateString();*/
                            sms.MailOutID = mid;
                            sms.UserID = GetCurrentUser().ID;

                            int smsid = smsDO.Create(sms);
                            if (smsid > 0)
                            {
                                hfOrder.Value = smsid.ToString();
                                MailDetails md = new MailDetails();
                                MailDetailsDO mdDO = new MailDetailsDO();

                                ResponsibleContractorDO rcDO = new ResponsibleContractorDO();
                                foreach (ResponsibleContractor rc in sm.ResponsibleContractor)
                                {
                                    int rcid = rcDO.Create(rc);
                                    md.MailSendID = smsid;
                                    md.ResponsibleContractorID = rcid;
                                    int msmid = mdDO.Create(md);
                                }
                                Response.Redirect("MailOutJournal.aspx?id=" + smsid.ToString());
                            }
                        }
                    }
                    if (sm.Type == (short) Mail.Physical)
                    {
                        PhysicalMailOut pmo = sm.PhysicalMailOut;
                        PhysicalMailOutDO pmDO = new PhysicalMailOutDO();
                        int pmid = pmDO.Create(pmo);
                        if (pmid > 0)
                        {
                            sm.PhysicalMailOut.ID = pmid;
                            Session["Mail"] = sm;
                            PhysicalMailSend pms = new PhysicalMailSend();
                            PhysicalMailSendDO pmsDO = new PhysicalMailSendDO();
                            pms.DateReg = tbDateRegistration.Text.Trim();
                            pms.PhysicalMailOutID = pmid;
                            pms.UserID = GetCurrentUser().ID;

                            int pmsid = pmsDO.Create(pms);
                            if (pmsid > 0)
                            {
                                hfOrder.Value = pmsid.ToString();
                                PhysicalMailDetails pmd = new PhysicalMailDetails();
                                PhysicalMailDetailsDO pmdDO = new PhysicalMailDetailsDO();

                                ResponsibleContractorDO rcDO = new ResponsibleContractorDO();
                                foreach (ResponsibleContractor rc in sm.ResponsibleContractor)
                                {
                                    int rcid = rcDO.Create(rc);
                                    pmd.PhysicalMailSendID = pmsid;
                                    pmd.ResponsibleContractorID = rcid;
                                    int msmid = pmdDO.Create(pmd);
                                }
                                Response.Redirect("PhysicalMailOutJournal.aspx?id=" + pmsid.ToString());
                            }
                        }

                    }
                    if (sm.Type == (short) Mail.Other)
                    {
                        OtherMailOut omo = sm.OtherMailOut;
                        OtherMailOutDO omoDO = new OtherMailOutDO();
                        int oid = omoDO.Create(omo);
                        if (oid > 0)
                        {
                            sm.OtherMailOut.ID = oid;
                            Session["Mail"] = sm;
                            OtherMailSend oms = new OtherMailSend();
                            OtherMailSendDO omsDO = new OtherMailSendDO();
                            oms.DateRegister = tbDateRegistration.Text.Trim();
                            oms.OtherMailOutID = oid;
                            oms.UserID = GetCurrentUser().ID;

                            int osid = omsDO.Create(oms);
                            if (osid > 0)
                            {
                                hfOrder.Value = osid.ToString();
                                OtherMailDetails omd = new OtherMailDetails();
                                OtherMailDetailsDO omdDO = new OtherMailDetailsDO();

                                ResponsibleContractorDO rcDO = new ResponsibleContractorDO();
                                foreach (ResponsibleContractor rc in sm.ResponsibleContractor)
                                {
                                    int rcid = rcDO.Create(rc);
                                    omd.OtherMailSendID = osid;
                                    omd.ResponsibleContractorID = rcid;
                                    int omsid = omdDO.Create(omd);
                                }
                                Response.Redirect("OtherMailOutJournal.aspx?id=" + osid.ToString());
                            }
                        }
                    }
                }
                    
                
            }
        }
    }
}