using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for VodomerType
/// </summary>
/// 
namespace Entities
{
    public class CartridgesDirect: UniversalEntity
    {


        #region Attributes

        int _ID;
        string _NameCartridge;
        int _Number;
        string _Information;
        bool _RefuelingCondition;
        string _Comment;
        int _TypeCrtridgesID;
        int _DepartamentID;
        DateTime? _DateFueling;
        int _CartDirectID;
        bool _InTheWork;
        DateTime? _DateInWork;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string NameCartridge
        {
            get { return _NameCartridge; }
            set { _NameCartridge = value; }
        }

        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        public string Information
        {
            get { return _Information; }
            set { _Information = value; }
        }

        public bool RefuelingCondition
        {
            get { return _RefuelingCondition; }
            set { _RefuelingCondition = value; }
        }

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public int TypeCrtridgesID
        {
            get { return _TypeCrtridgesID; }
            set { _TypeCrtridgesID = value; }
        }

        public int DepartamentID
        {
            get { return _DepartamentID; }
            set { _DepartamentID = value; }
        }

        public DateTime? DateFueling
        {
            get { return _DateFueling; }
            set { _DateFueling = value; }
        }

        public int CartDirectID
        {
            get { return _CartDirectID; }
            set { _CartDirectID = value; }
        }

        public bool InTheWork
        {
            get { return _InTheWork; }
            set { _InTheWork = value; }
        }

        public DateTime? DateInWork
        {
            get { return _DateInWork; }
            set { _DateInWork = value; }
        }


        #endregion

        #region Methods

        public CartridgesDirect()
        {
            _ID = 0;
            _NameCartridge = string.Empty;
            _Number = 0;
            _Information = string.Empty;
            _RefuelingCondition = false;
            _Comment = string.Empty;
            _TypeCrtridgesID = 0;
            _DepartamentID = 0;
            _DateFueling = null;
            _CartDirectID = 0;
            _InTheWork = false;
            _DateInWork = null;
        }

        #endregion

    }
}