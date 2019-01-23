using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class WPDevice : UniversalEntity
    {
        int _ID;        
        int _TypeID;        
        string _Title;        
        string _FN;        
        string _Description;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string FN
        {
            get { return _FN; }
            set { _FN = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

    }
}