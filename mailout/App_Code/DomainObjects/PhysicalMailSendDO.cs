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
    public class PhysicalMailSendDO : UniversalDO
    {
        void AddParametersToSqlCommand(PhysicalMailSend ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@PhysicalMailOutID", ent.PhysicalMailOutID);
            sc.Parameters.Add("@UserID", ent.UserID);
            sc.Parameters.Add("@date_registration", ent.DateReg);
            //sc.Parameters.Add("@DateOut", ent.DateOut);
            /*if (ent.DateRegister.HasValue)
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value.ToShortDateString());*/
        }

        void addParameters(PhysicalMailSend ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(PhysicalMailSend ent)
        {
            int createid = 0;
            PhysicalMailSendDAO msDAO = new PhysicalMailSendDAO();
            sc = new SqlCommand("CreatePhysicalMailSend");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = msDAO.createEntity(sc);
            return createid;
        }

        
         public UniversalEntity RetrievePhysicalMailSendID(int id)
        {
            PhysicalMailSendDAO msDAO = new PhysicalMailSendDAO();
            sc = new SqlCommand("RetrievePhysicalMailSendByID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (msDAO.retrieveEntity(sc));
        }


        public bool Update(PhysicalMailSend ms)
        {
            bool success = true;
            PhysicalMailSendDAO msDAO = new PhysicalMailSendDAO();
            sc = new SqlCommand("UpdatePhysicalMailSend");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ms.ID);
            sc.Parameters.Add("@UserID", ms.UserID);
            sc.Parameters.Add("@date_registration", ms.DateReg);
             
            success = msDAO.updateEntity(sc);
            return success;
        }

    }
}