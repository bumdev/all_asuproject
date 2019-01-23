using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class FAbonentDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                FAbonent ent = new FAbonent();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public FAbonent createEntityFromReader(SqlDataReader dr)
        {
            FAbonent ent = new FAbonent();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("FirstName")))
                ent.FirstName = dr["FirstName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Surname")))
                ent.Surname = dr["Surname"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("LastName")))
                ent.LastName = dr["LastName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Phone")))
                ent.Phone = dr["Phone"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DistrictID")))
                ent.DistrictID = Convert.ToInt32(dr["DistrictID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("NotPay")))
                ent.NotPay = Convert.ToBoolean(dr["NotPay"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}