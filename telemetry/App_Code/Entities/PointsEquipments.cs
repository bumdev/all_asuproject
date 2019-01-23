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
    public class PointsEquipments:UniversalEntity
    {
        #region Attributes

        int _ID;
        int _EquipmentID;
        string _InventoryNumber;
        string _State;
        bool _Repairs;
        bool _IsWithdrawn;
        EquipmentPreview _EquipPreview;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int EquipmentID
        {
            get { return _EquipmentID; }
            set { _EquipmentID = value; }
        }

        public string InventoryNumber
        {
            get { return _InventoryNumber; }
            set { _InventoryNumber = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public bool Repairs
        {
            get { return _Repairs; }
            set { _Repairs = value; }
        }

        public bool IsWithdrawn
        {
            get { return _IsWithdrawn; }
            set { _IsWithdrawn = value; }
        }


        public EquipmentPreview EquipPreview
        {
            get { return _EquipPreview; }
            set { _EquipPreview = value; }
        }
        #endregion

        #region Methods

        public PointsEquipments()
        {
            _ID = 0;
            _EquipmentID = 0;
            _InventoryNumber = "";
            _State = "";
            _Repairs = false;
            _IsWithdrawn = false;
        }

        #endregion
    }
}