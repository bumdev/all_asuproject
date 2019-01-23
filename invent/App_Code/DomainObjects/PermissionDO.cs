using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

/// <summary>
/// Summary description for PermissionDO
/// </summary>
/// 

namespace DomainObjects
{
    public class PermissionDO:UniversalDO
    {
        void AddParametersToSqlCommand(Permission ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@PermissionName", ent.PermissionName);
        }
        void addParameters(Permission ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int CreatePermission(Permission ent)
        {
            int createdid = 0;
            PermissionDAO entDAO = new PermissionDAO();
            sc = new SqlCommand("CreatePermission");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public int AddPermissionToUser(int userid, int permissionid)
        {
            int createdid = 0;
            PermissionDAO entDAO = new PermissionDAO();
            sc = new SqlCommand("AddPermissionToUser");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@PermissionID", permissionid);
            sc.Parameters.Add("@UserID", userid);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool DeletePermission(int id)
        {
            bool success = true;
            PermissionDAO entDAO = new PermissionDAO();
            sc = new SqlCommand("DeletePermission");
            sc.Parameters.Add("@ID", id);
            sc.CommandType = CommandType.StoredProcedure;
            success = entDAO.deleteEntity(sc);
            return success;
        }
        public bool DeletePermissionFromUser(int userid, int permissionid)
        {
            bool success = true;
            PermissionDAO entDAO = new PermissionDAO();
            sc = new SqlCommand("DeletePermissionFromUser");
            sc.Parameters.Add("@PermissionID", permissionid);
            sc.Parameters.Add("@UserID", userid);
            sc.CommandType = CommandType.StoredProcedure;
            success = entDAO.deleteEntity(sc);
            return success;
        }
        public UniversalEntity RetrievePermissionsByUser(int id)
        {
            PermissionDAO entDAO = new PermissionDAO();
            sc = new SqlCommand("RetrievePermissionsByUser");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrievePermissionsByUserLeft(int id)
        {
            PermissionDAO entDAO = new PermissionDAO();
            sc = new SqlCommand("RetrievePermissionsByUserLeft");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}