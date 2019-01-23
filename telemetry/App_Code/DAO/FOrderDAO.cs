using System;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class FOrderDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                FOrder ent = new FOrder();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public FOrder createEntityFromReader(SqlDataReader dr)
        {
            FOrder ent = new FOrder();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("FAbonentID")))
                ent.FAbonentID = Convert.ToInt32(dr["FAbonentID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Coment")))
                ent.Coment = dr["Coment"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("IsPaid")))
                ent.IsPaid = Convert.ToBoolean(dr["IsPaid"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DefectVodomer")))
                ent.DefectVodomer = Convert.ToBoolean(dr["DefectVodomer"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ActionType")))
                ent.ActionType = dr["ActionType"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("PaymentDay")))
                ent.PaymentDay =Convert.ToDateTime(dr["PaymentDay"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.UserID = Convert.ToInt32(dr["UserID"]);

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