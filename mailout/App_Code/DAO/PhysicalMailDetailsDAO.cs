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
    public class PhysicalMailDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                PhysicalMailDetails ent = new PhysicalMailDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public PhysicalMailDetails createEntityFromReader(SqlDataReader dr)
        {
            PhysicalMailDetails ent = new PhysicalMailDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PhysicalMailSendID")))
                ent.PhysicalMailSendID = Convert.ToInt32(dr["PhysicalMailSendID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                ent.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ResponsibleContractorID")))
                ent.ResponsibleContractorID = Convert.ToInt32(dr["ResponsibleContractorID"]);

            return ent;
        }

        

    public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}