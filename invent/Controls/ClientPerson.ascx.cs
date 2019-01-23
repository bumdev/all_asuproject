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
    public partial class Controls_ClientPerson : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

            }
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
        }
        protected void lbCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        protected void AbonentID_ValueChanged(object sender, EventArgs e)
        {

        }
        protected void CityTextBox_TextChanged(object sender, EventArgs e)
        {
            string s = CityIdTextBox.Text;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            List<string> surname = new List<string>();
            FAbonent fa = new FAbonent();
            FAbonentDO fado = new FAbonentDO();
            UniversalEntity ue = new UniversalEntity();
            ue = fado.RetrieveLikeSurname(prefixText);
            if (ue.Count > 0)
            {
                for (int i = 0; i < ue.Count; i++)
                {
                    fa = (FAbonent)ue[i];
                    surname.Add(fa.Surname);
                }
            }
            return surname.ToArray();
        }
    }
}