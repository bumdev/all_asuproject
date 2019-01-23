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


namespace outcomming_mail.Controls
{
    public partial class OtherMailOutDet : ULControl
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
        /*void BindPhysicalAdresatType()
        {
            ddlPhysicalAdresatType.Items.Clear();
            //ddlDistrict.Items.Add(new ListItem("Выбор района", "0"));
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdo.RetrieveAdresatType();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlAdresatType.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }*/
        public void EditorMode()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            OtherMailSend ms = new OtherMailSend();
            OtherMailSendDO msDO = new OtherMailSendDO();
            UniversalEntity uen = new UniversalEntity();
            uen = msDO.RetrieveOtherMailSendByID(_SendID);
            if (uen.Count > 0)
            {
                ms = (OtherMailSend)uen[0];
                if (GetCurrentUser().ID == ms.UserID)
                {
                    punOtherMailOutEditor.Visible = true;
                    //gvMailOutJournal.Visible = true;
                    gvOtherMailOutJournal2.Visible = true;
                    //BindPhysicalAdresatType();
                    UniversalEntity ue = new UniversalEntity();
                    OtherMailOut fa = new OtherMailOut();
                    OtherMailOutDO fado = new OtherMailOutDO();
                    ue = fado.RetrieveByOtherMailSendID(_SendID);
                    if (ue.Count > 0)
                    {
                        fa = (OtherMailOut)ue[0];
                        tbOtherRegNumber.Text = fa.RegNumber;
                        tbOtherAdresat.Text = fa.AdreastType;
                        tbOtherWhom.Text = fa.Whom;
                        tbOtherNotation.Text = fa.Notation;
                        tbOtherAbout.Text = fa.About;
                        tbOtherDate.Text = fa.AnswerDate;
                        tbOtherAnswer.Text = fa.AnswerAbout;
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
                punOtherMailOutEditor.Visible = false;
                //gvMailOutJournal.Visible = true;
                gvOtherMailOutJournal2.Visible = true;
            }
        }

        public void Bind()
        {

            EditorMode();

            tbRegDate.Enabled = true;
            tbRegDate.Text = DateTime.Now.ToShortDateString();


            OtherMailSend ms = new OtherMailSend();
            OtherMailSendDO fodo = new OtherMailSendDO();
            UniversalEntity ue = new UniversalEntity();
            ue = fodo.RetrieveOtherMailSendByID(_SendID);
            if (ue.Count > 0)
            {

                ms = (OtherMailSend)ue[0];
                /*if (ms.DateRegister.HasValue)
                    tbRegDate.Text = ms.DateRegister.Value.ToShortDateString();*/
                tbRegDate.Text = ms.DateRegister;
                //tbRegDate.Enabled = true;
            }

            /*tbAnswerDate.Enabled = true;
            tbAnswerDate.Text = DateTime.Now.ToShortDateString();*/

            //UniversalEntity ue = new UniversalEntity();
            hfODID.Value = _SendID.ToString();
            StringBuilder sb = new StringBuilder();
            OtherMailOut fa = new OtherMailOut();
            OtherMailOutDO fado = new OtherMailOutDO();
            ue = fado.RetrieveByOtherMailSendID(_SendID);
            if (ue.Count > 0)
            {
                fa = (OtherMailOut)ue[0];
                sb.AppendLine("<span><b>Рег. номер:" + fa.RegNumber + "</span><br/>");
                sb.AppendLine("<span><b>Кому(адресат):" + fa.AdreastType + "</span><br/>");
                sb.AppendLine("<span><b>Кому(ФИО/название организации):" + fa.Whom + "</span><br/>");
                sb.AppendLine("<span><b>Примечание(кому ФИО): " + fa.Notation + "</span><br/>");
                sb.AppendLine("<span><b>О чем:" + fa.About + "</span><br/>");
                sb.AppendLine("<span><b>Дата ответа:" + fa.AnswerDate + "</span><br/>");
                sb.AppendLine("<span><b>О чем ответ:" + fa.AnswerAbout + "</span><br/>");

                litOtherMailOutInfo.Text = sb.ToString();
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
            MailSend ms = new MailSend();
            MailSendDO msDO = new MailSendDO();
            ms.ID = Convert.ToInt32(hfODID.Value);
            ms.UserID = GetCurrentUser().ID;
            /*if (ms.DateRegister.HasValue)
                tbRegDate.Text = ms.DateRegister.Value.ToShortDateString();*/
            //ms.DateRegister = Convert.ToDateTime(tbRegDate.Text);
            ms.DateRegister = tbRegDate.Text.Trim();
            _SendID = ms.ID;
            msDO.Update(ms);
            Bind();


            //fo.DateRegister = Convert.ToDateTime(tbRegDate.Text);
            //bool rez = msDO.Update(ms);

            //(this.Parent.FindControl("gvJournal") as GridView).DataBind();
            //this.Visible = false;
            //_SendID = fo.ID;
            //Bind();

        }




        protected void radbutSaveOtherMailOutInfo_Click(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            OtherMailOut pmo = new OtherMailOut();
            OtherMailOutDO pmDO = new OtherMailOutDO();

            ue = pmDO.RetrieveByOtherMailSendID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                pmo = (OtherMailOut)ue[0];
                pmo.RegNumber = tbOtherRegNumber.Text;
                pmo.AdreastType = tbOtherAdresat.Text;
                pmo.Whom = tbOtherWhom.Text;
                pmo.Notation = tbOtherNotation.Text;
                pmo.About = tbOtherAbout.Text;
                pmo.AnswerDate = tbOtherDate.Text;
                pmo.AnswerAbout = tbOtherAnswer.Text;
                //pmo.PhysicalAdresatID = Utilities.ConvertToInt(ddlPhysicalAdresat.SelectedValue);
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
            OtherMailOutDO omoDO = new OtherMailOutDO();
            omoDO.Delete(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _SendID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }
    }
}