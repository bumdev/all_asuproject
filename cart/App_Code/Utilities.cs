using System;
using System.Net.Mail;
using System.Security.Cryptography;
using DomainObjects;
using System.Collections;
using Entities;
using System.Web;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Drawing;
using System.Configuration;
using System.Collections.Generic;



	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
public class Utilities
{
    
    public Utilities()
    {

    }
    /// <summary>
    /// Converts a string to an integer. Returns 0 if the string is invalid.
    /// </summary>
    /// <param name="s">The string to convert</param>
    /// <returns></returns>
    public static int ConvertToInt(string s)
    {
        int temp = 0;
        Int32.TryParse(s, out temp);
        return temp;
    }
    public static double ConvertToDouble(string s)
    {
        double temp = 0;
        Double.TryParse(s, out temp);
        return temp;
    }
    public static string FirstToUpper(string s)
    {
        string fix = string.Empty;

        if (s.Length > 1)
        {
            fix += s.Substring(0, 1).ToUpper();
            fix += s.Substring(1, s.Length);
        }
        else if (s.Length == 1)
        {
            fix = s.ToUpper();
        }

        return fix;
    }
    public static string MD5Hash(string str)
    {
        byte[] hash = Encoding.UTF8.GetBytes(str);
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] hashenc = md5.ComputeHash(hash);
        string result = "";
        foreach (byte b in hashenc)
        {
            result += b.ToString("x2");
        }
        return result;
    }
    //This method checks for illegal charachter such as ' that could be used.
    public static string ÑheckForIllegalCharacters(string stringToCheck)
    {
        if (stringToCheck.Contains("'"))
        {
            stringToCheck = stringToCheck.Replace("'", "''");
        }

        return stringToCheck;
    }
    public static double GetVAT(double number)
    {
        return (number / 100 * 20);
    }

	    public static double GetVATRubU(double number)
	    {
	        return (number/100*20);
	    }
    public static int FindColumnIndex(System.Web.UI.WebControls.GridView gridView, string accessibleHeaderText)
    {
        for (int index = 0; index < gridView.Columns.Count; index++)
        {
            if (String.Compare(gridView.Columns[index].AccessibleHeaderText, accessibleHeaderText, true) == 0)
                return index;
        }
        return -1;
    }
    /*
   

    //error net
    public static string SerializeObjectToXMLString(object obj)
    {
        StringBuilder sb = new StringBuilder();
        Type objType = obj.GetType();
        try
        {
            XmlSerializer serializer = new XmlSerializer(objType);
            TextWriter writer = new StringWriter(sb);
            serializer.Serialize(writer, obj);
            writer.Close();
        }
        catch (Exception e)
        {
            //AppDiagnostics.ReportError(e, "UtilitiesXML: SerializeObjectToXMLString", "type=" + objType.ToString());
        }
        return sb.ToString();
    }
    */
	public static string JavascriptErrorMessage(string errormessage, Control par, Control lbl)
	{
		string strVal = "<script language='javascript'>";
		strVal += "var ctl = document.getElementById('" + lbl.ClientID + "');";
		strVal += "ctl.innerHTML = '" + errormessage + "';";
		strVal += "var par = document.getElementById('" + par.ClientID + "');";
		strVal += errormessage == "" ? "" : "par.style.display = 'block'";
		strVal += "</script>";
		return strVal;
	}
	public static string JavascriptAlertMessage(string message)
	{
		string strVal = "<script language='javascript'>";
		strVal += "alert('" + message + "');";
		strVal += "</script>";
		return strVal;
	}
    /*
	public static void EmptyGridRowFix(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			int id = (int)((DataRowView)e.Row.DataItem).Row.ItemArray[0];
			if (id == -1)
				e.Row.Visible = false;
		}
	}
	public static string IsNull(string s, string sr)
	{
		if (!string.IsNullOrEmpty(s))
		{
			sr = s;
		}
		return sr;
	}
	public static string IsNull(string s, string sr, string cn)
	{
		return IsNull(s, sr) == cn ? sr : s;
		
	}
	public static string GetApplicationPath()
	{
		string path = HttpContext.Current.Request.ApplicationPath;
		if (path == "/")
		{
			path = "";
		}
		return path;
	}

	public static System.Web.UI.Control GetPostBackControl(System.Web.UI.Page page)
	{
		Control control = null;
		string ctrlname = page.Request.Params["__EVENTTARGET"];
		if (ctrlname != null && ctrlname != String.Empty)
		{
			control = page.FindControl(ctrlname);
		}
		// if __EVENTTARGET is null, the control is a button type and we need to 
		// iterate over the form collection to find it
		else
		{
			string ctrlStr = String.Empty;
			Control c = null;
			foreach (string ctl in page.Request.Form)
			{
				// handle ImageButton controls ...
				if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
				{
					ctrlStr = ctl.Substring(0, ctl.Length - 2);
					c = page.FindControl(ctrlStr);
				}
				else
				{
					c = page.FindControl(ctl);
				}
				if (c is System.Web.UI.WebControls.Button ||
						 c is System.Web.UI.WebControls.ImageButton)
				{
					control = c;
					break;
				}
			}
		}
		return control;
	}
	public static int FindColumnIndex(System.Web.UI.WebControls.GridView gridView, string accessibleHeaderText)
	{
		for (int index = 0; index < gridView.Columns.Count; index++)
		{
			if (String.Compare(gridView.Columns[index].AccessibleHeaderText, accessibleHeaderText, true) == 0)
				return index;
		}
		return -1;
	}	
	public static void OriginLoginRedirect(HttpResponse rp, HttpRequest rq)
	{
		rp.Redirect("AdminLogin.aspx?Origin=" + Encryption.Encrypt(rq.Url.AbsolutePath.ToString() + rq.Url.Query.ToString()));
	}
	public static string AdminLinkFormat(string lnk)
	{
		return ConfigurationManager.AppSettings.Get("SecureURL") + lnk;
	}
	public static string NonAdminLinkFormat(string lnk)
	{
		return ConfigurationManager.AppSettings.Get("UnsecureURL") + lnk;
	}
	*/
}
