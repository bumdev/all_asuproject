using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;

namespace invent_app
{
    public class ManufacturerDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();
            while (!dr.Read())
            {
                Manufacturer ent = new Manufacturer();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public Manufacturer createEntityFromReader(SqlDataReader dr)
        {
            Manufacturer ent = new Manufacturer();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ProdName")))
                ent.ProdName = dr["ProdName"].ToString();
            
            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity(dr);
        }
    }
}