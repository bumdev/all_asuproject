using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class ServiceTable : UniversalEntity
    {
        int _ID;
        string _Services_Name;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Services_Name
        {
            get { return _Services_Name; }
            set { _Services_Name = value; }
        }

        public ServiceTable()
        {
            _ID = 0;
            _Services_Name = "";
        }
    }
}