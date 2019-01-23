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
    public class TypeCartridgesDO:UniversalDO
    {
        void AddParametersToSqlCommand(TypeCartridges ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@NameCartridge", ent.NameCartridge);
        }
        void addParameters(TypeCartridges ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateTypeCart(TypeCartridges ent)
        {
            int createid = 0;
            TypeCartridgesDAO entDAO = new TypeCartridgesDAO();
            sc = new SqlCommand("CreateTypeCart");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveSellers()
        {
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("RetrieveSellers");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveTypeCart()
        {
            TypeCartridgesDAO entDAO = new TypeCartridgesDAO();
            sc = new SqlCommand("RetrieveTypeCart");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
      
    }
}