using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;
using System.Data.SqlClient;
namespace Entities
{
    /// <summary>
    /// Summarizes exception information for logging and throwing to ErrorNet
    /// </summary>
    /// 
    [Serializable]
    public partial class Error
    {


        #region Constructors
        public Error()
        {
            _Date = DateTime.Parse("01/01/1900");
            _ProjectID = 0;
            _Message = "";
            _Source = "";
            _StackTrace = "";
            _IsUnhandled = false;
            _UserID = "";
            _UserFriendlyMessage = "";
        }
        public Error(Exception e, int projectId, DateTime date, string userID)
        {
            _Date = date;
            _ProjectID = projectId;
            _IsUnhandled = false;
            _UserID = userID;
            _Source = GetSource(e);
            _UserFriendlyMessage = GetUserFriendlyMessage(e);
            if (e.GetType() != typeof(HttpUnhandledException))
            {
                _Message = e.Message;
                //_Source = e.Source;
                _StackTrace = e.StackTrace;
            }
            else // use the InnerException
            {
                _Message = e.InnerException.Message;
                //_Source = e.InnerException.Source;
                _StackTrace = e.InnerException.StackTrace;
                _IsUnhandled = true;
            }


        }

        #endregion

        #region properties
        private DateTime _Date;
        private int _ProjectID;
        private string _Message;
        private string _Source;
        private string _StackTrace;
        private bool _IsUnhandled;
        private string _UserID;
        private string _UserFriendlyMessage;

        /// <summary>
        /// The Date/Time that the exception was first logged.  This will ensure consistent logging across both systems.
        /// </summary>
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        /// <summary>
        /// The pre-defined ProjectID as stored in the UL database.  This should be set in configuration file for the project. 
        /// </summary>
        public int ProjectID
        {
            get { return _ProjectID; }
            set { _ProjectID = value; }
        }
        /// <summary>
        /// Message from the Exception Object
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary>
        /// The Stored Proc. or Method that caused the exception
        /// </summary>
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        /// <summary>
        /// StackTrace from the Exception Object
        /// </summary>
        public string StackTrace
        {
            get { return _StackTrace; }
            set { _StackTrace = value; }
        }
        /// <summary>
        /// Determines if an Error originated from an Unhandled Exception
        /// </summary>
        public bool IsUnhandled
        {
            get { return _IsUnhandled; }
            set { _IsUnhandled = value; }
        }
        /// <summary>
        /// The UserID of the user causing the exception as defined in the Project throwing the Error. 
        /// </summary>
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        /// <summary>
        /// The user friendly version of the error message, if applicable
        /// </summary>
        public string UserFriendlyMessage
        {
            get { return _UserFriendlyMessage; }
            set { _UserFriendlyMessage = value; }
        }


        #endregion

        #region Methods
        private string GetSource(Exception e)
        {
            string source = "";
            if (e.Data["PageSource"] != null)
            {
                source = e.Data["PageSource"].ToString();
            }
            else
            {
                if (e.GetType() == typeof(HttpUnhandledException))
                {
                    source = ParseStack(e.InnerException.StackTrace);
                }
                else if (e.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException sEx = (System.Data.SqlClient.SqlException)e;
                    source = sEx.Procedure;
                }
                else
                {
                    source = ParseStack(e.StackTrace);
                }
            }
            return source;
        }

        private string GetUserFriendlyMessage(Exception e)
        {
            string msg = "";
            if (e.GetType() == typeof(HttpUnhandledException))
            {
                msg = e.Message;
            }
            else if (e.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException sEx = (System.Data.SqlClient.SqlException)e;
                foreach (SqlError se in sEx.Errors)
                {
                    if (se.Number >= 50000)
                    {
                        // these should be custom errors thrown - do the lookup
                        msg = "SQL Error";
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    msg = "SQL Error";
                }
            }
            else
            {
                msg = "SQL Error";
            }
            return msg;
        }

        private string ParseStack(string input)
        {
            string source = "";
            int idx_s = input.IndexOf("at ") + 3;
            int idx_e = input.IndexOf(")");
            if (idx_s >= 0 && idx_e > idx_s)
            {
                source = input.Substring(idx_s, idx_e + 1 - idx_s);
            }
            return source;
        }
        #endregion



    }
}
