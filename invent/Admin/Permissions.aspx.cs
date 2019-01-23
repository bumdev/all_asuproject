using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;


namespace invent_app
{
    public partial class admin_Permissions : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void gvPermissions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
                InsertPermisssion();
        }
        private void InsertPermisssion()
        {
            GridViewRow g = gvPermissions.FooterRow;

            Permission p = new Permission();
            PermissionDO pdo = new PermissionDO();

            TextBox tbPermission = g.FindControl("tbPermission") as TextBox;

            if (!string.IsNullOrEmpty(tbPermission.Text))
            {
                p.PermissionName = tbPermission.Text;
                int cid = pdo.CreatePermission(p);
                if (cid != 0)
                {
                    nlPermission.SetCleanNotification("Разрешение успешно созданно.");
                    gvPermissions.DataBind();
                }
                else
                {
                    nlPermission.SetDirtyNotification("Ошибка при создании разрешения");
                }
            }

        }
    }
}