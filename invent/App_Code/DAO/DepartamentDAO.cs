using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;

namespace DAO
{
    public class DepartamentDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                Departament ent = new Departament();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }

        public Departament createEntityFromReader(SqlDataReader dr)
        {
            Departament ent = new Departament();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DepartName")))
                ent.DepartName = dr["DepartName"].ToString();
            
            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}