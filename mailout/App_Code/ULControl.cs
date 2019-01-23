using System.Web;
using Entities;
using DomainObjects;

namespace outcomming_mail
{
    public class ULControl : System.Web.UI.UserControl
    {

        public void SetMessege(string header, string message)
        {
            (this.Page.Master.FindControl("MessageBox1") as MessageBox).Visible = true;
            (this.Page.Master.FindControl("MessageBox1") as MessageBox).Header = header;
            (this.Page.Master.FindControl("MessageBox1") as MessageBox).Message = message;
        }

        public User GetCurrentUser()
        {
            User u = new User();
            UniversalEntity ue = new UniversalEntity();
            UserDO UDO = new UserDO();
            if (HttpContext.Current.Request.Cookies["name"] != null && HttpContext.Current.Request.Cookies["pass"] != null)
            {
                ue = UDO.RetrieveUserAccess(HttpContext.Current.Request.Cookies["name"].Value, Utilities.MD5Hash(HttpContext.Current.Request.Cookies["pass"].Value));
                if (ue.Count > 0)
                {
                    u = (User)ue[0];
                }
            }
            return u;
        }
        public bool IsRegister(string login, string pass)
        {
            bool ok = false;

            UserDO uDO = new UserDO();
            UniversalEntity ue = new UniversalEntity();
            ue = uDO.RetrieveUserAccess(login, pass);
            if (ue.Count > 0)
            {
                ok = true;
            }
            return ok;

        }
        public bool IsLogin()
        {
            if (HttpContext.Current.Request.Cookies["name"] != null && HttpContext.Current.Request.Cookies["pass"] != null)
            {
                User u = new User();
                UniversalEntity ue = new UniversalEntity();
                UserDO UDO = new UserDO();
                ue = UDO.RetrieveUserAccess(HttpContext.Current.Request.Cookies["name"].Value, Utilities.MD5Hash(HttpContext.Current.Request.Cookies["pass"].Value));
                if (ue.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }
}