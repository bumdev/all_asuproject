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
    public class WaterPointDO : UniversalDO
    {
        void AddParametersToSqlCommand(WaterPoint ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@LocationID", ent.LocationID);
            sc.Parameters.Add("@Title", ent.Title);     
            sc.Parameters.Add("@LF", ent.LineFirst);  
            sc.Parameters.Add("@LS", ent.LineSecond);  
            sc.Parameters.Add("@D", ent.D);  
            sc.Parameters.Add("@DCalc", ent.DCalc);  
            sc.Parameters.Add("@Qmin", ent.QMin);  
            sc.Parameters.Add("@Qmax", ent.QMax);  
            sc.Parameters.Add("@Comment", ent.Comment);  
            sc.Parameters.Add("@TypeID", ent.Wptype);  
        }
        void addParameters(WaterPoint ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(WaterPoint ent,string xml)
        {
            int createdid = 0;
            WaterPointDAO entDAO = new WaterPointDAO();
            sc = new SqlCommand("CreateWaterPoint");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            if (!string.IsNullOrEmpty(xml))
            {
                sc.Parameters.Add("@Devicesxml", xml);
            }
            else
            {
                sc.Parameters.Add("@Devicesxml", DBNull.Value);
            }
            createdid = entDAO.createEntity(sc);
            return createdid;
        }



        /*
        public bool Update(WaterPoint ent)
        {
            bool success = true;
            WaterPointDAO entDAO = new WaterPointDAO();
            sc = new SqlCommand("UpdateWaterPoint");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveWaterPointById(int id)
        {
            WaterPointDAO entDAO = new WaterPointDAO();
            sc = new SqlCommand("RetrieveWaterPointById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
         
         */
    }
}