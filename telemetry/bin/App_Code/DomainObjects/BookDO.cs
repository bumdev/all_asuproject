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
    public class BookDO : UniversalDO
    {
        void AddParametersToSqlCommand(Book ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@Title", ent.Title);
            sc.Parameters.Add("@Description", ent.Description);
        }
        void addParameters(Book ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateWPLocation(Book ent)
        {
            int createdid = 0;
            BookDAO entDAO = new BookDAO();
            sc = new SqlCommand("CreateWPLocation");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public UniversalEntity RetrieveWPLocations()
        {
            BookDAO entDAO = new BookDAO();
            sc = new SqlCommand("RetrieveWPLocations");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveWPTypes()
        {
            BookDAO entDAO = new BookDAO();
            sc = new SqlCommand("RetrieveWPTypes");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
        /*
        public bool UpdateBook(Book ent)
        {
            bool success = true;
            BookDAO entDAO = new BookDAO();
            sc = new SqlCommand("UpdateBook");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveBookAccess(string login, string pass)
        {
            BookDAO entDAO = new BookDAO();
            sc = new SqlCommand("RetrieveBookByAccess");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@BookLogin", login);
            sc.Parameters.Add("@BookPassword", pass);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveBookById(int id)
        {
            BookDAO entDAO = new BookDAO();
            sc = new SqlCommand("RetrieveBookById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}