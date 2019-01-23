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
    public class DepartamentDO:UniversalDO
    {
        void AddParametersToSqlCommand(Departament ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@DepartamentName", ent.DepartamentName);
        }

        void addParameters(Departament ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public UniversalEntity RetrieveResponContractDepartament(int d)
        {
            DepartamentDAO entDAO = new DepartamentDAO();
            sc = new SqlCommand("RetrieveResponContractDepartament");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@DepartamentName", d);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveDeprtaments()
        {
            DepartamentDAO entDAO = new DepartamentDAO();
            sc = new SqlCommand("RetrieveDepartaments");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
    }
}