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
            sc.Parameters.Add("@DepartName", ent.DepartName);
        }
        void addParameters(Departament ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateDepart(Departament ent)
        {
            int createid = 0;
            DepartamentDAO entDAO = new DepartamentDAO();
            sc = new SqlCommand("CreateDepart");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveDepartaments()
        {
            DepartamentDAO entDAo = new DepartamentDAO();
            sc = new SqlCommand("RetrieveDepart");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAo.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveSellersByDiameter(int d)
        {
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("RetrieveSellersByDiameter");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Diameter", d);
            return (entDAO.retrieveEntity(sc));
        }
    }
}