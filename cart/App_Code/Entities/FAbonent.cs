using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAbonent
/// </summary>
/// 
namespace Entities
{
    public class FAbonent:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _FirstName;
        string _Surname;
        string _LastName;
        string _Address;
        string _Phone;
        string _PhysicalNumberJournal;
        int _DistrictID;
        bool _NotPay;
        //bool _RejectVodomer;

       

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string PhysicalNumberJournal
        {
            get { return _PhysicalNumberJournal; }
            set { _PhysicalNumberJournal = value; }
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
        public int DistrictID
        {
            get { return _DistrictID; }
            set { _DistrictID = value; }
        }

        public bool NotPay
        {
            get { return _NotPay; }
            set { _NotPay = value; }
        }

        /*public bool RejectVodomer
        {
            get { return _RejectVodomer; }
            set { _RejectVodomer = value; }
        }*/

        #endregion

        #region Methods
        public FAbonent()
        {
            _ID = 0;
            _FirstName="";
            _Surname = "";
            _LastName = "";
            _Address = "";
            _PhysicalNumberJournal = "";
            _Phone = "";
            _DistrictID = 0;
            _NotPay = false;
        }
        #endregion
    }
}