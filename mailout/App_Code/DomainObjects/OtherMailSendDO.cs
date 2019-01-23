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
    public class OtherMailSendDO : UniversalDO
    {
        void AddParametersToSqlCommand(OtherMailSend ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@OtherMailOutID", ent.OtherMailOutID);
            sc.Parameters.Add("@UserID", ent.UserID);
            sc.Parameters.Add("@date_registration", ent.DateRegister);
            //sc.Parameters.Add("@DateOut", ent.DateOut);
            /*if (ent.DateRegister.HasValue)
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value.ToShortDateString());*/
        }

        void addParameters(OtherMailSend ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(OtherMailSend ent)
        {
            int createid = 0;
            OtherMailSendDAO msDAO = new OtherMailSendDAO();
            sc = new SqlCommand("CreateOtherMailSend");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = msDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity RetrieveOtherMailSendByID(int id)
        {
            OtherMailSendDAO msDAO = new OtherMailSendDAO();
            sc = new SqlCommand("RetrieveOtherMailSendByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (msDAO.retrieveEntity(sc));
        }

        public bool Update(OtherMailSend ms)
        {
            bool success = true;
            OtherMailSendDAO msDAO = new OtherMailSendDAO();
            sc = new SqlCommand("UpdateOtherMailSend");
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