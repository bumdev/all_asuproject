using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class FOrderDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                FOrderDetails ent = new FOrderDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public FOrderDetails createEntityFromReader(SqlDataReader dr)
        {
            FOrderDetails ent = new FOrderDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("FOrderID")))
                ent.FOrderID = Convert.ToInt32(dr["FOrderID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("VodomerID")))
                ent.VodomerID = Convert.ToInt32(dr["VodomerID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("StartValue")))
                ent.StartValue = dr["StartValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("EndValue")))
                ent.EndValue = dr["EndValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Price")))
                ent.Price = Convert.ToDouble(dr["Price"]);


            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}