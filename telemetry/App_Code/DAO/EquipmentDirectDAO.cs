using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class EquipmentDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                EquipmentDirect ent = new EquipmentDirect();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public EquipmentDirect createEntityFromReader(SqlDataReader dr)
        {
            EquipmentDirect ent = new EquipmentDirect();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("EquipmentName")))
                ent.EquipmentName = dr["EquipmentName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("SellerID")))
                ent.SellerID = Convert.ToInt32(dr["SellerID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("State")))
                ent.State = dr["State"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("TechnicalCharachteristic")))
                ent.TechnicalCharachteristic = dr["TechnicalCharachteristic"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("TypeEquipmentID")))
                ent.TypeEquipmentID = Convert.ToInt32(dr["TypeEquipmentID"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}