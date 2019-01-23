using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;
using ExcelLibrary.BinaryFileFormat;


namespace DAO
{
    public class OtherMailSendDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                OtherMailSend ent = new OtherMailSend();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public OtherMailSend createEntityFromReader(SqlDataReader dr)
        {
            OtherMailSend ent = new OtherMailSend();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("OtherMailOutID")))
                ent.OtherMailOutID = Convert.ToInt32(dr["OtherMailOutID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            if (!dr.IsDBNull(dr.GetOrdinal("date_registration")))
                ent.DateRegister = dr["date_registration"].ToString();

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