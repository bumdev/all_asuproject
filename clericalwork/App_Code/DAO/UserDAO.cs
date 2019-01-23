using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class UserDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                User ent = new User();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public User createEntityFromReader(SqlDataReader dr)
        {
            Entities.User ent = new User();

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.ID = Convert.ToInt32(dr["UserID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UserName")))
                ent.UserName = dr["UserName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("UserLogin")))
                ent.UserLogin = dr["UserLogin"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("UserPassword")))
                ent.UserPassword = dr["UserPassword"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("IsActive")))
                ent.IsActive = Convert.ToBoolean(dr["IsActive"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UserLocationID")))
                ent.Location = Convert.ToInt32(dr["UserLocationID"]);

            /*if (!dr.IsDBNull(dr.GetOrdinal("ServicesID")))
                ent.SerivceID = Convert.ToInt32(dr["ServicesID"]);*/

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
    
}