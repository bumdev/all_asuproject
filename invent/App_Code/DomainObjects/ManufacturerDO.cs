using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DomainObjects;
using Entities;
using DAO;

namespace invent_app
{
    public class ManufacturerDO:UniversalDO
    {
        void AddParameterToSqlCommand(Manufacturer ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@ProdName", ent.ProdName);
        }

        void addParamaters(Manufacturer ent)
        {
            AddParameterToSqlCommand(ent, ref sc);
        }

        public int CreateManufacturer(Manufacturer ent)
        {
            int createid = 0;
            ManufacturerDAO entDAO = new ManufacturerDAO();
            sc = new SqlCommand("CreateManufacturer");
            sc.CommandType = CommandType.StoredProcedure;
            addParamaters(ent);
            createid = (entDAO.createEntity(sc));
            return createid;
        }
    }
}