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
    public class Points:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _Address;
        string _Name;
        int _DistrictID;
        int _TypePointsID;
        string _TypePoints;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int DistrictID
        {
            get { return _DistrictID; }
            set { _DistrictID = value; }
        }

        public int TypePointsID
        {
            get { return _TypePointsID; }
            set { _TypePointsID = value; }
        }

        public string TypePoints
        {
            get { return _TypePoints; }
            set { _TypePoints = value; }
        }

        #endregion

        #region Methods

        public Points()
        {
            _ID = 0;
            _Address = "";
            _Name = "";
            _DistrictID = 0;
            _TypePointsID = 0;
            _TypePoints = "";
        }

        #endregion
    }
}