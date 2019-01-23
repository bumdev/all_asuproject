using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class WPLocation
    {
        int _ID;
        string _Title;
        string _Description;


        



        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}