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
    public class Departament:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _DeprtName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string DepartName
        {
            get { return _DeprtName; }
            set { _DeprtName = value; }
        }

        #endregion

        #region Methods

        public Departament()
        {
            _ID = 0;
            //_DeprtName = "";
        }

        #endregion

    }
}