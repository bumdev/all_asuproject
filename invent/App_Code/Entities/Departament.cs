using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Departament:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _DepartName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string DepartName
        {
            get { return _DepartName; }
            set { _DepartName = value; }
        }

        #endregion


        #region Methods

        public Departament()
        {
            _ID = 0;
            _DepartName = string.Empty;
        }

        #endregion
    }
}