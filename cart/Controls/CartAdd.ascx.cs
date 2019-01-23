using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Text;

namespace cartridges_app
{
    public partial class CartAdd : ULControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTypeCart();
                BindDepart();
                BindCartDirect();
            }
        }
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        //Привязка производителей
       

        void BindTypeCart()
        {
            TypeCartridges t = new TypeCartridges();
            TypeCartridgesDO tcDO = new TypeCartridgesDO();
            UniversalEntity ue = new UniversalEntity();
            ue = tcDO.RetrieveTypeCart();
            ddlTypeCart.Items.Add(new ListItem("Модель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                t = (TypeCartridges) ue[i];
                ddlTypeCart.Items.Add(new ListItem(t.NameCartridge, t.ID.ToString()));
            }
        }

        void BindCartDirect()
        {
            CartDirect cd = new CartDirect();
            CartDirectDO cdDO = new CartDirectDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdDO.RetrieveCartDirect();
            ddlCartName.Items.Add(new ListItem("Картридж", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                cd = (CartDirect) ue[i];
                ddlCartName.Items.Add(new ListItem(cd.CartName, cd.ID.ToString()));
            }
        }

        void BindDepart()
        {
            Departament d = new Departament();
            DepartamentDO dDO = new DepartamentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = dDO.RetrieveDepartaments();
            ddlDepartName.Items.Add(new ListItem("Отдел", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                d = (Departament) ue[i];
                ddlDepartName.Items.Add(new ListItem(d.DepartName, d.ID.ToString()));
            }
        }

        
        CartridgesDirect Collect()
        {
            CartridgesDirect cd = new CartridgesDirect();
            cd.NameCartridge = tbNameCart.Text.Trim();
            cd.CartDirectID = Utilities.ConvertToInt(ddlCartName.SelectedValue);
            cd.TypeCrtridgesID = Utilities.ConvertToInt(ddlTypeCart.SelectedValue);
            cd.DepartamentID = Utilities.ConvertToInt(ddlDepartName.SelectedValue);
            cd.Number = Utilities.ConvertToInt(tbNumber.Text.Trim());
            cd.Information = tbInfo.Text.Trim();
            cd.Comment = tbComment.Text.Trim();
            cd.RefuelingCondition = cbRefuelingCondition.Checked;
            cd.InTheWork = cbInTheWork.Checked;

            return cd;
        }

        bool Validate()
        {
            bool ok = true;

            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(tbNameCart.Text))
            {
                sb.Append("Имя не заполнен<br/>");
                ok = false;
            }
            if (ddlCartName.SelectedValue == "0")
            {
                sb.Append("Не выбран картридж<br/>");
                ok = false;
            }
            if (ddlTypeCart.SelectedValue == "0")
            {
                sb.Append("Не выбрана модель<br/>");
                ok = false;
            }
            if (ddlDepartName.SelectedValue == "0")
            {
                sb.Append("Не выбран отдел<br/>");
            }
            /*if (string.IsNullOrEmpty(tbModel.Text))
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
            }*/
            if (!ok)
            {
                SetMessege("Предупреждение", sb.ToString());
            }
            return ok;
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            /*VodomerType vt;
            VodomerTypeDO vtDO = new VodomerTypeDO();*/

            CartridgesDirect cd;
            CartridgesDirectDO cdDO = new CartridgesDirectDO();

            int rez = 0;
            cd = Collect();
            if (Validate())
            {
                rez = cdDO.CreateCartridgesDirect(cd);

                if (rez > 0)
                {
                    SetMessege("Уведомление", "Картридж успешно добавлен.");
                    // nlTypeAdd.SetCleanNotification("Тип водомера успешно добавлен.");
                }
                else
                {
                    SetMessege("Уведомление", "Произошла ошибка при добавлении картриджа.");
                    //nlTypeAdd.SetDirtyNotification("");
                }
            }
        }
    }
}