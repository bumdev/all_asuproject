using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    /// <summary>
    /// Сущность производителя 
    /// </summary>
    public class CartDirect:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _CartName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string CartName
        {
            get { return _CartName; }
            set { _CartName = value; }
        }

        #endregion

        #region Methods

        public CartDirect()
        {
            _ID = 0;
        }

        #endregion

    }
}






