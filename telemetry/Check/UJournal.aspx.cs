using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using DomainObjects;
using System.Data;
using Telerik.Web.UI;

namespace leak_detectors
{
    public partial class UJournal : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Response.Write(Request.ApplicationPath);
                CheckLogin();
                if (Request["id"] != null)
                {
                    RadWindow window1 = new RadWindow();
                    window1.NavigateUrl = "check/UabonentDet.aspx?id=" + Request["id"];// "FabonentDet.aspx?id=" + Request["id"];
                    window1.VisibleOnPageLoad = true;
                    window1.Width = 800;
                    window1.Height = 700;
                    window1.Title = "Просмотр абонента";
                    this.Form.Controls.Add(window1);
                }
            }
        }
        private void CheckPermissions()
        {
            User u = GetCurrentUser();
            hfUserID.Value = u.ID.ToString();
        }
        private void CheckLogin()
        {
            if (!IsLogin())
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                CheckPermissions();

            }
        }

        
    }
}