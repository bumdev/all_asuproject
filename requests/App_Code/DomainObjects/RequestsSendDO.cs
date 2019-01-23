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
    public class RequestsSendDO : UniversalDO
    {
        void AddParametersToSqlCommand(RequestsSend ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@RequestsID", ent.RequestsID);
            //sc.Parameters.Add("@Comment", ent.Comment);
            sc.Parameters.Add("@UserID", ent.UserID);
        }
        void addParameters(RequestsSend ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(RequestsSend ent)
        {
            int createdid = 0;
            RequestsSendDAO entDAO = new RequestsSendDAO();
            sc = new SqlCommand("CreateRequestsSend");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }

        public bool Update(RequestsSend ent)
        {
            bool success = true;
            RequestsSendDAO entDAO = new RequestsSendDAO();
            sc =  new SqlCommand("UpdateRequestsSend");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            if (ent.DateInstallation == null)
            {
                sc.Parameters.Add("@DateInstallation", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@DateInstallation", ent.DateInstallation.Value);
            }
            if (ent.DateWithdrawal == null)
            {
                sc.Parameters.Add("@DateWithdrawal", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@DateWithdrawal", ent.DateWithdrawal.Value);
            }
            success = entDAO.updateEntity(sc);
            return success;
        }


        public UniversalEntity RetrieveRequestsSendByID(int id)
        {
            RequestsSendDAO entDAO = new RequestsSendDAO();
            sc = new SqlCommand("RetrieveRequestsSendByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveRequestsSendByOrderID(int id)
        {
            RequestsSendDAO entDAO = new RequestsSendDAO();
            sc = new SqlCommand("RetrieveRequestsSendByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }

        
    }
}