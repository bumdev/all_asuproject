using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class OrderDetails:UniversalEntity
    {
        int _ID;
        int _OrderID;
        string _FactoryNumber;
        string _Code;
        string _Source;
        double _ResultIn;
        string _Country;
        string _Maker;
        string _Info;
        string _DDpercent;
        string _QuantityText;
        string _LotQuantity;
        double _ResultPrice;
        string _ADDays;
        string _DeliverTimeGuaranteed;
        string _PriceCountry;
        int _Count;
        string _MakeLogo;
        string _PriceLogo;
        string _DestinationLogo;
        string _GlobalId;
        string _OrderNum;
        bool _IsBack;
        bool _IsSeld;
        int _StatusID;
        string _Comment;
        int _StatusEmex;

        bool _Officed;
        bool _Payed;
        bool _Printed;
        DateTime _OfficedDate;
        DateTime _PayedDate;
        DateTime _isSeldDate;

        




        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }   
        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public string FactoryNumber
        {
            get { return _FactoryNumber; }
            set { _FactoryNumber = value; }
        }
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        public double ResultIn
        {
            get { return _ResultIn; }
            set { _ResultIn = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string Maker
        {
            get { return _Maker; }
            set { _Maker = value; }
        }
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
        }
        public string DDpercent
        {
            get { return _DDpercent; }
            set { _DDpercent = value; }
        }
        public string QuantityText
        {
            get { return _QuantityText; }
            set { _QuantityText = value; }
        }
        public string LotQuantity
        {
            get { return _LotQuantity; }
            set { _LotQuantity = value; }
        }
        public double ResultPrice
        {
            get { return _ResultPrice; }
            set { _ResultPrice = value; }
        }
        public string ADDays
        {
            get { return _ADDays; }
            set { _ADDays = value; }
        }  
        public string DeliverTimeGuaranteed
        {
            get { return _DeliverTimeGuaranteed; }
            set { _DeliverTimeGuaranteed = value; }
        }
        public string PriceCountry
        {
            get { return _PriceCountry; }
            set { _PriceCountry = value; }
        }
        public int OCount
        {
            get { return _Count; }
            set { _Count = value; }
        }  
        public string MakeLogo
        {
            get { return _MakeLogo; }
            set { _MakeLogo = value; }
        }
        public string PriceLogo
        {
            get { return _PriceLogo; }
            set { _PriceLogo = value; }
        }
        public string DestinationLogo
        {
            get { return _DestinationLogo; }
            set { _DestinationLogo = value; }
        }
        public string GlobalId
        {
            get { return _GlobalId; }
            set { _GlobalId = value; }
        } 
        public string OrderNum
        {
            get { return _OrderNum; }
            set { _OrderNum = value; }
        }
        public bool IsBack
        {
            get { return _IsBack; }
            set { _IsBack = value; }
        }
        public bool IsSeld
        {
            get { return _IsSeld; }
            set { _IsSeld = value; }
        }
        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        public int StatusEmex
        {
            get { return _StatusEmex; }
            set { _StatusEmex = value; }
        }
        public bool Officed
        {
            get { return _Officed; }
            set { _Officed = value; }
        }
        public bool Payed
        {
            get { return _Payed; }
            set { _Payed = value; }
        }
        public bool Printed
        {
            get { return _Printed; }
            set { _Printed = value; }
        }
        public DateTime OfficedDate
        {
            get { return _OfficedDate; }
            set { _OfficedDate = value; }
        }
        public DateTime PayedDate
        {
            get { return _PayedDate; }
            set { _PayedDate = value; }
        }
        public DateTime IsSeldDate
        {
            get { return _isSeldDate; }
            set { _isSeldDate = value; }
        }

        #endregion
    }
}