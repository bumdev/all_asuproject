using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class PhysicalMailOut : UniversalEntity
    {
        #region Attributes

        int _ID;
        string _RegNumber;
        DateTime? _DateOut;
        string _AnswerDate;
        string _About;
        string _Whom;
        int _PhysicalAdresatID;
        string _AnswerAbout;
        string _Notation;
        string _ContractNumber;
        int _SenderID; 
        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string RegNumber
        {
            get { return _RegNumber; }
            set { _RegNumber = value; }
        }

        public string About
        {
            get { return _About; }
            set { _About = value; }
        }

        public DateTime? DateOut
        {
            get { return _DateOut; }
            set { _DateOut = value; }
        }

        public string AnswerDate
        {
            get { return _AnswerDate; }
            set { _AnswerDate = value; }
        }

        public string Whom
        {
            get { return _Whom; }
            set { _Whom = value; }
        }

        public int PhysicalAdresatID
        {
            get { return _PhysicalAdresatID; }
            set { _PhysicalAdresatID = value; }
        }

        public string AnswerAbout
        {
            get { return _AnswerAbout; }
            set { _AnswerAbout = value; }
        }

        public string Notation
        {
            get { return _Notation; }
            set { _Notation = value; }
        }

        public string ContractNumber
        {
            get { return _ContractNumber; }
            set { _ContractNumber = value; }
        }

        public int SenderID
        {
            get { return _SenderID; }
            set { _SenderID = value; }
        }

        #endregion

        #region Methods

        public PhysicalMailOut()
        {
            _ID = 0;
            _RegNumber = "";
            _DateOut = null;
            _AnswerAbout = "";
            _AnswerDate = "";
            _Whom = "";
            _About = "";
            _PhysicalAdresatID = 0;
            _Notation = "";
            _ContractNumber = "";
            _SenderID = 0;

        }

        #endregion
    }
}