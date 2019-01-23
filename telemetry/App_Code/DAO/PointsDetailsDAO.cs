using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class PointsDetailsDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                PointsDetails ent = new PointsDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public PointsDetails createEntityFromReader(SqlDataReader dr)
        {
            PointsDetails ent = new PointsDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PointsInfoID")))
                ent.PointsInfoID = Convert.ToInt32(dr["PointsInfoID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PointsEquipmentID")))
                ent.PointsEquipmendID = Convert.ToInt32(dr["PointsEquipmentID"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}