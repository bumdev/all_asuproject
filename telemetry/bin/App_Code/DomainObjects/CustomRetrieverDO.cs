using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DAO;
using Entities;

/// <summary>
/// Summary description for CustomRetrieverDO
/// </summary>
namespace DomainObjects
{
    public class CustomRetrieverDO : UniversalDO
    {
        public UniversalEntity RetrieveDiameters()
        {
            sc = new SqlCommand("RetrieveVodomerDiameters");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveDiameters(sc);
        }
        public List<Book> RetrieveWPTypeDevice()
        {
            sc = new SqlCommand("RetrieveWPTypeDevice");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveWPTypeDevice(sc);
        }
        public UniversalEntity RetrieveDistricts()
        {
            sc = new SqlCommand("RetrieveDistricts");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveDistricts(sc);
        }

        public UniversalEntity RetrieveGroups()
        {
            sc = new SqlCommand("RetrieveGroups");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveGroups(sc);
        }
        public UniversalEntity RetrieveFActByOrderID5Low(int OrderID)
        {
            sc = new SqlCommand("RetrieveFActByOrderID5Low");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@orderid", OrderID);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveFActByOrderID5Low(sc);
        }
        public UniversalEntity RetrieveFActByOrderID5High(int OrderID)
        {
            sc = new SqlCommand("RetrieveFActByOrderID5High");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@orderid", OrderID);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveFActByOrderID5High(sc);
        }
        public UniversalEntity RetrieveUActByOrderID5Low(int OrderID)
        {
            sc = new SqlCommand("RetrieveUActByOrderID5Low");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@orderid", OrderID);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveFActByOrderID5Low(sc);
        }
        public UniversalEntity RetrieveUActByOrderID5High(int OrderID)
        {
            sc = new SqlCommand("RetrieveUActByOrderID5High");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@orderid", OrderID);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveFActByOrderID5High(sc);
        }
        public UniversalEntity GetRateByDateAndWP(int wp,int type, DateTime sd,DateTime ed)
        {
            sc = new SqlCommand("GetRateByDateAndWP");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@WPID", wp);
            sc.Parameters.Add("@type", type);
            sc.Parameters.Add("@DateLow", sd);
            sc.Parameters.Add("@DateHigh", ed);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.GetRateByDateAndWP(sc);
        }

        public UniversalEntity RetrieveUserLocation(int id)
        {
            sc = new SqlCommand("RetrieveUserLocation");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@id", id);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveUserLocation(sc);
        }

        public UniversalEntity RetrieveUserLocationByFOrder(int id)
        {
            sc = new SqlCommand("RetrieveUserLocationByFOrder");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@id", id);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveUserLocationByFOrder(sc);
        }
        public UniversalEntity RetrieveUserLocationBySurname(string surname)
        {
            sc = new SqlCommand("RetrieveUserLocationBySurname");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@surname", surname);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveUserLocationBySurname(sc);
        }


        /*
        public UniversalEntity RetrieveContractors()
        {
            sc = new SqlCommand("RetrieveContactors");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveContractors(sc);
        }
       

        public UniversalEntity RetrieveContracts()
        {
            sc = new SqlCommand("RetrieveContracts");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveContracts(sc);
        }

        public UniversalEntity RetrieveTransporters()
        {
            sc = new SqlCommand("RetrieveTransporters");
            sc.CommandType = CommandType.StoredProcedure;
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return cDAO.RetrieveTransporters(sc);
        }

        public UniversalEntity RetrieveMarksbyName(int id, string name)
        {
            sc = new SqlCommand("RetrieveMarksByName");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ContractID", id);
            sc.Parameters.Add("@Name", name);
            CustomRetrieverDAO cDAO = new CustomRetrieverDAO();
            return (cDAO.RetrieveMarksbyName(sc));
        }*/

       
    }
}