using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class CartDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                CartDirect ent = new CartDirect();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public CartDirect createEntityFromReader(SqlDataReader dr)
        {
            CartDirect ent = new CartDirect();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Cartname")))
                ent.CartName = dr["Cartname"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}