using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;
using DAO;

namespace DomainObjects
{
    public class DistrDO:UniversalDO
    {
        void AddParamatersToSqlCommand(Distr ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("DistrictName", ent.DistrictName);
        }

        void addParameters(Distr ent)
        {
            AddParamatersToSqlCommand(ent, ref sc);
        }

        public int CreateDistricts(Distr ent)
        {
            int createid = 0;
            DistrictDAO entDAO = new DistrictDAO();
            //UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("CreateDistrict");
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