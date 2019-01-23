using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class TypeCartridgesDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                TypeCartridges ent = new TypeCartridges();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public TypeCartridges createEntityFromReader(SqlDataReader dr)
        {
            TypeCartridges ent = new TypeCartridges();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("NameCartridge")))
                ent.NameCartridge = dr["NameCartridge"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}