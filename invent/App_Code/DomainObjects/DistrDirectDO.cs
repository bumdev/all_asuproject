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
    public class DistrDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(Distr ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("DistrictName", ent.DistrictName);
        }

        void addParameters(Distr ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateDistr(Distr ent)
        {
            int createid = 0;
            DistrictDAO entDAO = new DistrictDAO();
            sc = new SqlCommand("CreateDistricts");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveDistricts()
        {
            DistrictDAO entDAO = new DistrictDAO();
            sc = new SqlCommand("RetrieveDistricts");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
    }
}