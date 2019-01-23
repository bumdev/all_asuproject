using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class Departament : UniversalEntity
    {
        #region Attributes

        int _ID;
        string _CodeDepartament;
        string _DepartamentName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string CodeDepartament
        {
            get { return _CodeDepartament; }
            set { _CodeDepartament = value; }
        }

        public string DepartamentName
        {
            get { return _DepartamentName; }
            set { _DepartamentName = value; }
        }

        #endregion

        #region Methods

        public Departament()
        {
            _ID = 0;
            _CodeDepartament = "";
            _DepartamentName = "";
        }

        #endregion
    }
}