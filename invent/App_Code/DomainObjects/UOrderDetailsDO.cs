using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

namespace DomainObjects
{
    public class UOrderDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(UOrderDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@UOrderID", ent.UOrderID);
            sc.Parameters.Add("@VodomerID", ent.VodomerID);
            sc.Parameters.Add("@StartValue", ent.StartValue);
            //sc.Parameters.Add("@EndValue", ent.EndValue);
        }
        void addParameters(UOrderDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(UOrderDetails ent)
        {
            int createdid = 0;
            UOrderDetailsDAO entDAO = new UOrderDetailsDAO();
            sc = new SqlCommand("CreateUOrderDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public UniversalEntity RetrieveUOrderDetailsByOrderID(int id)
        {
            UOrderDetailsDAO entDAO = new UOrderDetailsDAO();
            sc = new SqlCommand("RetrieveUOrderDetailsByOrderID");
            sc.Parameters.Add("@OrderID", id);
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }

        /*
        public bool UpdateUOrderDetails(UOrderDetails ent)
        {
            bool success = true;
            UOrderDetailsDAO entDAO = new UOrderDetailsDAO();
            sc = new SqlCommand("UpdateUOrderDetails");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveUOrderDetailsById(int id)
        {
            UOrderDetailsDAO entDAO = new UOrderDetailsDAO();
            sc = new SqlCommand("RetrieveUOrderDetailsById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveUOrderDetailss()
        {
            UOrderDetailsDAO entDAO = new UOrderDetailsDAO();
            sc = new SqlCommand("RetrieveUOrderDetailss");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveUOrderDetailssByDiameter(int d)
        {
            UOrderDetailsDAO entDAO = new UOrderDetailsDAO();
            sc = new SqlCommand("RetrieveUOrderDetailssByDiameter");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Diameter", d);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}