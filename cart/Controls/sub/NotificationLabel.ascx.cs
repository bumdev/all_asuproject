using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Entities;

namespace cartridges_app
{
    public partial class Controls_NotificationLabel : ULControl
    {
        public enum DisplayTypeSelector
        {
            Simple = 1,
            Advanced = 2
        }
        private string _CleanCSS = "Clean";
        private string _DirtyCSS = "Dirty";
        private bool _UseJavascriptPopupOnClean = false;
        private bool _UseJavascriptPopupOnDirty = false;
        private bool _UseTextOnClean = true;
        private bool _UseTextOnDirty = true;
        private bool _UseJavascriptUnhide = true;
        private string _Text = "";
        private DisplayTypeSelector _DisplayType = DisplayTypeSelector.Simple;



        [Bindable(true),
        Description("Whether or not to use Javascript to unhide the text"),
        Category("Behavior"),
        DefaultValue("")]
        public bool UseJavascriptUnhide
        {
            get { return _UseJavascriptUnhide; }
            set
            {
                _UseJavascriptUnhide = value;

            }
        }

        [Bindable(true),
        Description("Text for Error"),
        Category("Behavior"),
        DefaultValue("")]
        public string Text
        {
            get { return _Text; }
        }

        [Bindable(true),
        Description("Whether or not to use Javascript to unhide the text"),
        Category("Behavior"),
        DefaultValue("")]
        public DisplayTypeSelector DisplayType
        {
            get { return _DisplayType; }
            set
            {
                _DisplayType = value;

            }
        }

        [Bindable(true),
        Description("CSS Class to use if the notification is 'positive'"),
        Category("Behavior"),
        DefaultValue("")]
        public string CleanCSS
        {
            get { return _CleanCSS; }
            set
            {
                _CleanCSS = value;

            }
        }

        [Bindable(true),
        Description("CSS Class to use if the notification is 'negative'"),
        Category("Behavior"),
        DefaultValue("")]
        public string DirtyCSS
        {
            get { return _DirtyCSS; }
            set
            {
                _DirtyCSS = value;

            }
        }

        [Bindable(true),
        Description("Use a javascript pop up to display the 'positive' notification"),
        Category("Behavior"),
        DefaultValue(false)]
        public bool UseJavascriptPopupOnClean
        {
            get { return _UseJavascriptPopupOnClean; }
            set
            {
                _UseJavascriptPopupOnClean = value;
            }
        }
        [Bindable(true),
        Description("Use a javascript pop up to display the 'negative' notification"),
        Category("Behavior"),
        DefaultValue(false)]
        public bool UseJavascriptPopupOnDirty
        {
            get { return _UseJavascriptPopupOnDirty; }
            set
            {
                _UseJavascriptPopupOnDirty = value;
            }
        }
        [Bindable(true),
        Description("Use the textual display the 'positive' notification"),
        Category("Behavior"),
        DefaultValue(false)]
        public bool UseTextOnClean
        {
            get { return _UseTextOnClean; }
            set
            {
                _UseTextOnClean = value;
            }
        }
        [Bindable(true),
        Description("Use the textual display the 'negative' notification"),
        Category("Behavior"),
        DefaultValue(false)]
        public bool UseTextOnDirty
        {
            get { return _UseTextOnDirty; }
            set
            {
                _UseTextOnDirty = value;
            }
        }

        private bool IsDebugMode
        {
            get
            {
                string debug = System.Configuration.ConfigurationManager.AppSettings.Get("DebugMode");
                if (debug == "On")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ClearText()
        {
            lbl_Notification.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // lbl_Notification.Text = "";
            //if (lbl_Notification.Text == "")
            //    lbl_Notification.Attributes.Add("style", "display: none;");
            //else
            //    lbl_Notification.Attributes.Remove("style");
        }
        protected override void OnInit(EventArgs e)
        {
            if (DisplayType == DisplayTypeSelector.Simple)
                pnl.CssClass += " Simple";


            base.OnInit(e);
        }
        /// <summary>
        /// Sets the Notification control to 'clean' and displays the message based on the pre-defined parameters 
        /// </summary>
        /// <param name="message">Message to display</param>
        public void SetCleanNotification(string message)
        {
            _Text = message;
            string addCss = pnl.CssClass.IndexOf(CleanCSS) == -1 ? " " + CleanCSS : "";
            pnl.CssClass += addCss;
            pnl.CssClass = pnl.CssClass.Replace(DirtyCSS, "");

            if (UseJavascriptPopupOnClean)
            {
                RegisterJavascriptAlert(message);
            }

            if (UseTextOnClean)
            {
                //lbl_Notification.Text = message;
                if (UseJavascriptUnhide)
                    RegisterJavascriptUnhiding(message);
                else
                {
                    pnl.Attributes.Remove("style");
                    lbl_Notification.Text = message;
                }
            }
        }
        /// <summary>
        /// Sets the Notification control to 'dirty' and displays the message based on the pre-defined parameters 
        /// </summary>
        /// <param name="message">Message to display</param>
        public void SetDirtyNotification(string message)
        {
            _Text = message;
            string addCss = pnl.CssClass.IndexOf(DirtyCSS) == -1 ? " " + DirtyCSS : "";
            pnl.CssClass += addCss;
            pnl.CssClass = pnl.CssClass.Replace(CleanCSS, "");
            if (UseJavascriptPopupOnDirty)
            {
                RegisterJavascriptAlert(message);
            }

            if (UseTextOnDirty)
            {
                //lbl_Notification.Text = message;
                if (UseJavascriptUnhide)
                    RegisterJavascriptUnhiding(message);
                else
                {
                    pnl.Attributes.Remove("style");
                    lbl_Notification.Text = message;
                }
            }
        }
        /// <summary>
        /// Sets the Notification control to 'dirty' and displays the message based on the pre-defined parameters 
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="err">Relevant Error object</param>
        public void SetDirtyNotification(string message, Error err)
        {
            SetDirtyNotification(message);
            if (IsDebugMode)
            {
                lbl_Notification.Text += "<br/>(Debug information: ";
                lbl_Notification.Text += "<br/> Message: " + err.Message;
                lbl_Notification.Text += "<br/> Source: " + err.Source + ")";
            }
        }

        private void RegisterJavascriptAlert(string message)
        {
            message = message.Replace("'", "\\'");
            message = message.Replace("\r\n", "\\r\\n");
            Page.RegisterStartupScript(this.ClientID + "NotificationMessageScript", @Utilities.JavascriptAlertMessage(message));
        }
        /* Added by Travis Brown on Dec 10.
         * Reason:	Since we are using a block level element to display the error message (in the CSS) it displays regardless of the text in
         *			the error message. We need it to be hidden until it is shown, and disappear on refresh.
         */
        private void RegisterJavascriptUnhiding(string message)
        {
            message = message.Replace("'", "\\'");
            message = message.Replace("\r\n", "<br />");
            if (message.IndexOf("<br />") != -1)
                message = message.Substring(0, message.IndexOf("<br />"));

            Page.RegisterStartupScript(this.ClientID + "NotificationMessageUnhideScript", @Utilities.JavascriptErrorMessage(message, pnl, lbl_Notification));
        }

    }
}