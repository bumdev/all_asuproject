using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;

namespace invent_app
{
    public partial class Default : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (IsLogin())
                {
                    Response.Redirect("Direction.aspx");
                }
            }
        }

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            if (IsRegister(tbLogin.Text.Trim(), Utilities.MD5Hash(tbPass.Text.Trim())))
            {
                HttpCookie name = new HttpCookie("name");
                HttpCookie pass = new HttpCookie("pass");

                name.Value = tbLogin.Text;
                pass.Value = tbPass.Text;

                Response.Cookies.Add(name);
                Response.Cookies.Add(pass);

                name.Expires = DateTime.Now.AddYears(1);
                pass.Expires = DateTime.Now.AddYears(1);

                Response.Redirect("Direction.aspx");
            }
            else
            {
                var validator = new CustomValidator();
                validator.IsValid = false;
                validator.ErrorMessage = "Логин или пароль не верны.";
                Page.Validators.Add(validator); 
            }
        }
    }
}