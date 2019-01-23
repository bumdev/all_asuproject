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
    public class SellerDO:UniversalDO
    {
        void AddParametersToSqlCommand(Seller ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@seller", ent.SellerName);
        }
        void addParameters(Seller ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int CreateUser(Seller ent)
        {
            int createdid = 0;
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("CreateSeller");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool UpdateSeller(Seller ent)
        {
            bool success = true;
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("UpdateSeller");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveSellerById(int id)
        {
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("RetrieveSellerById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveSellers()
        {
            SellerDAO entDAO = new SellerDAO();
            sc = new SqlCommand("RetrieveSellers");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
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