using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DomainObjects;
using Entities;
using DAO;

namespace DomainObjects
{
    public class MailDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(MailDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@MailSendID", ent.MailSendID);
            //sc.Parameters.Add("@IsDeleted", ent.IsDeleted);
            sc.Parameters.Add("@ResponsibleContractorID", ent.ResponsibleContractorID);
        }

        void addParameters(MailDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(MailDetails ent)
        {
            int createid = 0;
            MailDetailsDAO entDAO = new MailDetailsDAO();
            sc = new SqlCommand("CreateMailDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }
    }
}