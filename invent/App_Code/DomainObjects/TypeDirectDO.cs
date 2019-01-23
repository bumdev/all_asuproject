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
    public class TypeDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(TypeDir ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("NameType", ent.ID);
        }

        void addParameters(TypeDir ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateTypeDirect(TypeDir ent)
        {
            int createid = 0;
            TypeDirectDAO entDAO = new TypeDirectDAO();
            UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("CreateTypeInventory");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = (entDAO.createEntity(sc));
            return createid;
        }

        public UniversalEntity RetrieveTypeDirect()
        {
            TypeDirectDAO entDAO = new TypeDirectDAO();
            //UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("RetrieveTypeImplem");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
    }
}