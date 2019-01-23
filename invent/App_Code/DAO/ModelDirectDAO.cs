using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;

namespace DAO
{
    public class ModelDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                ModelDirect ent = new ModelDirect();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }


        public ModelDirect createEntityFromReader(SqlDataReader dr)
        {
            ModelDirect ent = new ModelDirect();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ModelName")))
                ent.ModelName = dr["ModelName"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}