using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class UOrder:UniversalEntity
    {
        
        #region Attributes

        int _ID;
        int _UAbonentID;
        DateTime _DateIn;
        DateTime? _DateOut;
        string _Coment;
        bool _IsPaid;
        string _ActionType;
        DateTime? _PaymentDay;
        int _UserID;

       

        


        #endregion

        #region Properties

       
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int UAbonentID
        {
            get { return _UAbonentID; }
            set { _UAbonentID = value; }
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
        #endregion

        #region Methods

        public UOrder()
        {
            _ID = 0;
            _UAbonentID=0;
            _DateIn=DateTime.MinValue;
            _DateOut=null;
            _Coment="";
            _IsPaid=false;
            _ActionType="Определения метрологических характеристик водомера";
            _PaymentDay=null;
            _UserID = 0;
        }
        #endregion
    }
}