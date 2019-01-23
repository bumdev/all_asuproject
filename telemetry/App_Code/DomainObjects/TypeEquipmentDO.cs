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
    public class TypeEquipmentDO:UniversalDO
    {
        void AddParametersToSqlCommand(TypeEquipment ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@TypeName", ent.TypeName);
        }
        void addParameters(TypeEquipment ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        /*public int CreateUser(Seller ent)
        {
            int createdid = 0;
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("CreateSeller");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }*/
        
        /*public UniversalEntity RetrieveSellers()
        {
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("RetrieveSellers");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }*/

        public UniversalEntity RetrieveTypeEquipment()
        {
            TypeEquipmentDAO entDAO = new TypeEquipmentDAO();
            sc = new SqlCommand("RetrieveTypeEquipment");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        
    }
}