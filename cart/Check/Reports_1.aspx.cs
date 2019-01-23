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
    public partial class Reports_1 : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
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
               // if (IsEdit())
                    //gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        

        protected void OnSelectedChangedHandler(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["TypeCrtridgesID"] = e.Value;
        }


        private void ExecuteSearch()
        {
            DataView view = (DataView)dsJournal.Select(DataSourceSelectArguments.Empty);
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


        protected void ImageButton_OnClick(object sender, ImageClickEventArgs e)
        {
            radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            radgrid.ExportSettings.OpenInNewWindow = true;
            radgrid.ExportSettings.ExportOnlyData = true;
            radgrid.MasterTableView.ExportToExcel();
        }

        protected void radgridExport_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                radgrid.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                radgrid.ExportSettings.ExportOnlyData = true;
                radgrid.ExportSettings.IgnorePaging = true;
                radgrid.ExportSettings.OpenInNewWindow = true;
                radgrid.MasterTableView.ExportToExcel();
            }
        }

    }
}