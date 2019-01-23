using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class AlternativeOrder : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _SAbonentID;
        string _WorkType;
        int _UserID;
        bool _IsPaid;
        DateTime _DateIn;
        DateTime? _DateOut;
        DateTime? _PaymentDay;
        string _Prefix;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int SAbonentID
        {
            get { return _SAbonentID; }
            set { _SAbonentID = value; }
        }

        public string WorkType
        {
            get { return _WorkType; }
            set { _WorkType = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        public DateTime DateIn
        {
            get { return _DateIn; }
            set { _DateIn = value; }
        }

        public DateTime? DateOut
        {
            get { return _DateOut; }
            set { _DateOut = value; }
        }

        public DateTime? PaymentDay
        {
            get { return _PaymentDay; }
            set { _PaymentDay = value; }
        }

        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        #endregion

        #region Methods

        public AlternativeOrder()
        {
            _ID = 0;
            _SAbonentID = 0;
            _WorkType = "Снятие/установка водомеров";
            _UserID = 0;
            _IsPaid = false;
            _DateIn = DateTime.MinValue;
            _PaymentDay = null;
            _DateOut = null;
            _Prefix = "";
        }

        #endregion
    }
}