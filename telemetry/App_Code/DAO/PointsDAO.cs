using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class PointsDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                Points ent = new Points();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public Points createEntityFromReader(SqlDataReader dr)
        {
            Points ent = new Points();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                ent.Name = dr["Name"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DistrictID")))
                ent.DistrictID = Convert.ToInt32(dr["DistrictID"]);

           /* if (!dr.IsDBNull(dr.GetOrdinal("TypePointsID")))
                ent.TypePointsID = Convert.ToInt32(dr["TypePointsID"]);*/

            if (!dr.IsDBNull(dr.GetOrdinal("TypePoints")))
                ent.TypePoints = dr["TypePoints"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}