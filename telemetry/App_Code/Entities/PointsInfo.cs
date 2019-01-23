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
    public class PointsInfo:UniversalEntity
    {
        #region Attributes

        int _ID;
        int _PointsID;
        int _UserID;
        bool _IsRepairs;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int PointsID
        {
            get { return _PointsID; }
            set { _PointsID = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public bool IsRepairs
        {
            get { return _IsRepairs; }
            set { _IsRepairs = value; }
        }

        #endregion

        #region Methods

        public PointsInfo()
        {
            _ID = 0;
            _PointsID = 0;
            _UserID = 0;
            _IsRepairs = false;
        }

        #endregion

    }
}