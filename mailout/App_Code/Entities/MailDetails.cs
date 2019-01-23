using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class MailDetails : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _MailSendID;
        bool _IsDeleted;
        int _ResponsibleContractorID;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int MailSendID
        {
            get { return _MailSendID; }
            set { _MailSendID = value; }
        }

        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        public int ResponsibleContractorID
        {
            get { return _ResponsibleContractorID; }
            set { _ResponsibleContractorID = value; }
        }

        #endregion

        #region Methods

        public MailDetails()
        {
            _ID = 0;
            _MailSendID = 0;
            _IsDeleted = false;
            _ResponsibleContractorID = 0;
        }

        #endregion
    }
}