using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Text;
using Entities;

namespace leak_detectors
{
    public partial class EquipAdd : ULControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTypeEquip();
            }
        }
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        void BindTypeEquip()
        {
            TypeEquipment t = new TypeEquipment();
            TypeEquipmentDO tDO = new TypeEquipmentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = tDO.RetrieveTypeEquipment();
            ddlTypeEquipment.Items.Add(new ListItem("Тип оборудования", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                t = (TypeEquipment)ue[i];
                ddlTypeEquipment.Items.Add(new ListItem(t.TypeName, t.ID.ToString()));
            }
        }
        EquipmentDirect Collect()
        {

            EquipmentDirect vt = new EquipmentDirect();
            vt.EquipmentName = tbModel.Text.Trim();
            vt.TypeEquipmentID = Utilities.ConvertToInt(ddlTypeEquipment.SelectedValue);
            vt.TechnicalCharachteristic = tbTechnicalCharacteristic.Text.Trim();


            return vt;
        }
        bool Validate()
        {
            bool ok = true;

            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(tbModel.Text))
            {
                sb.Append("Модель не заполнена<br/>");
                ok = false;
            }
            if (ddlTypeEquipment.SelectedValue == "0")
            {
                sb.Append("Не выбран производитель<br/>");
                ok = false;
            }
            /*if (string.IsNullOrEmpty(tbTechnicalCharacteristic.Text))
            {
                sb.Append("Модель не заполнена<br/>");
                ok = false;
            }*/


            if (!ok)
            {
                SetMessege("Предупреждение", sb.ToString());
            }
            return ok;
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            EquipmentDirect vt;
            EquipmentDirectDO vtDO = new EquipmentDirectDO();

            int rez = 0;
            vt = Collect();
            if (Validate())
            {
                rez = vtDO.CreateEquipmentDirect(vt);

                if (rez > 0)
                {
                    SetMessege("Уведомление", "Обрудованиие успешно добавлено.");
                    nlEquipAdd.SetCleanNotification("Тип водомера успешно добавлен.");
                }
                else
                {
                    SetMessege("Уведомление", "Произошла ошибка при добавлении обрудования.");
                    nlEquipAdd.SetDirtyNotification("");
                }
            }
        }
    }
}