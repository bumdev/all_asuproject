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
    public class AlternativeAbonentDO : UniversalDO
    {
        void AddParametersToSqlCommand(AlternativeAbonent ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
        }

        void addParametres(AlternativeAbonent ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

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

        public bool CreateSAbonentByFAbonentID(AlternativeAbonent ent, int UserID)
        {
            bool success = true;
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("CreateSAbonentByFAbonentID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", ent.ID);
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@Address", ent.Address);
            success = entDAO.updateEntity(sc);
            return success;
        }


        public bool CreateUpdate(AlternativeAbonent ent, int UserID)
        {
            bool success = true;
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("CreateSAbonentByOrderID");
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", ent.ID);
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@Address", ent.Address);
            success = entDAO.updateEntity(sc);
            return success;
        }

        /*
         public bool Update(FAbonent ent,int UserID)
        {
            bool success = true;
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("UpdateFAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID ", UserID);
            sc.Parameters.Add("@FAbonentID ", ent.ID);
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@NotPay", ent.NotPay);
            //sc.Parameters.Add("@RejectVodomer", ent.RejectVodomer);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
         
         */

        public bool UpdateWithHistory(AlternativeAbonent ent, int UserID)
        {
            bool success = true;
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("UpdateAlternativeAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", ent.ID);
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool Update(AlternativeAbonent ent)
        {
            bool success = true;
            AlternativeAbonentDAO aaDAO = new AlternativeAbonentDAO();
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

        
        public bool Delete(int AlternativeabonentID, int UserID)
        {
            bool success = true;
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("DeleteAlternativeAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID ", UserID);
            sc.Parameters.Add("@SAbonentID ", AlternativeabonentID);
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


        public UniversalEntity RetrieveBySOrderID(int id)
        {
            AlternativeAbonentDAO entDAO = new AlternativeAbonentDAO();
            sc = new SqlCommand("RetrieveSAbonentByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}