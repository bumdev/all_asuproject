using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;


/// <summary>
/// Summary description for VodomerDAO
/// </summary>
/// 

namespace DAO
{
    public class VodomerDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                Vodomer ent = new Vodomer();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public Vodomer createEntityFromReader(SqlDataReader dr)
        {
            Vodomer ent = new Vodomer();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("nom_zavod")))
                ent.FactoryNumber= dr["nom_zavod"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("date_make")))
                ent.DateOfProduce = Convert.ToDateTime(dr["date_make"]);

            if (!dr.IsDBNull(dr.GetOrdinal("exploited")))
                ent.Exploited = Convert.ToBoolean(dr["exploited"]);

            if (!dr.IsDBNull(dr.GetOrdinal("id_type_vodomer")))
                ent.VodomerType = Convert.ToInt32(dr["id_type_vodomer"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}