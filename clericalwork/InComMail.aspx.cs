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
using Telerik.Web.UI;
using Entities;

namespace ClericalWork_WebApp
{
    public partial class InComMail : ULPage
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
                RadComboBox rc = (RadComboBox) editItem["rc"].Controls[0];
                rc.Width = Unit.Pixel(400);
                rc.Filter = RadComboBoxFilter.Contains;
            }
            if (e.Item is GridDataItem)
            {
                
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

            if (e.Item.OwnerTableView.Name == "IncommingMail")
            {
                Entities.User u = GetCurrentUser();

                dsJournal.InsertParameters.Add(new Parameter("RegNumber", DbType.String, (insertedItem["RegNumber"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Correspondent", DbType.String, (insertedItem["Correspondent"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Address", DbType.String, (insertedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("RegNumberJuridical", DbType.String, (insertedItem["RegNumberJuridical"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateJuridical", DbType.DateTime, (insertedItem["date_juridical"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("CodeFrom", DbType.String, (insertedItem["CodeFrom"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("PlaneDate", DbType.DateTime, (insertedItem["date_plane"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("DateExecution", DbType.DateTime, (insertedItem["date_exec"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("TypeLetter", DbType.String, (insertedItem["TypeLetter"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("TextResolution", DbType.String, (insertedItem["TextResolution"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("DateView", DbType.DateTime, (insertedItem["date_view"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("Viewer", DbType.String, (insertedItem["Viewer"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("RegNumberOut", DbType.String, (insertedItem["RegNumberOut"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("TypeMail", DbType.String, (insertedItem["TypeMail"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("ResponsibleContractorID", DbType.Int32, (insertedItem["rc"].Controls[0] as RadComboBox).SelectedValue));

                
                dsJournal.Insert();
            }
            e.Item.Edit = false;
            e.Canceled = true;
            radgrid.Rebind();
        }

        protected void radgrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "IncommingMail")
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
            if (e.Item.OwnerTableView.Name == "IncommingMail")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));


                dsJournal.UpdateParameters.Add(new Parameter("RegNumber", DbType.String, (updatedItem["RegNumber"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateRegistration", DbType.DateTime, (updatedItem["date_reg"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Correspondent", DbType.String, (updatedItem["Correspondent"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Address", DbType.String, (updatedItem["Address"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("RegNumberJuridical", DbType.String, (updatedItem["RegNumberJuridical"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateJuridical", DbType.DateTime, (updatedItem["date_juridical"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("CodeFrom", DbType.String, (updatedItem["CodeFrom"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("PlaneDate", DbType.DateTime, (updatedItem["date_plane"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("DateExecution", DbType.DateTime, (updatedItem["date_exec"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("TypeLetter", DbType.String, (updatedItem["TypeLetter"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("TextResolution", DbType.String, (updatedItem["TextResolution"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("DateView", DbType.DateTime, (updatedItem["date_view"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("Viewer", DbType.String, (updatedItem["Viewer"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("RegNumberOut", DbType.String, (updatedItem["RegNumberOut"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("TypeMail", DbType.String, (updatedItem["TypeMail"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("ResponsibleContractorID", DbType.Int32, (updatedItem["rc"].Controls[0] as RadComboBox).SelectedValue));

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
