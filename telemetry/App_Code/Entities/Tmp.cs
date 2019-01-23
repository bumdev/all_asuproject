using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
/// <summary>
/// Summary description for Tmp
/// </summary>
/// 
using leak_detectors.Controls;

namespace Entities
{
    public enum Abonent
    {
        Private = 0,
        Corporate = 1,
        Special = 2
    }

    public enum LeakPoints
    {
        Common = 0
    }

    public class EquipmentPreview
    {
        string _InventoryNumber;
        string _State;
        string _Model;
        string _TypeEquip;
        string _Seller;
        public bool Repairs { get; set; }

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

        public string Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public string Seller
        {
            get { return _Seller; }
            set { _Seller = value; }
        }

        public string TypeEquip
        {
            get { return _TypeEquip; }
            set { _TypeEquip = value; }
        }
    }

    public class VodomerPreview
    {
        int _Diameter;
        string _StartValue;
        string _Model;
        string _Seller;
        public bool IsNew { get; set; }
        public int Year { get; set; }
        public string New
        {
            get
            {
                if (IsNew) return "Новый";
                else return "";
            }
        }


        public string Seller
        {
            get { return _Seller; }
            set { _Seller = value; }
        }
        public int Diameter
        {
            get { return _Diameter; }
            set { _Diameter = value; }
        }
        public string StartValue
        {
            get { return _StartValue; }
            set { _StartValue = value; }
        }
        public string Model
        {
            get { return _Model; }
            set { _Model = value; }
        }
    }

    public class SessionPoints
    {
        short _Type;

        public short Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        Points _PointsType;

        public Points PointsType
        {
            get { return _PointsType; }
            set { _PointsType = value; }
        }
        List<PointsEquipments> _PointsEquip = new List<PointsEquipments>();

        public List<PointsEquipments> PointsEquip
        {
            get { return _PointsEquip; }
            set { _PointsEquip = value; }
        }

        public void AddEquip(PointsEquipments pe)
        {
            _PointsEquip.Add(pe);
        }
    }


    public class SessionAbonent
    {
        short _Type;

        public short Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        FAbonent _FAbon;
        UAbonent _UAbon;
        AlternativeAbonent _AlternativeAbon;


        public UAbonent UAbon
        {
            get { return _UAbon; }
            set { _UAbon = value; }
        }

        public FAbonent FAbon
        {
            get { return _FAbon; }
            set { _FAbon = value; }
        }

        public AlternativeAbonent AlternativeAbon
        {
            get { return _AlternativeAbon; }
            set { _AlternativeAbon = value; }
        }
        List<Vodomer> _Vodomer = new List<Vodomer>();

        public List<Vodomer> Vodomer
        {
            get { return _Vodomer; }
            set { _Vodomer = value; }
        }
        public void AddVodomer(Vodomer v)
        {
            _Vodomer.Add(v);
        }
    }
}