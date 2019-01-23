using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Data;
using Telerik.Web.UI;

namespace leak_detectors
{
    public partial class EquipRegistration : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                BindTypeEquipment();
                //BindDiameter();
            }
        }

        public bool IsEdit()
        {
            bool ok = false;
            User u = GetCurrentUser();
            u.GetPermissions();
            //ok = u.ChekPermission(Permissions.RegisterEditor.ToString());

            return ok;

        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (IsEdit())
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }



        void BindTypeEquipment()
        {
            TypeEquipment te = new TypeEquipment();
            TypeEquipmentDO teDO = new TypeEquipmentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = teDO.RetrieveTypeEquipment();
            ddlTypeEquipment.Items.Add(new ListItem("Тип оборудования", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                te = (TypeEquipment)ue[i];
                ddlTypeEquipment.Items.Add(new ListItem(te.TypeName, te.ID.ToString()));
            }
        }


        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

            if (ddlTypeEquipment.SelectedValue == "0")
            {
                e.Command.Parameters["@TypeName"].Value = DBNull.Value;
            }
        }


        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["TypeEquipmentID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlTypeEquipment") as DropDownList).SelectedValue);
            e.NewValues["EquipmentName"] = (gvJournal.Rows[e.RowIndex].FindControl("tbModel") as TextBox).Text;
            e.NewValues["TechnicalCharachteristic"] = (gvJournal.Rows[e.RowIndex].FindControl("tbTechnicalCharacteristic") as TextBox).Text;
        }

        protected void lbTypeAdd_Click(object sender, EventArgs e)
        {
            EquipAdd1.Visible = true;
        }



        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
            }
        }
        protected void radgrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            if (e.Item.OwnerTableView.Name == "EquipmentName")
            {
                Entities.User u = GetCurrentUser();



                dsJournal.InsertParameters.Add(new Parameter("EquipmentName", DbType.String, (insertedItem["EquipmentName"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("TypeEquipmentID", DbType.Int32, (insertedItem["sl"].Controls[0] as RadComboBox).SelectedValue));


                dsJournal.InsertParameters.Add(new Parameter("TechnicalCharachteristic", DbType.String, (insertedItem["TechnicalCharachteristic"].Controls[0] as TextBox).Text));
                /*dsJournal.InsertParameters.Add(new Parameter("State", DbType.String,
                    (insertedItem["State"].Controls[0] as TextBox).Text));*/



                /*dsJournal.InsertParameters.Add(new Parameter("WPID", DbType.Int32, (insertedItem["WP"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("Rate", DbType.Int32, (insertedItem["Rate"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("dateIn", DbType.DateTime, (insertedItem["DateIn"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("userID", DbType.Int32, u.ID.ToString()));*/

                dsJournal.Insert();
                //radWM.RadAlert("Показания успешно добавлены.", 300, 200, "", "123");
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "EquipmentName")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
                //radWM.RadAlert("Показания были успешно удалены.", 300, 200, "", "123");
            }


            e.Canceled = false;
        }
        protected void radgridDevice_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "EquipmentName")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));

                dsJournal.UpdateParameters.Add(new Parameter("TypeEquipmentID", DbType.Int32, (updatedItem["sl"].Controls[0] as RadComboBox).SelectedValue));

                dsJournal.UpdateParameters.Add(new Parameter("EquipmentName", DbType.String, (updatedItem["EquipmentName"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("TechnicalCharachteristic", DbType.String, (updatedItem["TechnicalCharachteristic"].Controls[0] as TextBox).Text));






                //dsJournal.UpdateParameters.Add(new Parameter("ServiceTypeID", DbType.Int32, (updatedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("dateIn", DbType.DateTime, (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");

            }
            e.Item.Edit = false;
            // e.Canceled = true;
            //radgrid.Rebind();
        }
    }
}