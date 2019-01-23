using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class RequestsDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                Requests ent = new Requests();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public Requests createEntityFromReader(SqlDataReader dr)
        {
            Requests ent = new Requests();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Phone")))
                ent.Phone = dr["Phone"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("AmountDevice")))
                ent.AmountDevice = Convert.ToInt32(dr["AmountDevice"]);

            /*if (!dr.IsDBNull(dr.GetOrdinal("IsInstallation")))
                ent.IsInstallation = Convert.ToBoolean(dr["IsInstallation"]);*/

            /*if (!dr.IsDBNull(dr.GetOrdinal("DateWithdrawal")))
                ent.DateWithdrawal = Convert.ToDateTime(dr["DateDiwthdrawal"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateInstallation")))
                ent.DateInstallation = Convert.ToDateTime(dr["DateInstallation"]);*/

            if (!dr.IsDBNull(dr.GetOrdinal("Comment")))
                ent.Comment = dr["Comment"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}