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
    public class FOrderDO : UniversalDO
    {
        void AddParametersToSqlCommand(FOrder ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@FAbonentID", ent.FAbonentID);
            sc.Parameters.Add("@ActionType", ent.ActionType);
            sc.Parameters.Add("@UserID", ent.UserID);
        }
        void addParameters(FOrder ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(FOrder ent)
        {
            int createdid = 0;
            FOrderDAO entDAO = new FOrderDAO();
            sc = new SqlCommand("CreateFOrder");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool UpdateFOrder(FOrder ent)
        {
            bool success = true;
            FOrderDAO entDAO = new FOrderDAO();
            sc = new SqlCommand("UpdateFOrder");
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

        
        public UniversalEntity RetrieveFOrderById(int id)
        {
            FOrderDAO entDAO = new FOrderDAO();
            sc = new SqlCommand("RetrieveFOrderById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        /*
        public UniversalEntity RetrieveFOrders()
        {
            FOrderDAO entDAO = new FOrderDAO();
            sc = new SqlCommand("RetrieveFOrders");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveFOrdersByDiameter(int d)
        {
            FOrderDAO entDAO = new FOrderDAO();
            sc = new SqlCommand("RetrieveFOrdersByDiameter");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Diameter", d);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}