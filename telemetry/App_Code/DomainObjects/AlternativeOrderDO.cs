using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;

namespace DomainObjects
{
    public class AlternativeOrderDO : UniversalDO
    {
        void AddParametresToSqlCommand(AlternativeOrder ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@SAbonentID", ent.SAbonentID);
            sc.Parameters.Add("@WorkType", ent.WorkType);
            sc.Parameters.Add("@UserID", ent.UserID);
        }

        void addParametres(AlternativeOrder ent)
        {
            AddParametresToSqlCommand(ent, ref sc);
        }

        public int CreateSOrder(AlternativeOrder ent)
        {
            int createid = 0;
            AlternativeOrderDAO entDAO = new AlternativeOrderDAO();
            sc = new SqlCommand("CreateSOrder");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }


        public bool UpdateSOrder(AlternativeOrder ent)
        {
            bool success = true;
            AlternativeOrderDAO entDAO = new AlternativeOrderDAO();
            sc = new SqlCommand("UpdateSOrder");
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

        /* public UniversalEntity RetrieveSOrderByID(int id)
         {
             SOrderDAO entDAO = new SOrderDAO();
             sc = new SqlCommand("RetrieveSOrderById");
             sc.CommandType = CommandType.StoredProcedure;
             sc.Parameters.Add("@ID", id);
             return (entDAO.retrieveEntity(sc));
         }*/


        public UniversalEntity RetrieveSOrderById(int id)
        {
            AlternativeOrderDAO entDAO = new AlternativeOrderDAO();
            sc = new SqlCommand("RetrieveSOrderById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}