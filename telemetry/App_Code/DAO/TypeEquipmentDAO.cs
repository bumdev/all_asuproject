using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class TypeEquipmentDAO: UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                TypeEquipment ent = new TypeEquipment();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public TypeEquipment createEntityFromReader(SqlDataReader dr)
        {
            TypeEquipment ent = new TypeEquipment();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("TypeName")))
                ent.TypeName = dr["TypeName"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}