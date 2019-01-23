using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Tmp
/// </summary>
/// 


namespace Entities
{
    public enum Abonent
    {
        Private = 0,
        Corporate = 1,
        Special = 2
    }

    public enum Mail
    {
        Out = 0,
        Physical = 1,
        Other = 2
    }

    public class ResponContractPreview
    {
        string _CodeDepartament;
        string _DepartamentName;
        string _ResponName;
        //string _Position;

        public string CodeDepartament
        {
            get { return _CodeDepartament; }
            set { _CodeDepartament = value; }
        }

        public string DepartamentName
        {
            get { return _DepartamentName; }
            set { _DepartamentName = value; }
        }

        public string ResponName
        {
            get { return _ResponName; }
            set { _ResponName = value; }
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

    public class SessionMail
    {
        short _Type;

        public short Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        MailOut _MailOut;
        PhysicalMailOut _PhysicalMailOut;
        OtherMailOut _OtherMailOut;

        public MailOut OutMail
        {
            get { return _MailOut; }
            set { _MailOut = value; }
        }

        public PhysicalMailOut PhysicalMailOut
        {
            get { return _PhysicalMailOut; }
            set { _PhysicalMailOut = value; }
        }

        public OtherMailOut OtherMailOut
        {
            get { return _OtherMailOut; }
            set { _OtherMailOut = value; }
        }

        List<ResponsibleContractor> _ResponsibleContractor = new List<ResponsibleContractor>();

        public List<ResponsibleContractor> ResponsibleContractor
        {
            get { return _ResponsibleContractor; }
            set { _ResponsibleContractor = value; }
        }

        public void AddResponContract(ResponsibleContractor rc)
        {
            _ResponsibleContractor.Add(rc);
        }
    }


    
}