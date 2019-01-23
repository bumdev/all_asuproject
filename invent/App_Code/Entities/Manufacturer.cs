using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace invent_app
{
    public class Manufacturer:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _ProdName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ProdName
        {
            get { return _ProdName; }
            set { _ProdName = value; }
        }

        #endregion

        #region Methods

        public Manufacturer()
        {
            _ID = 0;
            _ProdName = "";
        }

        #endregion
    }
}