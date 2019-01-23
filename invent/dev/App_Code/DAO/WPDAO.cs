using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.Data.SqlClient;

namespace DAO
{
    public class WaterPointDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                WaterPoint ent = new WaterPoint();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public List<WaterPoint> CreateList()
        {
            List<WaterPoint> WPList = new List<WaterPoint>();
            while (dr.Read())
            {
                WaterPoint ent = new WaterPoint();
                ent = createEntityFromReader(dr);
                WPList.Add(ent);
            }
            return WPList;
        }

        public WaterPoint createEntityFromReader(SqlDataReader dr)
        {
            WaterPoint ent = new WaterPoint();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("WPLocationID")))
                ent.LocationID = Convert.ToInt32(dr["WPLocationID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                ent.Title = dr["Title"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("LineFirst")))
                ent.LineFirst = Convert.ToDouble(dr["LineFirst"]);

            if (!dr.IsDBNull(dr.GetOrdinal("LineSecond")))
                ent.LineSecond = Convert.ToDouble(dr["LineSecond"]);

            if (!dr.IsDBNull(dr.GetOrdinal("D")))
                ent.D = Convert.ToInt32(dr["D"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DCalc")))
                ent.DCalc = Convert.ToInt32(dr["DCalc"]);

            if (!dr.IsDBNull(dr.GetOrdinal("QMin")))
                ent.QMin = Convert.ToInt32(dr["QMin"]);

            if (!dr.IsDBNull(dr.GetOrdinal("QMax")))
                ent.QMax = Convert.ToInt32(dr["QMax"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Comment")))
                ent.Comment = dr["Comment"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("WPType")))
                ent.Wptype = Utilities.ConvertToInt(dr["WPType"].ToString());

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}