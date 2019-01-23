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
    public class UAbonentDO:UniversalDO
    {
        void AddParametersToSqlCommand(UAbonent ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@Title", ent.Title);
            sc.Parameters.Add("@NumberJournal", ent.NumberJournal);
            sc.Parameters.Add("@DogID", ent.DogID);
            sc.Parameters.Add("@OKPO", ent.OKPO);
            sc.Parameters.Add("@MFO", ent.MFO);
            sc.Parameters.Add("@RS", ent.RS);
            sc.Parameters.Add("@Contract", ent.Contract);
            sc.Parameters.Add("@Bank", ent.Bank);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@IsBudget", ent.IsBudget);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@ContactFace", ent.ContactFace);
            sc.Parameters.Add("@Cause", ent.Cause);
            sc.Parameters.Add("@INN", ent.INN);
            sc.Parameters.Add("@VATPay", ent.VATPay);
            sc.Parameters.Add("@TaxType", ent.TaxType);
        }
        void addParameters(UAbonent ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(UAbonent ent)
        {
            int createdid = 0;
            UAbonentDAO entDAO = new UAbonentDAO();
            sc = new SqlCommand("CreateUAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool Update(UAbonent ent)
        {
            bool success = true;
            UAbonentDAO entDAO = new UAbonentDAO();
            sc = new SqlCommand("UpdateUabonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        /*
        public UniversalEntity RetrieveClientPersonById(int id)
        {
            UAbonentDAO entDAO = new UAbonentDAO();
            sc = new SqlCommand("RetrieveClientPersonById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }*/
        public UniversalEntity RetrieveLikeSurname(string name)
        {
            UAbonentDAO entDAO = new UAbonentDAO();
            sc = new SqlCommand("RetrieveUAbonentLikeSurname");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@name", name);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveByOrderID(int id)
        {
            UAbonentDAO entDAO = new UAbonentDAO();
            sc = new SqlCommand("RetrieveUAbonentByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}