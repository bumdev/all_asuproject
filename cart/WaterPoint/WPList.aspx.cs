using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik;
using Telerik.Web.UI;
using System.Data;
using Entities;

namespace cartridges_app
{
    public partial class DeviceList : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void radgridDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radgrid.DataSource = (DataView)dsWP.Select(DataSourceSelectArguments.Empty);
            }

            /*radgrid.MasterTableView.DetailTables[0].DataSource = (DataView)dsDevices.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[1].DataSource = (DataView)dsEvents.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[0].DetailTables[0].DataSource = (DataView)dsService.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[1].DetailTables[0].DataSource = (DataView)dsEventWork.Select(DataSourceSelectArguments.Empty);*/
            
        }

        protected void radgridDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = true;
                    }
                }
            }
            if (e.CommandName == "AddExistingDevice" || e.CommandName == "AddNewDevice")
            {
                Session["WPID"] = e.Item.OwnerTableView.ParentItem.GetDataKeyValue("ID").ToString();
                //radExistingDevices.  EnableShadow = true;
                //radExistingDevices.VisibleOnPageLoad = true;
            }
            
        }

        protected void radgridDevice_InsertCommand(object sender, GridCommandEventArgs e)
        {
           
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            if (e.Item.OwnerTableView.Name == "Service")
            {
                Entities.User u = GetCurrentUser();
                string s = e.Item.OwnerTableView.ParentItem.GetDataKeyValue("ID").ToString(); ;//e.Item.Parent.ite
                dsService.InsertParameters.Add(new Parameter("DeviceID", DbType.Int32, s));
                dsService.InsertParameters.Add(new Parameter("TypeID", DbType.Int32, (insertedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                dsService.InsertParameters.Add(new Parameter("dateIn", DbType.DateTime, (insertedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                //Заменить текущим юзером
                dsService.InsertParameters.Add(new Parameter("userID", DbType.Int32, u.ID.ToString()));
               
                Parameter p = new Parameter();
                p.Direction = ParameterDirection.Output;
                p.Name = "ID";
                p.DbType = DbType.Int32;
                dsService.InsertParameters.Add(p);
                dsService.Insert();
                radWM.RadAlert("Тип обслуживания был успешно добавлен.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Device")
            {
                dsDevices.InsertParameters.Add(new Parameter("fn", DbType.String, (insertedItem["FN"].Controls[0] as TextBox).Text));
                dsDevices.InsertParameters.Add(new Parameter("title", DbType.String, (insertedItem["Title"].Controls[0] as TextBox).Text));
                dsDevices.InsertParameters.Add(new Parameter("description", DbType.String, (insertedItem["Description"].Controls[0] as TextBox).Text));
                dsDevices.InsertParameters.Add(new Parameter("typeid", DbType.Int32, (insertedItem["DeviceType"].Controls[0] as RadComboBox).SelectedValue));
                Parameter p = new Parameter();
                p.Direction = ParameterDirection.Output;
                p.Name = "ID";
                p.DbType = DbType.Int32;
                dsDevices.InsertParameters.Add(p);
                dsDevices.Insert();
                radWM.RadAlert("Устройство было успешно добавлено.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Works")
            {
                string s = e.Item.OwnerTableView.ParentItem.GetDataKeyValue("ID").ToString();
                dsEventWork.InsertParameters["EventID"].DefaultValue = s;
                dsEventWork.InsertParameters["TypeWorkID"].DefaultValue = (insertedItem["WorkType"].Controls[0] as RadComboBox).SelectedValue;
               
                dsEventWork.Insert();
                radWM.RadAlert("Вид работы \"" + (insertedItem["WorkType"].Controls[0] as RadComboBox).SelectedItem.Text + "\" был успешно добавлен.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "WP")
            {
                //string s = e.Item.OwnerTableView.ParentItem.GetDataKeyValue("ID").ToString();

                dsWP.InsertParameters.Add("LocationID", DbType.Int32, (insertedItem["WPLocation"].Controls[0] as RadComboBox).SelectedValue);
                dsWP.InsertParameters.Add("Title", DbType.String, (insertedItem["Title"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("LF", DbType.Double, (insertedItem["LineFirst"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("LS", DbType.Double, (insertedItem["LineSecond"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("D", DbType.Int32, (insertedItem["D"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("DCalc", DbType.Int32, (insertedItem["DCalc"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("Qmin", DbType.Int32, (insertedItem["QMin"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("Qmax", DbType.Int32, (insertedItem["QMax"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("Comment", DbType.String, (insertedItem["Comment"].Controls[0] as TextBox).Text);
                dsWP.InsertParameters.Add("TypeID", DbType.Int32, (insertedItem["WPType"].Controls[0] as RadComboBox).SelectedValue);
                dsWP.InsertParameters.Add("StartDate", DbType.DateTime, (insertedItem["StartDate"].Controls[0] as RadDatePicker).SelectedDate.ToString());
                

                dsWP.Insert();
                radWM.RadAlert("Водоузел был успешно добавлен.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Device")
            {
                dsDevices.InsertParameters.Add(new Parameter("fn", DbType.String, (insertedItem["FN"].Controls[0] as TextBox).Text));
                dsDevices.InsertParameters.Add(new Parameter("title", DbType.String, (insertedItem["Title"].Controls[0] as TextBox).Text));
                dsDevices.InsertParameters.Add(new Parameter("description", DbType.String, (insertedItem["Description"].Controls[0] as TextBox).Text));
                dsDevices.InsertParameters.Add(new Parameter("typeid", DbType.Int32, (insertedItem["DeviceType"].Controls[0] as RadComboBox).SelectedValue));

                dsDevices.Insert();
                radWM.RadAlert("Устройство было успешно добавлено.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Event")
            {

                Entities.User u = GetCurrentUser();
                string s = e.Item.OwnerTableView.ParentItem.GetDataKeyValue("ID").ToString(); ;//e.Item.Parent.ite
                dsEvents.InsertParameters["WPID"].DefaultValue = s;
                dsEvents.InsertParameters["WPTypeEventID"].DefaultValue = (insertedItem["EventType"].Controls[0] as RadComboBox).SelectedValue;
                dsEvents.InsertParameters["WPEventSourceTypeID"].DefaultValue = (insertedItem["EventSource"].Controls[0] as RadComboBox).SelectedValue;
                dsEvents.InsertParameters["DateIn"].DefaultValue = (insertedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString();
                dsEvents.InsertParameters["UserID"].DefaultValue = u.ID.ToString();

                dsEvents.Insert();
                e.Item.Edit = false;
                radWM.RadAlert("Выезд был успешно добавлен.", 300, 200, "", "123");
                //radgrid.Rebind();
            }
            e.Item.Edit = false;
            e.Canceled = false;
            //radgridDevice.Rebind();

        }

        protected void radgridDevice_EditCommand(object sender, GridCommandEventArgs e)
        {
           
        }

        protected void radgridDevice_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updatedItem = (GridEditableItem)e.Item;
            if (e.Item.OwnerTableView.Name == "Service")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsService.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsService.UpdateParameters.Add(new Parameter("ServiceTypeID", DbType.Int32, (updatedItem["ServiceType"].Controls[0] as RadComboBox).SelectedValue));
                dsService.UpdateParameters.Add(new Parameter("dateIn", DbType.DateTime, (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString()));
                dsService.Update();
                radWM.RadAlert("Тип обслуживания был успешно обновлен.", 300, 200, "", "123");

            }
            if (e.Item.OwnerTableView.Name == "Device")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsDevices.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsDevices.UpdateParameters.Add(new Parameter("fn", DbType.String, (updatedItem["FN"].Controls[0] as TextBox).Text));
                dsDevices.UpdateParameters.Add(new Parameter("title", DbType.String, (updatedItem["Title"].Controls[0] as TextBox).Text));
                dsDevices.UpdateParameters.Add(new Parameter("description", DbType.String, (updatedItem["Description"].Controls[0] as TextBox).Text));
                dsDevices.UpdateParameters.Add(new Parameter("typeid", DbType.Int32, (updatedItem["DeviceType"].Controls[0] as RadComboBox).SelectedValue));
                dsDevices.Update();

                radWM.RadAlert("Устройство было успешно обновлено.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Event")
            {

                Entities.User u = GetCurrentUser();
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsEvents.UpdateParameters["ID"].DefaultValue = s;
                dsEvents.UpdateParameters["WPTypeEventID"].DefaultValue = (updatedItem["EventType"].Controls[0] as RadComboBox).SelectedValue;
                dsEvents.UpdateParameters["WPEventSourceTypeID"].DefaultValue = (updatedItem["EventSource"].Controls[0] as RadComboBox).SelectedValue;
                dsEvents.UpdateParameters["DateIn"].DefaultValue = (updatedItem["DateInService"].Controls[0] as RadDatePicker).SelectedDate.ToString();
                dsEvents.UpdateParameters["UserID"].DefaultValue = u.ID.ToString();

                dsEvents.Update();
                e.Item.Edit = false;
                radWM.RadAlert("Выезд был успешно обновлен.", 300, 200, "", "123");
                //radgrid.Rebind();
            }
            if (e.Item.OwnerTableView.Name == "WP")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsWP.UpdateParameters.Add("ID", DbType.Int32, s);
                dsWP.UpdateParameters.Add("LocationID", DbType.Int32, (updatedItem["WPLocation"].Controls[0] as RadComboBox).SelectedValue);
                dsWP.UpdateParameters.Add("Title", DbType.String, (updatedItem["Title"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("LF", DbType.Double, (updatedItem["LineFirst"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("LS", DbType.Double, (updatedItem["LineSecond"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("D", DbType.Int32, (updatedItem["D"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("DCalc", DbType.Int32, (updatedItem["DCalc"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("Qmin", DbType.Int32, (updatedItem["QMin"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("Qmax", DbType.Int32, (updatedItem["QMax"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("Comment", DbType.String, (updatedItem["Comment"].Controls[0] as TextBox).Text);
                dsWP.UpdateParameters.Add("TypeID", DbType.Int32, (updatedItem["WPType"].Controls[0] as RadComboBox).SelectedValue);
                dsWP.UpdateParameters.Add("StartDate", DbType.DateTime, (updatedItem["StartDate"].Controls[0] as RadDatePicker).SelectedDate.ToString());

                dsWP.Update();
                radgrid.Rebind();
                radWM.RadAlert("Водовод был успешно обновлён.", 300, 200, "", "123");
            }
            e.Canceled = false;
            //radgrid.Rebind();
        }

        protected void radgridDevice_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "Service")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsService.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsService.Delete();
                radWM.RadAlert("Тип обслуживания был успешно удален.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Device")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsDevices.DeleteParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsDevices.Delete();
                radWM.RadAlert("Устройство было успешно удалено.", 300, 200, "", "123");
            }
            
            if (e.Item.OwnerTableView.Name == "Works")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsEventWork.DeleteParameters["ID"].DefaultValue = s;
                dsEventWork.Delete();
                radWM.RadAlert("Вид работы был успешно удален.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "Event")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsEvents.DeleteParameters["ID"].DefaultValue = s;
                dsEvents.Delete();
                radWM.RadAlert("Выезд был успешно удален.", 300, 200, "", "123");
            }
            if (e.Item.OwnerTableView.Name == "WP")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                dsWP.DeleteParameters["ID"].DefaultValue = s;
                dsWP.Delete();
                radWM.RadAlert("Водовод был успешно удален.", 300, 200, "", "123");
            }

           
            e.Canceled = false;
            //radgrid.Rebind();
        }

        protected void brExport_Click(object sender, EventArgs e)
        {
            //isExport = true;
            radgrid.MasterTableView.Columns[0].Visible = false;
            radgrid.MasterTableView.Columns[1].Visible = false;
            radgrid.MasterTableView.Columns[2].Visible = false;//Unit.Pixel(300);
            radgrid.MasterTableView.Columns[3].HeaderStyle.Width = Unit.Pixel(150);
            //radgrid.MasterTableView.Columns[4].HeaderStyle.Width = Unit.Percentage(90);
            radgrid.MasterTableView.ExportToPdf();
        }

        protected void radgrid_ItemCreated(object sender, GridItemEventArgs e)
        {
            Entities.User u = GetCurrentUser();
            u.GetPermissions();
            if (u.ChekPermission(Permissions.WaterPointReadOnly.ToString()))
            {
                e.Item.OwnerTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
        }

        protected void radgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            

            Entities.User u = GetCurrentUser();
            u.GetPermissions();
            if (u.ChekPermission(Permissions.WaterPointReadOnly.ToString()))
            {
                
                if (e.Item.OwnerTableView.Name == "WP" || e.Item.OwnerTableView.Name == "Service" || e.Item.OwnerTableView.Name == "Event")
                    if (e.Item is GridDataItem)
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;

                        dataItem["EditCommandColumn"].Visible = false;// Controls[0] as ImageButton).Visible = false;
                    }
            }
               



            //Задаём ширину для дропдауна в режиме редактирования
            if(e.Item.OwnerTableView.Name == "Works")
            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox combo = (RadComboBox)item["WorkType"].Controls[0];
                combo.Width = Unit.Pixel(500);
            }
                        
        }

        protected void radgrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            /*
             radgrid.MasterTableView.DetailTables[0].DataSource = (DataView)dsDevices.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[1].DataSource = (DataView)dsEvents.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[0].DetailTables[0].DataSource = (DataView)dsService.Select(DataSourceSelectArguments.Empty);
            radgrid.MasterTableView.DetailTables[1].DetailTables[0].DataSource = (DataView)dsEventWork.Select(DataSourceSelectArguments.Empty); 
             * */
            switch (e.DetailTableView.Name)
            {
                case "Device":
                    {
                        string WPID = dataItem.GetDataKeyValue("ID").ToString();
                        dsDevices.SelectParameters.Clear();
                        dsDevices.SelectParameters.Add("WPID", DbType.Int32, WPID);
                        e.DetailTableView.DataSource = (DataView)dsDevices.Select(DataSourceSelectArguments.Empty);
                        break;
                    }
                case "Event":
                    {
                        string WPID = dataItem.GetDataKeyValue("ID").ToString();
                        dsEvents.SelectParameters.Clear();
                        dsEvents.SelectParameters.Add("WPID", DbType.Int32, WPID);
                        e.DetailTableView.DataSource = (DataView)dsEvents.Select(DataSourceSelectArguments.Empty);
                        break;
                    }
                case "Works":
                    {
                        string WPEventID = dataItem.GetDataKeyValue("ID").ToString();
                        dsEventWork.SelectParameters.Clear();
                        dsEventWork.SelectParameters.Add("WPEventID", DbType.Int32, WPEventID);
                        e.DetailTableView.DataSource = (DataView)dsEventWork.Select(DataSourceSelectArguments.Empty);
                        break;
                    }
                case "Service":
                    {
                        string WPDeviceID = dataItem.GetDataKeyValue("ID").ToString();
                        dsService.SelectParameters.Clear();
                        dsService.SelectParameters.Add("DeviceID", DbType.Int32, WPDeviceID);
                        e.DetailTableView.DataSource = (DataView)dsService.Select(DataSourceSelectArguments.Empty);
                        break;
                    }
            }
        }

        protected void radgrid_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //radgrid.MasterTableView.Items[0].Expanded = true;
                //radgrid.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }       
    }
}