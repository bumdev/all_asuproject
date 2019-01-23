using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class PhysicalMailSend : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _PhysicalMailOutID;
        int _UserID;
        string _DateReg;
        DateTime? _DateOut;
        DateTime? _DateIn;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int PhysicalMailOutID
        {
            get { return _PhysicalMailOutID; }
            set { _PhysicalMailOutID = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string DateReg
        {
            get { return _DateReg; }
            set { _DateReg = value; }
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

        #endregion

        #region Methods

        public PhysicalMailSend()
        {
            _ID = 0;
            _PhysicalMailOutID = 0;
            _UserID = 0;
            _DateReg = "";
            _DateOut = null;
            _DateIn = null;
        }

        #endregion
    }
}