using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for VodomerType
/// </summary>
/// 
namespace Entities
{
    public class VodomerType: UniversalEntity
    {    
        
        
        #region Attributes

        int _ID;
        string _ConventionalSignth;
        int _Diameter;
        string _GearRatio;
        int _SellerID;
        bool _IsActive;
        string _Description;
        string _GovRegister;
        string _DateProduced;
        int _CheckInterval;
        bool _Approve;

        

        #endregion

        #region Properties
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string ConventionalSignth
        {
            get { return _ConventionalSignth; }
            set { _ConventionalSignth = value; }
        }
        public int Diameter
        {
            get { return _Diameter; }
            set { _Diameter = value; }
        }
        public string GearRatio
        {
            get { return _GearRatio; }
            set { _GearRatio = value; }
        }
        public int SellerID
        {
            get { return _SellerID; }
            set { _SellerID = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string GovRegister
        {
            get { return _GovRegister; }
            set { _GovRegister = value; }
        }
        public string DateProduced
        {
            get { return _DateProduced; }
            set { _DateProduced = value; }
        }
        public int CheckInterval
        {
            get { return _CheckInterval; }
            set { _CheckInterval = value; }
        }
        public bool Approve
        {
            get { return _Approve; }
            set { _Approve = value; }
        }
        #endregion

        #region Methods
        public VodomerType()
        {
            _ID=0;
            _ConventionalSignth = string.Empty;
            _Diameter=0;
            _GearRatio="";
            _SellerID=0;
            _IsActive=false;
            _Description = string.Empty;
            _GovRegister = string.Empty;
            _DateProduced = string.Empty;
            _CheckInterval = 0;
            _Approve = false;
        }
        #endregion
       
    }
}