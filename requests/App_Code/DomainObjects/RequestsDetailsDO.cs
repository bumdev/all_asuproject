using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

namespace DomainObjects
{
    public class RequestsDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(RequestsDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@RequestsSendID", ent.RequestsSendID);
            //sc.Parameters.Add("@EndValue", ent.EndValue);
        }
        void addParameters(RequestsDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(RequestsDetails ent)
        {
            int createdid = 0;
            RequestsDetailsDAO entDAO = new RequestsDetailsDAO();
            sc = new SqlCommand("CreateRequestsDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
       
    }
}