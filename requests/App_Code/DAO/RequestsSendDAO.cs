using System;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class RequestsSendDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                RequestsSend ent = new RequestsSend();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public RequestsSend createEntityFromReader(SqlDataReader dr)
        {
            RequestsSend ent = new RequestsSend();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("RequestsID")))
                ent.RequestsID = Convert.ToInt32(dr["RequestsID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateWithdrawal")))
                ent.DateWithdrawal = Convert.ToDateTime(dr["DateWithdrawal"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateInstallation")))
                ent.DateInstallation = Convert.ToDateTime(dr["DateInstallation"]);

            /*if (!dr.IsDBNull(dr.GetOrdinal("Comment")))
                ent.Comment = dr["Comment"].ToString();*/

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