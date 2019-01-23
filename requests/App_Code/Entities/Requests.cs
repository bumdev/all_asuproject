using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for FAbonent
/// </summary>
/// 
using AjaxControlToolkit.HTMLEditor.ToolbarButton;

namespace Entities
{
    public class Requests:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _Address;
        string _Phone;
        int _AmountDevice;
        bool _IsInstallation;
        DateTime? _DateWithdrawal;
        DateTime? _DateInstallation;
        string _Comment;
        bool _IsAnnulment;
        bool _IsSubstitution;
        bool _IsArchive;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public int AmountDevice
        {
            get { return _AmountDevice; }
            set { _AmountDevice = value; }
        }

        public bool IsInstallation
        {
            get { return _IsInstallation; }
            set { _IsInstallation = value; }
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

        public bool IsAnnulment
        {
            get { return _IsAnnulment; }
            set { _IsAnnulment = value; }
        }

        public bool IsSubstitution
        {
            get { return _IsSubstitution; }
            set { _IsSubstitution = value; }
        }

        public bool IsArchive
        {
            get { return _IsArchive; }
            set { _IsArchive = value; }
        }

        #endregion

        #region Methods

        public Requests()
        {
            _ID = 0;
            _Address = "";
            _Phone = "";
            _AmountDevice = 0;
            _DateInstallation = null;
            _DateWithdrawal = null;
            _Comment = "";
        }

        #endregion
    }
}