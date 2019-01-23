using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace parts2
{
    public class MyError
    {
        public static void LogError(Exception ex)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "/log.txt"))
                {
                    // Add some text to the file.
                    sw.WriteLine(DateTime.Now.ToString() + " " + ex.Source + " " + ex.Message + " " + ex.StackTrace);
                }
            }
            catch
            {
                HttpContext.Current.Response.Write(ex.Source + " " + ex.Message + " " + ex.StackTrace);
            }
        }
    }
}