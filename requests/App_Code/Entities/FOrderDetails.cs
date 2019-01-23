using System;
using DomainObjects;

namespace Entities
{
    public class FOrderDetails : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _FOrderID;
        int _VodomerID;
        string _StartValue;
        string _EndValue;
        double _Price;
        double _PriceRub;
        double _SpecialPrice;
        string _MarriageVodomer;

        #endregion

        #region Properties


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int FOrderID
        {
            get { return _FOrderID; }
            set { _FOrderID = value; }
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
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public double PriceRub
        {
            get { return _PriceRub; }
            set { _PriceRub = value; }
        }

        public double SpecialPrice
        {
            get { return _SpecialPrice; }
            set { _SpecialPrice = value; }
        }

        public string MarriageVodomer
        {
            get { return _MarriageVodomer; }
            set { _MarriageVodomer = value; }
        }

        #endregion

        #region Methods

        public FOrderDetails()
        {
            _ID = 0;
            _FOrderID = 0;
            _VodomerID = 0;
            _StartValue = "";
            _EndValue = "";
            _Price = 0;
            _PriceRub = 0;
            _SpecialPrice = 0;
            _MarriageVodomer = "";
        }
        #endregion
    }
    public class FOrderDetailsAct : FOrderDetails
    {
        int _OCount;
        string _SN;
        int _Diametr;

        public int Diametr
        {
            get { return _Diametr; }
        }

        public string SN
        {
            get { return _SN; }
        }

        public int OCount
        {
            get { return _OCount; }
            set { _OCount = value; }
        }
        public void Fill()
        {
            VodomerDO vdo = new VodomerDO();
            VodomerTypeDO vtdo = new VodomerTypeDO();
            UniversalEntity ue = new UniversalEntity();
            ue = vdo.RetrieveVodomerById(VodomerID);
            if (ue.Count > 0)
            {
                _SN = ((Vodomer)ue[0]).FactoryNumber;
            }
            else
            {
                _SN = "";
            }
            ue = vtdo.RetrieveVodomerTypeByVodomerId(VodomerID);
            if (ue.Count > 0)
            {
                _Diametr = ((VodomerType)ue[0]).Diameter;
            }
            else
            {
                _Diametr = 0;
            }
        }

    }
}