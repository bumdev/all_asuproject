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
    public class PhysicalMailSendDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                PhysicalMailSend ent = new PhysicalMailSend();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public PhysicalMailSend createEntityFromReader(SqlDataReader dr)
        {
            PhysicalMailSend ent = new PhysicalMailSend();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PhysicalMailOutID")))
                ent.PhysicalMailOutID = Convert.ToInt32(dr["PhysicalMailOutID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.UserID = Convert.ToInt32(dr["UserID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("date_registration")))
                ent.DateReg = dr["date_registration"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            return ent;
        }

    public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}