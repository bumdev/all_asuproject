using System;
using System.Text;
using System.Web.UI.MobileControls.Adapters;
using DomainObjects;
using Entities;

namespace requests_app
{
    public partial class AddRequests : ULPage
    {
        private void CloseWizard()
        {
            Step1.Visible = true;
            Session["Requests"] = null;
            this.Visible = true;
        }

        private void LoadStep3()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetRequests();
            }
        }

        private void SetRequests()
        {
            if (hfRequestsType.Value == RequestDoc.Withdrawal.ToString())
            {
                lbRequestClick.Text = "Заявка на снятие";
                panWithGrid.Visible = true;
            }
        }

        protected void lbRequestsAdd_Click(object sender, EventArgs e)
        {
            Session["Requests"] = null;
            SessionRequests sr = new SessionRequests();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                sbError.Append("Необходимо ввести номер телефона.<br/>");
            }
            if (string.IsNullOrEmpty(tbAmountDevice.Text))
            {
                sbError.Append("Необходимо ввести количество водомеров.<br/>");
            }
            if (string.IsNullOrEmpty(tbAddress.Text))
            {
                sbError.Append("Необходимо ввести адрес.</br>");
            }
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                if (hfRequestsType.Value == RequestDoc.Withdrawal.ToString())
                {
                    sr.Type = (short)RequestDoc.Withdrawal;
                    Requests rq = new Requests();
                    rq.Phone = tbPhone.Text.Trim();
                    //rq.AmountDevice = Utilities.ConvertToInt(tbAmountDevice.Text);
                    rq.AmountDevice = Utilities.ConvertToInt(tbAmountDevice.Text);
                    rq.Address = tbAddress.Text.Trim();
                    rq.Comment = tbComment.Text.Trim();
                    sr.Req = rq;
                    sb.AppendLine("<b><span>Телефон: " + rq.Phone + "</span><br/>");
                    sb.AppendLine("<b><span>Количество водомеров: " + rq.AmountDevice + "</span><br/>");
                    sb.AppendLine("<b><span>Адрес: " + rq.Address + "</span><br/>");
                    sb.AppendLine("<b><span>Примечание: " + rq.Comment + "</span><br/>");
                }
                Session["Requests"] = sr;
                Step1.Visible = true;
                ClearRequestsForm();
                lbSaveRequests.Visible = true;

            }
        }

        public void ClearRequestsForm()
        {
            tbPhone.Text = "";
            tbAmountDevice.Text = "";
            tbAddress.DataTextField = "";
            tbComment.Text = "";
        }

        protected void lbSaveRequests_OnClick(object sender, EventArgs e)
        {
            Session["Requests"] = null;
            SessionRequests sr = new SessionRequests();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                sbError.Append("Необходимо ввести номер телефона.<br/>");
            }
            if (string.IsNullOrEmpty(tbAmountDevice.Text))
            {
                sbError.Append("Необходимо ввести количество водомеров.<br/>");
            }
            if (string.IsNullOrEmpty(tbAddress.Text))
            {
                sbError.Append("Необходимо ввести адрес.</br>");
            }
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                if (hfRequestsType.Value == RequestDoc.Withdrawal.ToString())
                {
                    sr.Type = (short)RequestDoc.Withdrawal;
                    Requests rq = new Requests();
                    rq.Phone = tbPhone.Text.Trim();
                    //rq.AmountDevice = Utilities.ConvertToInt(tbAmountDevice.Text);
                    rq.AmountDevice = Utilities.ConvertToInt(tbAmountDevice.Text);
                    rq.Address = tbAddress.Text.Trim();
                    rq.Comment = tbComment.Text.Trim();
                    sr.Req = rq;
                    sb.AppendLine("<b><span>Телефон: " + rq.Phone + "</span><br/>");
                    sb.AppendLine("<b><span>Количество водомеров: " + rq.AmountDevice + "</span><br/>");
                    sb.AppendLine("<b><span>Адрес: " + rq.Address + "</span><br/>");
                    sb.AppendLine("<b><span>Примечание: " + rq.Comment + "</span><br/>");
                }
                Session["Requests"] = sr;
                Step1.Visible = true;
                ClearRequestsForm();
                lbSaveRequests.Visible = true;

            }
            if (Session["Requests"] != null)
            {
                SessionRequests srs = (SessionRequests)Session["Requests"];
                if (srs.Type == (short)RequestDoc.Withdrawal)
                {
                    Requests rq = sr.Req;
                    RequestsDO rqDO = new RequestsDO();
                    int rid = rqDO.Create(rq);
                    if (rid > 0)
                    {
                        srs.Req.ID = rid;
                        Session["Requests"] = srs;
                        RequestsSend rs = new RequestsSend();
                        RequestsSendDO rsDO = new RequestsSendDO();
                        rs.RequestsID = rid;
                        rs.UserID = GetCurrentUser().ID;

                        int rsoid = rsDO.Create(rs);
                        if (rsoid > 0)
                        {
                            hfOrder.Value = rsoid.ToString();
                            RequestsDetails rd = new RequestsDetails();
                            RequestsDetailsDO rdDO = new RequestsDetailsDO();
                            rd.RequestsSendID = rsoid;
                            int rdsoid = rdDO.Create(rd);
                            Response.Redirect("RemoveJournal.aspx?id=" + rsoid.ToString());
                        }
                    }
                }
            }
        }
    }
}