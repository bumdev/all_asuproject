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
    public class MailOutDO : UniversalDO
    {
        void AddParametersToSqlCommand(MailOut ent, ref SqlCommand sc)
        {
            //sc.Parameters.Add("@ID", ent.ID);
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@Whom", ent.Whom);
            sc.Parameters.Add("@AdresatID", ent.AdresatID);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@PersonalAccount", ent.PersonalAccount);
            //sc.Parameters.Add("@Notation", ent.Notation);
        }

        void addParametres(MailOut ent)
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

        
         public int Create(MailOut ent)
        {
            int createdid = 0;
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("CreateMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
         
          public int Create_1(MailOut ent)
        {
            int createdid = 0;
            MailOutDAO entDAO = new MailOutDAO();
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



        public bool UpdateWithHistory(MailOut ent, int UserID)
        {
            bool success = true;
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("UpdateMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@MailOutID", ent.ID);
            sc.Parameters.Add("@RegNumber", ent.RegNumber);
            sc.Parameters.Add("@About", ent.About);
            sc.Parameters.Add("@AdresatID", ent.AdresatID);
            sc.Parameters.Add("@Whom", ent.Whom);
            sc.Parameters.Add("@AnswerAbout", ent.AnswerAbout);
            sc.Parameters.Add("@AnswerDate", ent.AnswerDate);
            sc.Parameters.Add("@PersonalAccount", ent.PersonalAccount);
            //sc.Parameters.Add("@Notation", ent.Notation);
            /*if (ent.AnswerDate == null)
            {
                sc.Parameters.Add("@AnswerDate", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@AnswerDate", ent.AnswerDate.Value);
            }*/
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool Update(MailOut ent)
        {
            bool success = true;
            MailOutDAO aaDAO = new MailOutDAO();
            sc = new SqlCommand("UpdateAlternativeAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParametres(ent);
            success = aaDAO.updateEntity(sc);
            return success;
        }

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


        
         
        public bool Delete(int MailoutID, int UserID)
        {
            bool success = true;
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("DeleteMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID ", UserID);
            sc.Parameters.Add("@MailOutID ", MailoutID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }

        
         
         public bool DeleteMailoutByID(int id)
        {
            bool success = true;
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("DeleteMailOutByID");
            sc.Parameters.Add("@MailOutID", id);
            sc.CommandType = CommandType.StoredProcedure;
            success = entDAO.deleteEntity(sc);
            return success;
        }
         
         




        /*public bool Delete(int MailOutID, int UserID)
        {
            bool success = true;
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("DeleteMailOut");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@MailOutID", MailOutID);
            sc.Parameters.Add("@UserID", UserID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }*/
         

        /* public UniversalEntity RetrieveSAbonentByOrderID(int id)
         {
             SAbonentDAO entDAO = new SAbonentDAO();
             sc = new SqlCommand("RetrieveSAbonentByOrderID");
             sc.CommandType = CommandType.StoredProcedure;
             sc.Parameters.Add("@OrderID", id);
             return (entDAO.retrieveEntity(sc));
         }*/


        public UniversalEntity RetrieveByMailSendID(int id)
        {
            MailOutDAO entDAO = new MailOutDAO();
            sc = new SqlCommand("RetrieveMailOutByMailSendID");
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