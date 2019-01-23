using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;

namespace DAO
{
    public class AlternativeOrderDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                AlternativeOrder ent = new AlternativeOrder();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }

        public AlternativeOrder createEntityFromReader(SqlDataReader dr)
        {
            AlternativeOrder ent = new AlternativeOrder();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("SAbonentID")))
                ent.SAbonentID = Convert.ToInt32(dr["SAbonentID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("WorkType")))
                ent.WorkType = dr["WorkType"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.UserID = Convert.ToInt32(dr["UserID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PaymentDay")))
                ent.PaymentDay = Convert.ToDateTime(dr["PaymentDay"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Prefix")))
                ent.Prefix = dr["Prefix"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}