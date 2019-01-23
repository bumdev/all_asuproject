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
    public class WPDeviceDO:UniversalDO
    {
        void AddParametersToSqlCommand(WPDevice ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@fn", ent.FN);
            sc.Parameters.Add("@title", ent.Title);
            sc.Parameters.Add("@typeid", ent.TypeID);
            sc.Parameters.Add("@description", ent.Description);
        }
        void addParameters(WPDevice ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(WPDevice ent)
        {
            int createdid = 0;
            WPDeviceDAO entDAO = new WPDeviceDAO();
            sc = new SqlCommand("CreateWPDevice");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public int CreateWithAssign(WPDevice ent,int wpid,int userID)
        {
            int createdid = 0;
            WPDeviceDAO entDAO = new WPDeviceDAO();
            sc = new SqlCommand("CreateWPDeviceWithAssign");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            sc.Parameters.Add("@wpid", wpid);
            sc.Parameters.Add("@UserID", userID);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public UniversalEntity RetrieveDevicesByNameAndType(int type, string title)
        {
            WPDeviceDAO entDAO = new WPDeviceDAO();
            sc = new SqlCommand("RetrieveDevicesByNameAndType");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@type", type);
            if (!string.IsNullOrEmpty(title))
            {
                sc.Parameters.Add("@Title", title);
            }
            else
            {
                sc.Parameters.Add("@Title", DBNull.Value);
            }
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveDevicesByID(int id)
        {
            WPDeviceDAO entDAO = new WPDeviceDAO();
            sc = new SqlCommand("RetrieveDevicesByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@id", id);
            return (entDAO.retrieveEntity(sc));
        }

        /*public bool Update(WPDevice ent)
        {
            bool success = true;
            WPDeviceDAO entDAO = new WPDeviceDAO();
            sc = new SqlCommand("UpdateWPDevice");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveWPDeviceById(int id)
        {
            WPDeviceDAO entDAO = new WPDeviceDAO();
            sc = new SqlCommand("RetrieveWPDeviceById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}