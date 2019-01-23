using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Text;

namespace leak_detectors
{
    public partial class TypeAdd : ULControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSellers();
            }
        }
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
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
        VodomerType Collect()
        {
            VodomerType vt = new VodomerType();
            vt.Diameter = Utilities.ConvertToInt(tbDiametr.Text.Trim());
            vt.SellerID = Utilities.ConvertToInt(ddlSeller.SelectedValue);
            vt.ConventionalSignth = tbModel.Text.Trim();
            vt.Description = tbDescription.Text.Trim();
            vt.GovRegister = tbGovRegistry.Text.Trim();
            vt.DateProduced = tbDateProduce.Text;
            vt.CheckInterval = Utilities.ConvertToInt(tbChreckInterval.Text.Trim());
            vt.IsActive = cbActive.Checked;
            vt.Approve = cbApprove.Checked;
            

            return vt;
        }
        bool Validate()
        {
            bool ok = true;

            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(tbDiametr.Text))
            {
                sb.Append("Диаметр не заполнен<br/>");
                ok = false;
            }
            if (ddlSeller.SelectedValue=="0")
            {
                sb.Append("Не выбран производитель<br/>");
                ok = false;
            }
            if (string.IsNullOrEmpty(tbModel.Text))
            {
                sb.Append("Модель не заполнена<br/>");
                ok = false;
            }
            if (string.IsNullOrEmpty(tbGovRegistry.Text))
            {
                sb.Append("Номер гос реестра не заполнен<br/>");
                ok = false;
            }
            if (string.IsNullOrEmpty(tbChreckInterval.Text))
            {
                sb.Append("Межповерочный интервал не заполнен<br/>");
                ok = false;
            }
            if (!ok)
            {
                SetMessege("Предупреждение", sb.ToString());
            }
            return ok;
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            VodomerType vt;
            VodomerTypeDO vtDO = new VodomerTypeDO();

            int rez = 0;
            vt = Collect();
            if (Validate())
            {
                rez = vtDO.CreateVodomerType(vt);

                if (rez > 0)
                {
                    SetMessege("Уведомление", "Тип водомера успешно добавлен.");
                    // nlTypeAdd.SetCleanNotification("Тип водомера успешно добавлен.");
                }
                else
                {
                    SetMessege("Уведомление", "Произошла ошибка при добавлении типа водомера.");
                    //nlTypeAdd.SetDirtyNotification("");
                }
            }
        }
    }
}