using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class MailOut : UniversalEntity
    {
        #region Attributes

        int _ID;
        string _RegNumber;
       // DateTime _DateRegistration;
        DateTime? _DateOut;
        string _AnswerDate;
        string _About;
        int _AdresatID;
        string _Whom;
        string _AnswerAbout;
        string _Notation;
        string _PersonalAccount;

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

       /* public DateTime DateRegistration
        {
            get { return _DateRegistration; }
            set { _DateRegistration = value; }
        }*/

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

        public string About
        {
            get { return _About; }
            set { _About = value; }
        }

        public int AdresatID
        {
            get { return _AdresatID; }
            set { _AdresatID = value; }
        }

        public string Whom
        {
            get { return _Whom; }
            set { _Whom = value; }
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

        public string PersonalAccount
        {
            get { return _PersonalAccount; }
            set { _PersonalAccount = value; }
        }

        #endregion

        #region Methods

        public MailOut()
        {
            _ID = 0;
            _RegNumber = "";
            //_DateRegistration = DateTime.MinValue;
            _DateOut = null;
            _AnswerAbout = "";
            _About = "";
            _AdresatID = 0;
            _Whom = "";
            _AnswerAbout = "";
            _Notation = "";
            _PersonalAccount = "";
        }

        #endregion
    }
}