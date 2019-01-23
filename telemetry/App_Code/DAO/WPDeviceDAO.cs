using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;

namespace DAO
{
    public class WPDeviceDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                WPDevice ent = new WPDevice();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public WPDevice createEntityFromReader(SqlDataReader dr)
        {
            WPDevice ent = new WPDevice();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("WPTypeDeviceID")))
                ent.TypeID = Convert.ToInt32(dr["WPTypeDeviceID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                ent.Title = dr["Title"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("FN")))
                ent.FN = dr["FN"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Description")))
                ent.Description = dr["Description"].ToString();


            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}