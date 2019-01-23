using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;
using cartridges_app.Controls;

namespace DomainObjects
{
    public class RequestsDO:UniversalDO
    {
        void AddParametersToSqlCommand(Requests ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@AmountDevice", ent.AmountDevice);
            sc.Parameters.Add("@Comment", ent.Comment);
            //sc.Parameters.Add("@RejectVodomer", ent.RejectVodomer);
        }
        void addParameters(Requests ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(Requests ent)
        {
            int createdid = 0;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("CreateRequests");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }


        public bool Update(Requests ent, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("UpdateRequests");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@RequestsID", ent.ID);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@AmountDevice", ent.AmountDevice);
            sc.Parameters.Add("@Comment", ent.Comment);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool UpdateRequests(Requests ent)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("UpdateRequestsHistory");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@AmountDevice", ent.AmountDevice);
            sc.Parameters.Add("@Comment", ent.AmountDevice);
            success = entDAO.updateEntity(sc);
            return success;
        }

        
        public bool Delete(int requestsID, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("DeleteRequests");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@RequestsID", requestsID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool Annulment(int requestsID, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("RequestsRemoveAnnulment");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@RequestsID", requestsID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool BackToRemove(int requestsID, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("BackToRemove");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@RequestsID", requestsID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool SubstitutionRemove(int requestsID, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("SubstitutionRemove");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@RequestsID", requestsID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
            
        }

        public bool RequestsIsDone(int requestsID, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("RequestsIsDone");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@RequestsID", requestsID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        
        public bool Common(int requestsID, int UserID)
        {
            bool success = true;
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("CommonRequests");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@RequestsID", requestsID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
         

        public UniversalEntity RetrieveRequestsByID(int id)
        {
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("RetrieveRequestsByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveRequestsByOrderID(int id)
        {
            RequestsDAO entDAO = new RequestsDAO();
            sc = new SqlCommand("RetrieveRequestsByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

       
    }
}