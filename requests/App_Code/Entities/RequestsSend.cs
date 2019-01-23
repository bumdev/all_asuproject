using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class RequestsSend: UniversalEntity
    {
        #region Attributes

        int _ID;
        int _RequestsID;
        DateTime? _DateWithdrawal;
        DateTime? _DateInstallation;
        string _Comment;
        int _UserID;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int RequestsID
        {
            get { return _RequestsID; }
            set { _RequestsID = value; }
        }

        public DateTime? DateWithdrawal
        {
            get { return _DateWithdrawal; }
            set { _DateWithdrawal = value; }
        }

        public DateTime? DateInstallation
        {
            get { return _DateInstallation; }
            set { _DateInstallation = value; }
        }

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        #endregion

        #region Methods

        public RequestsSend()
        {
            _ID = 0;
            _RequestsID = 0;
            _DateWithdrawal = null;
            _DateInstallation = null;
            _Comment = "";
            _UserID = 0;
        }

        #endregion
    }

}