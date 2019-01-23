using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class GroupDirect:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _GroupName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        #endregion

        #region Methods

        public GroupDirect()
        {
            _ID = 0;
            _GroupName = string.Empty;
        }

        #endregion
    }
}