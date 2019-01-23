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
    public class PointsInfoDO : UniversalDO
    {
        void AddParametersToSqlCommand(PointsInfo ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@PointsID", ent.PointsID);
            sc.Parameters.Add("@UserID", ent.UserID);
        }
        void addParameters(PointsInfo ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(PointsInfo ent)
        {
            int createdid = 0;
            PointsInfoDAO entDAO = new PointsInfoDAO();
            sc = new SqlCommand("CreatePointsInfo");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }


        public UniversalEntity RetrieveByID(int id)
        {
            PointsInfoDAO entDAO = new PointsInfoDAO();
            //sc = new SqlCommand("RetrievePointsInfoById");
            sc = new SqlCommand("RetrievePointsInfoByInfoId");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
         
         
    }
}