using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using DomainObjects;
using Entities;
using DAO;

namespace DomainObjects
{
    public class DirectoryResponContractDO : UniversalDO
    {
        private void AddParametersToSqlCommand(DirectoryResponContract ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@ResponName", ent.ResponName);
            sc.Parameters.Add("@DepartamentID", ent.DepartamentID);
            sc.Parameters.Add("@Approve", ent.Approve);
            
        }

        private void addParameters(DirectoryResponContract ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(DirectoryResponContract ent)
        {
            int createid = 0;
            DirectoryResponContractDAO entDAO = new DirectoryResponContractDAO();
            sc = new SqlCommand("CreateDirectoryResponContract");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveResponContractByDepartament(int d)
        {
            DirectoryResponContractDAO entDAO = new DirectoryResponContractDAO();
            sc = new SqlCommand("RetrieveResponContractByDepartament");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ResponName", d);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveResponContractByID(int id)
        {
            DirectoryResponContractDAO entDAO = new DirectoryResponContractDAO();
            sc = new SqlCommand("RetrieveResponContractByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}