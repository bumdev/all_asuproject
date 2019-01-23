using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Timetable_WebApp
{
    public partial class WindowBox : ULPage
    {
        private string[] links = {};

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void radgallery_NeedDataSource(object sender, ImageGalleryNeedDataSourceEventArgs e)
        {
            int count = int.Parse((string)Session["Count"]);
            links = new string[count];
            for (int i = 0; i < count; i++)
            {
                links[i] = (string)Session["image" + i];
            }
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("imageURL", typeof(string));
            table.Columns.Add("imageTitle", typeof(string));
            for (int i = 0; i < links.Length; i++)
            {
                table.Rows.Add(1, links[i], "Документ " + (i+1));
            }
            (sender as RadImageGallery).DataSource = table;
        }

        protected void BenjaminButtonReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ServiceAccept.aspx");
        }
    }
}