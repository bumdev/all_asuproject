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
using Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;

namespace invent_app
{
    public partial class Invent : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                //BindSellers();
                //BindDiameter();
            }
        }

        public bool IsEdit()
        {
            bool ok = false;
            User u = GetCurrentUser();
            u.GetPermissions();
            ok = u.ChekPermission(Permissions.RegisterEditor.ToString());

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

        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (ddlDiametr.SelectedValue == "-1")
            {
                e.Command.Parameters["@Diametr"].Value = DBNull.Value;
            }
            if (ddlSeller.SelectedValue == "0")
            {
                e.Command.Parameters["@Seller"].Value = DBNull.Value;
            }
        }


        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["id_seller"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlSeller") as DropDownList).SelectedValue);
            e.NewValues["conventional_signth"] = (gvJournal.Rows[e.RowIndex].FindControl("tbModel") as TextBox).Text;
            e.NewValues["description"] = (gvJournal.Rows[e.RowIndex].FindControl("tbDescription") as TextBox).Text;
        }

        protected void lbInventAdd_Click(object sender, EventArgs e)
        {
            //InventAdd1.Visible = true;
        }


        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                radgrid.ExportSettings.IgnorePaging = true;
                radgrid.ExportSettings.ExportOnlyData = true;
                radgrid.ExportSettings.OpenInNewWindow = true;
                radgrid.MasterTableView.ExportToExcel();
            }
        }

        protected void RadGrid1_ExcelMLWorkBookCreated(object sender, GridExcelMLWorkBookCreatedEventArgs e)
        {
            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {
                row.Cells[0].StyleValue = "Style1";
            }
            StyleElement style = new StyleElement("Style1");
            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.LightGray;
            e.WorkBook.Styles.Add(style);
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
                //dsJournal.InsertParameters.Add(new Parameter("DepartID", DbType.Int32, (insertedItem["dp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("ModelID", DbType.Int32, (insertedItem["md"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("GroupID", DbType.Int32, (insertedItem["gp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("ManufacturerID", DbType.Int32, (insertedItem["mf"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.InsertParameters.Add(new Parameter("DistrictID", DbType.Int32, (insertedItem["dt"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DateExploitation", DbType.DateTime, (insertedItem["DateExploitation"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                /*dsJournal.InsertParameters.Add(new Parameter("DateBalance", DbType.DateTime, (insertedItem["DateBalance"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Wear", DbType.String, (insertedItem["Wear"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("InitialCost", DbType.Double, (insertedItem["InitialCost"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("LiquidationValue", DbType.Double, (insertedItem["LiquidationValue"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Weariness", DbType.String, (insertedItem["Weariness"].Controls[0] as TextBox).Text));*/
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("MethodCalculation", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("CountImplements", DbType.Int32, (insertedItem["CountImplements"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("TermOfUse", DbType.String, (insertedItem["TermOfUse"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Address", DbType.String, (insertedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("InventoryNumber", DbType.Int32, (insertedItem["InventoryNumber"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("DepreciationRate", DbType.String, (insertedItem["DepreciationRate"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("NomenclativeNumber", DbType.Int32, (insertedItem["NomenclativeNumber"].Controls[0] as TextBox).Text));


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

        protected void radgrid_OnDatabBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem) e.Item;
                RadComboBox rcb1 = (RadComboBox) editItem["ti"].Controls[0];
                rcb1.Width = Unit.Pixel(450);
                rcb1.Filter = RadComboBoxFilter.Contains;
                //RadComboBox rcb2 = (RadComboBox) editItem["dp"].Controls[0];
                RadComboBox rcb3 = (RadComboBox) editItem["md"].Controls[0];
                RadComboBox rcb4 = (RadComboBox) editItem["gp"].Controls[0];
                RadComboBox rcb6 = (RadComboBox) editItem["mf"].Controls[0];
                //RadComboBox rcb5 = (RadComboBox) editItem["dt"].Controls[0];
                //rcb2.Width = Unit.Pixel(450);
                //rcb2.Filter = RadComboBoxFilter.Contains;
                rcb3.Width = Unit.Pixel(450);
                rcb3.Filter = RadComboBoxFilter.Contains;
                rcb4.Width = Unit.Pixel(450);
                rcb4.Filter = RadComboBoxFilter.Contains;
                rcb6.Width = Unit.Pixel(450);
                rcb6.Filter = RadComboBoxFilter.Contains;
                //rcb5.Width = Unit.Pixel(450);
                //rcb5.Filter = RadComboBoxFilter.Contains;
            }
        }


        protected void radgridDevice_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Invent")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                
                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.UpdateParameters.Add(new Parameter("TypeFurnID", DbType.Int32, (updatedItem["ti"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("DepartID", DbType.Int32, (updatedItem["dp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("ModelID", DbType.Int32, (updatedItem["md"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("GroupID", DbType.Int32, (updatedItem["gp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("ManufacturerID", DbType.Int32, (updatedItem["mf"].Controls[0] as RadComboBox).SelectedValue));
                //dsJournal.UpdateParameters.Add(new Parameter("DistrictID", DbType.Int32, (updatedItem["dt"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DateExploitation", DbType.DateTime, (updatedItem["DateExploitation"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //dsJournal.UpdateParameters.Add(new Parameter("DateBalance", DbType.DateTime, (updatedItem["DateBalance"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //dsJournal.UpdateParameters.Add(new Parameter("Wear", DbType.String, (updatedItem["Wear"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("InitialCost", DbType.Double, (updatedItem["InitialCost"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("LiquidationValue", DbType.Double, (updatedItem["LiquidationValue"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("Weariness", DbType.String, (updatedItem["Weariness"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("MethodCalculation", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("CountImplements", DbType.Int32, (updatedItem["CountImplements"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("TermOfUse", DbType.String, (updatedItem["TermOfUse"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Address", DbType.String, (updatedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("InventoryNumber", DbType.Int32, (updatedItem["InventoryNumber"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("DepreciationRate", DbType.String, (updatedItem["DepreciationRate"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("NomenclativeNumber", DbType.Int32, (updatedItem["NomenclativeNumber"].Controls[0] as TextBox).Text));






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