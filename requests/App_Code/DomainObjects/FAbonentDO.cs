using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;
using requests_app.Controls;

namespace DomainObjects
{
    public class FAbonentDO:UniversalDO
    {
        void AddParametersToSqlCommand(FAbonent ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@NotPay", ent.NotPay);
            //sc.Parameters.Add("@RejectVodomer", ent.RejectVodomer);
        }
        void addParameters(FAbonent ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(FAbonent ent)
        {
            int createdid = 0;
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("CreateFAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
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

        public bool UpdateAbonent(FAbonent ent, int UserID)
        {
            bool succes = true;
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("UpdateFAbonentNew");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@FAbonentID", ent.ID);
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("Address", ent.Address);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@NotPay", ent.NotPay);
            //sc.Parameters.Add("@RejectVodomer", ent.RejectVodomer);
            succes = entDAO.updateEntity(sc);
            return succes;
        }
        public bool Delete(int FabonentID, int UserID)
        {
            bool success = true;
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("DeleteFAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID ", UserID);
            sc.Parameters.Add("@FAbonentID ", FabonentID);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public bool UpdateClientPerson(FAbonent ent)
        {
            bool success = true;
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("UpdateClientPerson");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveClientPersonById(int id)
        {
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("RetrieveClientPersonById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveLikeSurname(string name)
        {
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("RetrieveFAbonentLikeSurname");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@name", name);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveByOrderID(int id)
        {
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("RetrieveFAbonentByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        /*public UniversalEntity RetrieveBySOrderIDAct(int id)
        {
            SAbonentDAO entDAO = new SAbonentDAO();
            sc = new SqlCommand("RetrieveSAbonentByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }*/

        public UniversalEntity RetrieveByNewOrederID(int id)
        {
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("RetrieveNewFAbonentByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveByVodomerOrderID(int id)
        {
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("RetrieveByVodomerAndOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveVodomerAbonentOrderByID(int id)
        {
            FAbonentDAO entDAO = new FAbonentDAO();
            sc = new SqlCommand("RetrieveFAbonentByOrderByVodomerID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}