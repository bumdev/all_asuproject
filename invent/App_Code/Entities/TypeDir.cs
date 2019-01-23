using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class TypeDir:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _NameType;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string NameType
        {
            get { return _NameType; }
            set { _NameType = value; }
        }

        #endregion

        #region Methods

        public TypeDir()
        {
            _ID = 0;
            _NameType = "";
        }

        #endregion
    }
}