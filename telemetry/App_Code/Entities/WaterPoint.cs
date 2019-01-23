using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class WaterPoint : UniversalEntity
    {
        int _ID;
        int _LocationID;
        string _Title;
        double _LineFirst;
        double _LineSecond;
        int _D;
        int _DCalc;
        int _QMin;
        int _QMax;
        string _Comment;
        private int _wptype; 




        public int Wptype
        {
            get { return _wptype; }
            set { _wptype = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public double LineFirst
        {
            get { return _LineFirst; }
            set { _LineFirst = value; }
        }

        public double LineSecond
        {
            get { return _LineSecond; }
            set { _LineSecond = value; }
        }

        public int D
        {
            get { return _D; }
            set { _D = value; }
        }

        public int DCalc
        {
            get { return _DCalc; }
            set { _DCalc = value; }
        }

        public int QMin
        {
            get { return _QMin; }
            set { _QMin = value; }
        }

        public int QMax
        {
            get { return _QMax; }
            set { _QMax = value; }
        }

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
    }
}