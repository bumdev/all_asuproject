using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AjaxControlToolkit;
using Entities;

namespace DAO
{
    public class DistrictDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();
            
            
            while (dr.Read())
            {
                Distr ent = new Distr();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }

        public Distr createEntityFromReader(SqlDataReader dr)
        {
            Distr ent = new Distr();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DistrictName")))
                ent.DistrictName = dr["DistrictName"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}