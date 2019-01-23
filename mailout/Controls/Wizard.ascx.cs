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
using NPOI.HPSF;
using NPOI.SS.Formula.Functions;
using outcomming_mail.Controls;
using Telerik.Web.UI;


namespace outcomming_mail
{
    public partial class Wizard : ULControl
    {

        private void SetMailOutType()
        {
            if (hfClientType.Value == Mail.Out.ToString())
            {
                lbClient.Text = "Исходящая почта";
                panMailOut.Visible = true;
                BindAdresatType();
            }
        }
        
        private void CloseWizard()
        {
            Step1.Visible = true;
            Step2.Visible = false;
            repResponContract.DataBind();
            litMailInfo.Text = "";
            Session["Mail"] = null;
            this.Visible = true;

        }

        private void LoadStep3()
        {
            if (Session["Mail"] != null)
            {
                Step1.Visible = false;
                Step2.Visible = false;
                StringBuilder sb = new StringBuilder();
                SessionMail sm = (SessionMail) Session["Mail"];
                if (hfClientType.Value == Mail.Out.ToString())
                {
                    MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                    (p.FindControl("MailOutDet2") as MailOutDet).SendID = Utilities.ConvertToInt(hfOrder.Value);
                    (p.FindControl("MailOutDet2") as MailOutDet).Visible = true;
                    (p.FindControl("MailOutDet2") as MailOutDet).Bind();
                    CloseWizard();
                }
            }
        }

        //привязка отдела по
        /*void BindDepartament(int d)
        {
            ddlDepartament.Items.Clear();
            Departament s = new Departament();
            DepartamentDO sdo = new DepartamentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = sdo.RetrieveResponContractDepartament(d);
            ddlDepartament.Items.Add(new ListItem("Отдел", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                s = (Departament) ue[i];
                ddlDepartament.Items.Add(new ListItem(s.DepartamentName, s.ID.ToString()));
            }
        }*/

        //привязка отдела
        void BindDepartament()
        {
            Departament d = new Departament();
            DepartamentDO dDO = new DepartamentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = dDO.RetrieveDeprtaments();
            ddlDepartament1.Items.Add(new ListItem("Отдел", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                d = (Departament) ue[i];
                ddlDepartament1.Items.Add(new ListItem(d.DepartamentName, d.ID.ToString()));
            }
        }

        //привязка ответственного исполнителя по отделу
        void BindResponContract(int id)
        {
            if (id > 0)
            {
                ddlResponContract.Items.Clear();
                DirectoryResponContract rc = new DirectoryResponContract();
                DirectoryResponContractDO rcDO = new DirectoryResponContractDO();
                UniversalEntity ue = new UniversalEntity();
                ue = rcDO.RetrieveResponContractByDepartament(id);
                ddlResponContract.Items.Add(new ListItem("Ответственный исполнитель", "0"));
                for (int i = 0; i < ue.Count; i++)
                {
                    rc = (DirectoryResponContract)ue[i];
                    ddlResponContract.Items.Add(new ListItem(rc.ResponName, rc.ID.ToString()));
                }

            }
            
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetMailOutType();
            }
        }

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

        private void CheckCountResponContract()
        {
            if (Session["Mail"] == null) return;
            var sm = (SessionMail)Session["Mail"];
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
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                if (hfClientType.Value == Mail.Out.ToString())
                {
                    sm.Type = (short)Mail.Out;
                    MailOut mo = new MailOut();
                    mo.RegNumber = tbRegNumber.Text.Trim();
                    mo.Whom = tbWhom.Text.Trim();
                    mo.About = tbAbout.Text.Trim();
                    mo.AdresatID = Convert.ToInt32(ddlAdresatType.SelectedValue);
                    sm.OutMail = mo;
                    sb.AppendLine("<b><span>Регистрационный номер: " + mo.RegNumber + "</span><br/>");
                    sb.AppendLine("<b><span>Кому: " + mo.Whom + "</span><br/>");
                    sb.AppendLine("<b><span>О чем: " + mo.About + "</span><br/>");
                    litMailInfo.Text = sb.ToString();
                }

                Session["Mail"] = sm;
                ClearMailOutForm();
                Step1.Visible = false;
                Step2.Visible = true;
                BindResponContract();
                BindDepartament();
            }
        }

        public void ClearMailOutForm()
        {
            tbRegNumber.Text = "";
            tbWhom.Text = "";
            tbAbout.Text = "";
            ddlAdresatType.SelectedValue = "0";
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

        protected void lbAddMail_OnClick(object sender, EventArgs e)
        {

        }

        protected void lbClient_Click(object sender, EventArgs e)
        {
            if (hfClientType.Value == Mail.Out.ToString())
            {
                SetMailOutType();
                return;
            }
        }

        /*protected void radgridD_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgridD.DataSource = (DataView)dsD.Select(DataSourceSelectArguments.Empty);
            }
        }*/

        /*protected void radgridD_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    hfDepartamentName.Value = dataItem["DepartamentName"].Text;
                    radgridD.Visible = false;
                    radgridP.Visible = true;
                    radgridP.Rebind();
                    litD.Text = dataItem["DepartamentName"].Text;
                }
            }
        }*/

        /*protected void radgridP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgridP.DataSource = (DataView)dsP.Select(DataSourceSelectArguments.Empty);
            }
        }*/

       /* protected void radgridP_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    hfResponContractor.Value = dataItem.GetDataKeyValue("ID").ToString();
                    litS.Text = dataItem["ResponName"].Text;
                    panValues.Visible = true;
                }
                if (e.CommandName == "BackToDepartamentName")
                {
                    radgridP.Visible = false;
                    radgridD.Visible = true;
                    litS.Text = "";
                }
            }
        }*/

        protected void ddlDepartament_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindResponContract(Convert.ToInt32(ddlResponContract.SelectedValue));
        }

        //добавление ответственного исполнителя
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            if (ddlResponContract.SelectedValue == "0" || ddlResponContract.SelectedValue == "" ||
                ddlDepartament1.SelectedValue == "0" || ddlDepartament1.SelectedValue == "0")
            {
                StringBuilder sb = new StringBuilder();
                if (ddlDepartament1.SelectedValue == "-1")
                {
                    sb.Append("Необходимо выбрать отдел.<br/>");
                }
                else
                {
                    if (ddlResponContract.SelectedValue == "0" || ddlResponContract.SelectedValue == "")
                    {
                        sb.Append("Необходимо выбрать ответственного исполнителя.<br/>");
                    }
                }
                SetMessege("Предупреждение", sb.ToString());
            }
            else
            {
                ResponsibleContractor rc = new ResponsibleContractor();
                rc.ResponContractDirectory = Convert.ToInt32(ddlResponContract.SelectedValue);
                ResponContractPreview rcp = new ResponContractPreview();
                rcp.DepartamentName = ddlDepartament1.SelectedItem.Text;
                rcp.ResponName = ddlResponContract.SelectedItem.Text;
                rc.ResponContractPreview = rcp;

                SessionMail sa = (SessionMail) Session["Mail"];
                sa.AddResponContract(rc);
                BindResponContract();
            }
        }

        //удаление ответственного исполнителя
        protected void repResponContract_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SessionMail sm = (SessionMail)Session["Mail"];
            string fn = (e.Item.FindControl("lbResponContract") as Literal).Text;
            foreach (ResponsibleContractor rc in sm.ResponsibleContractor)
            {
                if (rc.District == fn)
                {
                    sm.ResponsibleContractor.Remove(rc);
                    break;
                }
            }
            Session["Mail"] = sm;
            BindResponContract();
            CheckCountResponContract();
        }

        //Сохранение в базу
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            if (Session["Mail"] != null)
            {
                SessionMail sm = (SessionMail)Session["Mail"];
                if (sm.ResponsibleContractor.Count == 0)
                {
                    radWM.RadAlert("Необходимо добавить ответственного исполнителя.", null, null, "Предупреждение", "");
                }
                else
                {
                    if (sm.Type == (short)Mail.Out)
                    {
                        MailOut mo = new MailOut();
                        MailOutDO moDO = new MailOutDO();
                        int mid = moDO.Create(mo);
                        if (mid > 0)
                        {
                            sm.OutMail.ID = mid;
                            Session["Mail"] = sm;
                            MailSend sms = new MailSend();
                            MailSendDO smsDO = new MailSendDO();
                            sms.MailOutID = mid;
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
                    LoadStep3();
                }
            }
        }

        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        protected void ddlDepartament1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindResponContract(Utilities.ConvertToInt(ddlDepartament1.SelectedValue));
        }


        private void ExecuteSearch()
        {
            DataView view = (DataView)dsMailJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void lbResponContractSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
            tbResponContractSearch.Focus();
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

        protected void lbRC_Click(object sender, EventArgs e)
        {
            panResponContractSearch.Visible = false;
            tbResponContractSearch.Focus();
            Step2.CssClass = "Step2";
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            tbResponContractSearch.Text = "";
            gvMailJournal.DataBind();
            panResponContractSearch.Visible = false;
            Step2.CssClass = "";
        }

        protected void gvMailJournal_SelctedIndexChanged(object sender, EventArgs e)
        {
            ResponsibleContractor rc = new ResponsibleContractor();
            ResponsibleContractorDO rcDO = new ResponsibleContractorDO();
            UniversalEntity ue = new UniversalEntity();

            ue = rcDO.RetrieveResponContractByID(Utilities.ConvertToInt(gvMailJournal.SelectedDataKey.Value.ToString()));
            if (ue.Count > 0)
            {
                rc = (ResponsibleContractor) ue[0];
            }

            ddlDepartament1.SelectedValue = rc.ResponContractDirectory.ToString();
            BindResponContract(rc.ResponContractDirectory);
            ddlResponContract.SelectedValue = rc.ID.ToString();
            tbResponContractSearch.Text = "";
            gvMailJournal.DataBind();
            panResponContractSearch.Visible = false;
            Step2.CssClass = "";
        }
    }

        
}