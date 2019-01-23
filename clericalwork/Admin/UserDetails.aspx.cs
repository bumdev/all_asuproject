using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;

namespace ClericalWork_WebApp
{
    public partial class Admin_UserAdd : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrop();
                if (Request["New"] == null && Request["UserID"] == null)
                {
                    Response.Redirect("UserListing.aspx");
                }
                if (Request["New"] != null)
                {
                    panPermissions.Visible = false;
                }
                else
                {
                    if (Request["UserID"] != null)
                    {
                        ddlGroup.Enabled = false;
                        panPermissions.Visible = true;
                        int id = Utilities.ConvertToInt(Request["UserID"]);
                        LoadData(id);
                        LoadPermissionsListAll(id);
                        LoadPermissionListUser(id);
                    }
                }
            }
        }

        private void LoadGrop()
        {
            ddlGroup.Items.Clear();
            CustomRetrieverDO crDO = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();
            ue = crDO.RetrieveGroups();
            foreach (ArrayList al in ue)
            {
                ddlGroup.Items.Add(new ListItem(al[1].ToString(), al[0].ToString()));
            }
        }

        private void LoadPermissionListUser(int id)
        {
            lbUserPermissions.Items.Clear();
            Permission p = new Permission();
            PermissionDO pdo = new PermissionDO();
            UniversalEntity ue = new UniversalEntity();
            ue = pdo.RetrievePermissionsByUser(id);
            if (ue.Count > 0)
            {
                for (int i = 0; i < ue.Count; i++)
                {
                    p = (Permission)ue[i];
                    lbUserPermissions.Items.Add(new ListItem(p.PermissionName, p.ID.ToString()));
                }
            }
        }

        private void LoadPermissionsListAll(int id)
        {
            lbAllPermissions.Items.Clear();
            Permission p = new Permission();
            PermissionDO pdo = new PermissionDO();
            UniversalEntity ue = new UniversalEntity();
            ue = pdo.RetrievePermissionsByUserLeft(id);
            if (ue.Count > 0)
            {
                for (int i = 0; i < ue.Count; i++)
                {
                    p = (Permission)ue[i];
                    lbAllPermissions.Items.Add(new ListItem(p.PermissionName, p.ID.ToString()));
                }
            }
        }

        private void LoadData(int id)
        {
            User u = new User();
            UserDO udo = new UserDO();
            UniversalEntity ue = new UniversalEntity();
            ue = udo.RetrieveUserById(id);
            if (ue.Count > 0)
            {
                u = (User)ue[0];
                tbLogin.Text = u.UserLogin;
                tbName.Text = u.UserName;
                cbIsActive.Checked = u.IsActive;
                hfUserPassword.Value = u.UserPassword;
                hfID.Value = u.ID.ToString();
                ddlGroup.SelectedValue = u.Location.ToString();
            }
        }

        User CollectUser()
        {
            User u = new User();

            u.UserName = tbName.Text;
            u.UserLogin = tbLogin.Text;
            if (!string.IsNullOrEmpty(tbPasswordConfirm.Text))
                u.UserPassword = Utilities.MD5Hash(tbPasswordConfirm.Text);
            else
                u.UserPassword = hfUserPassword.Value;
            u.IsActive = cbIsActive.Checked;
            u.ID = Utilities.ConvertToInt(hfID.Value);
            u.Location = Utilities.ConvertToInt(ddlGroup.SelectedValue);
            return u;
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            User u = new User();
            UserDO udo = new UserDO();
            u = CollectUser();

            if (Request["New"] != null)
            {
                int id = udo.CreateUser(u);
                if (id > 0)
                {
                    nlUser.SetCleanNotification("Пользователь успешно создан. ID=" + id.ToString());
                }
                else
                {
                    nlUser.SetDirtyNotification("Произошла ошибка при создании пользователя");
                }
            }
            else
            {
                bool ok = udo.UpdateUser(u);
                if (ok)
                {
                    nlUser.SetCleanNotification("Пользователь успешно обновлен.");
                }
                else
                {
                    nlUser.SetDirtyNotification("Произошла ошибка при обновлении пользователя");
                }
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (lbAllPermissions.SelectedIndex >= 0)
            {
                Permission p = new Permission();
                PermissionDO pdo = new PermissionDO();
                int id = Utilities.ConvertToInt(hfID.Value);
                int pid = Utilities.ConvertToInt(lbAllPermissions.SelectedValue);
                int upid = pdo.AddPermissionToUser(id, pid);
                if (upid > 0)
                {
                    LoadPermissionsListAll(id);
                    LoadPermissionListUser(id);
                    nlUser.SetCleanNotification("Разрешение успешно добавлено.");
                }
                else
                {
                    nlUser.SetDirtyNotification("Произошла ошибка при добавлении разрешения.");
                }
            }
            else
            {
                nlUser.SetDirtyNotification("Не выбрано разрешение.");
            }
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            if (lbUserPermissions.SelectedIndex >= 0)
            {
                Permission p = new Permission();
                PermissionDO pdo = new PermissionDO();
                int id = Utilities.ConvertToInt(hfID.Value);
                int pid = Utilities.ConvertToInt(lbUserPermissions.SelectedValue);
                bool ok = pdo.DeletePermissionFromUser(id, pid);
                if (ok)
                {
                    LoadPermissionsListAll(id);
                    LoadPermissionListUser(id);
                    nlUser.SetCleanNotification("Разрешение успешно снято.");
                }
                else
                {
                    nlUser.SetDirtyNotification("Произошла ошибка при снятии разрешения.");
                }
            }
            else
            {
                nlUser.SetDirtyNotification("Не выбрано разрешение.");
            }
        }
    }
}