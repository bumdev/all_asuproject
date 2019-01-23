using System;

namespace Entities
{
    public class UOrderDetails : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _UOrderID;
        int _VodomerID;
        string _StartValue;
        string _EndValue;
        double _Price;

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        #endregion

        #region Properties

       
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int UOrderID
        {
            get { return _UOrderID; }
            set { _UOrderID = value; }
        }
        public int VodomerID
        {
            get { return _VodomerID; }
            set { _VodomerID = value; }
        }
        public string StartValue
        {
            get { return _StartValue; }
            set { _StartValue = value; }
        }
        public string EndValue
        {
            get { return _EndValue; }
            set { _EndValue = value; }
        }
        #endregion

        #region Methods

        public UOrderDetails()
        {
            _ID = 0;
            _UOrderID = 0;
            _VodomerID = 0;
            _StartValue = "";
            _EndValue = "";
            _Price = 0;
        }
        public double GetPriceVAT()
        {
            return Utilities.GetVAT(_Price);
        }
        public double GetPriceWithVAT()
        {
            return _Price + GetPriceVAT();
        }
        #endregion
    }
}