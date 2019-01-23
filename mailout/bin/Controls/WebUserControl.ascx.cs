using System;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConvincingMail.AdvancedAutoSuggest;

public partial class WebUserControl : UserControl {
	public static string ConnectionString()
	{
		return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
	}
    protected void Page_Load(object sender, EventArgs e) {

    }
}
