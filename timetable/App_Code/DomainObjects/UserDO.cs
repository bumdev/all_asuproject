using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

namespace DomainObjects
{
    public class UserDO : UniversalDO
    {
        void AddParametersToSqlCommand(User ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@UserName", ent.UserName);
            sc.Parameters.Add("@UserLogin", ent.UserLogin);
            sc.Parameters.Add("@UserPassword", ent.UserPassword);
            sc.Parameters.Add("@IsActive", ent.IsActive);
            //sc.Parameters.Add("@UserLocationID", ent.Location);
            sc.Parameters.Add("@ServicesID", ent.SerivceID);
        }
        void addParameters(User ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int CreateUser(User ent)
        {
            int createdid = 0;
            UserDAO entDAO = new UserDAO();
            sc = new SqlCommand("CreateUser");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool UpdateUser(User ent)
        {
            bool success = true;
            UserDAO entDAO = new UserDAO();
            sc = new SqlCommand("UpdateUser");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveUserAccess(string login, string pass)
        {
            UserDAO entDAO = new UserDAO();
            sc = new SqlCommand("RetrieveUserByAccess");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserLogin", login);
            sc.Parameters.Add("@UserPassword", pass);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveUserById(int id)
        {
            UserDAO entDAO = new UserDAO();
            sc = new SqlCommand("RetrieveUserById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}