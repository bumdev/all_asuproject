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
    public class PointsDetails:UniversalEntity
    {
        #region Attributes

        int _ID;
        int _PointsInfoID;
        int _PointsEquipmentID;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int PointsInfoID
        {
            get { return _PointsInfoID; }
            set { _PointsInfoID = value; }
        }

        public int PointsEquipmendID
        {
            get { return _PointsEquipmentID; }
            set { _PointsEquipmentID = value; }
        }

        #endregion

        #region Methods

        public PointsDetails()
        {
            _ID = 0;
            _PointsInfoID = 0;
            _PointsEquipmentID = 0;
        }

        #endregion
    }
}