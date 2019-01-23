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
    public class PhysicalMailOutDO : UniversalDO
    {
        void AddParametersToSqlCommand(PhysicalMailOut ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@Whom", ent.Whom);
            sc.Parameters.Add("@PhysicalAdresatID", ent.PhysicalAdresatID);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@Notation", ent.Notation);
            sc.Parameters.Add("@ContractNumber", ent.ContractNumber);
            sc.Parameters.Add("@SenderID", ent.SenderID);
            //sc.Parameters.Add("@DateOut", ent.DateOut);
            /*if (ent.DateRegister.HasValue)
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value.ToShortDateString());*/
        }

        void addParameters(PhysicalMailOut ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(PhysicalMailOut ent)
        {
            int createid = 0;
            PhysicalMailOutDAO msDAO = new PhysicalMailOutDAO();
            sc = new SqlCommand("CreatePhysicalMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = msDAO.createEntity(sc);
            return createid;
        }

        
         public UniversalEntity RetrieveBySendID(int id)
        {
            PhysicalMailOutDAO entDAO = new PhysicalMailOutDAO();
            sc = new SqlCommand("RetrievePhysicalMailOutBySendID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@SendID", id);
            return (entDAO.retrieveEntity(sc));
        }

        
         public bool Update(PhysicalMailOut ent)
        {
            bool success = true;
            PhysicalMailOutDAO entDAO = new PhysicalMailOutDAO();
            sc = new SqlCommand("UpdatePhysicalMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }

        
         public bool UpdateWithHistory(PhysicalMailOut ent, int UserID)
        {
            bool success = true;
            PhysicalMailOutDAO entDAO = new PhysicalMailOutDAO();
            sc = new SqlCommand("UpdatePhysicalMailOutWithHistory");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@PhysicalMailOutID", ent.ID);
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@PhysicalAdresatID", ent.PhysicalAdresatID);
            sc.Parameters.Add("@Whom", ent.Whom);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@Notation", ent.Notation);
            sc.Parameters.Add("@ContractNumber", ent.ContractNumber);
             sc.Parameters.Add("@SenderID", ent.SenderID);
            success = entDAO.updateEntity(sc);
            return success;
        }

        
         
         public bool Delete(int PhysicalMailoutID, int UserID)
        {
            bool success = true;
            PhysicalMailOutDAO entDAO = new PhysicalMailOutDAO();
            sc = new SqlCommand("DeletePhysicalMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID ", UserID);
            sc.Parameters.Add("@PhysicalMailOutID ", PhysicalMailoutID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
         
         



    }
}