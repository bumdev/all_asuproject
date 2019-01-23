using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    /// <summary>
    /// Сущность производителя 
    /// </summary>
    public class Seller:UniversalEntity
    {

        #region Attributes

        int _ID;
        string _SellerName;

        #endregion

        #region Properties

       
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string SellerName
        {
            get { return _SellerName; }
            set { _SellerName = value; }
        }

        #endregion

        #region Methods

        public Seller()
        {
            _ID = 0;
        }
        #endregion
       
    }
}






