using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;

namespace invent_app
{
    public partial class DeviceAdd : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTypeDevice();
            }
        }
        private void LoadTypeDevice()
        {
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            List<Book> lbook = crdo.RetrieveWPTypeDevice();

            ddlTypeDevice.DataSource = lbook;
            ddlTypeDevice.DataTextField = "Title";
            ddlTypeDevice.DataValueField = "ID";
            ddlTypeDevice.DataBind();
        }
        private WPDevice CollectDevice()
        {
            WPDevice wpd = new WPDevice();
            wpd.Title = tbTitle.Text.Trim();
            wpd.FN = tbFN.Text.Trim();
            wpd.Description = tbDescription.Text.Trim();
            wpd.TypeID = Utilities.ConvertToInt(ddlTypeDevice.SelectedValue);

            return wpd;
        }
        private void ClearForm()
        {
            tbDescription.Text = "";
            tbFN.Text = "";
            tbTitle.Text = "";
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            WPDevice wpd = CollectDevice();
            WPDeviceDO wpddo = new WPDeviceDO();
            int rez = wpddo.Create(wpd);
            if (rez > 0)
            {
                nlDevice.SetCleanNotification("Прибор успешно добавлен");
                ClearForm();               
            }
            else
            {
                nlDevice.SetDirtyNotification("Придобавлении произошла ошибка");
            }
        }
    }  
}