using System;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class UOrderDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                UOrder ent = new UOrder();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public UOrder createEntityFromReader(SqlDataReader dr)
        {
            UOrder ent = new UOrder();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UAbonentID")))
                ent.UAbonentID = Convert.ToInt32(dr["UAbonentID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Coment")))
                ent.Coment = dr["Coment"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("IsPaid")))
                ent.IsPaid = Convert.ToBoolean(dr["IsPaid"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ActionType")))
                ent.ActionType = dr["ActionType"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("PaymentDay")))
                ent.PaymentDay = Convert.ToDateTime(dr["PaymentDay"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.UserID = Convert.ToInt32(dr["UserID"]);



            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}