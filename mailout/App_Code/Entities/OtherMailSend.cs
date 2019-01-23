using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class OtherMailSend : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _OtherMailOutID;
        DateTime? _DateOut;
        DateTime? _DateIn;
        string _DateRegister;
        int _UserID;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int OtherMailOutID
        {
            get { return _OtherMailOutID; }
            set { _OtherMailOutID = value; }
        }

        public DateTime? DateOut
        {
            get { return _DateOut; }
            set { _DateOut = value; }
        }

        public DateTime? DateIn
        {
            get { return _DateIn; }
            set { _DateIn = value; }
        }

        public string DateRegister
        {
            get { return _DateRegister; }
            set { _DateRegister = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        #endregion

        #region Methods

        public OtherMailSend()
        {
            _ID = 0;
            _OtherMailOutID = 0;
            _DateOut = null;
            _DateIn = null;
            _DateRegister = "";
            _UserID = 0;
        }

        #endregion
    }
}