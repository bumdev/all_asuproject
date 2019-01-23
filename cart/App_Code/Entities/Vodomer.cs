using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Vodomer
/// </summary>
/// 
namespace Entities
{
    public  class Vodomer:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _FactoryNumber;
        DateTime _DateOfProduce;
        bool _Exploited;
        int _VodomerType;
        VodomerPreview _VodomerPreview;

        

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string FactoryNumber
        {
            get { return _FactoryNumber; }
            set { _FactoryNumber = value; }
        }
        public DateTime DateOfProduce
        {
            get { return _DateOfProduce; }
            set { _DateOfProduce = value; }
        }
        public bool Exploited
        {
            get { return _Exploited; }
            set { _Exploited = value; }
        }
        public int VodomerType
        {
            get { return _VodomerType; }
            set { _VodomerType = value; }
        }
        public VodomerPreview VodomerPreview
        {
            get { return _VodomerPreview; }
            set { _VodomerPreview = value; }
        }
        #endregion

        #region Methods
        public Vodomer()
        {
            _ID = 0;
            _FactoryNumber="";
            _DateOfProduce = DateTime.MinValue;
            _Exploited=false;
            _VodomerType=0;  
        }
        #endregion
    }
}