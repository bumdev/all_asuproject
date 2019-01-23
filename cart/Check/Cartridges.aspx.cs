using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Entities;
using DomainObjects;
using System.Data;
using Juice;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Excel;
using ExcelLibrary;

namespace cartridges_app
{
    public partial class Cartridges : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                BindTypeCart();
                BindDepartament();
                BindCartDirect();
            }
        }

        protected void FilterComboBox_SelectedChaged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string filterExpression;
            filterExpression = "([Cartname] = '" + e.Value + "')";
            radgrid.MasterTableView.FilterExpression = filterExpression;
            radgrid.MasterTableView.Rebind();
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

        void BindTypeCart()
        {
            TypeCartridges tc = new TypeCartridges();
            TypeCartridgesDO tcDO = new TypeCartridgesDO();
            UniversalEntity ue = new UniversalEntity();
            ue = tcDO.RetrieveTypeCart();
            ddlTypeCart.Items.Add(new ListItem("Модель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                tc = (TypeCartridges) ue[i];
                ddlTypeCart.Items.Add(new ListItem(tc.NameCartridge, tc.ID.ToString()));
            }
        }

        void BindCartDirect()
        {
            CartDirect cd = new CartDirect();
            CartDirectDO  cdDO = new CartDirectDO();
            UniversalEntity ue = new UniversalEntity();
            ue = cdDO.RetrieveCartDirect();
            ddlCartName.Items.Add(new ListItem("Картридж", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                cd = (CartDirect) ue[i];
                ddlCartName.Items.Add(new ListItem(cd.CartName, cd.ID.ToString()));
            }
        }

        void BindDepartament()
        {
            Departament d = new Departament();
            DepartamentDO dDO = new DepartamentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = dDO.RetrieveDepartaments();
            ddlDepart.Items.Add(new ListItem("Отдел", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                d = (Departament) ue[i];
                ddlDepart.Items.Add(new ListItem(d.DepartName, d.ID.ToString()));
            }
        }

        protected void OnSelectedChangedHandler(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["TypeCrtridgesID"] = e.Value;
        }


        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
        }

        protected void dsJournal_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

            if (ddlCartName.SelectedValue == "0")
            {
                e.Command.Parameters["@Cartname"].Value = DBNull.Value;
            }
            if (ddlTypeCart.SelectedValue == "0")
            {
                e.Command.Parameters["@NameCartridge"].Value = DBNull.Value;
            }
            if (ddlDepart.SelectedValue == "0")
            {
                e.Command.Parameters["@DepartName"].Value = DBNull.Value;
            }
        }


        protected void gvJournal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            
            //e.NewValues["NameCartridges"] = (gvJournal.Rows[e.RowIndex].FindControl("tbNameCart") as TextBox).Text;
            e.NewValues["CartDirectID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlCartName") as DropDownList).SelectedValue);
            e.NewValues["TypeCrtridgesID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlTypeCart") as DropDownList).SelectedValue);
            e.NewValues["DepartamentID"] = Utilities.ConvertToInt((gvJournal.Rows[e.RowIndex].FindControl("ddlDepartName") as DropDownList).SelectedValue);
            e.NewValues["Number"] = (gvJournal.Rows[e.RowIndex].FindControl("tbNumber") as TextBox).Text;
            e.NewValues["Information"] = (gvJournal.Rows[e.RowIndex].FindControl("tbInfo") as TextBox).Text;
            e.NewValues["DateFueling"] = Convert.ToDateTime((gvJournal.Rows[e.RowIndex].FindControl("tbDateFueling") as TextBox).Text);
        }

        protected void lbCartAdd_Click(object sender, EventArgs e)
        {
            CartAdd1.Visible = true;
        }


        protected void radgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem) e.Item;
                RadComboBox comboBox = (RadComboBox) editItem["cd"].Controls[0];
                comboBox.Width = Unit.Pixel(450);
                RadComboBox comboBox1 = (RadComboBox) editItem["tp"].Controls[0];
                comboBox1.Width = Unit.Pixel(450);
                RadComboBox comboBox2 = (RadComboBox) editItem["sl"].Controls[0];
                comboBox2.Width = Unit.Pixel(450);
                comboBox.Filter = RadComboBoxFilter.Contains;
                comboBox1.Filter = RadComboBoxFilter.Contains;
                comboBox2.Filter = RadComboBoxFilter.Contains;
                TextBox textBox = (TextBox) editItem["Information"].Controls[0];
                textBox.Width = Unit.Pixel(450);
                textBox.Height = Unit.Pixel(100);
                textBox.TextMode = TextBoxMode.MultiLine;                
            }
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
            //insertedItem.Width = 600;

            if (e.Item.OwnerTableView.Name == "NameCartridges")
            {
                Entities.User u = GetCurrentUser();

                //dsJournal.InsertParameters.Add(new Parameter("NameCartridges", DbType.String, (insertedItem["NameCartridges"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Number", DbType.Int32, (insertedItem["Number"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Information", DbType.String, (insertedItem["Information"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("RefuelingCondition", DbType.Boolean, (insertedItem["RefuelingCondition"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("TypeCrtridgesID", DbType.Int32, (insertedItem["sl"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DepartamentID", DbType.Int32, (insertedItem["tp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("CartDirectID", DbType.Int32, (insertedItem["cd"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DateFueling", DbType.DateTime, (insertedItem["DateFueling"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("InTheWork", DbType.Boolean,(insertedItem["InTheWork"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateinWork", DbType.DateTime, (insertedItem["DateInWork"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
               

                dsJournal.Insert();
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "NameCartridges")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
            }


            e.Canceled = false;
        }


        protected void radgridCart_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            //updatedItem.Width = 600;
            if (e.Item.OwnerTableView.Name == "NameCartridges")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));

               
                //dsJournal.UpdateParameters.Add(new Parameter("NameCartridges", DbType.String, (updatedItem["NameCartridges"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateInsert", DbType.DateTime, (updatedItem["DateInsert"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Number", DbType.Int32, (updatedItem["Number"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Information", DbType.String, (updatedItem["Information"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("RefuelingCondition", DbType.Boolean, (updatedItem["RefuelingCondition"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("TypeCrtridgesID", DbType.Int32, (updatedItem["sl"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DepartamentID", DbType.Int32, (updatedItem["tp"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("CartDirectID", DbType.Int32, (updatedItem["cd"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DateFueling", DbType.DateTime, (updatedItem["DateFueling"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("InTheWork", DbType.Boolean, (updatedItem["InTheWork"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("DateInWork", DbType.DateTime, (updatedItem["DateInWork"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //updatedItem.Width = 600; 
                 
                 



               
                dsJournal.Update();
                //radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");

            }
            e.Item.Edit = false;
            //e.Item.Width = 600;
            // e.Canceled = true;
            //radgrid.Rebind();
        }

        protected void radgrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                radgrid.ExportSettings.ExportOnlyData = true;
                radgrid.ExportSettings.OpenInNewWindow = true;
                radgrid.MasterTableView.ExportToExcel();
            }
        }

        protected void radgrid_ExportItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                ConfigureExport();
            }
        }

        public void ConfigureExport()
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.IgnorePaging = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.MasterTableView.ExportToExcel();            
        }

        protected void ImageButton_OnClick(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.MasterTableView.ExportToExcel();
        }

    }
}