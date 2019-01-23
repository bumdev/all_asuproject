using System;
using System.Web.UI;
using Entities;
using System.Data;
using ExcelLibrary.BinaryFileFormat;
using Telerik.Web.UI;

namespace outcomming_mail
{
    public partial class PhysicalMailOutJournal : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                if (Request["id"] != null)
                {
                    RadWindow window1 = new RadWindow();
                    window1.NavigateUrl = "check/PhysicalMailOutDet.aspx?id" + Request["id"];
                    window1.VisibleOnPageLoad = true;
                    window1.Width = 800;
                    window1.Height = 600;
                    window1.Title = "Просмотр исходящей почты";
                    Page.Controls.Add(window1);
                }
            }
        }

        private void CheckPermission()
        {
            User u = GetCurrentUser();
            hfUserID.Value = u.ID.ToString();
        }

        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                CheckPermission();
            }
        }

        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsPhysicalMailOutJournal.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radGridDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ShowMail")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string itemValue = dataItem["MailSendID"].Text;
                PhysicalMailOutDet1.SendID = Utilities.ConvertToInt(itemValue);
                PhysicalMailOutDet1.Bind();
            }
        }
    }
}