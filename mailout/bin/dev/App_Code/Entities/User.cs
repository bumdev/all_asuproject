using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainObjects;

namespace Entities
{
    public class User : UniversalEntity
    {
        #region Attributes

        int _ID;
        string _UserName;
        string _UserLogin;
        string _UserPassword;
        bool _IsActive;
        int _Location;

        List<Permission> _Permissions=new List<Permission>();//Коллекция разрешений

        
       
        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; }
        }
        public string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public int Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public List<Permission> Permissions
        {
            get { return _Permissions; }
        }
        #endregion

        #region Methods
        public User()
        {
            _ID = 0;
            _UserLogin = string.Empty;
            _UserName = string.Empty;
            _UserPassword = string.Empty;
            _IsActive = false;
            _Location = 0;
        }

        /// <summary>
        /// Получение списка разрешений для пользователя
        /// </summary>
        public void GetPermissions()
        {
            PermissionDO pDO = new PermissionDO();
            UniversalEntity ue = new UniversalEntity();
            ue = pDO.RetrievePermissionsByUser(_ID);
            foreach (Permission p in ue)
            {
                _Permissions.Add(p);
            }

        }

        /// <summary>
        /// Проверка наличия соответствующих разрешений
        /// </summary>
        /// <param name="permission">Название разрешения</param>
        /// <returns>Наличие разрешения</returns>
        public bool ChekPermission(string permission)
        {
            bool ok = false;
            foreach (Permission p in _Permissions)
            {
                if (p.PermissionName == permission)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }
        #endregion
    }
}