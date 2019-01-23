using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;

namespace DAO
{
    public class TypeDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                TypeDir ent = new TypeDir();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public TypeDir createEntityFromReader(SqlDataReader dr)
        {
            TypeDir ent = new TypeDir();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("NameType")))
                ent.NameType = dr["NameType"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}