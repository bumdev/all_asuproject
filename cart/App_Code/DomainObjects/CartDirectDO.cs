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
    public class CartDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(CartDirect ent, ref SqlCommand sc)
        {
            //sc.Parameters.Add("@Cartname", ent.NameCartridge);
            sc.Parameters.Add("@Cartname", ent.CartName);
        }
        void addParameters(CartDirect ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        

        public int CreateCartDirect(CartDirect ent)
        {
            int createid = 0;
            CartDirectDAO entDAO = new CartDirectDAO();
            sc = new SqlCommand("CreateCartDirect");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveCartDirect()
        {
            CartDirectDAO entDAO = new CartDirectDAO();
            sc = new SqlCommand("RetrieveCartDirect");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
      
    }
}