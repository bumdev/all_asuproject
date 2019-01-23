using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

/// <summary>
/// Summary description for VodomerDO
/// </summary>
/// 

namespace DomainObjects
{
    public class VodomerDO:UniversalDO
    {
        void AddParametersToSqlCommand(Vodomer ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@nom_zavod", ent.FactoryNumber);
            sc.Parameters.Add("@date_make", ent.DateOfProduce);
           // sc.Parameters.Add("@exploited", ent.Exploited);
            sc.Parameters.Add("@id_type_vodomer", ent.VodomerType);
        }
        void addParameters(Vodomer ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(Vodomer ent)
        {
            int createdid = 0;
            VodomerDAO entDAO = new VodomerDAO();
            sc = new SqlCommand("CreateVodomer");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public int Create1(Vodomer ent)
        {
            int createdid = 0;
            VodomerDAO entDAO = new VodomerDAO();
            sc = new SqlCommand("CreateVodomer1");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@exploited", ent.Exploited);
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
        public bool Update(Vodomer ent)
        {
            bool success = true;
            VodomerDAO entDAO = new VodomerDAO();
            sc = new SqlCommand("UpdateVodomer");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }
        public UniversalEntity RetrieveVodomerById(int id)
        {
            VodomerDAO entDAO = new VodomerDAO();
            sc = new SqlCommand("RetrieveVodomerById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
       /* public UniversalEntity RetrieveVodomerAndFOrderID(int id)
        {
            FOrderDAO entDAO = new FOrderDAO();
            sc = new SqlCommand("RetrieveFOrderAndVodomerById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }*/

       /* public UniversalEntity RetrieveVodomerByFOrderDetailsID(int id)
        {
            FOrderDetailsDAO entDAO = new FOrderDetailsDAO();
            sc = new SqlCommand("RetrieveFOrderDetailsByVododmerID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }*/
    }
}