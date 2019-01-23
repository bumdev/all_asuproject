using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class InventoryDirect : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _TypeFurnID;
        int _DepartID;
        int _ModelID;
        int _GroupID;
        DateTime? _DateExploitation;
        DateTime? _DateBalance;
        string _Wear;
        double _InitialCost;
        double _LiquidationCost;
        string _Weariness;
        string _Comment;
        string _MethodCalculation;
        int _CountImplements;
        string _TermOfUse;
        string _Address;
        int _DistrictID;
        int _InventoryNumber;
        string _DepreciationRate;
        int _ManufacturerID;
        int _NomenclativeNumber;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int TypeFurnID
        {
            get { return _TypeFurnID; }
            set { _TypeFurnID = value; }
        }

        public int DepartID
        {
            get { return _DepartID; }
            set { _DepartID = value; }
        }

        public int ModelID
        {
            get { return _ModelID; }
            set { _ModelID = value; }
        }

        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        public DateTime? DateExploitation
        {
            get { return _DateExploitation; }
            set { _DateExploitation = value; }
        }

        public DateTime? DateBalance
        {
            get { return _DateBalance; }
            set { _DateBalance = value; }
        }

        public string Wear
        {
            get { return _Wear; }
            set { _Wear = value; }
        }

        public double InitialCost
        {
            get { return _InitialCost; }
            set { _InitialCost = value; }
        }

        public double LiquidationCost
        {
            get { return _LiquidationCost; }
            set { _LiquidationCost = value; }
        }

        public string Weariness
        {
            get { return _Weariness; }
            set { _Weariness = value; }
        }

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public string MethodCalculation
        {
            get { return _MethodCalculation; }
            set { _MethodCalculation = value; }
        }

        public int CountImplements
        {
            get { return _CountImplements; }
            set { _CountImplements = value; }
        }

        public string TermOfUse
        {
            get { return _TermOfUse; }
            set { _TermOfUse = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public int DistrictID
        {
            get { return _DistrictID; }
            set { _DistrictID = value; }
        }

        public int InventoryNumber
        {
            get { return _InventoryNumber; }
            set { _InventoryNumber = value; }
        }

        public string DepreciationRate
        {
            get { return _DepreciationRate; }
            set { _DepreciationRate = value; }
        }

        public int ManufacturerID
        {
            get { return _ManufacturerID; }
            set { _ManufacturerID = value; }
        }

        public int NomenlativeNumber
        {
            get { return _NomenclativeNumber; }
            set { _NomenclativeNumber = value; }
        }

        #endregion

        #region Methods

        public InventoryDirect()
        {
            _ID = 0;
            _TypeFurnID = 0;
            _DepartID = 0;
            _ModelID = 0;
            _GroupID = 0;
            _DateExploitation = null;
            _DateBalance = null;
            _Wear = string.Empty;
            _InitialCost = 0;
            _LiquidationCost = 0;
            _Weariness = string.Empty;
            _Comment = string.Empty;
            _MethodCalculation = string.Empty;
            _CountImplements = 0;
            _TermOfUse = string.Empty;
            _Address = string.Empty;
            _DistrictID = 0;
            _InventoryNumber = 0;
            _DepreciationRate = null;
            _ManufacturerID = 0;
            _NomenclativeNumber = 0;
        }

        #endregion
    }
}