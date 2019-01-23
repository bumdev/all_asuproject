using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;

namespace DAO
{
    public class AlternativeOrderDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                AlternativeOrderDetails ent = new AlternativeOrderDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public AlternativeOrderDetails createEntityFromReader(SqlDataReader dr)
        {
            AlternativeOrderDetails ent = new AlternativeOrderDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("SOrderID")))
                ent.SOrderID = Convert.ToInt32(dr["SOrderID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("VodomerID")))
                ent.VodomerID = Convert.ToInt32(dr["VodomerID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("StartValue")))
                ent.StartValue = dr["StartValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("EndValue")))
                ent.EndValue = dr["EndValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("SpecialPrice")))
                ent.SpecialPrice = Convert.ToDouble(dr["SpecialPrice"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ReplacementPrice")))
                ent.ReplacementPrice = Convert.ToDouble(dr["ReplacementPrice"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}