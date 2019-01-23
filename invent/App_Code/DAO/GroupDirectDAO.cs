using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;

namespace DAO
{
    public class GroupDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                GroupDirect ent = new GroupDirect();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }

        public GroupDirect createEntityFromReader(SqlDataReader dr)
        {
            GroupDirect ent = new GroupDirect();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("GroupName")))
                ent.GroupName = dr["GroupName"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}