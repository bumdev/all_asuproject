using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using DomainObjects;
using Entities;
using DAO;

namespace DomainObjects
{
    public class MailSendDO : UniversalDO
    {
        void AddParametersToSqlCommand(MailSend ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@MailOutID", ent.MailOutID);
            sc.Parameters.Add("@UserID", ent.UserID);
            sc.Parameters.Add("@date_registration", ent.DateRegister);
            //sc.Parameters.Add("@DateOut", ent.DateOut);
            /*if (ent.DateRegister.HasValue)
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value.ToShortDateString());*/
        }

        void addParameters(MailSend ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(MailSend ent)
        {
            int createid = 0;
            MailSendDAO msDAO = new MailSendDAO();
            sc = new SqlCommand("CreateMailSend");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = msDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveMailSendByID(int id)
        {
            MailSendDAO msDAO = new MailSendDAO();
            sc = new SqlCommand("RetrieveMailSendByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (msDAO.retrieveEntity(sc));
        }

        
         
         public bool DeleteMailSendByID(int id)
        {
            bool success = true;
            MailSendDAO entDAO = new MailSendDAO();
            sc = new SqlCommand("DeleteMailOutByID");
            sc.Parameters.Add("@MailOutID", id);
            sc.CommandType = CommandType.StoredProcedure;
            success = entDAO.deleteEntity(sc);
            return success;
        }
         
         

        public bool Update(MailSend ms)
        {
            bool success = true;
            MailSendDAO msDAO = new MailSendDAO();
            sc = new SqlCommand("UpdateMailSend");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ms.ID);
            sc.Parameters.Add("@UserID", ms.UserID);
            sc.Parameters.Add("@date_registration", ms.DateRegister);
            /*if (ms.DateRegister == null)
            {
                sc.Parameters.Add("@date_registration", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@date_registration", ms.DateRegister.Value);
            }*/
            success = msDAO.updateEntity(sc);
            return success;
        }
    }
}