using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class ModelDirect:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _ModelName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ModelName
        {
            get { return _ModelName; }
            set { _ModelName = value; }
        }

        #endregion

        #region Methods

        public ModelDirect()
        {
            _ID = 0;
            _ModelName = string.Empty;
        }

        #endregion
    }
}