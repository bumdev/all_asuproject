using System;
//using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using DomainObjects;

namespace Entities
{
    public class RequestsDetails : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _RequestsSendID;
        bool _IsInstallation;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int RequestsSendID
        {
            get { return _RequestsSendID; }
            set { _RequestsSendID = value; }
        }

        public bool IsInstallation
        {
            get { return _IsInstallation; }
            set { _IsInstallation = value; }
        }

        #endregion

        #region Methods

        public RequestsDetails()
        {
            _ID = 0;
            _IsInstallation = false;
            _RequestsSendID = 0;
        }

        #endregion
    }
   
}