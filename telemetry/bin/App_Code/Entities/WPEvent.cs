using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class WPEvent : UniversalEntity
    {
        int _ID;
        int _WPID;
        int _EventType;
        string _Source;
        DateTime _DateIn;
        int _UserID;




        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int WPID
        {
            get { return _WPID; }
            set { _WPID = value; }
        }

        public int EventType
        {
            get { return _EventType; }
            set { _EventType = value; }
        }

        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        public DateTime DateIn
        {
            get { return _DateIn; }
            set { _DateIn = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
    }
}