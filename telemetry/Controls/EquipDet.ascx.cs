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


namespace leak_detectors.Controls
{
    public partial class EquipDet : ULControl
    {



        int _OrderID = 0;
        // ID по которму осуществляется привязка данных
        public int OrderID
        {
            //get { return _OrderID; }
            set { _OrderID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindEquipName();
            gvPoints2.DataBind();
            DataBind();
            litScript.Text = "";//Обнуляем iframe для выгрузки сгенерированного документа
            

        }
        //Закрывает контрол
        protected void lbClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        /// <summary>
        /// Получение инфы об абоненте
        /// </summary>
        /// <param name="id">ID абонента</param>
        /// <returns>Разметка для вывода информации об абоненте.</returns>
        StringBuilder GetPointsInfo(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            StringBuilder sb = new StringBuilder();
            BindDistricts();
            //BindTypePoints();
            Points p = new Points();
            PointsDO pDO = new PointsDO();
            ue = pDO.RetrieveByInfoID(id);
            if (ue.Count > 0)
            {
                p = (Points)ue[0];
                sb.AppendLine("<span><b>Точка: " + p.Name + "</b></span><br/>");
                sb.AppendLine("<span><b>Тип точки: " + p.TypePoints + "</b></span><br/>");
                sb.AppendLine("<span><b>Адрес: " + p.Address + "</b></span><br/>");
                //sb.AppendLine("<span><b>Район: " + p.DistrictID + "</b></span><br/>");

            }
            return sb;
        }
        //Привязка данных в контрол 
        public void Bind()
        {
            User u = GetCurrentUser();
            u.GetPermissions();

            gvPoints.Visible = true;
            gvPoints2.Visible = true;

            panEdit.Visible = false;
            panView.Visible = true;

           /* cbSeld.Checked = false;
            cbSeld.Enabled = true;
            cbPaid.Checked = false;
            cbPaid.Enabled = true;*/

            UniversalEntity ue = new UniversalEntity();
            PointsInfo pi = new PointsInfo();
            PointsInfoDO piDO = new PointsInfoDO();
            ue = piDO.RetrieveByID(_OrderID);

            if (ue.Count > 0)
            {
                pi = (PointsInfo)ue[0];
                /*hfDateIn.Value = uo.DateIn.ToShortDateString();
                if (uo.DateOut != null)
                {
                    cbSeld.Checked = true;
                    cbSeld.Enabled = false;
                }
                if (uo.IsPaid)
                {
                    cbPaid.Checked = true;
                    cbPaid.Enabled = false;
                    if (uo.PaymentDay.HasValue)
                        tbPaymentDay.Text = uo.PaymentDay.Value.ToShortDateString();
                    tbPaymentDay.Enabled = false;
                }*/
            }

            hfODID.Value = _OrderID.ToString();
            litEquipInfo.Text = GetPointsInfo(_OrderID).ToString();

        }

        /*void BindTypeEquip()
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
        }*/

        void BindEquipName()
        {
            EquipmentDirect ed = new EquipmentDirect();
            EquipmentDirectDO edDO = new EquipmentDirectDO();
            UniversalEntity ue = new UniversalEntity();
            ue = edDO.RetrieveEquipName();
            ddlEquipName.Items.Add(new ListItem("Имя оборудования", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                ed = (EquipmentDirect) ue[i];
                ddlEquipName.Items.Add(new ListItem(ed.EquipmentName, ed.ID.ToString()));
            }
        }

        protected void btAddEquip(object sender, EventArgs e)
        {
            BindEquipName();
            //BindTypeEquip();
            gvPoints2.DataBind();
            dsPoints2.Insert();
        }

        void BindDistricts()
        {
            ddlDistrict.Items.Clear();
            CustomRetrieverDO cdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ArrayList al = new ArrayList();
            ue = cdo.RetrieveDistricts();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlDistrict.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

      
        //Режим редактиорвания абонента
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            User u = GetCurrentUser();
            u.GetPermissions();


            panEdit.Visible = true;
            panView.Visible = false;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);
            StringBuilder sb = new StringBuilder();
            BindDistricts();
           // BindTypePoints();
            Points p = new Points();
            PointsDO pDO = new PointsDO();
            ue = pDO.RetrieveByInfoID(id);
            if (ue.Count > 0)
            {
                p = (Points)ue[0];

                tbNamePoints.Text = p.Name;
                tbTypePoints.Text = p.TypePoints;
               // ddlTypePoints.SelectedValue = p.TypePointsID.ToString();
                tbAddress.Text = p.Address;
                ddlDistrict.SelectedValue = p.DistrictID.ToString();
            }
        }
        //Сбор информации об абоненте из UI
        Points CollectPoints()
        {
            Points p = new Points();

            p.Name = tbNamePoints.Text;
            p.TypePoints = tbTypePoints.Text;
            p.Address = tbAddress.Text;
            p.DistrictID = Utilities.ConvertToInt(ddlDistrict.SelectedValue);

            return p;
        }
        //Обновляем конечные показания
        protected void gvPoints_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BindEquipName();
            e.NewValues["ID"] = (gvPoints.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["State"] = (gvPoints.Rows[e.RowIndex].FindControl("tbState") as Literal).Text;
        }

        protected void gvPoints2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BindEquipName();
            e.NewValues["ID"] = (gvPoints2.Rows[e.RowIndex].FindControl("ODID") as Literal).Text;
            e.NewValues["State"] = (gvPoints2.Rows[e.RowIndex].FindControl("tbState") as TextBox).Text;
            e.NewValues["InventoryNumber"] =
                (gvPoints2.Rows[e.RowIndex].FindControl("tbInventoryNumber") as TextBox).Text;
            e.NewValues["EquipmentsID"] = Utilities.ConvertToInt((gvPoints2.Rows[e.RowIndex].FindControl("ddlEquipName") as DropDownList).SelectedValue);
            //Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlTypeEquipment") as DropDownList).SelectedValue);
        }



        //Сохраняем всё
        protected void lbSaveAll_Click(object sender, EventArgs e)
        {


        }

        protected void lbSavePoints_Click(object sender, EventArgs e)
        {
            /*PointsDO pDO = new PointsDO();
            pDO.Update(CollectPoints());

            panEdit.Visible = false;
            panView.Visible = true;
            UniversalEntity ue = new UniversalEntity();
            int id = Convert.ToInt32(hfODID.Value);

            litEquipInfo.Text = GetPointsInfo(id).ToString();*/

            UniversalEntity ue = new UniversalEntity();
            Points p = new Points();
            PointsDO pDO = new PointsDO();
            ue = pDO.RetrieveByInfoID(Utilities.ConvertToInt(hfODID.Value));
            if (ue.Count > 0)
            {
                p = (Points) ue[0];
                p.Name = tbNamePoints.Text;
                p.TypePoints = tbTypePoints.Text;
               // p.TypePointsID = Utilities.ConvertToInt(ddlTypePoints.SelectedValue);
                p.Address = tbAddress.Text;
                p.DistrictID = Utilities.ConvertToInt(ddlDistrict.SelectedValue);
                pDO.UpdateWithHistory(p, GetCurrentUser().ID);
            }

            _OrderID = Utilities.ConvertToInt(hfODID.Value);
            Bind();
        }

    }
}