using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

namespace DomainObjects
{
    public class FOrderDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(FOrderDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@FOrderID", ent.FOrderID);
            sc.Parameters.Add("@VodomerID", ent.VodomerID);
            sc.Parameters.Add("@StartValue", ent.StartValue);
            sc.Parameters.Add("@DefectVodomer", ent.MarriageVodomer);
            //sc.Parameters.Add("@EndValue", ent.EndValue);
        }
        void addParameters(FOrderDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(FOrderDetails ent)
        {
            int createdid = 0;
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("CreateFOrderDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public UniversalEntity RetrieveFOrderDetailsByOrderID(int id)
        {
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("RetrieveFOrderDetailsByOrderID");
            sc.Parameters.Add("@OrderID", id);
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveFOrderDetailsByVodomerandFOrderID(int id)
        {
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("RetrieveVodomersByOrderID");
            sc.Parameters.Add("@OrderID", id);
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        /*
        public bool UpdateFOrderDetails(FOrderDetails ent)
        {
            bool success = true;
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("UpdateFOrderDetails");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveFOrderDetailsById(int id)
        {
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("RetrieveFOrderDetailsById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveFOrderDetailss()
        {
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("RetrieveFOrderDetailss");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveFOrderDetailssByDiameter(int d)
        {
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("RetrieveFOrderDetailssByDiameter");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Diameter", d);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}