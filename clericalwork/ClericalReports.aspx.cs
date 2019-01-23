using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.Formula.Functions;
using Telerik.Web.UI;
using Entities;

namespace ClericalWork_WebApp
{
    public partial class ClericalReports : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
            }
            
        }


        public bool IsEdit()
        {
            bool ok = false;
            User u = GetCurrentUser();
            u.GetPermissions();

            return ok;

        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("Home.aspx");
            }
            
        }
        
        protected void radgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem) e.Item;
                RadComboBox orCB = (RadComboBox) editItem["org"].Controls[0];
                orCB.Visible = true;
                orCB.Filter = RadComboBoxFilter.Contains;
                orCB.Width = Unit.Pixel(200);
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem) e.Item;
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

            if (e.Item.OwnerTableView.Name == "Enterprises")
            {
                Entities.User u = GetCurrentUser();

                dsJournal.InsertParameters.Add(new Parameter("NameEnter", DbType.String, (insertedItem["NameEnter"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("AddressEnter", DbType.String, (insertedItem["AddressEnter"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Description", DbType.String, (insertedItem["Description"].Controls[0] as TextBox).Text));
                //dsJournal.InsertParameters.Add(new Parameter("OrganiztionsID", DbType.Int32, (insertedItem["or"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("OrganiztionsID", DbType.Int32, (insertedItem["org"].Controls[0] as RadComboBox).SelectedValue));

                dsJournal.Insert();
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Enterprises")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsJournal.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.Delete();
            }


            e.Canceled = false;
        }


        protected void radgridServices_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Enterprises")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));


                dsJournal.UpdateParameters.Add(new Parameter("NameEnter", DbType.String, (updatedItem["NameEnter"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("AddressEnter", DbType.String, (updatedItem["AddressEnter"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Description", DbType.String, (updatedItem["Description"].Controls[0] as TextBox).Text));
                //dsJournal.UpdateParameters.Add(new Parameter("OrganiztionsID", DbType.Int32, (updatedItem["or"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("OrganiztionsID", DbType.Int32, (updatedItem["org"].Controls[0] as RadComboBox).SelectedValue));
                
                dsJournal.Update();

            }
            e.Item.Edit = false;
            radgrid.Rebind();
        }


        protected void radgrid_itemcommand(object sender, GridCommandEventArgs e)
        {
           
        }

    }
}
