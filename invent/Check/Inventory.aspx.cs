using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Entities;
using DomainObjects;
using DAO;

namespace invent_app
{
    public partial class Inventory : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                BindTypeInvent();
                BindModel();
                BindGroup();
                BindDistrict();
                BindDepart();
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


        void BindTypeInvent()
        {
            ddlTypeImplem.Items.Clear();
            //CustomRetrieverDO crDO = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            TypeDir ti = new TypeDir();
            TypeDirectDO tiDO = new TypeDirectDO();
            ue = tiDO.RetrieveTypeDirect();
            ddlTypeImplem.Items.Add(new ListItem("Тип инвентаря", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                ti = (TypeDir)ue[i];
                ddlTypeImplem.Items.Add(new ListItem(ti.NameType, ti.ID.ToString()));
            }
        }

        void BindDepart()
        {
            ddlDepart.Items.Clear();
            Departament d = new Departament();
            DepartamentDO dDO = new DepartamentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = dDO.RetrieveDepartaments();
            ddlDepart.Items.Add(new ListItem("Отдел", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                d = (Departament)ue[i];
                ddlDepart.Items.Add(new ListItem(d.DepartName, d.ID.ToString()));
            }

        }

        void BindModel()
        {
            ddlModel.Items.Clear();
            ModelDirect m = new ModelDirect();
            ModelDirectDO mDO = new ModelDirectDO();
            UniversalEntity ue = new UniversalEntity();
            ue = mDO.RetrieveModelDirect();
            ddlModel.Items.Add(new ListItem("Модель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                m = (ModelDirect)ue[i];
                ddlModel.Items.Add(new ListItem(m.ModelName, m.ID.ToString()));
            }
        }

        void BindGroup()
        {
            ddlGroup.Items.Clear();
            GroupDirect g = new GroupDirect();
            GroupDirectDO gDO = new GroupDirectDO();
            UniversalEntity ue = new UniversalEntity();
            ue = gDO.RetrieveGroupDirect();
            ddlGroup.Items.Add(new ListItem("Группа", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                g = (GroupDirect)ue[i];
                ddlGroup.Items.Add(new ListItem(g.GroupName, g.ID.ToString()));
            }
        }

        void BindDistrict()
        {
            ddlDistrict.Items.Clear();
            Distr ds = new Distr();
            DistrDirectDO dsDO = new DistrDirectDO();
            UniversalEntity ue = new UniversalEntity();
            ue = dsDO.RetrieveDistricts();
            ddlDistrict.Items.Add(new ListItem("Район", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                ds = (Distr)ue[i];
                ddlDistrict.Items.Add(new ListItem(ds.DistrictName, ds.ID.ToString()));
            }
        }

        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (ddlDepart.SelectedValue == "0")
            {
                e.Command.Parameters["@DepartName"].Value = DBNull.Value;
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                e.Command.Parameters["@DistrictName"].Value = DBNull.Value;
            }
            if (ddlGroup.SelectedValue == "0")
            {
                e.Command.Parameters["@GroupName"].Value = DBNull.Value;
            }
            if (ddlModel.SelectedValue == "0")
            {
                e.Command.Parameters["@ModelName"].Value = DBNull.Value;
            }
            if (ddlTypeImplem.SelectedValue == "0")
            {
                e.Command.Parameters["@NameType"].Value = DBNull.Value;
            }
        }


        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["ModelID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlModelImplem") as DropDownList).SelectedValue);
            e.NewValues["TypeFurnID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlType") as DropDownList).SelectedValue);
            e.NewValues["GroupID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlGroup") as DropDownList).SelectedValue);
        }

        protected void lbInventAdd_Click(object sender, EventArgs e)
        {
            //InventAdd1.Visible = true;
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

            if (e.Item.OwnerTableView.Name == "Invent")
            {
                Entities.User u = GetCurrentUser();

                dsJournal.InsertParameters.Add(new Parameter("TypeFurnID", DbType.Int32, (insertedItem["ti"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DepartID", DbType.Int32, (insertedItem["dp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("ModelID", DbType.Int32, (insertedItem["md"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("GroupID", DbType.Int32, (insertedItem["gp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DistrictID", DbType.Int32, (insertedItem["dis"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DateExploitation", DbType.DateTime, (insertedItem["DateExploitation"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateBalance", DbType.DateTime, (insertedItem["DateTime"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Wear", DbType.String, (insertedItem["Wear"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("InitialCost", DbType.Decimal, (insertedItem["InitialCost"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Weariness", DbType.String, (insertedItem["Weariness"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("MethodCalculation", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("CountImplements", DbType.Int32, (insertedItem["CountImplements"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("TermOfUse", DbType.String, (insertedItem["TermOfUse"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Address", DbType.String, (insertedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("InventoryNumber", DbType.Int32, (insertedItem["InventoryNumber"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DepreciationRate", DbType.String, (insertedItem["DepreciationRate"].Controls[0] as TextBox).Text));




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
            if (e.Item.OwnerTableView.Name == "Invent")
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
            if (e.Item.OwnerTableView.Name == "Invent")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.UpdateParameters.Add(new Parameter("TypeFurnID", DbType.Int32, (updatedItem["ti"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DepartID", DbType.Int32, (updatedItem["dp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("ModelID", DbType.Int32, (updatedItem["md"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("GroupID", DbType.Int32, (updatedItem["gp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DistrictID", DbType.Int32, (updatedItem["dis"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DateExploitation", DbType.DateTime, (updatedItem["DateExploitation"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("DateBalance", DbType.DateTime, (updatedItem["DateTime"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Wear", DbType.String, (updatedItem["Wear"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("InitialCost", DbType.Decimal, (updatedItem["InitialCost"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Weariness", DbType.String, (updatedItem["Weariness"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("MethodCalculation", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("CountImplements", DbType.Int32, (updatedItem["CountImplements"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("TermOfUse", DbType.String, (updatedItem["TermOfUse"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Address", DbType.String, (updatedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("InventoryNumber", DbType.Int32, (updatedItem["InventoryNumber"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DepreciationRate", DbType.String, (updatedItem["DepreciationRate"].Controls[0] as TextBox).Text));





                //dsJournal.UpdateParameters.Add(new Parameter("ServiceTypeID", DbType.Int32, (updatedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("dateIn", DbType.DateTime, (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");

            }
            e.Item.Edit = false;
            // e.Canceled = true;
            //radgrid.Rebind();
        }

        protected void radgrid_OnDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                RadComboBox rcb1 = (RadComboBox)editItem["ti"].Controls[0];
                RadComboBox rcb2 = (RadComboBox)editItem["dp"].Controls[0];
                RadComboBox rcb3 = (RadComboBox)editItem["md"].Controls[0];
                RadComboBox rcb4 = (RadComboBox)editItem["gp"].Controls[0];
                RadComboBox rcb5 = (RadComboBox)editItem["dis"].Controls[0];
                rcb1.Width = Unit.Pixel(450);
                rcb1.Filter = RadComboBoxFilter.Contains;
                rcb2.Width = Unit.Pixel(450);
                rcb2.Filter = RadComboBoxFilter.Contains;
                rcb3.Width = Unit.Pixel(450);
                rcb3.Filter = RadComboBoxFilter.Contains;
                rcb4.Width = Unit.Pixel(450);
                rcb4.Filter = RadComboBoxFilter.Contains;
                rcb5.Width = Unit.Pixel(450);
                rcb5.Filter = RadComboBoxFilter.Contains;
            }
        }
    }
}