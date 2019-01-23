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
    public class DepartamentDO:UniversalDO
    {
        void AddParametersToSqlCommand(Departament ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@DepartName", ent.DepartName);
        }

        void addParameters(Departament ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateDepartament(Departament ent)
        {
            int createid = 0;
            DepartamentDAO entDAO = new DepartamentDAO();
            UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("CreateDepartament");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = (entDAO.createEntity(sc));
            return createid;
        }

        public UniversalEntity RetrieveDepartaments()
        {
            DepartamentDAO entDAO = new DepartamentDAO();
            sc = new SqlCommand("RetrieveDepart");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
    }
}