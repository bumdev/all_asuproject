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
    public class ResponsibleContractorDO : UniversalDO
    {
        private void AddParametersToSqlCommand(ResponsibleContractor ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@id_directory_respon_contract", ent.ResponContractDirectory);
            //sc.Parameters.Add("@date_registration", SqlDbType.Date).Value = ent.DateRegister.HasValue.ToString();
            //sc.Parameters.Add("@District", ent.District);
            /*if (ent.DateRegister.HasValue)
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value.ToShortDateString());*/
        }

        private void addParameters(ResponsibleContractor ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(ResponsibleContractor ent)
        {
            int createid = 0;
            ResponsibleContractorDAO entDAO = new ResponsibleContractorDAO();
            sc = new SqlCommand("CreateResponsibleContractor");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public bool Update(ResponsibleContractor ent)
        {
            bool success = true;
            ResponsibleContractorDAO entDAO = new ResponsibleContractorDAO();
            sc = new SqlCommand("UpdateRC");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            if (ent.DateRegister == null)
            {
                sc.Parameters.Add("@date_registration", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value);
            }
            entDAO.updateEntity(sc);
            return success;
        }

        public UniversalEntity RetrieveResponContractByDepartament(int d)
        {
            ResponsibleContractorDAO entDAO = new ResponsibleContractorDAO();
            sc = new SqlCommand("RetrieveResponContractByDepartament");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ResponName", d);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveResponContractByID(int id)
        {
            ResponsibleContractorDAO entDAO = new ResponsibleContractorDAO();
            sc = new SqlCommand("RetrieveResponContractByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveRCBySendID(int id)
        {
            ResponsibleContractorDAO entDAO = new ResponsibleContractorDAO();
            sc = new SqlCommand("RetrieveRCById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@SendID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}