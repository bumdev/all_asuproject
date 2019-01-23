using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using NPOI.SS.Formula.Functions;
using Telerik.Web.UI;


namespace leak_detectors
{
    public partial class AddPoints : ULPage
    {
        //Закрываем мастер
        private void CloseWizard()
        {
            Step1.Visible = true;
            Step2.Visible = false;
            repEquipment.DataBind();
            litPointsInfo.Text = "";
            //litPointsInfoStep3.Text = "";
            Session["LeakPoints"] = null;
            this.Visible = false;
        }
        private void LoadStep3()
        {
            /*if (Session["Abonent"] != null)
            {
                Step1.Visible = false;
                Step2.Visible = false;


                StringBuilder sb = new StringBuilder();
                SessionAbonent sa = (SessionAbonent)Session["Abonent"];
                if (hfClientType.Value == Abonent.Private.ToString())
                {
                    MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                    (p.FindControl("FAbonDet2") as FAbonDet).OrderID = Utilities.ConvertToInt(hfOrder.Value);
                    (p.FindControl("FAbonDet2") as FAbonDet).Visible = true;
                    (p.FindControl("FAbonDet2") as FAbonDet).Bind();
                    CloseWizard();
                }
                if (hfClientType.Value == Abonent.Corporate.ToString())
                {
                    MasterPage p = this.Parent.Parent.Parent.Parent as MasterPage;
                    (p.FindControl("UAbonDet2") as UAbonDet).OrderID = Utilities.ConvertToInt(hfOrder.Value);
                    (p.FindControl("UAbonDet2") as UAbonDet).Visible = true;
                    (p.FindControl("UAbonDet2") as UAbonDet).Bind();
                    CloseWizard();
                }
            }*/


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetPointsType();
                //panPoints.Visible = true;
                //Step1.Visible = true;
                //LoadDDLYear();
            }
        }

        private void SetPointsType()
        {
            if (hfPointsType.Value == LeakPoints.Common.ToString())
            {
                lbPointsClick.Text = "Добавить точку";
                panPoints.Visible = true;
                BindDistricts();
                //BindTypePoints();
            }
        }

        protected void lbPointsClick_onClick(object sender, EventArgs e)
        {
            if (hfPointsType.Value == LeakPoints.Common.ToString())
            {
                SetPointsType();
                panPoints.Visible = true;
            }
        }

        public void CheckCountEquipment()
        {
            if (Session["LeakPoints"] == null) return;
            var sa = (SessionPoints) Session["LeakPoints"];
            lbSaveAll.Visible = sa.PointsEquip.Count > 0;
        }



        void BindDistricts()
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Add(new ListItem("Выбор района", "0"));
            CustomRetrieverDO cDO = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cDO.RetrieveDistricts();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList) ue[i];
                ddlDistrict.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

       /* void BindTypePoints()
        {
            ddlTypePoints.Items.Clear();
            ddlTypePoints.Items.Add(new ListItem("Тип точки", "0"));
            CustomRetrieverDO cDO = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cDO.RetrieveTypePoints();
            ArrayList al = new ArrayList();
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList) ue[i];
                ddlTypePoints.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }*/

        private void BindEquipment()
        {
            if (Session["LeakPoints"] != null)
            {
                SessionPoints sp = (SessionPoints) Session["LeakPoints"];

                List<EquipmentPreview> epl = new List<EquipmentPreview>();
                EquipmentPreview ep = new EquipmentPreview();

                repEquipment.DataSource = sp.PointsEquip;
                repEquipment.DataBind();
            }
        }

        

        protected void PointsAdd_Click(object sender, EventArgs e)
        {
            Session["LeakPoints"] = null;
            SessionPoints sp = new SessionPoints();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(tbNamePoints.Text))
            {
                sbError.Append("Необходимо ввести имя точки.<br/>");
            }
            if (string.IsNullOrEmpty(tbTypePoints.Text))
            {
                sbError.Append("Необходимо указать тип точки.<br/>");
            }
            if (string.IsNullOrEmpty(tbAddress.Text))
            {
                sbError.Append("Необходимо указать адрес.<br/>");
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                sbError.Append("Необходимо указать район.<br/>");
            }
            if (sbError.Length > 0)
            {
                radWM.RadAlert(sbError.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                if (hfPointsType.Value == LeakPoints.Common.ToString())
                {
                    sp.Type = (short) LeakPoints.Common;
                    Points po = new Points();
                    po.Name = tbNamePoints.Text.Trim();
                    po.TypePoints = tbTypePoints.Text.Trim();
                    //po.TypePointsID = Convert.ToInt32(ddlTypePoints.SelectedValue);
                    po.Address = tbAddress.Text.Trim();
                    po.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    sp.PointsType = po;
                    sb.AppendLine("<b><span>Имя точки: " + po.Name + "</span><br/>");
                    sb.AppendLine("<b><span>Адрес: " + po.Address + "</span><br/>");
                    litPointsInfo.Text = sb.ToString();
                }

                Session["LeakPoints"] = sp;
                ClearPointsAddForm();
                Step1.Visible = false;
                Step2.Visible = true;
                radgridD.Rebind();
            }
        }

        public void ClearPointsAddForm()
        {
            tbNamePoints.Text = "";
            tbTypePoints.Text = "";
            //ddlTypePoints.SelectedValue = "0";
            tbAddress.DataTextField = "";
            ddlDistrict.SelectedValue = "0";
        }

        private void ExecuteSearch()
        {
            DataView view = (DataView) dsPoints.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsPoints_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (string.IsNullOrEmpty(tbEquipmentSearch.Text))
            {
                e.Command.Parameters["@q"].Value = "+++";
            }
        }

        protected void gvPoints_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
        }

        protected void lbEquipmentSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
            tbEquipmentSearch.Focus();
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            tbEquipmentSearch.Text = "";
            gvPoints.DataBind();
            panEquipmentSearch.Visible = false;
            Step2.CssClass = "";
        }

        protected void gvPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void radgridTE_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgridD.DataSource = (DataView) dsTE.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radgridTE_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    hfTypeEquip.Value = dataItem.GetDataKeyValue("ID").ToString();
                    //hfTypeEquip.Value = dataItem["TypeEquipments"].Text;
                    radgridD.Visible = false;
                    radgridME.Visible = true;
                    radgridME.Rebind();
                    litTE.Text = dataItem["TypeName"].Text;
                }
            }
        } 
         

        protected void radgridME_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgridME.DataSource = (DataView) dsM.Select(DataSourceSelectArguments.Empty);
            }
        }

        protected void radgridME_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    hfModel.Value = dataItem.GetDataKeyValue("ID").ToString();
                    litME.Text = dataItem["EquipmentName"].Text;
                    panValues.Visible = true;
                }
            }
            if (e.CommandName == "BackToTypeEquipment")
            {
                radgridME.Visible = false;
                radgridD.Visible = true;
                //radgridP.Visible = true;
                panValues.Visible = false;
                litME.Text = "";
            }
        }

        protected void repEquipment_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            SessionPoints sp = (SessionPoints)Session["LeakPoints"];
            string fn = (e.Item.FindControl("litFN") as Literal).Text;
            foreach (PointsEquipments pe in sp.PointsEquip)
            {
                if (pe.InventoryNumber == fn)
                {
                    sp.PointsEquip.Remove(pe);
                    break;
                }
            }

            Session["LeakPoints"] = sp;
            BindEquipment();
            CheckCountEquipment();

        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            if (string.IsNullOrEmpty(tbInventoryNumber.Text) || string.IsNullOrEmpty(tbState.Text))
            {
                StringBuilder sb  = new StringBuilder();

                if (string.IsNullOrEmpty(tbInventoryNumber.Text))
                {
                    sb.Append("Необходимо ввести инвентарный номер.<br/>");
                }
                if (string.IsNullOrEmpty(tbState.Text))
                {
                    sb.Append("Необходимо описать состояние оборудования.<br/>");
                }
                radWM.RadAlert(sb.ToString(), null, null, "Предупреждение", "");
            }
            else
            {
                PointsEquipments pe = new PointsEquipments();
                pe.EquipmentID = Convert.ToInt32(hfModel.Value);
                pe.InventoryNumber = tbInventoryNumber.Text;
                pe.State = tbState.Text.Trim();
                pe.Repairs = cbRepairs.Checked;

                EquipmentPreview ep = new EquipmentPreview();
                //ep.Seller = litTE.Text;
                ep.Model = litME.Text;
                ep.TypeEquip = litTE.Text;
                ep.Repairs = pe.Repairs;
                ep.State = tbState.Text.Trim();
                ep.InventoryNumber = tbInventoryNumber.Text;
                pe.EquipPreview = ep;

                SessionPoints sp = (SessionPoints) Session["LeakPoints"];
                sp.AddEquip(pe);
                BindEquipment();
                tbState.Text = tbInventoryNumber.Text = "";
            }
            CheckCountEquipment();
        }

        protected void lbSaveAll_Click(object sender, EventArgs e)
        {
            //lbSaveAll.Visible = true;
            if (Session["LeakPoints"] != null)
            {
                SessionPoints sp = (SessionPoints) Session["LeakPoints"];
                if (sp.PointsEquip.Count == 0)
                {
                    radWM.RadAlert("Необходимо добавить минимум одно оборудование.", null, null, "Предупреждение", "");
                }
                else
                {
                    if (sp.Type == (short) LeakPoints.Common)
                    {
                        Points p = sp.PointsType;
                        PointsDO pDO = new PointsDO();

                        int pid = pDO.Create(p); //id Points
                        if (pid > 0)
                        {
                            sp.PointsType.ID = pid;
                            Session["LeakPoints"] = sp;
                            PointsInfo pi = new PointsInfo();
                            PointsInfoDO piDO = new PointsInfoDO();
                            pi.PointsID = pid;
                            pi.UserID = GetCurrentUser().ID;

                            int piid = piDO.Create(pi); //id PointsInfo
                            if (piid > 0)
                            {
                                hfOrder.Value = piid.ToString();
                                PointsDetails pd = new PointsDetails();
                                PointsDetailsDO pdDO = new PointsDetailsDO();
                                PointsEquipmentsDO peDO = new PointsEquipmentsDO();

                                foreach (PointsEquipments pe in sp.PointsEquip)
                                {
                                    int pdi = peDO.Create(pe); //id PointsEquipment
                                    pd.PointsInfoID = piid;
                                    pd.PointsEquipmendID = pdi;
                                    int poidid = pdDO.Create(pd); //id PointsDetails
                                }
                                Response.Redirect("Equipment.aspx?id=" + piid.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}