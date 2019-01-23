using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Entities
{
    public class ResponsibleContractor : UniversalEntity
    {
        #region Attributes

        int _ID;
        ResponContractPreview _ResponContractPreview;
        string _District;
        DateTime? _DateRegister;
        DateTime? _DateIn;
        int _UserID;
        int _ResponContractDirectory;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

       

        public ResponContractPreview ResponContractPreview
        {
            get { return _ResponContractPreview; }
            set { _ResponContractPreview = value; }
        }

        public string District
        {
            get { return _District; }
            set { _District = value; }
        }

        public DateTime? DateRegister
        {
            get { return _DateRegister; }
            set { _DateRegister = value; }
        }

        public DateTime? DateIn
        {
            get { return _DateIn; }
            set { _DateIn = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public int ResponContractDirectory
        {
            get { return _ResponContractDirectory; }
            set { _ResponContractDirectory = value; }
        }

        #endregion

        #region Methods

        public ResponsibleContractor()
        {
            _ID = 0;
            _DateRegister = null;
            _DateIn = null;
            _ResponContractDirectory = 0;
            _District = "";
            _UserID = 0;
        }

        #endregion
    }
}