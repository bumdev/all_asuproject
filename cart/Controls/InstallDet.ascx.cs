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
using cartridges_app;


namespace cartridges_app.Controls
{
    public partial class InstallDet : ULControl
    {



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
        /*void BindAdresatType()
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
        }*/
        public void EditorMode()
        {
            User u = GetCurrentUser();
            u.GetPermissions();
            RequestsSend ms = new RequestsSend();
            RequestsSendDO msDO = new RequestsSendDO();
            UniversalEntity uen = new UniversalEntity();
            uen = msDO.RetrieveRequestsSendByOrderID(_OrderID);
            if (uen.Count > 0)
            {
                ms = (RequestsSend)uen[0];
                if (GetCurrentUser().ID == ms.UserID)
                {
                    punRequestsEditor.Visible = true;
                    //gvRequestsJournal.Visible = true;
                    //gvRequestsJournal2.Visible = true;
                    //BindAdresatType();
                    UniversalEntity ue = new UniversalEntity();
                    Requests fa = new Requests();
                    RequestsDO fado = new RequestsDO();
                    ue = fado.RetrieveRequestsByOrderID(_OrderID);
                    if (ue.Count > 0)
                    {
                        fa = (Requests)ue[0];
                        tbPhone.Text = fa.Phone;
                        tbAmountDevice.Text = fa.AmountDevice.ToString();
                        tbAddress.Text = fa.Address;
                        tbComment.Text = fa.Comment;

                    }
                }
            }
            //проверяем наличие прав для редактировнаия
            /*if (u.ChekPermission("Editor"))
            {
                punRequestsEditor.Visible = true;
                //gvRequestsJournal.Visible = true;
                gvRequestsJournal2.Visible = true;
                BindAdresatType();
                UniversalEntity ue = new UniversalEntity();
                Requests fa = new Requests();
                RequestsDO fado = new RequestsDO();
                ue = fado.RetrieveByMailOrderID(_OrderID);
                if (ue.Count > 0)
                {
                    fa = (Requests)ue[0];
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
                punRequestsEditor.Visible = false;
                //gvRequestsJournal.Visible = true;
                //gvRequestsJournal2.Visible = true;
            }
        }

        public void Bind()
        {

            EditorMode();

            tbRegDate.Enabled = true;
            tbRegDate.Text = DateTime.Now.ToShortDateString();

            tbDateInstall.Enabled = true;
            tbDateInstall.Text = DateTime.Now.ToShortDateString();


            RequestsSend ms = new RequestsSend();
            RequestsSendDO fodo = new RequestsSendDO();
            UniversalEntity ue = new UniversalEntity();
            ue = fodo.RetrieveRequestsSendByID(_OrderID);
            if (ue.Count > 0)
            {

                ms = (RequestsSend)ue[0];
                if (ms.DateWithdrawal.HasValue)
                    tbRegDate.Text = ms.DateWithdrawal.Value.ToShortDateString();
                if (ms.DateInstallation.HasValue)
                    tbDateInstall.Text = ms.DateWithdrawal.Value.ToShortDateString();
                //tbRegDate.Text = ms.DateRegister;
                //tbRegDate.Enabled = true;
            }

            /*tbAnswerDate.Enabled = true;
            tbAnswerDate.Text = DateTime.Now.ToShortDateString();*/

            //UniversalEntity ue = new UniversalEntity();
            hfODID.Value = _OrderID.ToString();
            StringBuilder sb = new StringBuilder();
            Requests fa = new Requests();
            RequestsDO fado = new RequestsDO();
            ue = fado.RetrieveRequestsByOrderID(_OrderID);
            if (ue.Count > 0)
            {
                fa = (Requests)ue[0];

                sb.AppendLine("<span>Рег. номер: " + fa.Phone + "</span><br/>");
                sb.AppendLine("<span>Лицевой счет: " + fa.AmountDevice + "</span><br/>");
                sb.AppendLine("<span>Кому: " + fa.Address + "</span><br/>");
                sb.AppendLine("<span>О чем: " + fa.Comment + "</span><br/>");
                /*sb.AppendLine("<span>Дата ответа: " + fa.AnswerDate + "</span><br/>");
                sb.AppendLine("<span>О чем ответ: " + fa.AnswerAbout + "</span><br/>");*/

                /*if (fa.AnswerDate.HasValue)
                    tbAnswerDate.Text = fa.AnswerDate.Value.ToShortDateString();*/

                litRequestsInfo.Text = sb.ToString();
            }

            //lbSaveAll.Visible = false;
            //litRequestsInfo.Text = sb.ToString();
        }

        /* protected void gvRequestsJournal2_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
         {
             e.NewValues["ID"] = (gvRequestsJournal2.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
             e.NewValues["date_registration"] = (gvRequestsJournal2.Rows[e.RowIndex].FindControl("tbRegDate") as TextBox).Text;
             GridViewRow row = (GridViewRow)gvRequestsJournal2.Rows[e.RowIndex];
             TextBox tbDateRegsiter = (TextBox)gvRequestsJournal2.Rows[row.RowIndex].FindControl("tbRegDate");
             ResponsibleContractor rc = new ResponsibleContractor();
             rc.DateRegister = Convert.ToDateTime(tbDateRegsiter.Text);
             if (rc.DateRegister.HasValue)
                 tbDateRegsiter.Text = rc.DateRegister.Value.ToShortDateString();
         }*/


        //Сохраняем всё
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            RequestsSend ms = new RequestsSend();
            RequestsSendDO msDO = new RequestsSendDO();
            ms.ID = Convert.ToInt32(hfODID.Value);
            ms.UserID = GetCurrentUser().ID;
            //ms.DateWithdrawal = Convert.ToDateTime(tbRegDate.Text);
            /*if (ms.DateWithdrawal.HasValue)
                tbRegDate.Text = ms.DateWithdrawal.Value.ToShortDateString();*/
            ms.DateWithdrawal = Convert.ToDateTime(tbRegDate.Text);
            //ms.DateWithdrawal = DateTime.ParseExact(tbRegDate.Text, "dd/MM/yyyy", null);
            /*if (ms.DateInstallation.HasValue)
                tbDateInstall.Text = ms.DateInstallation.Value.ToShortDateString();*/
            ms.DateInstallation = Convert.ToDateTime(tbDateInstall.Text);
            _OrderID = ms.ID;
            msDO.Update(ms);
            Bind();


            //fo.DateRegister = Convert.ToDateTime(tbRegDate.Text);
            //bool rez = msDO.Update(ms);

            //(this.Parent.FindControl("gvJournal") as GridView).DataBind();
            //this.Visible = false;
            //_OrderID = fo.ID;
            //Bind();

        }




        protected void radbutSaveRequestsInfo_Click(object sender, EventArgs e)
        {
            UniversalEntity ue = new UniversalEntity();
            Requests fa = new Requests();
            RequestsDO fado = new RequestsDO();
            ue = fado.RetrieveRequestsByOrderID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                fa = (Requests)ue[0];
                fa.Phone = tbPhone.Text;
                fa.AmountDevice = Convert.ToInt32(tbAmountDevice.Text);
                fa.Address = tbAddress.Text;
                fa.Comment = tbComment.Text;
                /*fa.AnswerDate = tbAnswerDate.Text;
                fa.AnswerAbout = tbAnswerAbout.Text;*/
                //fa.AnswerDate = Convert.ToDateTime(tbAnswerDate.Text);
                //fa.AdresatID = Utilities.ConvertToInt(ddlAdresatType.SelectedValue);
                fado.Update(fa, GetCurrentUser().ID);
            }

            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }


        //radbutDeleteAbonentInfo_Click
        protected void radbutDeleteRequestsInfo_Click(object sender, EventArgs e)
        {
            RequestsDO fado = new RequestsDO();
            fado.Delete(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }

        protected void radbutCommonRequests_Click(object sender, EventArgs e)
        {
            RequestsDO fado = new RequestsDO();
            fado.Common(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }

        protected void radbutBackToRemove_OnClick(object sender, EventArgs e)
        {
            RequestsDO fado = new RequestsDO();
            fado.BackToRemove(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }

        protected void radbutRequestsIsDone_OnClick(object sender, EventArgs e)
        {
            RequestsDO fado = new RequestsDO();
            fado.RequestsIsDone(Utilities.ConvertToInt(hfODID.Value), GetCurrentUser().ID);
            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }
    }
}