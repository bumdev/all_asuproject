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
using Telerik.Web.UI;


namespace outcomming_mail.Controls
{
    public partial class PhysicalMailOutDet : ULControl
    {



        int _SendID = 0;

        public int SendID
        {
            //get { return _OrderID; }
            set { _SendID = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            litScript.Text = "";
            //Bind();
        }
        void BindPhysicalAdresatType()
        {
            ddlPhysicalAdresat.Items.Clear();
            //ddlDistrict.Items.Add(new ListItem("Выбор района", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrievePhysicalAdresatType();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlPhysicalAdresat.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        void BindSender()
        {
            ddlSender.Items.Clear();
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

        public void EditorMode()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            PhysicalMailSend ms = new PhysicalMailSend();
            PhysicalMailSendDO msDO = new PhysicalMailSendDO();
            UniversalEntity uen = new UniversalEntity();
            uen = msDO.RetrievePhysicalMailSendID(_SendID);
            if (uen.Count > 0)
            {
                ms = (PhysicalMailSend)uen[0];
                if (GetCurrentUser().ID == ms.UserID)
                {
                    punPhysicalMailOutEditor.Visible = true;
                    //gvMailOutJournal.Visible = true;
                    gvPhysicalMailOutJournal2.Visible = true;
                    BindPhysicalAdresatType();
                    BindSender();
                    UniversalEntity ue = new UniversalEntity();
                    PhysicalMailOut fa = new PhysicalMailOut();
                    PhysicalMailOutDO fado = new PhysicalMailOutDO();
                    ue = fado.RetrieveBySendID(_SendID);
                    if (ue.Count > 0)
                    {
                        fa = (PhysicalMailOut)ue[0];
                        tbPhysicalRegNumber.Text = fa.RegNumber;
                        tbPhysicalContractNumber.Text = fa.ContractNumber;
                        tbPhysicalWhom.Text = fa.Whom;
                        tbPhysicalNotation.Text = fa.Notation;
                        tbPhysicalAbout.Text = fa.About;
                        tbPhysicalDate.Text = fa.AnswerDate;
                        tbPhysicalAnswer.Text = fa.AnswerAbout;
                        ddlPhysicalAdresat.SelectedValue = fa.PhysicalAdresatID.ToString();
                        ddlSender.SelectedValue = fa.SenderID.ToString();
                    }
                }
            }
            //проверяем наличие прав для редактировнаия
            /*if (u.ChekPermission("Editor"))
            {
                punMailOutEditor.Visible = true;
                //gvMailOutJournal.Visible = true;
                gvMailOutJournal2.Visible = true;
                BindAdresatType();
                UniversalEntity ue = new UniversalEntity();
                MailOut fa = new MailOut();
                MailOutDO fado = new MailOutDO();
                ue = fado.RetrieveByMailSendID(_SendID);
                if (ue.Count > 0)
                {
                    fa = (MailOut)ue[0];
                    tbRegNumber.Text = fa.RegNumber;
                    tbPersonalAccount.Text = fa.PersonalAccount;
                    tbWhom.Text = fa.Whom;
                    tbAbout.Text = fa.About;
                    tbAnswerAbout.Text = fa.AnswerAbout;
                    tbAnswerDate.Text = fa.AnswerDate;
                    ddlAdresatType.SelectedValue = fa.AdresatID.ToString();  
                }
            }*/
            else
            {
                punPhysicalMailOutEditor.Visible = false;
                //gvMailOutJournal.Visible = true;
                gvPhysicalMailOutJournal2.Visible = true;
            }
        }

        public void Bind()
        {

            EditorMode();

            tbRegDate.Enabled = true;
            tbRegDate.Text = DateTime.Now.ToShortDateString();


            PhysicalMailSend ms = new PhysicalMailSend();
            PhysicalMailSendDO fodo = new PhysicalMailSendDO();
            UniversalEntity ue = new UniversalEntity();
            ue = fodo.RetrievePhysicalMailSendID(_SendID);
            if (ue.Count > 0)
            {

                ms = (PhysicalMailSend)ue[0];
                /*if (ms.DateRegister.HasValue)
                    tbRegDate.Text = ms.DateRegister.Value.ToShortDateString();*/
                tbRegDate.Text = ms.DateReg;
                //tbRegDate.Enabled = true;
            }

            /*tbAnswerDate.Enabled = true;
            tbAnswerDate.Text = DateTime.Now.ToShortDateString();*/

            //UniversalEntity ue = new UniversalEntity();
            hfODID.Value = _SendID.ToString();
            StringBuilder sb = new StringBuilder();
            PhysicalMailOut fa = new PhysicalMailOut();
            PhysicalMailOutDO fado = new PhysicalMailOutDO();
            ue = fado.RetrieveBySendID(_SendID);
            if (ue.Count > 0)
            {
                fa = (PhysicalMailOut)ue[0];
                sb.AppendLine("<span><b>Рег. номер: " + fa.RegNumber + "</span><br/>");
                sb.AppendLine("<span><b>Номер договора: " + fa.ContractNumber + "</span><br/>");
                sb.AppendLine("<span><b>Кому: " + fa.Whom + "</span><br/>");
                sb.AppendLine("<span><b>Примечание: " + fa.Notation + "</span><br/>");
                sb.AppendLine("<span><b>О чем: " + fa.About + "</span><br/>");
                sb.AppendLine("<span><b>Дата ответа: " + fa.AnswerDate + "</span><br/>");
                sb.AppendLine("<span><b>О чем ответ: " + fa.AnswerAbout + "</span><br/>");

                litPhysicalMailOutInfo.Text = sb.ToString();
            }

            //lbSaveAll.Visible = false;
            //litMailOutInfo.Text = sb.ToString();
        }

        /* protected void gvMailOutJournal2_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
         {
             e.NewValues["ID"] = (gvMailOutJournal2.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
             e.NewValues["date_registration"] = (gvMailOutJournal2.Rows[e.RowIndex].FindControl("tbRegDate") as TextBox).Text;
             GridViewRow row = (GridViewRow)gvMailOutJournal2.Rows[e.RowIndex];
             TextBox tbDateRegsiter = (TextBox)gvMailOutJournal2.Rows[row.RowIndex].FindControl("tbRegDate");
             ResponsibleContractor rc = new ResponsibleContractor();
             rc.DateRegister = Convert.ToDateTime(tbDateRegsiter.Text);
             if (rc.DateRegister.HasValue)
                 tbDateRegsiter.Text = rc.DateRegister.Value.ToShortDateString();
         }*/


        //Сохраняем всё
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {

            PhysicalMailSend pms = new PhysicalMailSend();
            PhysicalMailSendDO pmsdo = new PhysicalMailSendDO();
            pms.ID = Convert.ToInt32(hfODID.Value);
            pms.UserID = GetCurrentUser().ID;
            pms.DateReg = tbRegDate.Text;

            bool rez = pmsdo.Update(pms);

            //(this.Parent.FindControl("gvJournal") as GridView).DataBind();
            //this.Visible = false;
            _SendID = pms.ID;
            Bind();

        }




        protected void lbSavePhysicalMail_Click(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            PhysicalMailOut pmo = new PhysicalMailOut();
            PhysicalMailOutDO pmDO = new PhysicalMailOutDO();

            ue = pmDO.RetrieveBySendID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                pmo = (PhysicalMailOut)ue[0];
                pmo.RegNumber = tbPhysicalRegNumber.Text;
                pmo.ContractNumber = tbPhysicalContractNumber.Text;
                pmo.Whom = tbPhysicalWhom.Text;
                pmo.Notation = tbPhysicalNotation.Text;
                pmo.About = tbPhysicalAbout.Text;
                pmo.AnswerDate = tbPhysicalDate.Text;
                pmo.AnswerAbout = tbPhysicalAnswer.Text;
                pmo.PhysicalAdresatID = Utilities.ConvertToInt(ddlPhysicalAdresat.SelectedValue);
                pmo.SenderID = Utilities.ConvertToInt(ddlSender.SelectedValue);
                pmDO.UpdateWithHistory(pmo, GetCurrentUser().ID);
                //panEdit.Visible = false;
                //panView.Visible = true;
            }

            _SendID = Utilities.ConvertToInt(hfODID.Value);
            Bind();

        }


        //radbutDeleteAbonentInfo_Click
        protected void lbDelete_Click(object sender, EventArgs e)
        {
            PhysicalMailOutDO pmoDO = new PhysicalMailOutDO();
            pmoDO.Delete(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _SendID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }
    }
}