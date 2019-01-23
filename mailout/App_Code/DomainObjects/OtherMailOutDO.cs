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
    public class OtherMailOutDO : UniversalDO
    {
        void AddParametersToSqlCommand(OtherMailOut ent, ref SqlCommand sc)
        {
            //sc.Parameters.Add("@ID", ent.ID);
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@Whom", ent.Whom);
            sc.Parameters.Add("@AdresatType", ent.AdreastType);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@Notation", ent.Notation);
        }

        void addParametres(OtherMailOut ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

      /*  public int Create(MailOut ent)
        {
            int createid = 0;
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("CreateMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }*/

        
         public int Create(OtherMailOut ent)
        {
            int createdid = 0;
            OtherMailOutDAO entDAO = new OtherMailOutDAO();
            sc = new SqlCommand("CreateOtherMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
         
          public int Create_1(OtherMailOut ent)
        {
            int createdid = 0;
            OtherMailOutDAO entDAO = new OtherMailOutDAO();
            sc = new SqlCommand("CreateMailOut_1");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
          

         /*
          public int Create(AlternativeAbonent ent)
         {
             int createid = 0;
             AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
             sc = new SqlCommand("CreateSAbonent");
             sc.CommandType = CommandType.StoredProcedure;
             addParametres(ent);
             createid = entDAO.createEntity(sc);
             return createid;
         }
          */



        /*public bool UpdateWithHistory(OtherMailOut ent, int UserID)
        {
            bool success = true;
            OtherMailOutDAO entDAO = new OtherMailOutDAO();
            sc = new SqlCommand("UpdateOtherMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OtherMailOutID", ent.ID);
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@AdresatType", ent.AdreastType);
            sc.Parameters.Add("@Whom", ent.Whom);
            success = entDAO.updateEntity(sc);
            return success;
        }*/

        
         
        public bool UpdateWithHistory(OtherMailOut ent, int UserID)
        {
            bool success = true;
            OtherMailOutDAO entDAO = new OtherMailOutDAO();
            sc = new SqlCommand("UpdateOtherMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@OtherMailOutID", ent.ID);
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@AdresatType", ent.AdreastType);
            sc.Parameters.Add("@Whom", ent.Whom);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@Notation", ent.Notation);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool Update(OtherMailOut ent)
        {
            bool succes = true;
            OtherMailOutDAO omoDAO = new OtherMailOutDAO();
            sc = new SqlCommand("UpdateAlternativeOtherMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParametres(ent);
            succes = omoDAO.updateEntity(sc);
            return succes;
        }
         
         

        /*public bool Update(MailOut ent)
        {
            bool success = true;
            MailOutDAO aaDAO = new MailOutDAO();
            sc = new SqlCommand("UpdateAlternativeAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParametres(ent);
            success = aaDAO.updateEntity(sc);
            return success;
        }*/

        /*public bool Delete(int AlternativeAbonentID, int UserID)
        {
            bool success = true;
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("DeleteSAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", AlternativeAbonentID);
            success = entDAO.updateEntity(sc);
            return success;
        }*/

        /*public bool Delete(int AlternativeAbonentID, int UserID)
        {
            bool success = true;
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("DeleteAlternativeAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", AlternativeAbonentID);
            success = entDAO.updateEntity(sc);
            return success;
        }*/

        
        public bool Delete(int OtherMailOutID, int UserID)
        {
            bool success = true;
            OtherMailOutDAO entDAO = new OtherMailOutDAO();
            sc = new SqlCommand("DeleteOtherMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OtherMailOutID", OtherMailOutID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
         

        /* public UniversalEntity RetrieveSAbonentByOrderID(int id)
         {
             SAbonentDAO entDAO = new SAbonentDAO();
             sc = new SqlCommand("RetrieveSAbonentByOrderID");
             sc.CommandType = CommandType.StoredProcedure;
             sc.Parameters.Add("@OrderID", id);
             return (entDAO.retrieveEntity(sc));
         }*/


        public UniversalEntity RetrieveByOtherMailSendID(int id)
        {
            OtherMailOutDAO entDAO = new OtherMailOutDAO();
            sc = new SqlCommand("RetrieveOtherMailOutByMailSendID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@SendID", id);
            return (entDAO.retrieveEntity(sc));
        }

        
         public UniversalEntity RetrieveBySendID(int id)
        {
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("RetrieveMailOutByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@SendID", id);
            return (entDAO.retrieveEntity(sc));
        }
         
         
    }
}