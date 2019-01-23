using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Entities;
using DomainObjects;
using System.Configuration;


namespace outcomming_mail
{
    public class ULMasterPage : MasterPage
    {
        public bool IsAdmin()
        {
            bool ok = false;
            if (HttpContext.Current.Request.Cookies["adminname"] != null && HttpContext.Current.Request.Cookies["adminpass"] != null)
            {
                if (HttpContext.Current.Request.Cookies["adminname"].Value == ConfigurationManager.AppSettings.Get("AdminLogin") && HttpContext.Current.Request.Cookies["adminpass"].Value == ConfigurationManager.AppSettings.Get("AdminPassword"))
                {
                    ok = true;
                }
            }
            return ok;
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
    }
    public class ULPage : Page
    {
        public void SetMessege(string header, string message)
        {
            (this.Master.FindControl("MessageBox1") as MessageBox).Visible = true;
            (this.Master.FindControl("MessageBox1") as MessageBox).Header = header;
            (this.Master.FindControl("MessageBox1") as MessageBox).Message = message;
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
        public bool IsAdmin()
        {
            bool ok = false;
            if (HttpContext.Current.Request.Cookies["adminname"] != null && HttpContext.Current.Request.Cookies["adminpass"] != null)
            {
                if (HttpContext.Current.Request.Cookies["adminname"].Value == ConfigurationManager.AppSettings.Get("AdminLogin") && HttpContext.Current.Request.Cookies["adminpass"].Value == ConfigurationManager.AppSettings.Get("AdminPassword"))
                {
                    ok = true;
                }
            }
            return ok;
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
        /*public User GetUser()
        { 
    
        }*/
    }
}
