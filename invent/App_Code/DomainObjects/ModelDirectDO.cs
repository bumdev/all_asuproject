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
    public class ModelDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(ModelDirect ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@ModelName", ent.ModelName);
        }

        void addParameters(ModelDirect ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateModel(ModelDirect ent)
        {
            int createid = 0;
            ModelDirectDAO entDAO = new ModelDirectDAO();
            UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("CreateModel");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = (entDAO.createEntity(sc));
            return createid;
        }

        public UniversalEntity RetrieveModelDirect()
        {
            ModelDirectDAO entDAO = new ModelDirectDAO();
            sc = new SqlCommand("RetrieveModel");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
    }
}