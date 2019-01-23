using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Permission
/// </summary>
/// 

namespace Entities
{
    public enum Permissions
    { 
        AllOrder,
        RegisterEditor,
        WaterPoint,
        WaterPointReadOnly
    }

    public class Permission:UniversalEntity
    {
        int _ID;
        string _PermissionName;


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string PermissionName
        {
            get { return _PermissionName; }
            set { _PermissionName = value; }
        }



        public Permission()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}