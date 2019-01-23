using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

/// <summary>
/// Summary description for PermissionDAO
/// </summary>
/// 

namespace DAO
{
    public class PermissionDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                Permission ent = new Permission();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public Permission createEntityFromReader(SqlDataReader dr)
        {
            Permission ent = new Permission();

            if (!dr.IsDBNull(dr.GetOrdinal("PermissionID")))
                ent.ID = Convert.ToInt32(dr["PermissionID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PermissionName")))
                ent.PermissionName = dr["PermissionName"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}