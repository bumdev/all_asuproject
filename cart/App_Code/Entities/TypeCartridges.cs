using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    /// <summary>
    /// Сущность производителя 
    /// </summary>
    public class TypeCartridges:UniversalEntity
    {

        #region Attributes

        int _ID;
        string _NameCartridge;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string NameCartridge
        {
            get { return _NameCartridge; }
            set { _NameCartridge = value; }
        }

        #endregion

        #region Methods

        public TypeCartridges()
        {
            _ID = 0;
        }

        #endregion

    }
}






