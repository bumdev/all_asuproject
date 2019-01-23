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
    public class EquipmentDirect:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _EquipmentName;
        int _SellerID;
        string _State;
        string _TechnicalCharachteristic;
        int _TypeEquipmentID;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string EquipmentName
        {
            get { return _EquipmentName; }
            set { _EquipmentName = value; }
        }

        public int SellerID
        {
            get { return _SellerID; }
            set { _SellerID = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }


        public string TechnicalCharachteristic
        {
            get { return _TechnicalCharachteristic; }
            set { _TechnicalCharachteristic = value; }
        }

        public int TypeEquipmentID
        {
            get { return _TypeEquipmentID; }
            set { _TypeEquipmentID = value; }
        }


        #endregion

        #region Methods

        public EquipmentDirect()
        {
            _ID = 0;
            _EquipmentName = "";
            _SellerID = 0;
            _State = string.Empty;
            _TechnicalCharachteristic = "";
            _TypeEquipmentID = 0;
        }

        #endregion
    }
}