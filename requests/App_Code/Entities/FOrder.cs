using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class FOrder : UniversalEntity
    {

        #region Attributes

        int _ID;
        int _FAbonentID;
        DateTime _DateIn;
        DateTime? _DateOut;
        string _Coment;
        string _FactoryNumber;
        bool _IsPaid;
        string _ActionType;
        DateTime? _PaymentDay;
        int _UserID;
        string _Prefix;
        bool _DefectVodomer;

        


        #endregion

        #region Properties


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string FactoryNumber
        {
            get { return _FactoryNumber; }
            set { _FactoryNumber = value; }
        }
        public int FAbonentID
        {
            get { return _FAbonentID; }
            set { _FAbonentID = value; }
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
        public string Coment
        {
            get { return _Coment; }
            set { _Coment = value; }
        }
        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        public bool DefectVodomer
        {
            get { return _DefectVodomer; }
            set { _DefectVodomer = value; }
        }
        public string ActionType
        {
            get { return _ActionType; }
            set { _ActionType = value; }
        }
        public DateTime? PaymentDay
        {
            get { return _PaymentDay; }
            set { _PaymentDay = value; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }
        #endregion

        #region Methods

        public FOrder()
        {
            _ID = 0;
            _FAbonentID=0;
            _DateIn=DateTime.MinValue;
            _DateOut=null;
            _Coment="";
            _FactoryNumber = "";
            _IsPaid=false;
            _DefectVodomer = false;
            _ActionType="Поверка водомера.";
            _PaymentDay = null;
            _UserID = 0;
            _Prefix = "";
        }
        #endregion
    }

}