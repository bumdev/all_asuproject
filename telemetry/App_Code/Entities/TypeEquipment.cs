using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    /// <summary>
    /// Сущность производителя 
    /// </summary>
    public class TypeEquipment:UniversalEntity
    {
        #region Attributes

        int _ID;
        string _TypeName;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        #endregion

        #region Methods

        public TypeEquipment()
        {
            _ID = 0;
            //_TypeName = "";
        }

        #endregion


    }
}






