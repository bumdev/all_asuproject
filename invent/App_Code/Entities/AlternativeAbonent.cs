using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class AlternativeAbonent : UniversalEntity
    {


        #region Attributes

        int _ID;
        int _FAbonentID;
        string _FirstName;
        string _Surname;
        string _LastName;
        string _Phone;
        string _PhysicalNumberJournal;
        string _Address;
        int _DistrictID;


        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int FAbonentID
        {
            get { return _FAbonentID; }
            set { _FAbonentID = value; }
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

        public int DistrictID
        {
            get { return _DistrictID; }
            set { _DistrictID = value; }
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

        #endregion

        #region Methods

        public AlternativeAbonent()
        {
            _ID = 0;
            _Address = "";
            _DistrictID = 0;
            _FAbonentID = 0;
            _FirstName = "";
            _Surname = "";
            _LastName = "";
            _PhysicalNumberJournal = "";
            _Phone = "";
        }

        #endregion
    }
}