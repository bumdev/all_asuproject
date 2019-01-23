using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;
using leak_detectors.Controls;

namespace DomainObjects
{
    public class PointsDO:UniversalDO
    {
        void AddParametersToSqlCommand(Points ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@Name", ent.Name);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@TypePoints", ent.TypePoints);
            //sc.Parameters.Add("@TypePointsID", ent.TypePointsID);
            //sc.Parameters.Add("@RejectVodomer", ent.RejectVodomer);
        }
        void addParameters(Points ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(Points ent)
        {
            int createdid = 0;
            PointsDAO entDAO = new PointsDAO();
            sc = new SqlCommand("CreatePoints");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }

        
         public UniversalEntity RetrieveByInfoID(int id)
        {
            PointsDAO entDAO = new PointsDAO();
            //sc = new SqlCommand("RetrieveFAbonentByOrderID");
            sc = new SqlCommand("RetrievePointsByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public bool Update(Points ent)
        {
            bool success = true;
            PointsDAO pDAO = new PointsDAO();
            sc = new SqlCommand("UpdatePoints");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = pDAO.updateEntity(sc);
            return success;
        }

        public bool UpdateWithHistory(Points ent, int UserID)
        {
            bool success = true;
            PointsDAO entDAO = new PointsDAO();
            sc = new SqlCommand("UpdatePointsWithHistory");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@PointsID", ent.ID);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@Name", ent.Name);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@TypePoints", ent.TypePoints);
            sc.Parameters.Add("@UserID", UserID);
            success = entDAO.updateEntity(sc);
            return success;
        }
    }
}