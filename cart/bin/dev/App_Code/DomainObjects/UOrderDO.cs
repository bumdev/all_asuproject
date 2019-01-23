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
    public class UOrderDO : UniversalDO
    {
        void AddParametersToSqlCommand(UOrder ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@UAbonentID", ent.UAbonentID);
            sc.Parameters.Add("@ActionType", ent.ActionType);
            sc.Parameters.Add("@UserID", ent.UserID);
        }
        void addParameters(UOrder ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(UOrder ent)
        {
            int createdid = 0;
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("CreateUOrder");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool UpdateUOrder(UOrder ent)
        {
            bool success = true;
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("UpdateUOrder");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            sc.Parameters.Add("@IsPaid", ent.IsPaid);
            sc.Parameters.Add("@UserID", ent.UserID);
            if (ent.DateOut == null)
            {
                sc.Parameters.Add("@DateOut", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@DateOut", ent.DateOut);
            }
            if (ent.PaymentDay == null)
            {
                sc.Parameters.Add("@PaymentDay", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@PaymentDay", ent.PaymentDay.Value);
            }
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }


        public UniversalEntity RetrieveUOrderById(int id)
        {
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("RetrieveUOrderById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        /*public bool UpdateUOrder(UOrder ent)
        {
            bool success = true;
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("UpdateUOrder");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }*/

        /*
        public UniversalEntity RetrieveUOrderById(int id)
        {
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("RetrieveUOrderById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveUOrders()
        {
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("RetrieveUOrders");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveUOrdersByDiameter(int d)
        {
            UOrderDAO entDAO = new UOrderDAO();
            sc = new SqlCommand("RetrieveUOrdersByDiameter");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Diameter", d);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}