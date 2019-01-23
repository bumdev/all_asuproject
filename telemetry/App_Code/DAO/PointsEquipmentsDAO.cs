using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class PointsEquipmentsDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                PointsEquipments ent = new PointsEquipments();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public PointsEquipments createEntityFromReader(SqlDataReader dr)
        {
            PointsEquipments ent = new PointsEquipments();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("EquipmentID")))
                ent.EquipmentID = Convert.ToInt32(dr["EquipmentID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("InventoryNumber")))
                ent.InventoryNumber = dr["InventoryNumber"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("State")))
                ent.State = dr["State"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Repairs")))
                ent.Repairs = Convert.ToBoolean(dr["Repairs"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}