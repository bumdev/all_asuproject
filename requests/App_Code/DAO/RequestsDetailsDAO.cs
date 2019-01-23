using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class RequestsDetailsDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                RequestsDetails ent = new RequestsDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public RequestsDetails createEntityFromReader(SqlDataReader dr)
        {
            RequestsDetails ent = new RequestsDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("RequestsID")))
                ent.RequestsSendID = Convert.ToInt32(dr["RequestsID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("IsInstallation")))
                ent.IsInstallation = Convert.ToBoolean(dr["IsInstallation"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}