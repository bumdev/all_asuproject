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
    public partial class LocationAdd : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            tbDescripton.Text = "";
            tbTitle.Text = "";
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            Book b = new Book();
            b.Title = tbTitle.Text.Trim();
            b.Description = tbDescripton.Text.Trim();
            BookDO bDO = new BookDO();
            int rez = 0;
            rez = bDO.CreateWPLocation(b);
            if (rez > 0)
            {
                nlWPLocation.SetCleanNotification("Объект успешно создан.");
                Clear();
            }
            else
            {
                nlWPLocation.SetDirtyNotification("Произошла ошибка при создании объекта.");
            }
        }
    }
}