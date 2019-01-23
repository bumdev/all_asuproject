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
    public partial class MailOutDet : ULControl
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
        void BindAdresatType()
        {
            ddlAdresatType.Items.Clear();
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
        }
        public void EditorMode()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            MailSend ms = new MailSend();
            MailSendDO msDO = new MailSendDO();
            UniversalEntity uen = new UniversalEntity();
            uen = msDO.RetrieveMailSendByID(_SendID);
            if (uen.Count > 0)
            {
                ms = (MailSend) uen[0];
                if (GetCurrentUser().ID == ms.UserID)
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
                punMailOutEditor.Visible = false;
                //gvMailOutJournal.Visible = true;
                gvMailOutJournal2.Visible = true;
            }
        }

        public void Bind()
        {

            EditorMode();
            
            tbRegDate.Enabled = true;
            tbRegDate.Text = DateTime.Now.ToShortDateString();


            MailSend ms = new MailSend();
            MailSendDO fodo = new MailSendDO();
            UniversalEntity ue = new UniversalEntity();
            ue = fodo.RetrieveMailSendByID(_SendID);
            if (ue.Count > 0)
            {

                ms = (MailSend)ue[0];
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
            MailOut fa = new MailOut();
            MailOutDO fado = new MailOutDO();
            ue = fado.RetrieveByMailSendID(_SendID);
            if (ue.Count > 0)
            {
                fa = (MailOut)ue[0];
                
                sb.AppendLine("<span>Рег. номер: " + fa.RegNumber + "</span><br/>");
                sb.AppendLine("<span>Лицевой счет: " + fa.PersonalAccount + "</span><br/>");
                sb.AppendLine("<span>Кому: " + fa.Whom + "</span><br/>");
                sb.AppendLine("<span>О чем: " + fa.About + "</span><br/>");
                sb.AppendLine("<span>Дата ответа: " + fa.AnswerDate + "</span><br/>");
                sb.AppendLine("<span>О чем ответ: " + fa.AnswerAbout + "</span><br/>");
                
                /*if (fa.AnswerDate.HasValue)
                    tbAnswerDate.Text = fa.AnswerDate.Value.ToShortDateString();*/
            
                litMailOutInfo.Text = sb.ToString();
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




        protected void radbutSaveMailOutInfo_Click(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            MailOut fa = new MailOut();
            MailOutDO fado = new MailOutDO();
            ue = fado.RetrieveByMailSendID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                fa = (MailOut)ue[0];
                fa.RegNumber = tbRegNumber.Text;
                fa.PersonalAccount = tbPersonalAccount.Text;
                fa.Whom = tbWhom.Text;
                fa.About = tbAbout.Text;
                fa.AnswerDate = tbAnswerDate.Text;
                fa.AnswerAbout = tbAnswerAbout.Text;
                //fa.AnswerDate = Convert.ToDateTime(tbAnswerDate.Text);
                fa.AdresatID = Utilities.ConvertToInt(ddlAdresatType.SelectedValue);
                fado.UpdateWithHistory(fa, GetCurrentUser().ID);
            }

            _SendID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }


        //radbutDeleteAbonentInfo_Click
        protected void radbutDeleteMailOutInfo_Click(object sender, EventArgs e)
        {
            MailOutDO fado = new MailOutDO();
            fado.Delete(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _SendID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }
    }
}