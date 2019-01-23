using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class DirectoryResponContract : UniversalEntity
    {
        #region Attributes

        int _ID;
        string _ResponName;
        string _Position;
        int _DepartamentID;
        bool _Approve;
        

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ResponName
        {
            get { return _ResponName; }
            set { _ResponName = value; }
        }

        public string Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public int DepartamentID
        {
            get { return _DepartamentID; }
            set { _DepartamentID = value; }
        }

        public bool Approve
        {
            get { return _Approve; }
            set { _Approve = value; }
        }

       

        #endregion

        #region Methods

        public DirectoryResponContract()
        {
            _ID = 0;
            _ResponName = "";
            _Position = "";
            _DepartamentID = 0;
            _Approve = false;
        }

        #endregion
    }
}