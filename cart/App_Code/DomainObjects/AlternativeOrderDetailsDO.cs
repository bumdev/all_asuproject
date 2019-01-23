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
    public class AlternativeOrderDetailsDO : UniversalDO
    {
        void AddParametresToSqlCommand(AlternativeOrderDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@SOrderID", ent.SOrderID);
            sc.Parameters.Add("@VodomerID", ent.VodomerID);
            sc.Parameters.Add("@StartValue", ent.StartValue);
            // sc.Parameters.Add("@EndValue", ent.EndValue);
            // sc.Parameters.Add("@SpecialPrice", ent.SpecialPrice);
        }

        void addParametres(AlternativeOrderDetails ent)
        {
            AddParametresToSqlCommand(ent, ref sc);
        }

        public int Create(AlternativeOrderDetails ent)
        {
            int createid = 0;
            AlternativeOrderDetailsDAO entDAO = new AlternativeOrderDetailsDAO();
            sc = new SqlCommand("CreateSOrderDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity VodomersBySOrder(int id)
        {
            AlternativeOrderDetailsDAO entDAO = new AlternativeOrderDetailsDAO();
            sc = new SqlCommand("VodomersBySOrder");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@SOrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveSOrderDetailsBySorderID(int id)
        {
            AlternativeOrderDetailsDAO entDAO = new AlternativeOrderDetailsDAO();
            sc = new SqlCommand("RetrieveSOrderDetailsByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}