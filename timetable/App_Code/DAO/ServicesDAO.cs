using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;

namespace Timetable_WebApp.App_Code.DAO
{
    public class ServicesDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                ServiceTable ent = new ServiceTable();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public ServiceTable createEntityFromReader(SqlDataReader dr)
        {
            ServiceTable ent = new ServiceTable();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("service_name")))
                ent.Services_Name = dr["service_name"].ToString();

            

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}