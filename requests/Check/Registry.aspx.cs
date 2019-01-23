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

namespace requests_app
{
    public partial class Registry : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLogin();
                BindSellers();
                BindDiameter();
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
                if(IsEdit())
                    gvJournal.Columns[Utilities.FindColumnIndex(gvJournal, "colEdit")].Visible = true;
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        //Привязка производителей
        void BindSellers()
        {
            Seller s = new Seller();
            SellerDO sdo = new SellerDO();
            UniversalEntity ue = new UniversalEntity();
            ue = sdo.RetrieveSellers();
            ddlSeller.Items.Add(new ListItem("Производитель", "0"));
            for (int i = 0; i < ue.Count; i++)
            {
                s = (Seller)ue[i];
                ddlSeller.Items.Add(new ListItem(s.SellerName, s.ID.ToString()));
            }
        }
        void BindDiameter()
        {
            ddlDiametr.Items.Clear();
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = crdo.RetrieveDiameters();
            ddlDiametr.Items.Add(new ListItem("Диаметр", "-1"));
            ArrayList al;
            for (int i = 0; i < ue.Count; i++)
            {
                al = (ArrayList)ue[i];
                ddlDiametr.Items.Add(new ListItem(al[0].ToString(), al[0].ToString()));
            }
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

        protected void lbTypeAdd_Click(object sender, EventArgs e)
        {
            TypeAdd1.Visible = true;
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

            if (e.Item.OwnerTableView.Name == "Vodomer")
            {
                Entities.User u = GetCurrentUser();


                dsJournal.InsertParameters.Add(new Parameter("diameter", DbType.Int32, (insertedItem["diameter"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Active", DbType.Boolean, (insertedItem["Active"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("GovRegister", DbType.String, (insertedItem["GovRegister"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("CheckInterval", DbType.Int32, (insertedItem["CheckInterval"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("Approve", DbType.Boolean, (insertedItem["Approve"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.InsertParameters.Add(new Parameter("id_seller", DbType.Int32, (insertedItem["sl"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.InsertParameters.Add(new Parameter("DateProduced", DbType.String, (insertedItem["DateProduced"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("conventional_signth", DbType.String, (insertedItem["conventional_signth"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("description", DbType.String, (insertedItem["description"].Controls[0] as TextBox).Text));
                dsJournal.InsertParameters.Add(new Parameter("gear_ratio", DbType.Double, (insertedItem["gear_ratio"].Controls[0] as TextBox).Text));
              


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
            if (e.Item.OwnerTableView.Name == "Vodomer")
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
            if (e.Item.OwnerTableView.Name == "Vodomer")
            {
                string s = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

                dsJournal.UpdateParameters.Add(new Parameter("ID", DbType.Int32, s));
                dsJournal.UpdateParameters.Add(new Parameter("diameter", DbType.Int32, (updatedItem["diameter"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Active", DbType.Boolean, (updatedItem["Active"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("GovRegister", DbType.String, (updatedItem["GovRegister"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("CheckInterval", DbType.Int32, (updatedItem["CheckInterval"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("Approve", DbType.Boolean, (updatedItem["Approve"].Controls[0] as CheckBox).Checked.ToString()));
                dsJournal.UpdateParameters.Add(new Parameter("id_seller", DbType.Int32, (updatedItem["sl"].Controls[0] as RadComboBox).SelectedValue));
                dsJournal.UpdateParameters.Add(new Parameter("DateProduced", DbType.String, (updatedItem["DateProduced"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("conventional_signth", DbType.String, (updatedItem["conventional_signth"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("description", DbType.String, (updatedItem["description"].Controls[0] as TextBox).Text));
                dsJournal.UpdateParameters.Add(new Parameter("gear_ratio", DbType.Double, (updatedItem["gear_ratio"].Controls[0] as TextBox).Text));
              




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