using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Distr:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _DistrictName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string DistrictName
        {
            get { return _DistrictName; }
            set { _DistrictName = value; }
        }

        #endregion

        #region Methods

        public Distr()
        {
            _ID = 0;
            _DistrictName = "";
        }

        #endregion
    }
}