using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

namespace DomainObjects
{
    public class PointsDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(PointsDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@PointsInfoID", ent.PointsInfoID);
            sc.Parameters.Add("@PointsEquipmentID", ent.PointsEquipmendID);
            //sc.Parameters.Add("@EndValue", ent.EndValue);
        }
        void addParameters(PointsDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(PointsDetails ent)
        {
            int createdid = 0;
            PointsDetailsDAO entDAO = new PointsDetailsDAO();
            sc = new SqlCommand("CreatePointsDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        
    }
}