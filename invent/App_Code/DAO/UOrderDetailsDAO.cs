using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class UOrderDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                UOrderDetails ent = new UOrderDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public UOrderDetails createEntityFromReader(SqlDataReader dr)
        {
            UOrderDetails ent = new UOrderDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UOrderID")))
                ent.UOrderID = Convert.ToInt32(dr["UOrderID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("VodomerID")))
                ent.VodomerID = Convert.ToInt32(dr["VodomerID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("StartValue")))
                ent.StartValue = dr["StartValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("EndValue")))
                ent.EndValue = dr["EndValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Price")))
                ent.Price = Convert.ToDouble(dr["Price"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PriceRub")))
                ent.PriceRub = Convert.ToDouble(dr["PriceRub"]);


            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}